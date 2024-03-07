using Scheduler.Logic;
using Scheduler.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scheduler.ViewModels
{
    public class RoleEditorViewModel : ViewModelBase
    {
        private EmployeeRoleEditDTO _employeeRoleDTO;

        public EmployeeRoleEditDTO EmployeeRoleDTO
        {
            get { return _employeeRoleDTO; }
            set
            {
                _employeeRoleDTO = value;
                OnPropertyChanged(nameof(EmployeeRoleDTO));
            }
        }

        public ICommand SaveChangesCommand { get; private set; }

        public RoleEditorViewModel()
        {
            SaveChangesCommand = new RelayCommand(SaveChangesExecute);
        }

        private void SaveChangesExecute(object parameter)
        {
            // Perform validation or additional logic if needed

            // Close the window
            CloseAction?.Invoke();
        }

        // Action to close the window
        public Action CloseAction { get; set; }
    }
}
