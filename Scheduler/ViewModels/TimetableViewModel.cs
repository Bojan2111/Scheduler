﻿using Scheduler.Logic;
using Scheduler.Models;
using Scheduler.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Scheduler.Views;

namespace Scheduler.ViewModels
{
    public class TimetableViewModel : ViewModelBase
    {
        private SchedulerDbContext _context;

        private ObservableCollection<TeamSchedule> _teamSchedules;

        public ObservableCollection<TeamSchedule> TeamSchedules
        {
            get { return _teamSchedules; }
            set
            {
                _teamSchedules = value;
                OnPropertyChanged(nameof(TeamSchedules));
            }
        }

        public ICommand EditRoleCommand { get; set; }
        public ICommand TransferEmployeeCommand { get; set; }
        public ICommand EditShiftCommand { get; set; }
        public ICommand DeleteShiftCommand { get; }

        public TimetableViewModel()
        {
            _context = new SchedulerDbContext();
            TeamSchedules = new ObservableCollection<TeamSchedule>();
            LoadContext();

            EditRoleCommand = new RelayCommand(EditRole);
            TransferEmployeeCommand = new RelayCommand(TransferEmployee);
            //AddShiftCommand = new RelayCommand(AddShift);
            EditShiftCommand = new RelayCommand(EditShift);
            DeleteShiftCommand = new RelayCommand(DeleteShift);
        }

        private void LoadContext()
        {
            bool hasShifts = _context.Shifts.Any();

            if (!hasShifts)
            {
                GenerateShiftsForEmployees();
            }
            else
            {
                int desiredMonth = 1;

                var employeeShifts = _context.Shifts
                    .Where(shift => shift.Employee.Team.CurrentMonth == desiredMonth)
                    .Include(s => s.Employee.Team)
                    .Include(s => s.Employee.TeamRole)
                    .ToList();

                var groupedShifts = employeeShifts
                    .GroupBy(shift => new { TeamId = shift.Employee.TeamId, EmployeeId = shift.EmployeeId })
                    .ToList();

                int year = employeeShifts.First().Date.Year;
                int month = employeeShifts.First().Month;
                List<DayType> days = GenerateWeekDays(year, month);

                foreach (var teamGroup in groupedShifts.GroupBy(g => g.Key.TeamId))
                {
                    var teamId = teamGroup.Key;
                    var teamSchedule = new TeamSchedule
                    {
                        Id = teamId,
                        TeamName = _context.Teams.FirstOrDefault(x => x.Id == teamId)?.Name,
                        MonthName = GenerateLocalMonthName(month),
                        Year = year,
                        Dates = days,
                        EmployeeSchedules = new List<EmployeeSchedule>()
                    };

                    foreach (var employeeGroup in teamGroup)
                    {
                        var employeeId = employeeGroup.Key.EmployeeId;
                        var employeeRole = employeeGroup.First().Employee.TeamRole.Name;
                        var shifts = employeeGroup.Select(shift => new Shift
                        {
                            Id = shift.Id,
                            Name = shift.Name,
                            EmployeeId = employeeId,
                            Date = shift.Date,
                            Month = shift.Month
                        }).ToList();
                        List<ShiftDisplayDTO> allShiftsInMonth = GenerateAllShiftsInMonth(shifts, days);
                        var employeeSchedule = new EmployeeSchedule
                        {
                            Id = employeeId,
                            EmployeeName = $"{employeeGroup.First().Employee.LastName} {employeeGroup.First().Employee.FirstName}",
                            EmployeeRole = employeeRole == "--" ? "" : employeeRole,
                            IsRolePresent = employeeRole != "--",
                            TeamId = teamId,
                            Shifts = allShiftsInMonth,
                        };

                        teamSchedule.EmployeeSchedules.Add(employeeSchedule);
                    }

                    TeamSchedules.Add(teamSchedule);
                }
            }
        }

        private List<ShiftDisplayDTO> GenerateAllShiftsInMonth(List<Shift> shifts, List<DayType> days)
        {
            int year = shifts.First().Date.Year;
            int month = shifts.First().Month;
            int employeeId = shifts.First().EmployeeId;
            List<ShiftDisplayDTO> allShifts = new List<ShiftDisplayDTO>();

            foreach(DayType day in days)
            {
                Shift shiftOnThatDay = shifts.Find(x => x.Date.Day == day.Day);
                if (shiftOnThatDay != null)
                {
                    allShifts.Add(new ShiftDisplayDTO
                    {
                        Id = shiftOnThatDay.Id,
                        Name = shiftOnThatDay.Name,
                        EmployeeId = shiftOnThatDay.EmployeeId,
                        Date = shiftOnThatDay.Date,
                        IsNotWorkDay = day.IsNotWorkDay,
                        IsShiftPresent = true,
                    });
                }
                else
                {
                    allShifts.Add(new ShiftDisplayDTO
                    {
                        Name = "",
                        EmployeeId = employeeId,
                        Date = new DateTime(year, month, day.Day),
                        IsNotWorkDay = day.IsNotWorkDay,
                    });
                }
            }
            return allShifts;
        }

