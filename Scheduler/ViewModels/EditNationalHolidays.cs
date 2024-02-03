using Scheduler.Logic;
using Scheduler.Models.DTOs;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Scheduler.Converters;

namespace Scheduler.ViewModels
{
    public class EditNationalHolidays : ViewModelBase
    {
        private SchedulerDbContext _context;

        private NationalHoliday _itemToEdit;

        public event EventHandler ItemUpdated;

        public NationalHoliday ItemToEdit
        {
            get { return _itemToEdit; }
            set
            {
                _itemToEdit = value;
                OnPropertyChanged(nameof(ItemToEdit));
            }
        }

        public EditNationalHolidays()
        {
            ItemToEdit = new NationalHoliday();
        }

        public void SetItem(NationalHoliday itemToEdit)
        {
            ItemToEdit = itemToEdit;
        }

        public void UpdateItem(NationalHoliday item)
        {
            _context = new SchedulerDbContext();
            var holiday = _context.NationalHolidays.FirstOrDefault(x => x.Id == item.Id);

            if (holiday != null)
            {
                holiday.Name = item.Name;
                holiday.Date = item.Date;
            }
            else
            {
                _context.NationalHolidays.Add(item);
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
