using Scheduler.Models;
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
    /// Interaction logic for EditNationalHolidayWindow.xaml
    /// </summary>
    public partial class EditNationalHolidayWindow : Window
    {
        private EditNationalHolidays _viewModel;
        public EditNationalHolidayWindow()
        {
            InitializeComponent();
        }

        public EditNationalHolidayWindow(NationalHoliday itemToEdit)
        {
            if (itemToEdit == null)
            {
                throw new ArgumentNullException(nameof(itemToEdit));
            }

            InitializeComponent();

            _viewModel = new EditNationalHolidays();
            _viewModel.SetItem(itemToEdit);

            DataContext = _viewModel;
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateTeamClick(object sender, RoutedEventArgs e)
        {
            EditNationalHolidays editedItemVM = (EditNationalHolidays)this.DataContext;
            var editedItem = editedItemVM.ItemToEdit;


            if (editedItem != null)
            {
                _viewModel.UpdateItem(editedItem);
            }
            else
            {
                throw new ArgumentNullException("Team", "Data for Team Object expected");
            }
            this.Close();
        }
    }
}
