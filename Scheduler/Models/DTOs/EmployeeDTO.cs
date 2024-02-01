using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string TeamName { get; set; }

        public string TeamRoleName { get; set; }

        public int MonthsEmployed { get; set; }

        public string? ReligiousHoliday { get; set; }
    }
}
