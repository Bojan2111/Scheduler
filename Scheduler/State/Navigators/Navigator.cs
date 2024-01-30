using Scheduler.Commands;
using Scheduler.Models;
using Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scheduler.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _viewModel;
        public ViewModelBase CurrentView
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewCommand(this);
    }
}
