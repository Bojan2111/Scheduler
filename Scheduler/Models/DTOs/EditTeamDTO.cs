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
        public List<ShiftPatternItem> ShiftPatterns { get; set; }

        public EditTeamDTO()
        {
            ShiftPatterns = new List<ShiftPatternItem>
            {
                new ShiftPatternItem { Value = "DN3", DisplayText = "Day-Night-3 days free" },
                new ShiftPatternItem { Value = "8", DisplayText = "Morning shift" }
            };
        }
    }
}
