using Scheduler.Logic;
using Scheduler.Models;
using Scheduler.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Scheduler.ViewModels
{
    public class TimetableViewModel : ViewModelBase
    {
        private SchedulerDbContext _context;

        private ObservableCollection<SchedulesDTO> _schedules;

        private ObservableCollection<TeamSchedule> _teamSchedules;

        private ObservableCollection<EmployeeSchedule> _employeeSchedules;

        public ObservableCollection<SchedulesDTO> Schedules
        {
            get { return _schedules; }
            set
            {
                _schedules = value;
                OnPropertyChanged(nameof(Schedules));
            }
        }
        public ObservableCollection<TeamSchedule> TeamSchedules
        {
            get { return _teamSchedules; }
            set
            {
                _teamSchedules = value;
                OnPropertyChanged(nameof(TeamSchedules));
            }
        }

        public ObservableCollection<EmployeeSchedule> EmployeeSchedules
        {
            get { return _employeeSchedules; }
            set
            {
                _employeeSchedules = value;
                OnPropertyChanged(nameof(EmployeeSchedules));
            }
        }

        private Shift _selectedShift;

        public Shift SelectedShift
        {
            get { return _selectedShift; }
            set
            {
                _selectedShift = value;
                OnPropertyChanged(nameof(SelectedShift));
            }
        }
        public ICommand DeleteCommand { get; }
        public EditShiftDTO GetShiftToEdit()
        {
            EditShiftDTO editShift = new EditShiftDTO();
            
            return editShift;
        }

        public EditShiftDTO GetEmptyShift()
        {
            EditShiftDTO emptyTeam = new EditShiftDTO();

            return emptyTeam;
        }
        public TimetableViewModel()
        {
            _context = new SchedulerDbContext();
            LoadContext();

            DeleteCommand = new RelayCommand(DeleteShift);
        }

        private void LoadContext()
        {
            Schedules = new ObservableCollection<SchedulesDTO>();
            EmployeeSchedules = new ObservableCollection<EmployeeSchedule>(); // get shifts for each employee
            TeamSchedules = new ObservableCollection<TeamSchedule>(); // get shifts for each team
            // db query to get all shifts for each employee for each team for specific month.
            // Month is in shift class, employeeId is in shift class, teamId is in employee class.
        }

        private void DeleteShift()
        {
            var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _context.Shifts.Remove(SelectedShift);
                _context.SaveChanges();
            }

            LoadContext();
        }
    }
}
