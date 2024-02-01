using Scheduler.Converters;
using Scheduler.Models;
using Scheduler.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        private SchedulerDbContext _context;

        private EditEmployeeDTO _itemToEdit;

        public event EventHandler ItemUpdated;

        public EditEmployeeDTO ItemToEdit
        {
            get { return _itemToEdit; }
            set
            {
                _itemToEdit = value;
                OnPropertyChanged(nameof(ItemToEdit));
            }
        }

        public EditEmployeeViewModel()
        {
            ItemToEdit = new EditEmployeeDTO();
        }

        public void SetItem(EditEmployeeDTO itemToEdit)
        {
            ItemToEdit = itemToEdit;
        }

        public void UpdateItem(EmployeeDTO item)
        {
            _context = new SchedulerDbContext();
            Team team = _context.Teams.FirstOrDefault(x => x.Name == item.TeamName);
            TeamRole teamRole = _context.TeamRoles.FirstOrDefault(x => x.Name == item.TeamRoleName);

            Employee converted = EmployeeConverter.ConvertToEntity(item, team, teamRole);
            var employeeToUpdate = _context.Employees.FirstOrDefault(x => x.Id == converted.Id);

            if (employeeToUpdate != null)
            {
                employeeToUpdate.LastName = converted.LastName;
                employeeToUpdate.FirstName = converted.FirstName;
                employeeToUpdate.TeamId = converted.TeamId;
                employeeToUpdate.TeamRoleId = converted.TeamRoleId;
                employeeToUpdate.MonthsEmployed = converted.MonthsEmployed;
                employeeToUpdate.ReligiousHoliday = converted.ReligiousHoliday;
            }
            else
            {
                _context.Employees.Add(converted);
            }

            _context.SaveChanges();
            OnItemUpdated();
        }

        private void OnItemUpdated()
        {
            ItemUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
