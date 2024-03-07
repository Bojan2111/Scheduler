using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Scheduler.Logic;
using Scheduler.Models;
using Scheduler.Models.DTOs;
using Scheduler.State.Navigators;
using Scheduler.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Scheduler.ViewModels
{
    public class TeamsViewModel : ViewModelBase
    {
        private SchedulerDbContext _context;

        private ObservableCollection<Team> _teams;

        public ObservableCollection<Team> Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                OnPropertyChanged(nameof(Teams));
            }
        }

        private Team _selectedTeam;

        public Team SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                _selectedTeam = value;
                OnPropertyChanged(nameof(SelectedTeam));
            }
        }
        public ICommand DeleteCommand { get; }
        public EditTeamDTO GetTeamToEdit()
        {
            EditTeamDTO editTeam = new EditTeamDTO();
            editTeam.Team = SelectedTeam;
            return editTeam;
        }

        public EditTeamDTO GetEmptyTeam()
        {
            EditTeamDTO emptyTeam = new EditTeamDTO();
            emptyTeam.Team = new Team()
            {
                Name = string.Empty,
                ShiftPattern = "DN3",
                CurrentMonth = DateTime.Now.Month,
                CurrentStartDate = DateTime.Now.Date,
                NextMonthStartDate = DateTime.Now.Date.AddMonths(1),
            };

            return emptyTeam;
        }
        public TeamsViewModel()
        {
            _context = new SchedulerDbContext();
            LoadContext();
            
            DeleteCommand = new RelayCommand(DeleteTeam);
        }

        private void LoadContext()
        {
            Teams = new ObservableCollection<Team>(_context.Teams);
        }

        private void DeleteTeam(object parameter)
        {
            var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _context.Teams.Remove(SelectedTeam);
                _context.SaveChanges();
            }

            LoadContext();
        }
    }
}
