using Scheduler.Logic;
using Scheduler.Models;
using Scheduler.Models.DTOs;
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
    public class EditTeamViewModel : ViewModelBase
    {
        private SchedulerDbContext _context;

        private EditTeamDTO _teamToEdit;

        public event EventHandler TeamUpdated;

        public EditTeamDTO TeamToEdit
        {
            get { return _teamToEdit; }
            set
            {
                _teamToEdit = value;
                OnPropertyChanged(nameof(TeamToEdit));
            }
        }

        public EditTeamViewModel()
        {
            TeamToEdit = new EditTeamDTO();
        }

        public void SetTeam(EditTeamDTO teamToEdit)
        {
            TeamToEdit = teamToEdit;
        }

        public void UpdateTeam(Team team)
        {
            _context = new SchedulerDbContext();
            var teamToUpdate = _context.Teams.FirstOrDefault(x => x.Id == team.Id);

            if (teamToUpdate != null)
            {
                teamToUpdate.Name = team.Name;
                teamToUpdate.ShiftPattern = team.ShiftPattern;
                teamToUpdate.CurrentMonth = team.CurrentMonth;
                teamToUpdate.CurrentStartDate = team.CurrentStartDate;
                teamToUpdate.NextMonthStartDate = team.NextMonthStartDate;
                teamToUpdate.NextMonthStartsWithNight = team.NextMonthStartsWithNight;
            } else
            {
                _context.Teams.Add(team);
            }

            _context.SaveChanges();
            OnTeamUpdated();
        }

        private void OnTeamUpdated()
        {
            TeamUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
