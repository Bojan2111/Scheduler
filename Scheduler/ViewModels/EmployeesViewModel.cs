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
            Employees = new ObservableCollection<Employee>(_context.Employees);
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
            EditEmployeeDTO dto = new EditEmployeeDTO();
            dto.Employee = _employee;
            dto.TeamNames = _context.Teams.Select(x => x.Name).ToList();
            dto.TeamRoleNames = _context.TeamRoles.Select(x => x.Name).ToList();

            return dto;
        }
        //public EditTeamDTO GetTeamToEdit()
        //{
        //    EditTeamDTO editTeam = new EditTeamDTO();
        //    editTeam.Team = SelectedTeam;
        //    return editTeam;
        //}

        //public EditTeamDTO GetEmptyTeam()
        //{
        //    EditTeamDTO emptyTeam = new EditTeamDTO();
        //    emptyTeam.Team = new Team()
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
