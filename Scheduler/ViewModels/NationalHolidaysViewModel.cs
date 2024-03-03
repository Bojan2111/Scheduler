using Scheduler.Logic;
using Scheduler.Models;
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
    public class NationalHolidaysViewModel : ViewModelBase
    {
        private SchedulerDbContext _context;

        private ObservableCollection<NationalHoliday> _nationalHolidays;
        public ObservableCollection<NationalHoliday> NationalHolidays
        {
            get { return _nationalHolidays; }
            set
            {
                _nationalHolidays = value;
                OnPropertyChanged(nameof(NationalHolidays));
            }
        }

        private NationalHoliday _nationalHoliday;
        public NationalHoliday NationalHoliday
        {
            get { return _nationalHoliday; }
            set
            {
                _nationalHoliday = value;
                OnPropertyChanged(nameof(NationalHoliday));
            }
        }

        public ICommand DeleteCommand { get; set; }
        public NationalHolidaysViewModel()
        {
            _context = new SchedulerDbContext();
            LoadContext();

            DeleteCommand = new RelayCommand(DeleteNationalHoliday);
        }

        private void LoadContext()
        {
            NationalHolidays = new ObservableCollection<NationalHoliday>(_context.NationalHolidays);
        }

        private void DeleteNationalHoliday()
        {
            var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _context.NationalHolidays.Remove(NationalHoliday);
                _context.SaveChanges();
            }

            LoadContext();
        }

        public NationalHoliday GetItemToEdit()
        {
            return NationalHoliday;
        }
    }
}
