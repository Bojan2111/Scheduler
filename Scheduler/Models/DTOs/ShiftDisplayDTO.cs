using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class ShiftDisplayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public bool IsNotWorkDay { get; set; }
    }
}
