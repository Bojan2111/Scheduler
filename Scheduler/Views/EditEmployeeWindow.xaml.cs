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
using System.Windows.Shapes;

namespace Scheduler.Views
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        private EditEmployeeViewModel _viewModel;

        public EditEmployeeWindow(EditEmployeeDTO employeeToEdit)
        {
            if (employeeToEdit == null)
            {
                throw new ArgumentNullException(nameof(employeeToEdit));
            }

            InitializeComponent();

            _viewModel = new EditEmployeeViewModel();
            _viewModel.SetItem(employeeToEdit);

            DataContext = _viewModel;
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            EditEmployeeViewModel editedEmployeeVM = (EditEmployeeViewModel)this.DataContext;
            var editedEmployee = editedEmployeeVM.ItemToEdit.Employee;


            if (editedEmployee != null)
            {
                _viewModel.UpdateItem(editedEmployee);
            }
            else
            {
                throw new ArgumentNullException("Employee", "Data for Employee Object expected");
            }
            this.Close();
        }
    }
}
