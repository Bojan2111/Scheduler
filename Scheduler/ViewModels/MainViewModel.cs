﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Scheduler.Logic;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var editedTeam = _context.Teams.Find(SelectedTeam.Id);

                    editedTeam.Name = SelectedTeam.Name;
                    editedTeam.ShiftPattern = SelectedTeam.ShiftPattern;
                    editedTeam.CurrentMonth = SelectedTeam.CurrentMonth;
                    editedTeam.CurrentStartDate = SelectedTeam.CurrentStartDate;
                    editedTeam.NextMonthStartDate = SelectedTeam.NextMonthStartDate;
                    editedTeam.NextMonthStartsWithNight = SelectedTeam.NextMonthStartsWithNight;

                    _context.SaveChanges();

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
            if (SelectedTeam != null)
            {
                Teams.Remove(SelectedTeam);
                _context.Teams.Remove(SelectedTeam);
                _context.SaveChanges();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
