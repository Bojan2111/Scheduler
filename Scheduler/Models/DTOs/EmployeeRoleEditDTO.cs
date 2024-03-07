using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models.DTOs
{
    public class EmployeeRoleEditDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeRole { get; set; }
        public int RoleId { get; set; }
        public List<DataOptionsItem> Roles { get; set; }
    }
}
