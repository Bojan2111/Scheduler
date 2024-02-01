using Scheduler.Models.DTOs;
using Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scheduler.Views
{
    /// <summary>
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl
    {
        public EmployeesView()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesViewModel viewModel = DataContext as EmployeesViewModel;

            if (viewModel != null )
            {
                EditEmployeeDTO selectedItem = viewModel.GetEmployeeToEdit();

                EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(selectedItem);
                editEmployeeWindow.ShowDialog();
            }
        }

        private void AddButton_Click(Object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //private void AddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    TeamsViewModel mainViewModel = DataContext as TeamsViewModel;
        //    EditTeamDTO emptyTeam = mainViewModel.GetEmptyTeam();
        //    EditTeamWindow editTeamWindow = new EditTeamWindow(emptyTeam);
        //    editTeamWindow.ShowDialog();
        //}
    }
}
