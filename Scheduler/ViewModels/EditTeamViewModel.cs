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

        private EditTeamDTO _itemToEdit;

        public event EventHandler ItemUpdated;

        public EditTeamDTO ItemToEdit
        {
            get { return _itemToEdit; }
            set
            {
                _itemToEdit = value;
                OnPropertyChanged(nameof(ItemToEdit));
            }
        }

        public EditTeamViewModel()
        {
            ItemToEdit = new EditTeamDTO();
        }

        public void SetItem(EditTeamDTO teamToEdit)
        {
            ItemToEdit = teamToEdit;
        }

        public void UpdateItem(Team team)
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
            OnItemUpdated();
        }

        private void OnItemUpdated()
        {
            ItemUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
