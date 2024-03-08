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
    public class EditShiftViewModel : ViewModelBase
    {
        private EditShiftDTO _editShift;

        public EditShiftDTO EditShift
        {
            get { return _editShift; }
            set
            {
                _editShift = value;
                OnPropertyChanged(nameof(EditShift));
            }
        }

        public ICommand SaveChangesCommand { get; private set; }

        public EditShiftViewModel()
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
