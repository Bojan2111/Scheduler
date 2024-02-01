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

        public void UpdateItem(Employee item)
        {
            _context = new SchedulerDbContext();
            var employeeToUpdate = _context.Employees.FirstOrDefault(x => x.Id == item.Id);

            if (employeeToUpdate != null)
            {
                employeeToUpdate.LastName = item.LastName;
                employeeToUpdate.FirstName = item.FirstName;
                employeeToUpdate.TeamId = item.TeamId;
                employeeToUpdate.TeamRoleId = item.TeamRoleId;
                employeeToUpdate.MonthsEmployed = item.MonthsEmployed;
                employeeToUpdate.ReligiousHoliday = item.ReligiousHoliday;
            }
            else
            {
                _context.Employees.Add(item);
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
