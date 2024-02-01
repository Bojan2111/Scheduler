using Scheduler.Models;
using Scheduler.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeDTO ConvertToDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                TeamName = employee.Team?.Name,
                TeamRoleName = employee.TeamRole?.Name,
                MonthsEmployed = employee.MonthsEmployed,
                ReligiousHoliday = employee.ReligiousHoliday
            };
        }

        public static Employee ConvertToEntity(EmployeeDTO employeeDTO, Team team, TeamRole teamRole)
        {
            return new Employee
            {
                Id = employeeDTO.Id,
                LastName = employeeDTO.LastName,
                FirstName = employeeDTO.FirstName,
                Team = team,
                TeamRole = teamRole,
                TeamId = team.Id,
                TeamRoleId = teamRole.Id,
                MonthsEmployed = employeeDTO.MonthsEmployed,
                ReligiousHoliday = employeeDTO.ReligiousHoliday
            };
        }
    }
}
