using Microsoft.EntityFrameworkCore;
using Scheduler.Converters;
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

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private Employee _employee;
        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }

        public ICommand DeleteCommand { get; set; }
        public EmployeesViewModel()
        {
            _context = new SchedulerDbContext();
            LoadContext();

            DeleteCommand = new RelayCommand(DeleteEmployee);
        }

        private void LoadContext()
        {
            Employees = new ObservableCollection<Employee>(_context.Employees.Include(x => x.Team).Include(x => x.TeamRole));
        }

        private void DeleteEmployee()
        {
            var result = MessageBox.Show("This item will be permanently deleted from the database. Are you sure you want to do this?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _context.Employees.Remove(Employee);
                _context.SaveChanges();
            }

            LoadContext();
        }

        public EditEmployeeDTO GetEmployeeToEdit()
        {
            EditEmployeeDTO dto = GetOptionsData();
            dto.Employee = EmployeeConverter.ConvertToDTO(_employee);

            return dto;
        }

        public EditEmployeeDTO GetEmptyEmployee()
        {
            EditEmployeeDTO emptyEmployee = GetOptionsData();
            emptyEmployee.Employee = new EmployeeDTO()
            {
                LastName = string.Empty,
                FirstName = string.Empty,
                TeamName = string.Empty,
                TeamRoleName = string.Empty,
                ReligiousHoliday = string.Empty,
            };

            return emptyEmployee;
        }

        private EditEmployeeDTO GetOptionsData()
        {
            EditEmployeeDTO dto = new EditEmployeeDTO();
            List<string> teamNames = _context.Teams.Select(x => x.Name).ToList();
            List<TeamRole> teamRoles = _context.TeamRoles.ToList();

            dto.TeamNames = new List<DataOptionsItem>();
            dto.TeamRoleNames = new List<DataOptionsItem>();

            foreach (var teamName in teamNames)
            {
                dto.TeamNames.Add(new DataOptionsItem() { Value = teamName, DisplayText = teamName });
            }

            foreach (var teamRole in teamRoles)
            {
                dto.TeamRoleNames.Add(new DataOptionsItem() { Value = teamRole.Name, DisplayText = teamRole.Description });
            }

            return dto;
        }
    }
}
