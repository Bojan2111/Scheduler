using Scheduler.Logic;
using Scheduler.Models;
using Scheduler.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scheduler.ViewModels
{
    public class EditTeamViewModel : INotifyPropertyChanged
    {
        private Team _team;

        public Team Team
        {
            get { return _team; }
            set
            {
                _team = value;
                OnPropertyChanged(nameof(Team));
                OnPropertyChanged(nameof(ShiftPatterns));
            }
        }

        public EditTeamViewModel(Team team)
        {
            Team = team;
        }

        public List<ShiftPatternItem> ShiftPatterns { get; } = new List<ShiftPatternItem>
    {
        new ShiftPatternItem { Value = "DN3", DisplayText = "Day-Night-3 days free" },
        new ShiftPatternItem { Value = "8", DisplayText = "Morning shift" }
    };

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ShiftPatternItem
    {
        public string Value { get; set; }
        public string DisplayText { get; set; }
    }
}
