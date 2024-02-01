using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class EditTeamDTO
    {
        public Team Team { get; set; }
        public List<DataOptionsItem> ShiftPatterns { get; set; }

        public EditTeamDTO()
        {
            ShiftPatterns = new List<DataOptionsItem>
            {
                new DataOptionsItem { Value = "DN3", DisplayText = "Day-Night-3 days free" },
                new DataOptionsItem { Value = "8", DisplayText = "Morning shift" }
            };
        }
    }
}
