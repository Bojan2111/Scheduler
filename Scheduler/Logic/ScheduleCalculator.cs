using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Logic
{
    public class ScheduleCalculator
    {
        //TODO - convert all methods from console app methods to EFC MVVM
        //void GenerateShifts(int teamId, int month, DateTime start)
        //{
        //    int year = start.Year;
        //    int daysInMonth = DateTime.DaysInMonth(year, month);

        //    // Select employees within the same team
        //    //List<Employee> allEmployeesInTeam = employees.FindAll(o => o.TeamId == teamId);
        //    //List<Employee> employeesInTeam = new List<Employee>();
        //    //List<int> vtIds = new List<int>();
        //    //foreach (Employee e in allEmployeesInTeam)
        //    //{
        //    //    if (e.TeamRoleId == 1)
        //    //    {
        //    //        employeesInTeam.Add(e);
        //    //        vtIds.Add(e.Id);
        //    //    }
        //    //}
        //    //if (vtIds.Count > 0)
        //    //{
        //    //    foreach (int idx in vtIds)
        //    //    {
        //    //        Employee toRemove = allEmployeesInTeam.FirstOrDefault(o => o.Id == idx);
        //    //        if (toRemove != null)
        //    //        {
        //    //            allEmployeesInTeam.Remove(toRemove);
        //    //        }
        //    //    }
        //    //}
        //    //List<Employee> sortedVts = employeesInTeam.OrderByDescending(o => o.MonthsEmployed).ToList();
        //    //List<Employee> sortedOthers = allEmployeesInTeam.OrderByDescending(o => o.MonthsEmployed).ToList();
        //    //foreach (Employee e in sortedOthers)
        //    //{
        //    //    sortedVts.Add(e);
        //    //}
        //    List<Employee> employeesInTeam = employees.Where(o => o.TeamId == teamId).ToList();

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

        //        void GenerateOtherShifts(int teamId, int month, int year, string pattern)
        //        {
        //            int daysInMonth = DateTime.DaysInMonth(year, month);

        //            // Select employees within the same team
        //            List<Employee> employeesInTeam = employees.Where(o => o.TeamId == teamId).ToList();

        //            foreach (Employee emp in employeesInTeam)
        //            {
        //                List<Absence> testAbsences = absences.FindAll(x => x.EmployeeId == emp.Id && x.Year == year && x.Month == month);
        //                List<int> datesAbsent = new List<int>();
        //                bool isAbsentThisMonth = testAbsences.Count() > 0;

        //                if (isAbsentThisMonth)
        //                {
        //                    foreach (Absence a in testAbsences)
        //                    {
        //                        for (int i = a.StartDay; i <= a.EndDay; i++)
        //                        {
        //                            datesAbsent.Add(i);
        //                        }
        //                    }
        //                }

        //                int dateCount = 1;

        //                while (dateCount <= daysInMonth)
        //                {
        //                    DateTime testingDate = new DateTime(year, month, dateCount);
        //                    bool isNotWeekend = (testingDate.DayOfWeek != DayOfWeek.Saturday) && (testingDate.DayOfWeek != DayOfWeek.Sunday);
        //                    bool isNotNationalHoliday = nationalHolidays.FirstOrDefault(x => x.Date == new DateTime(year, month, dateCount)) == null;
        //                    string shiftName = "";
        //                    if (datesAbsent.Contains(dateCount))
        //                    {
        //                        string absenceType = testAbsences.FirstOrDefault(x => dateCount >= x.StartDay && dateCount <= x.EndDay).Type;
        //                        shiftName = absenceType;
        //                    }
        //                    else if (isNotWeekend && isNotNationalHoliday)
        //                    {
        //                        shiftName = pattern;
        //                    }
        //                    else
        //                    {
        //                        dateCount++;
        //                        continue;
        //                    }

        //                    shifts.Add(new Shift(shiftName, month, new DateTime(year, month, dateCount), emp.Id));

        //                    dateCount++;
        //                }
        //            }
        //        }

        //        void PrintShifts(int teamId, int year, int month)
        //        {
        //            int daysInMonth = DateTime.DaysInMonth(year, month);

        //            //// Select employees within the same team
        //            List<Employee> employeesInTeam = employees.Where(o => o.TeamId == teamId).ToList();
        //            Console.WriteLine("====================================================================================================================================================");
        //            Console.WriteLine(" Dani u nedelji        |   | P | U | S | C | P | S | N | P | U | S | C | P | S | N | P | U | S | C | P | S | N | P | U | S | C | P | S | N | P | U | S |");
        //            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------");
        //            Console.WriteLine(" IME I PREZIME ZAPOSL. |   | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10| 11| 12| 13| 14| 15| 16| 17| 18| 19| 20| 21| 22| 23| 24| 25| 26| 27| 28| 29| 30| 31|");
        //            Console.WriteLine("====================================================================================================================================================");

        //            foreach (Employee emp in employeesInTeam)
        //            {
        //                Console.Write($" {emp.LastName} {emp.FirstName} | {teamRoles.FirstOrDefault(t => t.Id == emp.TeamRoleId).Name}|");

        //                List<Shift> employeeShifts = shifts.FindAll(x => x.EmployeeId == emp.Id);

        //                int dateCount = 1;

        //                while (dateCount <= daysInMonth)
        //                {
        //                    DateTime testDate = new DateTime(year, month, dateCount);
        //                    Shift? testShift = employeeShifts.FirstOrDefault(x => x.Date == testDate);
        //                    if (testShift != null)
        //                    {
        //                        Console.Write($" {testShift.Name} |");
        //                    }
        //                    else
        //                    {
        //                        Console.Write(" - |");
        //                    }
        //                    dateCount++;
        //                }

        //                Console.WriteLine("");
        //                Console.WriteLine("====================================================================================================================================================");
        //            }
        //            Console.WriteLine("\n");
        //        }

        //        DateTime nextMonthStartSelector(int teamId)
        //        {
        //            Shift lastShift = shifts.Last();

        //            bool dayShiftIsOnLastDay = (lastShift.Date.Day == DateTime.DaysInMonth(lastShift.Date.Year, lastShift.Date.Month)) && lastShift.Name == "D";
        //            if (dayShiftIsOnLastDay)
        //            {
        //                teams.FirstOrDefault(x => x.Id == teamId).NextMonthStartsWithNight = true;
        //            }
        //            int dateIncreaser;
        //            if (lastShift.Name == "D")
        //            {
        //                dateIncreaser = 5;
        //            }
        //            else
        //            {
        //                dateIncreaser = 4;
        //            }
        //            return lastShift.Date.AddDays(dateIncreaser);
        //        }

        //        void ThisMonthSchedule()
        //        {
        //            Console.WriteLine("Working schedule for this month\n");
        //            foreach (Team team in teams)
        //            {
        //                if (team.ShiftPattern == "DN3")
        //                {
        //                    GenerateShifts(team.Id, team.CurrentMonth, team.CurrentStartDate);

        //                    team.NextMonthStartDate = nextMonthStartSelector(team.Id);
        //                }
        //                else
        //                {
        //                    GenerateOtherShifts(team.Id, team.CurrentMonth, team.CurrentStartDate.Year, team.ShiftPattern);
        //                }

        //                PrintShifts(team.Id, team.CurrentStartDate.Year, team.CurrentMonth);
        //            }
        //        }

        //        void NextMonthSchedule()
        //        {
        //            Console.WriteLine("\nWorking schedule for next month\n");
        //            foreach (Team team in teams)
        //            {
        //                if (team.ShiftPattern == "DN3")
        //                {
        //                    team.CurrentStartDate = team.NextMonthStartDate;
        //                    team.CurrentMonth += 1;
        //                    GenerateShifts(team.Id, team.CurrentMonth, team.CurrentStartDate);

        //                    team.NextMonthStartDate = nextMonthStartSelector(team.Id);

        //                    Console.WriteLine($"Modified next month start is: {team.NextMonthStartDate}");
        //                }
        //                else
        //                {
        //                    team.CurrentStartDate = team.CurrentStartDate.AddMonths(1);
        //                    team.CurrentMonth += 1;
        //                    GenerateOtherShifts(team.Id, team.CurrentMonth, team.CurrentStartDate.Year, team.ShiftPattern);
        //                }
        //                PrintShifts(team.Id, team.CurrentStartDate.Year, team.CurrentMonth);
        //            }
        //        }
        //        Console.WriteLine("++++++++\nJanuary\n++++++++");
        //        ThisMonthSchedule();
        //        Console.WriteLine("++++++++\nFebruary\n++++++++");
        //        NextMonthSchedule();
        //        Console.WriteLine("++++++++\nMarch\n++++++++");

        //        NextMonthSchedule();
        //        Console.WriteLine("++++++++\nApril\n++++++++");
        //        NextMonthSchedule();
        //        Console.WriteLine("++++++++\nMay\n++++++++");
        //        NextMonthSchedule();
        //        Console.WriteLine("++++++++\nJune\n++++++++");
        //        NextMonthSchedule();

        //        Console.ReadLine();
    }
}
