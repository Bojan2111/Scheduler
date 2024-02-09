using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class SchedulesDTO
    {
        public int Month { get; set; }
        public List<DayType> Dates { get; set; }
        public List<TeamSchedule> TeamSchedules { get; set; }
    }
}
