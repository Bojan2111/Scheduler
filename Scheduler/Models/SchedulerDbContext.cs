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
                new NationalHoliday() { Name = "Nova Godina", Date = new DateTime(2024, 1, 1) },
                new NationalHoliday() { Name = "Nova Godina", Date = new DateTime(2024, 1, 2) },
                new NationalHoliday() { Name = "Bozic", Date = new DateTime(2024, 1, 7) },
                new NationalHoliday() { Name = "Dan Drzavnosti", Date = new DateTime(2024, 2, 15) },
                new NationalHoliday() { Name = "Dan Drzavnosti", Date =new DateTime(2024, 2, 16) },
                new NationalHoliday() { Name = "Praznik Rada", Date = new DateTime(2024, 5, 1) },
                new NationalHoliday() { Name = "Praznik Rada", Date = new DateTime(2024, 5, 2) },
                new NationalHoliday() { Name = "Veliki Petak", Date = new DateTime(2024, 5, 3) },
                new NationalHoliday() { Name = "Velika Subota", Date = new DateTime(2024, 5, 4) },
                new NationalHoliday() { Name = "Uskrs", Date = new DateTime(2024, 5, 5) },
                new NationalHoliday() { Name = "Uskrsnji ponedeljak", Date = new DateTime(2024, 5, 6) },
                new NationalHoliday() { Name = "Dan primirja u I svetskom ratu", Date = new DateTime(2024, 11, 11) }
            );
            //        // Setting TeamRoles
            //        List<TeamRole> teamRoles = new List<TeamRole>()
            //{
            //    new TeamRole("VT"),
            //    new TeamRole("RA"),
            //    new TeamRole("SR"),
            //    new TeamRole("--")
            //};

            //        // Setting Teams
            //        List<Team> teams = new List<Team>()
            //{
            //    new Team("Test1", "DN3", 1, new DateTime(2024, 1, 2), new DateTime(2023, 4, 6), false),
            //    new Team("Test2", "DN3", 1, new DateTime(2024, 1, 3), new DateTime(2022, 6, 4), false),
            //    new Team("TestOrg", "8", 1, new DateTime(2024, 1, 1), new DateTime(2021, 4, 9), false)
            //};

            //        // Setting users
            //        List<User> users = new List<User>()
            //{
            //    new User("TestUser1", "tesuser1@org.org", "123"),
            //    new User("TestUser2", "tesuser2@org.org", "123"),
            //    new User("TestUser3", "tesuser3@org.org", "123"),
            //    new User("TestUser4", "tesuser4@org.org", "123"),
            //    new User("TestUser5", "tesuser5@org.org", "123"),
            //    new User("TestUser6", "tesuser6@org.org", "123"),
            //    new User("TestUser7", "tesuser7@org.org", "123"),
            //    new User("TestUser8", "tesuser8@org.org", "123"),
            //    new User("TestUser9", "tesuser9@org.org", "123"),
            //};

            //        // Setting Employees 1, 2, 3, 5, 4, 6, 8, 9, 7
            //        List<Employee> employees = new List<Employee>()
            //{
            //    new Employee(1, "TestFname1", "TestLname1", 64, 1, 1),
            //    new Employee(2, "TestFname2", "TestLname2", 21, 1, 2),
            //    new Employee(3, "TestFname3", "TestLname3", 121, 1, 4),
            //    new Employee(4, "TestFname4", "TestLname4", 56, 2, 1),
            //    new Employee(5, "TestFname5", "TestLname5", 78, 2, 1),
            //    new Employee(6, "TestFname6", "TestLname6", 15, 2, 3),
            //    new Employee(7, "TestFname7", "TestLname7", 34, 3, 4),
            //    new Employee(8, "TestFname8", "TestLname8", 121, 3, 4),
            //    new Employee(9, "TestFname9", "TestLname9", 89, 3, 4),
            //};

            //        // Setting absence
            //        List<Absence> absences = new List<Absence>()
            //{
            //    new Absence(2, "G", 2024, 1, 8, 26),
            //    new Absence(4, "V", 2024, 1, 20, 20),
            //    new Absence(9, "B", 2024, 1, 8, 12),
            //    new Absence(9, "G", 2024, 1, 22, 26),
            //};

            modelBuilder.Entity<TeamRole>().HasData
            (
                new TeamRole() { Name = ""}
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
