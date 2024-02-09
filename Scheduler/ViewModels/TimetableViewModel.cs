using Scheduler.Logic;
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

namespace Scheduler.ViewModels
{
    public class TimetableViewModel : ViewModelBase
    {
        private SchedulerDbContext _context;

        private SchedulesDTO _schedule;

        private ObservableCollection<TeamSchedule> _teamSchedules;

        private ObservableCollection<EmployeeSchedule> _employeeSchedules;

        public SchedulesDTO Schedule
        {
            get { return _schedule; }
            set
            {
                _schedule = value;
                OnPropertyChanged(nameof(Schedule));
            }
        }
        public ObservableCollection<TeamSchedule> TeamSchedules
        {
            get { return _teamSchedules; }
            set
            {
                _teamSchedules = value;
                OnPropertyChanged(nameof(TeamSchedules));
            }
        }

        public ObservableCollection<EmployeeSchedule> EmployeeSchedules
        {
            get { return _employeeSchedules; }
            set
            {
                _employeeSchedules = value;
                OnPropertyChanged(nameof(EmployeeSchedules));
            }
        }

        private Shift _selectedShift;

        public Shift SelectedShift
        {
            get { return _selectedShift; }
            set
            {
                _selectedShift = value;
                OnPropertyChanged(nameof(SelectedShift));
            }
        }
        public ICommand DeleteCommand { get; }
        public EditShiftDTO GetShiftToEdit()
        {
            EditShiftDTO editShift = new EditShiftDTO();
            
            return editShift;
        }

        public EditShiftDTO GetEmptyShift()
        {
            EditShiftDTO emptyTeam = new EditShiftDTO();

            return emptyTeam;
        }
        public TimetableViewModel()
        {
            _context = new SchedulerDbContext();
            EmployeeSchedules = new ObservableCollection<EmployeeSchedule>();
            TeamSchedules = new ObservableCollection<TeamSchedule>();
            Schedule = new SchedulesDTO()
            {
                Month = 1,
                Dates = new List<DayType>(),
                TeamSchedules = TeamSchedules.ToList(),
            };
            LoadContext();

            DeleteCommand = new RelayCommand(DeleteShift);
        }

        private void LoadContext()
        {
            List<Shift> Shifts = _context.Shifts.ToList();

            if (Shifts.Count() == 0)
            {
                GenerateShiftsForEmployees();
            }
            else
            {
                int desiredMonth = 1;

                // Fetch shifts for each employee for each team for the specific month.
                var employeeShifts = _context.Shifts
                    .Where(shift => shift.Employee.Team.CurrentMonth == desiredMonth)
                    .ToList();

                // Group the shifts by team and employee.
                var groupedShifts = employeeShifts
                    .GroupBy(shift => new { TeamId = shift.Employee.TeamId, EmployeeId = shift.EmployeeId })
                    .ToList();

                // Now, you can create TeamSchedule and EmployeeSchedule objects.
                TeamSchedules.Clear();
                foreach (var group in groupedShifts)
                {
                    var teamSchedule = new TeamSchedule { Id = group.Key.TeamId, EmployeeSchedules = new List<EmployeeSchedule>() };

                    foreach (var shift in group)
                    {
                        var employeeSchedule = new EmployeeSchedule
                        {
                            Id = shift.EmployeeId,
                            EmployeeName = $"{shift.Employee.LastName} {shift.Employee.FirstName}",
                            EmployeeRole = shift.Employee.TeamRole.Name
                        };

                        teamSchedule.EmployeeSchedules.Add(employeeSchedule);
                    }

                    TeamSchedules.Add(teamSchedule);
                }
            }
        }

        private void GenerateWeekDays(int year, int month, int lastDay)
        {
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
                Schedule.Dates.Add(dayType);
            }
        }
        public void GenerateShiftsForEmployees()
        {
            //Schedule = new SchedulesDTO();
            List<TeamSchedule> teamSchedules = new List<TeamSchedule>();
            List<Team> teams =_context.Teams.ToList();
            Team testTeam = teams[0];
            int year = testTeam.CurrentStartDate.Year;
            int month = testTeam.CurrentMonth;
            int lastDayOfMonth = DateTime.DaysInMonth(year, month);
            GenerateWeekDays(year, month, lastDayOfMonth);

            foreach (Team team in teams)
            {
                TeamSchedule teamSchedule = new TeamSchedule();
                teamSchedule.Id = team.Id;
                teamSchedule.TeamName = team.Name;
                bool nextMonthStartsWithNight = team.NextMonthStartsWithNight;
                DateTime nextMonthStartDate = CalculateNextMonthStartDate(team.CurrentStartDate, lastDayOfMonth, nextMonthStartsWithNight);

                List<Employee> employeesInTeam = _context.Employees.Where(e => e.TeamId == team.Id).ToList();

                foreach (Employee employee in employeesInTeam)
                {
                    employee.Team = team;
                    List<Shift> shifts = GenerateShiftsForEmployee(employee);
                    EmployeeSchedule employeeSchedule = new EmployeeSchedule // ArgumentNullException - not set to an instance of obj
                    {
                        EmployeeName = $"{employee.LastName} {employee.FirstName}",
                        EmployeeRole = employee.TeamRole.Name,
                        Shifts = shifts
                    };

                    teamSchedule.EmployeeSchedules.Add(employeeSchedule);
                }

                teamSchedules.Add(teamSchedule);
                //Schedule.TeamSchedules.Add(teamSchedule);
            }
            Schedule.TeamSchedules = teamSchedules;
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

                    dateCount++;
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

        private void DeleteShift()
        {
            var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _context.Shifts.Remove(SelectedShift);
                _context.SaveChanges();
            }

            LoadContext();
        }
    }
}
