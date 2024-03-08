using Scheduler.Logic;
using Scheduler.Models;
using Scheduler.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scheduler.ViewModels
{
    public class TransferEmployeeViewModel : ViewModelBase
    {
        private TransferEmployeeDTO _transferEmployee;

        //private int _selectedTeam;

        public TransferEmployeeDTO TransferEmployee
        {
            get { return _transferEmployee; }
            set
            {
                _transferEmployee = value;
                OnPropertyChanged(nameof(TransferEmployee));
            }
        }

        //public int SelectedTeam
        //{
        //    get { return _selectedTeam; }
        //    set
        //    {
        //        _selectedTeam = value;
        //        OnPropertyChanged(nameof(SelectedTeam));
        //    }
        //}

        public ICommand SaveChangesCommand { get; private set; }

        public TransferEmployeeViewModel()
        {
            SaveChangesCommand = new RelayCommand(SaveChangesExecute);
        }

        private void SaveChangesExecute(object parameter)
        {
            // Perform validation or additional logic if needed
            //TransferEmployee.Employee.TeamId = SelectedTeam;
            // Close the window
            CloseAction?.Invoke();
        }

        // Action to close the window
        public Action CloseAction { get; set; }
    }
}
