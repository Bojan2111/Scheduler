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
            LoadContext();

            DeleteCommand = new RelayCommand(DeleteShift);
        }

        private void LoadContext()
        {
            /*
                public class EmployeeSchedule
                {
                    public int Id { get; set; }
                    public string EmployeeName { get; set; }
                    public string EmployeeRole { get; set; }
                    public List<Shift> Shifts { get; set; }
                }
                public class SchedulesDTO
                {
                    public int Month { get; set; }
                    public List<int> Dates { get; set; }
                    public List<TeamSchedule> TeamSchedules { get; set; }
                }
                public class TeamSchedule
                {
                    public int Id { get; set; }
                    public string TeamName { get; set; }
                    public List<EmployeeSchedule> EmployeeSchedules { get; set; }
                }
             */
            
            Schedule = new SchedulesDTO();
            EmployeeSchedules = new ObservableCollection<EmployeeSchedule>(); // get shifts for each employee
            TeamSchedules = new ObservableCollection<TeamSchedule>(); // get shifts for each team
            //Teams = new ObservableCollection<Team>(_context.Teams);
            //Employees = new ObservableCollection<Employee>(_context.Employees);
            // db query to get all shifts for each employee for each team for specific month.
            // Month is in shift class, employeeId is in shift class, teamId is in employee class.
            
            

            if (_context.Shifts.Count() == 0)
            {
                CreateShifts();
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
            List<NationalHoliday> nationalHolidays = _context.NationalHolidays.ToList();
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
                dayType.IsNotWorkDay = true;
                Schedule.Dates.Add(dayType);
            }
        }

        private void CreateShifts()
        {
            /*
               public class EmployeeSchedule
               {
                   public int Id { get; set; }
                   public string EmployeeName { get; set; }
                   public string EmployeeRole { get; set; }
                   public List<Shift> Shifts { get; set; }
               }
               public class SchedulesDTO
               {
                   public int Month { get; set; }
                   public List<int> Dates { get; set; }
                   public List<TeamSchedule> TeamSchedules { get; set; }
               }
               public class TeamSchedule
               {
                   public int Id { get; set; }
                   public string TeamName { get; set; }
                   public List<EmployeeSchedule> EmployeeSchedules { get; set; }
               }
                
            */
            var teams = _context.Teams.ToList();
            var employees = _context.Employees;
            var absences = _context.Absences;
            var holidays = _context.NationalHolidays;

            // Create shifts for each employee
            // Add list of shifts to the employeeSchedule
            // Add corresponding EmployeeSchedule list to the teamschedule
            // Add all teamSchedules for current month to the ScheduleDTO
            /*
             * for each employee I have to use the TeamId to connect him to the corresponding team
             * inside each team I have a starting date, current month, next month's start date and if the next month starts with Night
             * I will use these variables in creating shifts.
             * Start date has to be D, next date is N, next three days are skipped, and same again until the last day of the month
             * If the last day is assigned with D, then the next month will start with N, so the bool will be marked as true.
             * The start date for the next month will be calculated by adding 4 days to the last shift marked with D, and 5 days if the
             * last day of the month is marked with D.
             * Absences and national holidays are also important, as well as working on weekends. If there is a corresponding entry for
             * each employee with start and end date of absence, these shifts will be marked as the absence.Name suggests, but only if this
             * kind of shift falls on working days. Weekends will be skipped in this case. All entries from the nationalHolidays table should
             * be checked to see if the shift matches the national holiday.
             * There are two groups of schedules. One is DN3 type - where there is Day, Night and three days free, and the other is 8, where
             * there are 8 hours shifts from Monday to Friday. In the latter case, if National Holiday falls on any working day, it is not
             * marked as a shift, but skipped.
             * Weekends and national holidays will use different background color on the frontend, so I will implement this in the
             * viewModel later.
             */
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
                    EmployeeSchedule employeeSchedule = new EmployeeSchedule
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

            // Logic to generate shifts for the employee here based on the provided conditions.
            // Use lastDayOfMonth, nextMonthStartsWithNight, absences, national holidays, etc.
            // Add shifts to the 'shifts' list.
            int dateCount = 1;
            while (dateCount <= lastDayOfMonth)
            {
                Shift shift = new Shift();
                string shiftName = "";
                string pattern = employee.Team.ShiftPattern;
                if (pattern == "DN3")
                {
                    if (datesAbsent.Contains(dateCount))
                    {
                        string absenceType = absences.FirstOrDefault(x => dateCount >= x.StartDay && dateCount <= x.EndDay).Type;
                        shiftName = absenceType;
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
                    DateTime testingDate = new DateTime(year, month, dateCount);
                    bool isNotWeekend = (testingDate.DayOfWeek != DayOfWeek.Saturday) && (testingDate.DayOfWeek != DayOfWeek.Sunday);
                    bool isNotNationalHoliday = nationalHolidays.FirstOrDefault(x => x.Date == new DateTime(year, month, dateCount)) == null;
                    
                    if (datesAbsent.Contains(dateCount))
                    {
                        string absenceType = absences.FirstOrDefault(x => dateCount >= x.StartDay && dateCount <= x.EndDay).Type;
                        shiftName = absenceType;
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

            //for (int day = 1; day <= lastDayOfMonth; day++)
            //{
            //    DateTime shiftDate = new DateTime(year, month, day);
            //    string shiftName = CalculateShiftName(day, employee.Team.CurrentStartDate, employee.Team.NextMonthStartsWithNight);

            //    // Check for absences, national holidays, weekends, etc.
            //    // Check if shift is final in current month (D cannot be more than lastDayOfMonth - 4, same for N, except when D is last)

            //    Shift shift = new Shift
            //    {
            //        Name = shiftName,
            //        Month = month,
            //        Date = shiftDate,
            //        EmployeeId = employee.Id,
            //        Employee = employee
            //    };

            //    shifts.Add(shift);
            //}

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
        //void GenerateShifts(int teamId, int month, DateTime start)
        //{
        //    int year = start.Year;
        //    int daysInMonth = DateTime.DaysInMonth(year, month);

        //    //Select employees within the same team
        //    List<Employee> allEmployeesInTeam = Employees.SelectMany(o => o.TeamId == teamId);
        //    List<Employee> employeesInTeam = new List<Employee>();
        //    List<int> vtIds = new List<int>();
        //    foreach (Employee e in allEmployeesInTeam)
        //    {
        //        if (e.TeamRoleId == 1)
        //        {
        //            employeesInTeam.Add(e);
        //            vtIds.Add(e.Id);
        //        }
        //    }
        //    if (vtIds.Count > 0)
        //    {
        //        foreach (int idx in vtIds)
        //        {
        //            Employee toRemove = allEmployeesInTeam.FirstOrDefault(o => o.Id == idx);
        //            if (toRemove != null)
        //            {
        //                allEmployeesInTeam.Remove(toRemove);
        //            }
        //        }
        //    }
        //    List<Employee> sortedVts = employeesInTeam.OrderByDescending(o => o.MonthsEmployed).ToList();
        //    List<Employee> sortedOthers = allEmployeesInTeam.OrderByDescending(o => o.MonthsEmployed).ToList();
        //    foreach (Employee e in sortedOthers)
        //    {
        //        sortedVts.Add(e);
        //    }
        //    //List<Employee> employeesInTeam = employees.Where(o => o.TeamId == teamId).ToList();

        //    // Of selected employees, find all with TeamRoleId == 1 and sort them by MonthsEmployed
        //    // Sort other employees by MonthsEmployed
        //    bool nightStartsFirst = teams.FirstOrDefault(x => x.Id == teamId).NextMonthStartsWithNight;

        //    foreach (Employee emp in employeesInTeam)
        //    {
        //        List<Absence> testAbsences = absences.FindAll(x => x.EmployeeId == emp.Id && x.Year == year && x.Month == month);
        //        List<int> datesAbsent = new List<int>();
        //        bool isAbsentThisMonth = testAbsences.Count() > 0;

        //        if (isAbsentThisMonth)
        //        {
        //            foreach (Absence a in testAbsences)
        //            {
        //                for (int i = a.StartDay; i <= a.EndDay; i++)
        //                {
        //                    datesAbsent.Add(i);
        //                }
        //            }
        //        }

        //        int dateCount = 1;

        //        while (dateCount <= daysInMonth)
        //        {
        //            string shiftName = "";
        //            if (datesAbsent.Contains(dateCount))
        //            {
        //                string absenceType = testAbsences.FirstOrDefault(x => dateCount >= x.StartDay && dateCount <= x.EndDay).Type;
        //                shiftName = absenceType;
        //            }
        //            else if (nightStartsFirst && dateCount == 1)
        //            {
        //                shiftName = "N";
        //            }
        //            else if ((dateCount - start.Day) % 5 == 0)
        //            {
        //                shiftName = "D";
        //            }
        //            else if ((dateCount - start.Day) % 5 == 1)
        //            {
        //                shiftName = "N";
        //            }
        //            else
        //            {
        //                dateCount++;
        //                continue;
        //            }
        //            shifts.Add(new Shift(shiftName, month, new DateTime(year, month, dateCount), emp.Id));

        //            dateCount++;
        //        }
        //    }
        //}

        //void GenerateOtherShifts(int teamId, int month, int year, string pattern)
        //{
        //    int daysInMonth = DateTime.DaysInMonth(year, month);

        //    // Select employees within the same team
        //    List<Employee> employeesInTeam = employees.Where(o => o.TeamId == teamId).ToList();

        //    foreach (Employee emp in employeesInTeam)
        //    {
        //        List<Absence> testAbsences = absences.FindAll(x => x.EmployeeId == emp.Id && x.Year == year && x.Month == month);
        //        List<int> datesAbsent = new List<int>();
        //        bool isAbsentThisMonth = testAbsences.Count() > 0;

        //        if (isAbsentThisMonth)
        //        {
        //            foreach (Absence a in testAbsences)
        //            {
        //                for (int i = a.StartDay; i <= a.EndDay; i++)
        //                {
        //                    datesAbsent.Add(i);
        //                }
        //            }
        //        }

        //        int dateCount = 1;

        //        while (dateCount <= daysInMonth)
        //        {
        //            DateTime testingDate = new DateTime(year, month, dateCount);
        //            bool isNotWeekend = (testingDate.DayOfWeek != DayOfWeek.Saturday) && (testingDate.DayOfWeek != DayOfWeek.Sunday);
        //            bool isNotNationalHoliday = nationalHolidays.FirstOrDefault(x => x.Date == new DateTime(year, month, dateCount)) == null;
        //            string shiftName = "";
        //            if (datesAbsent.Contains(dateCount))
        //            {
        //                string absenceType = testAbsences.FirstOrDefault(x => dateCount >= x.StartDay && dateCount <= x.EndDay).Type;
        //                shiftName = absenceType;
        //            }
        //            else if (isNotWeekend && isNotNationalHoliday)
        //            {
        //                shiftName = pattern;
        //            }
        //            else
        //            {
        //                dateCount++;
        //                continue;
        //            }

        //            shifts.Add(new Shift(shiftName, month, new DateTime(year, month, dateCount), emp.Id));

        //            dateCount++;
        //        }
        //    }
        //}

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