        private string GenerateLocalMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "Mart";
                case 4:
                    return "April";
                case 5:
                    return "Maj";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Avgust";
                case 9:
                    return "Septembar";
                case 10:
                    return "Oktobar";
                case 11:
                    return "Novembar";
                case 12:
                    return "Decembar";
                default:
                    return "Nepoznat mesec";
            }
        }

        private List<DayType> GenerateWeekDays(int year, int month)
        {
            List<DayType> days = new List<DayType>();
            int lastDay = DateTime.DaysInMonth(year, month);
            List<NationalHoliday> nationalHolidays = _context.NationalHolidays
                .Where(n => n.Date.Year == year && n.Date.Month == month)
                .ToList();
            
            for (int i = 1; i <= lastDay; i++)
            {
                DateTime testingDate = new DateTime(year, month, i);
                bool isNationalHoliday = nationalHolidays.FirstOrDefault(x => x.Date == new DateTime(year, month, i)) != null;
                DayType dayType = new DayType()
                {
                    Day = i,
                    IsNotWorkDay = isNationalHoliday,
                };
                switch (testingDate.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        dayType.DayName = "P";
                        break;
                    case DayOfWeek.Tuesday:
                        dayType.DayName = "U";
                        break;
                    case DayOfWeek.Wednesday:
                        dayType.DayName = "S";
                        break;
                    case DayOfWeek.Thursday:
                        dayType.DayName = "Č";
                        break;
                    case DayOfWeek.Friday:
                        dayType.DayName = "P";
                        break;
                    case DayOfWeek.Saturday:
                        dayType.DayName = "S";
                        dayType.IsNotWorkDay = true;
                        break;
                    case DayOfWeek.Sunday:
                        dayType.DayName = "N";
                        dayType.IsNotWorkDay = true;
                        break;
                }
                days.Add(dayType);
            }
            return days;
        }
        public void GenerateShiftsForEmployees()
        {
            List<TeamSchedule> teamSchedules = new List<TeamSchedule>();
            List<Team> teams =_context.Teams.ToList();
            Team testTeam = teams[0];
            int year = testTeam.CurrentStartDate.Year;
            int month = testTeam.CurrentMonth;
            int lastDayOfMonth = DateTime.DaysInMonth(year, month);
            GenerateWeekDays(year, month);

            foreach (Team team in teams)
            {
                TeamSchedule teamSchedule = new TeamSchedule();
                teamSchedule.Id = team.Id;
                teamSchedule.TeamName = team.Name;
                teamSchedule.EmployeeSchedules = new List<EmployeeSchedule>();
                bool nextMonthStartsWithNight = team.NextMonthStartsWithNight;
                DateTime nextMonthStartDate = CalculateNextMonthStartDate(team.CurrentStartDate, lastDayOfMonth, nextMonthStartsWithNight);

                List<Employee> employeesInTeam = _context.Employees.Where(e => e.TeamId == team.Id).Include(e => e.TeamRole).ToList();

                foreach (Employee employee in employeesInTeam)
                {
                    employee.Team = team;
                    List<ShiftDisplayDTO> shifts = new List<ShiftDisplayDTO>(); //GenerateShiftsForEmployee(employee);
                    EmployeeSchedule employeeSchedule = new EmployeeSchedule
                    {
                        EmployeeName = $"{employee.LastName} {employee.FirstName}",
                        EmployeeRole = employee.TeamRole.Name,
                        Shifts = shifts
                    };

                    teamSchedule.EmployeeSchedules.Add(employeeSchedule);
                }

                TeamSchedules.Add(teamSchedule);
            }
        }

        private List<Shift> GenerateShiftsForEmployee(Employee employee)
        {
            DateTime start = employee.Team.CurrentStartDate;
            int year = start.Year;
            int month = employee.Team.CurrentMonth;
            int lastDayOfMonth = DateTime.DaysInMonth(year, month);

            List<Absence> absences = _context.Absences
                .Where(x => x.EmployeeId == employee.Id && x.Year == year && x.Month == month)
                .ToList();
            List<NationalHoliday> nationalHolidays = _context.NationalHolidays
                .Where(x => x.Date.Month == month)
                .ToList();

            List<int> datesAbsent = new List<int>();
            List<Shift> shifts = new List<Shift>();

            if (absences.Count > 0)
            {
                foreach (Absence absence in absences)
                {
                    for (int i = absence.StartDay; i <= absence.EndDay; i++)
                    {
                        datesAbsent.Add(i);
                    }
                }
            }

            int dateCount = 1;
            while (dateCount <= lastDayOfMonth)
            {
                Shift shift = new Shift();
                string shiftName = "";
                string pattern = employee.Team.ShiftPattern;
                DateTime testingDate = new DateTime(year, month, dateCount);
                bool isNotWeekend = (testingDate.DayOfWeek != DayOfWeek.Saturday) && (testingDate.DayOfWeek != DayOfWeek.Sunday);
                if (pattern == "DN3")
                {
                    if (datesAbsent.Contains(dateCount))
                    {
                        string absenceType = absences.FirstOrDefault(x => dateCount >= x.StartDay && dateCount <= x.EndDay).Type;
                        shiftName = isNotWeekend ? absenceType : "";
                    }
                    else if (employee.Team.NextMonthStartsWithNight && dateCount == 1)
                    {
                        shiftName = "N";
                    }
                    else if ((dateCount - start.Day) % 5 == 0)
                    {
                        shiftName = "D";
                    }
                    else if ((dateCount - start.Day) % 5 == 1)
                    {
                        shiftName = "N";
                    }
                    else
                    {
                        dateCount++;
                        continue;
                    }
                }
                else
                {
                    bool isNotNationalHoliday = nationalHolidays.FirstOrDefault(x => x.Date == new DateTime(year, month, dateCount)) == null;
                    
                    if (datesAbsent.Contains(dateCount))
                    {
                        string absenceType = absences.FirstOrDefault(x => dateCount >= x.StartDay && dateCount <= x.EndDay).Type;
                        shiftName = isNotWeekend ? absenceType : "";
                    }
                    else if (isNotWeekend && isNotNationalHoliday)
                    {
                        shiftName = pattern;
                    }
                    else
                    {
                        dateCount++;
                        continue;
                    }
                }

                if (shiftName != "")
                {
                    shift.Name = shiftName;
                    shift.Date = new DateTime(year, month, dateCount);
                    shift.Month = month;
                    shift.EmployeeId = employee.Id;
                    shifts.Add(shift);
                    _context.Shifts.Add(shift);
                    _context.SaveChanges();
                }

                dateCount++;
            }

            return shifts;
        }

        private string CalculateShiftName(int day, DateTime currentStartDate, bool nextMonthStartsWithNight)
        {
            // Logic to calculate the shift name based on the provided conditions.

            bool isNightShift = (nextMonthStartsWithNight && day == 1) || ((day - currentStartDate.Day) % 5 == 1);
            return isNightShift ? "N" : "D";
        }

        private DateTime CalculateNextMonthStartDate(DateTime currentStartDate, int lastDayOfMonth, bool nextMonthStartsWithNight)
        {
            // Logic to calculate the next month's start date based on the provided conditions.

            int dateIncreaser = nextMonthStartsWithNight ? 4 : 5;
            return currentStartDate.AddDays(lastDayOfMonth % 5 == 0 ? dateIncreaser : dateIncreaser - 1);
        }

        // Command methods

        private void EditRole(object parameter)
        {
            var employeeSchedule = parameter as EmployeeSchedule;
            int employeeId = employeeSchedule.Id;
            EmployeeRoleEditDTO employeeRoleEditDTO = new EmployeeRoleEditDTO();
            employeeRoleEditDTO.Roles = new List<DataOptionsItem>();
            employeeRoleEditDTO.Id = employeeId;
            employeeRoleEditDTO.EmployeeName = employeeSchedule.EmployeeName;
            employeeRoleEditDTO.EmployeeRole = employeeSchedule.EmployeeRole;

            List<TeamRole> roles = _context.TeamRoles.ToList();
            foreach (var role in roles)
            {
                employeeRoleEditDTO.Roles.Add(new DataOptionsItem()
                {
                    Value = role.Name,
                    DisplayText = role.Description,
                });
            }

            RoleEditorViewModel roleEditorViewModel = new RoleEditorViewModel();
            roleEditorViewModel.EmployeeRoleDTO = employeeRoleEditDTO;

            RoleEditorWindow roleEditorWindow = new RoleEditorWindow();
            roleEditorWindow.DataContext = roleEditorViewModel;

            roleEditorViewModel.CloseAction = () => roleEditorWindow.Close();

            roleEditorWindow.ShowDialog();

            EmployeeRoleEditDTO updatedEmployeeRole = roleEditorViewModel.EmployeeRoleDTO;
            TeamRole updatedRole = _context.TeamRoles.FirstOrDefault(x => x.Name == updatedEmployeeRole.EmployeeRole);
            Employee employeeToUpdate = _context.Employees.FirstOrDefault(x => x.Id == updatedEmployeeRole.Id);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.TeamRoleId = updatedRole.Id;
                employeeToUpdate.TeamRole = updatedRole;
                _context.Employees.Update(employeeToUpdate);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(employeeToUpdate));
            }

            LoadContext();
        }

        private void TransferEmployee(object parameter)
        {
            var employeeSchedule = parameter as EmployeeSchedule;
            int employeeId = employeeSchedule.Id;
            Employee employee = _context.Employees.FirstOrDefault(x => x.Id == employeeId);
            List<Team> teams = _context.Teams.ToList();
            TransferEmployeeDTO transferEmployee = new TransferEmployeeDTO()
            {
                Employee = employeeSchedule.EmployeeName,
                SelectedTeamValue = employeeSchedule.TeamId.ToString(),
                Teams = new List<DataOptionsItem>()
            };
            foreach (var team in teams)
            {
                transferEmployee.Teams.Add(new DataOptionsItem()
                {
                    Value = team.Id.ToString(),
                    DisplayText = team.Name,
                });
            }
            

            TransferEmployeeViewModel transferViewModel = new TransferEmployeeViewModel();
            transferViewModel.TransferEmployee = transferEmployee;

            TransferEmployeeWindow transferEmployeeWindow = new TransferEmployeeWindow();
            transferEmployeeWindow.DataContext = transferViewModel;

            transferViewModel.CloseAction = () => transferEmployeeWindow.Close();

            transferEmployeeWindow.ShowDialog();

            // Retrieve the updated data from the ViewModel
            TransferEmployeeDTO updatedTransfer = transferViewModel.TransferEmployee;
            Console.WriteLine(updatedTransfer);
            int updatedTeamId = Int32.Parse(updatedTransfer.SelectedTeamValue);
            Team updatedTeam = _context.Teams.FirstOrDefault(x => x.Id == updatedTeamId);

            employee.TeamId = updatedTeamId;
            employee.Team = updatedTeam;
            try
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditShift(object parameter)
        {
            var shiftDisplay = parameter as ShiftDisplayDTO;
            int employeeId = shiftDisplay.EmployeeId;
            Employee employee = _context.Employees.FirstOrDefault(x => x.Id == employeeId);
            string employeeName = $"{employee.LastName} {employee.FirstName}";
            EditShiftDTO shiftToEdit = new EditShiftDTO()
            {
                Id = shiftDisplay.Id,
                Name = shiftDisplay.Name,
                EmployeeId = employeeId,
                EmployeeName = employeeName,
                Date = shiftDisplay.Date,
                Month = shiftDisplay.Date.Month
            };

            EditShiftViewModel editShiftViewModel = new EditShiftViewModel();
            editShiftViewModel.EditShift = shiftToEdit;

            EditShiftWindow editShiftWindow = new EditShiftWindow();
            editShiftWindow.DataContext = editShiftViewModel;

            editShiftViewModel.CloseAction = () => editShiftWindow.Close();

            editShiftWindow.ShowDialog();

            EditShiftDTO updatedShift = editShiftViewModel.EditShift;
            Shift newShift;
            if (updatedShift.Id > 0)
            {
                newShift = _context.Shifts.FirstOrDefault(x => x.Id == updatedShift.Id);
                if (updatedShift.Name != newShift.Name)
                {
                    newShift.Name = updatedShift.Name;
                    _context.Shifts.Update(newShift);
                }
            }
            else
            {
                if (updatedShift.Name != shiftDisplay.Name)
                {
                    newShift = new Shift()
                    {
                        Name = updatedShift.Name,
                        Date = updatedShift.Date,
                        Month = updatedShift.Month,
                        EmployeeId = updatedShift.EmployeeId,
                        Employee = employee,
                    };

                
                    _context.Shifts.Add(newShift);
                }
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteShift(object parameter)
        {
            var shiftDisplay = parameter as ShiftDisplayDTO;
            Shift selectedShift = _context.Shifts.FirstOrDefault(x => x.Id == shiftDisplay.Id);

            if (selectedShift != null)
            {
                var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _context.Shifts.Remove(selectedShift);
                    _context.SaveChanges();
                }

                LoadContext();
            }
        }
    }
}
