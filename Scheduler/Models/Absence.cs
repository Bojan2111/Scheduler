using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class Absence
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int StartDay { get; set; }
        public int EndDay { get; set; }
    }
}
