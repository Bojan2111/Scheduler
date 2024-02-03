using Scheduler.State.Navigators;
using Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scheduler.Commands
{
    public class UpdateCurrentViewCommand : ICommand
    {
        private INavigator _navigator;

        public UpdateCurrentViewCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.Teams:
                        _navigator.CurrentView = new TeamsViewModel();
                        break;
                    case ViewType.Employees:
                        _navigator.CurrentView = new EmployeesViewModel();
                        break;
                    case ViewType.Schedule:
                        _navigator.CurrentView = new TimetableViewModel();
                        break;
                    case ViewType.NewSchedule:
                        _navigator.CurrentView = new NewScheduleViewModel();
                        break;
                    case ViewType.NationalHolidays:
                        _navigator.CurrentView = new NationalHolidaysViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
