using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class EmployeeSchedule
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeRole { get; set; }
        public bool IsRolePresent { get; set; }
        public List<ShiftDisplayDTO> Shifts { get; set; }
    }
}
