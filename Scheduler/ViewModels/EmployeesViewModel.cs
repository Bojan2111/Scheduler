using Microsoft.EntityFrameworkCore;
using Scheduler.Logic;
using Scheduler.Models;
using Scheduler.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Scheduler.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        private SchedulerDbContext _context;

        private ObservableCollection<Employee> _items;
        public ObservableCollection<Employee> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private Employee _item;
        public Employee Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public ICommand DeleteCommand { get; set; }
        public EmployeesViewModel()
        {
            _context = new SchedulerDbContext();
            LoadContext();

            DeleteCommand = new RelayCommand(DeleteItem);
        }

        private void LoadContext()
        {
            Items = new ObservableCollection<Employee>(_context.Employees.Include(x => x.Team).Include(x => x.TeamRole));
        }

        private void DeleteItem()
        {
            var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _context.Employees.Remove(Item);
                _context.SaveChanges();
            }

            LoadContext();
        }

        public EditEmployeeDTO GetItemToEdit()
        {
            EditEmployeeDTO dto = new EditEmployeeDTO();
            dto.Employee = _item;
            List<string> teamNames = _context.Teams.Select(x => x.Name).ToList();
            List<TeamRole> teamRoles = _context.TeamRoles.ToList();

            dto.TeamNames = new List<DataOptionsItem>();
            dto.TeamRoleNames = new List<DataOptionsItem>();

            foreach (var teamName in  teamNames)
            {
                dto.TeamNames.Add(new DataOptionsItem() { Value = teamName, DisplayText = teamName });
            }

            foreach (var teamRole in teamRoles)
            {
                dto.TeamRoleNames.Add(new DataOptionsItem() { Value = teamRole.Name, DisplayText = teamRole.Description });
            }

            return dto;
        }

        //public EditTeamDTO GetEmptyItem()
        //{
        //    EditEmployeeDTO emptyItem = new EditTeamDTO();
        //    emptyItem.Employee = new Employee()
        //    {
        //        Name = string.Empty,
        //        ShiftPattern = "DN3",
        //        CurrentMonth = DateTime.Now.Month,
        //        CurrentStartDate = DateTime.Now.Date,
        //        NextMonthStartDate = DateTime.Now.Date.AddMonths(1),
        //    };

        //    return emptyTeam;
        //}
    }
}
