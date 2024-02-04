using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class EditShiftDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public int Month { get; set; }
        public List<DataOptionsItem> ShiftOptions { get; set; }

        public EditShiftDTO()
        {
            ShiftOptions = new List<DataOptionsItem>
            {
                new DataOptionsItem { Value = "D", DisplayText = "Day shift (12hrs)" },
                new DataOptionsItem { Value = "N", DisplayText = "Night shift (12hrs)" },
                new DataOptionsItem { Value = "8", DisplayText = "Morning shift (8hrs)" },
                new DataOptionsItem { Value = "24", DisplayText = "24hrs shift" }
            };
        }
    }
}
