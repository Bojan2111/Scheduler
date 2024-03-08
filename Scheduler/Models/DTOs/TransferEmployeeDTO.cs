using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class TransferEmployeeDTO
    {
        public string Employee { get; set; }
        public string SelectedTeamValue { get; set; }
        public List<DataOptionsItem> Teams { get; set; }
    }
}
