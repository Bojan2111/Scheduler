using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
        }

        // Other properties for editable team details...

        // Constructor
        public EditTeamViewModel(Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));
            Team = team;
            // Initialize other properties as needed
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
