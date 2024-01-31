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
    public class NationalHolidays : ViewModelBase
    {
        private SchedulerDbContext _context;

        private ObservableCollection<NationalHoliday> _items;
        public ObservableCollection<NationalHoliday> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private NationalHoliday _item;
        public NationalHoliday Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public ICommand DeleteCommand { get; set; }
        public NationalHolidays()
        {
            _context = new SchedulerDbContext();
            LoadContext();

            DeleteCommand = new RelayCommand(DeleteNationalHoliday);
        }

        private void LoadContext()
        {
            Items = new ObservableCollection<NationalHoliday>(_context.NationalHolidays);
        }

        private void DeleteNationalHoliday()
        {
            var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _context.NationalHolidays.Remove(Item);
                _context.SaveChanges();
            }

            LoadContext();
        }

        public NationalHoliday GetItemToEdit()
        {
            NationalHoliday item = new NationalHoliday();

            return item;
        }
    }
}
