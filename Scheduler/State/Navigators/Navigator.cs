using Scheduler.Commands;
using Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scheduler.State.Navigators
{
    public class Navigator : INavigator
    {
        public ViewModelBase CurrentView { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewCommand(this);
    }
}
