using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class SchedulerDbContext : DbContext
    {
        public DbSet<NationalHoliday> NationalHolidays { get; set; }
        public DbSet<TeamRole> TeamRoles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Scheduler;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NationalHoliday>().HasData(
                new NationalHoliday() { Id = 1, Name = "Nova Godina", Date = new DateTime(2024, 1, 1) },
                new NationalHoliday() { Id = 2, Name = "Nova Godina", Date = new DateTime(2024, 1, 2) },
                new NationalHoliday() { Id = 3, Name = "Bozic", Date = new DateTime(2024, 1, 7) },
                new NationalHoliday() { Id = 4, Name = "Dan Drzavnosti", Date = new DateTime(2024, 2, 15) },
                new NationalHoliday() { Id = 5, Name = "Dan Drzavnosti", Date =new DateTime(2024, 2, 16) },
                new NationalHoliday() { Id = 6, Name = "Praznik Rada", Date = new DateTime(2024, 5, 1) },
                new NationalHoliday() { Id = 7, Name = "Praznik Rada", Date = new DateTime(2024, 5, 2) },
                new NationalHoliday() { Id = 8, Name = "Veliki Petak", Date = new DateTime(2024, 5, 3) },
                new NationalHoliday() { Id = 9, Name = "Velika Subota", Date = new DateTime(2024, 5, 4) },
                new NationalHoliday() { Id = 10, Name = "Uskrs", Date = new DateTime(2024, 5, 5) },
                new NationalHoliday() { Id = 11, Name = "Uskrsnji ponedeljak", Date = new DateTime(2024, 5, 6) },
                new NationalHoliday() { Id = 12, Name = "Dan primirja u I svetskom ratu", Date = new DateTime(2024, 11, 11) }
            );

            modelBuilder.Entity<TeamRole>().HasData(
                new TeamRole() { Id = 1, Name = "VT", Description = "Vođa smene" },
                new TeamRole() { Id = 2, Name = "RA", Description = "Reanimaciona ambulanta" },
                new TeamRole() { Id = 3, Name = "SR", Description = "Spoljna reanimacija" },
                new TeamRole() { Id = 4, Name = "--", Description = "Tehničar na strani" }
            );
            modelBuilder.Entity<Team>().HasData(
                new Team() { Id = 1, Name = "TeamBn", ShiftPattern = "DN3", CurrentMonth = 1, CurrentStartDate = new DateTime(2024, 1, 2), NextMonthStartDate = new DateTime(2024, 2, 1), NextMonthStartsWithNight = false },
                new Team() { Id = 2, Name = "TeamČa", ShiftPattern = "DN3", CurrentMonth = 1, CurrentStartDate = new DateTime(2024, 1, 3), NextMonthStartDate = new DateTime(2024, 2, 2), NextMonthStartsWithNight = false },
                new Team() { Id = 3, Name = "Teamić", ShiftPattern = "DN3", CurrentMonth = 1, CurrentStartDate = new DateTime(2024, 1, 1), NextMonthStartDate = new DateTime(2024, 2, 5), NextMonthStartsWithNight = true },
                new Team() { Id = 4, Name = "TeamRn", ShiftPattern = "DN3", CurrentMonth = 1, CurrentStartDate = new DateTime(2024, 1, 5), NextMonthStartDate = new DateTime(2024, 2, 4), NextMonthStartsWithNight = false },
                new Team() { Id = 5, Name = "TeamŠi", ShiftPattern = "DN3", CurrentMonth = 1, CurrentStartDate = new DateTime(2024, 1, 4), NextMonthStartDate = new DateTime(2024, 2, 3), NextMonthStartsWithNight = false },
                new Team() { Id = 6, Name = "TeamPr", ShiftPattern = "8", CurrentMonth = 1, CurrentStartDate = new DateTime(2024, 1, 1), NextMonthStartDate = new DateTime(2021, 4, 9), NextMonthStartsWithNight = false },
                new Team() { Id = 7, Name = "TeamBo", ShiftPattern = "8", CurrentMonth = 1, CurrentStartDate = new DateTime(2024, 1, 1), NextMonthStartDate = new DateTime(2023, 4, 6), NextMonthStartsWithNight = false }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, LastName = "TestLname1", FirstName = "TestFname1", MonthsEmployed = 99, TeamId = 1, TeamRoleId = 1 },
                new Employee() { Id = 2, LastName = "TestLname2", FirstName = "TestFname2", MonthsEmployed = 97, TeamId = 1, TeamRoleId = 1 },
                new Employee() { Id = 3, LastName = "TestLname3", FirstName = "TestFname3", MonthsEmployed = 93, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 4, LastName = "TestLname4", FirstName = "TestFname4", MonthsEmployed = 92, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 5, LastName = "TestLname5", FirstName = "TestFname5", MonthsEmployed = 88, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 6, LastName = "TestLname6", FirstName = "TestFname6", MonthsEmployed = 79, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 7, LastName = "TestLname7", FirstName = "TestFname7", MonthsEmployed = 64, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 8, LastName = "TestLname8", FirstName = "TestFname8", MonthsEmployed = 61, TeamId = 1, TeamRoleId = 2 },
                new Employee() { Id = 9, LastName = "TestLname9", FirstName = "TestFname9", MonthsEmployed = 56, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 10, LastName = "TestLname10", FirstName = "TestFname10", MonthsEmployed = 47, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 11, LastName = "TestLname11", FirstName = "TestFname11", MonthsEmployed = 35, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 12, LastName = "TestLname12", FirstName = "TestFname12", MonthsEmployed = 32, TeamId = 1, TeamRoleId = 2 },
                new Employee() { Id = 13, LastName = "TestLname13", FirstName = "TestFname13", MonthsEmployed = 28, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 14, LastName = "TestLname14", FirstName = "TestFname14", MonthsEmployed = 25, TeamId = 1, TeamRoleId = 4 },
                new Employee() { Id = 15, LastName = "TestLname15", FirstName = "TestFname15", MonthsEmployed = 99, TeamId = 2, TeamRoleId = 1 },
                new Employee() { Id = 16, LastName = "TestLname16", FirstName = "TestFname16", MonthsEmployed = 97, TeamId = 2, TeamRoleId = 1 },
                new Employee() { Id = 17, LastName = "TestLname17", FirstName = "TestFname17", MonthsEmployed = 93, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 18, LastName = "TestLname18", FirstName = "TestFname18", MonthsEmployed = 92, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 19, LastName = "TestLname19", FirstName = "TestFname19", MonthsEmployed = 88, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 20, LastName = "TestLname20", FirstName = "TestFname20", MonthsEmployed = 79, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 21, LastName = "TestLname21", FirstName = "TestFname21", MonthsEmployed = 64, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 22, LastName = "TestLname22", FirstName = "TestFname22", MonthsEmployed = 61, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 23, LastName = "TestLname23", FirstName = "TestFname23", MonthsEmployed = 56, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 24, LastName = "TestLname24", FirstName = "TestFname24", MonthsEmployed = 47, TeamId = 2, TeamRoleId = 3 },
                new Employee() { Id = 25, LastName = "TestLname25", FirstName = "TestFname25", MonthsEmployed = 35, TeamId = 2, TeamRoleId = 2 },
                new Employee() { Id = 26, LastName = "TestLname26", FirstName = "TestFname26", MonthsEmployed = 32, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 27, LastName = "TestLname27", FirstName = "TestFname27", MonthsEmployed = 28, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 28, LastName = "TestLname28", FirstName = "TestFname28", MonthsEmployed = 25, TeamId = 2, TeamRoleId = 4 },
                new Employee() { Id = 29, LastName = "TestLname29", FirstName = "TestFname29", MonthsEmployed = 99, TeamId = 3, TeamRoleId = 1 },
                new Employee() { Id = 30, LastName = "TestLname30", FirstName = "TestFname30", MonthsEmployed = 97, TeamId = 3, TeamRoleId = 1 },
                new Employee() { Id = 31, LastName = "TestLname31", FirstName = "TestFname31", MonthsEmployed = 93, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 32, LastName = "TestLname32", FirstName = "TestFname32", MonthsEmployed = 92, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 33, LastName = "TestLname33", FirstName = "TestFname33", MonthsEmployed = 88, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 34, LastName = "TestLname34", FirstName = "TestFname34", MonthsEmployed = 79, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 35, LastName = "TestLname35", FirstName = "TestFname35", MonthsEmployed = 64, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 36, LastName = "TestLname36", FirstName = "TestFname36", MonthsEmployed = 61, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 37, LastName = "TestLname37", FirstName = "TestFname37", MonthsEmployed = 56, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 38, LastName = "TestLname38", FirstName = "TestFname38", MonthsEmployed = 47, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 39, LastName = "TestLname39", FirstName = "TestFname39", MonthsEmployed = 35, TeamId = 3, TeamRoleId = 3 },
                new Employee() { Id = 40, LastName = "TestLname40", FirstName = "TestFname40", MonthsEmployed = 32, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 41, LastName = "TestLname41", FirstName = "TestFname41", MonthsEmployed = 28, TeamId = 3, TeamRoleId = 2 },
                new Employee() { Id = 42, LastName = "TestLname42", FirstName = "TestFname42", MonthsEmployed = 25, TeamId = 3, TeamRoleId = 4 },
                new Employee() { Id = 43, LastName = "TestLname43", FirstName = "TestFname43", MonthsEmployed = 99, TeamId = 4, TeamRoleId = 1 },
                new Employee() { Id = 44, LastName = "TestLname44", FirstName = "TestFname44", MonthsEmployed = 97, TeamId = 4, TeamRoleId = 1 },
                new Employee() { Id = 45, LastName = "TestLname45", FirstName = "TestFname45", MonthsEmployed = 93, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 46, LastName = "TestLname46", FirstName = "TestFname46", MonthsEmployed = 92, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 47, LastName = "TestLname47", FirstName = "TestFname47", MonthsEmployed = 88, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 48, LastName = "TestLname48", FirstName = "TestFname48", MonthsEmployed = 79, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 49, LastName = "TestLname49", FirstName = "TestFname49", MonthsEmployed = 64, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 50, LastName = "TestLname50", FirstName = "TestFname50", MonthsEmployed = 61, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 51, LastName = "TestLname51", FirstName = "TestFname51", MonthsEmployed = 56, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 52, LastName = "TestLname52", FirstName = "TestFname52", MonthsEmployed = 47, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 53, LastName = "TestLname53", FirstName = "TestFname53", MonthsEmployed = 35, TeamId = 4, TeamRoleId = 2 },
                new Employee() { Id = 54, LastName = "TestLname54", FirstName = "TestFname54", MonthsEmployed = 32, TeamId = 4, TeamRoleId = 3 },
                new Employee() { Id = 55, LastName = "TestLname55", FirstName = "TestFname55", MonthsEmployed = 28, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 56, LastName = "TestLname56", FirstName = "TestFname56", MonthsEmployed = 25, TeamId = 4, TeamRoleId = 4 },
                new Employee() { Id = 57, LastName = "TestLname57", FirstName = "TestFname57", MonthsEmployed = 99, TeamId = 5, TeamRoleId = 1 },
                new Employee() { Id = 58, LastName = "TestLname58", FirstName = "TestFname58", MonthsEmployed = 97, TeamId = 5, TeamRoleId = 1 },
                new Employee() { Id = 59, LastName = "TestLname59", FirstName = "TestFname59", MonthsEmployed = 93, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 60, LastName = "TestLname60", FirstName = "TestFname60", MonthsEmployed = 92, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 61, LastName = "TestLname61", FirstName = "TestFname61", MonthsEmployed = 88, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 62, LastName = "TestLname62", FirstName = "TestFname62", MonthsEmployed = 79, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 63, LastName = "TestLname63", FirstName = "TestFname63", MonthsEmployed = 64, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 64, LastName = "TestLname64", FirstName = "TestFname64", MonthsEmployed = 61, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 65, LastName = "TestLname65", FirstName = "TestFname65", MonthsEmployed = 56, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 66, LastName = "TestLname66", FirstName = "TestFname66", MonthsEmployed = 47, TeamId = 5, TeamRoleId = 3 },
                new Employee() { Id = 67, LastName = "TestLname67", FirstName = "TestFname67", MonthsEmployed = 35, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 68, LastName = "TestLname68", FirstName = "TestFname68", MonthsEmployed = 32, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 69, LastName = "TestLname69", FirstName = "TestFname69", MonthsEmployed = 28, TeamId = 5, TeamRoleId = 2 },
                new Employee() { Id = 70, LastName = "TestLname70", FirstName = "TestFname70", MonthsEmployed = 25, TeamId = 5, TeamRoleId = 4 },
                new Employee() { Id = 71, LastName = "TestLname71", FirstName = "TestFname71", MonthsEmployed = 99, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 72, LastName = "TestLname72", FirstName = "TestFname72", MonthsEmployed = 97, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 73, LastName = "TestLname73", FirstName = "TestFname73", MonthsEmployed = 93, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 74, LastName = "TestLname74", FirstName = "TestFname74", MonthsEmployed = 92, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 75, LastName = "TestLname75", FirstName = "TestFname75", MonthsEmployed = 88, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 76, LastName = "TestLname76", FirstName = "TestFname76", MonthsEmployed = 79, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 77, LastName = "TestLname77", FirstName = "TestFname77", MonthsEmployed = 64, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 78, LastName = "TestLname78", FirstName = "TestFname78", MonthsEmployed = 61, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 79, LastName = "TestLname79", FirstName = "TestFname79", MonthsEmployed = 56, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 80, LastName = "TestLname80", FirstName = "TestFname80", MonthsEmployed = 47, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 81, LastName = "TestLname81", FirstName = "TestFname81", MonthsEmployed = 35, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 82, LastName = "TestLname82", FirstName = "TestFname82", MonthsEmployed = 32, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 83, LastName = "TestLname83", FirstName = "TestFname83", MonthsEmployed = 28, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 84, LastName = "TestLname84", FirstName = "TestFname84", MonthsEmployed = 25, TeamId = 6, TeamRoleId = 4 },
                new Employee() { Id = 85, LastName = "TestLname85", FirstName = "TestFname85", MonthsEmployed = 99, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 86, LastName = "TestLname86", FirstName = "TestFname86", MonthsEmployed = 97, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 87, LastName = "TestLname87", FirstName = "TestFname87", MonthsEmployed = 93, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 88, LastName = "TestLname88", FirstName = "TestFname88", MonthsEmployed = 92, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 89, LastName = "TestLname89", FirstName = "TestFname89", MonthsEmployed = 88, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 90, LastName = "TestLname90", FirstName = "TestFname90", MonthsEmployed = 79, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 91, LastName = "TestLname91", FirstName = "TestFname91", MonthsEmployed = 64, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 92, LastName = "TestLname92", FirstName = "TestFname92", MonthsEmployed = 61, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 93, LastName = "TestLname93", FirstName = "TestFname93", MonthsEmployed = 56, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 94, LastName = "TestLname94", FirstName = "TestFname94", MonthsEmployed = 47, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 95, LastName = "TestLname95", FirstName = "TestFname95", MonthsEmployed = 35, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 96, LastName = "TestLname96", FirstName = "TestFname96", MonthsEmployed = 32, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 97, LastName = "TestLname97", FirstName = "TestFname97", MonthsEmployed = 28, TeamId = 7, TeamRoleId = 4 },
                new Employee() { Id = 98, LastName = "TestLname98", FirstName = "TestFname98", MonthsEmployed = 25, TeamId = 7, TeamRoleId = 4 }

            );

            modelBuilder.Entity<Absence>().HasData(
                new Absence() { Id = 1, EmployeeId = 2, Type = "GO", Year = 2024, Month = 1, StartDay = 8, EndDay = 26 },
                new Absence() { Id = 2, EmployeeId = 4, Type = "VP", Year = 2024, Month = 1, StartDay = 20, EndDay = 20 },
                new Absence() { Id = 3, EmployeeId = 9, Type = "BO", Year = 2024, Month = 1, StartDay = 8, EndDay = 12 },
                new Absence() { Id = 4, EmployeeId = 9, Type = "GO", Year = 2024, Month = 1, StartDay = 22, EndDay = 26 }

            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
