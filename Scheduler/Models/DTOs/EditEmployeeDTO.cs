using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class EditEmployeeDTO
    {
        public Employee Employee { get; set; }

        public List<DataOptionsItem> TeamNames { get; set; }
        public List<DataOptionsItem> TeamRoleNames { get; set; }
    }
}
