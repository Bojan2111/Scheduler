using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class DayType
    {
        public int Day { get; set; }
        public string DayName { get; set; }
        public bool IsNotWorkDay { get; set; }
    }
}
