using Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scheduler.State.Navigators
{
    public enum ViewType
    {
        Teams,
        Employees,
        Schedule,
        NewSchedule,
        NationalHolidays
    }
    public interface INavigator
    {
        ViewModelBase CurrentView { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
