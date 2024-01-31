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

        private EditEmployeeDTO _employeeToEdit;

        public event EventHandler EmployeeUpdated;

        public EditEmployeeDTO EmployeeToEdit
        {
            get { return _employeeToEdit; }
            set
            {
                _employeeToEdit = value;
                OnPropertyChanged(nameof(EmployeeToEdit));
            }
        }

        public EditEmployeeViewModel()
        {
            EmployeeToEdit = new EditEmployeeDTO();
        }

        public void SetEmployee(EditEmployeeDTO employeeToEdit)
        {
            EmployeeToEdit = employeeToEdit;
        }

        public void UpdateEmployee(Employee employee)
        {
            _context = new SchedulerDbContext();
            var employeeToUpdate = _context.Employees.FirstOrDefault(x => x.Id == employee.Id);

            if (employeeToUpdate != null)
            {
                employeeToUpdate.LastName = employee.LastName;
                employeeToUpdate.FirstName = employee.FirstName;
                employeeToUpdate.TeamId = employee.TeamId;
                employeeToUpdate.TeamRoleId = employee.TeamRoleId;
                employeeToUpdate.MonthsEmployed = employee.MonthsEmployed;
                employeeToUpdate.ReligiousHoliday = employee.ReligiousHoliday;
            }
            else
            {
                _context.Employees.Add(employee);
            }

            _context.SaveChanges();
            OnEmployeeUpdated();
        }

        private void OnEmployeeUpdated()
        {
            EmployeeUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
