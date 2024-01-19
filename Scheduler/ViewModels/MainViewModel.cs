using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Scheduler.Logic;
using Scheduler.Models;
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
    public class MainViewModel : INotifyPropertyChanged
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

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }


        private void EditTeam()
        {
            try
            {
                if (SelectedTeam != null)
                {
                    var editWindow = new EditTeamWindow(SelectedTeam);
                    // Set DataContext or pass SelectedTeam to the EditTeamWindow for editing
                    editWindow.DataContext = SelectedTeam;
                    editWindow.ShowDialog();

                    //var editedTeam = _context.Teams.Find(SelectedTeam.Id);
                    //if (editedTeam != null)
                    //{
                    //    editedTeam.Name = SelectedTeam.Name;
                    //    editedTeam.ShiftPattern = SelectedTeam.ShiftPattern;
                    //    editedTeam.CurrentMonth = SelectedTeam.CurrentMonth;
                    //    editedTeam.CurrentStartDate = SelectedTeam.CurrentStartDate;
                    //    editedTeam.NextMonthStartDate = SelectedTeam.NextMonthStartDate;
                    //    editedTeam.NextMonthStartsWithNight = SelectedTeam.NextMonthStartsWithNight;
                    //}
                    //else
                    //{
                    //    _context.Teams.Add(SelectedTeam);
                    //}

                    //_context.SaveChanges();

                    LoadContext();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public MainViewModel()
        {
            _context = new SchedulerDbContext();
            LoadContext();
            
            EditCommand = new RelayCommand(EditTeam);
            DeleteCommand = new RelayCommand(DeleteTeam);
        }

        private void LoadContext()
        {
            Teams = new ObservableCollection<Team>(_context.Teams);
        }

        private void DeleteTeam()
        {
            var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Handle logic for deleting the selected team
                Teams.Remove(SelectedTeam);
                _context.Teams.Remove(SelectedTeam);
                _context.SaveChanges();
            }
            //if (SelectedTeam != null)
            //{
            //    _context.Teams.Remove(SelectedTeam);
            //    _context.SaveChanges();
            //}

            LoadContext();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
