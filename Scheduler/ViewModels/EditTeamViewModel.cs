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
    public class EditTeamViewModel : INotifyPropertyChanged
    {
        private SchedulerDbContext _context;

        private EditTeamDTO _teamToEdit;

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
            teamToUpdate.Name = team.Name;
            teamToUpdate.ShiftPattern = team.ShiftPattern;
            teamToUpdate.CurrentMonth = team.CurrentMonth;
            teamToUpdate.CurrentStartDate = team.CurrentStartDate;
            teamToUpdate.NextMonthStartDate = team.NextMonthStartDate;
            teamToUpdate.NextMonthStartsWithNight = team.NextMonthStartsWithNight;

            _context.SaveChanges();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
