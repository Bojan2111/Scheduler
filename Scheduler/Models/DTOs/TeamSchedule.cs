using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class TeamSchedule
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public List<DayType> Dates { get; set; }
        public List<EmployeeSchedule> EmployeeSchedules { get; set; }
    }
}
