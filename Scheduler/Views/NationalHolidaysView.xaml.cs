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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scheduler.Views
{
    /// <summary>
    /// Interaction logic for NationalHolidaysView.xaml
    /// </summary>
    public partial class NationalHolidaysView : UserControl
    {
        public NationalHolidaysView()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NationalHolidaysViewModel viewModel = DataContext as NationalHolidaysViewModel;

            if (viewModel != null)
            {
                NationalHoliday selectedItem = viewModel.GetItemToEdit();

                EditNationalHolidayWindow editItemWindow = new EditNationalHolidayWindow(selectedItem)
                {

                };
                editItemWindow.ShowDialog();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NationalHolidaysViewModel viewModel = DataContext as NationalHolidaysViewModel;
            NationalHoliday emptyNationalHoliday = new NationalHoliday { Name = string.Empty };
            EditNationalHolidayWindow editNationalHolidayWindow = new EditNationalHolidayWindow(emptyNationalHoliday);
            editNationalHolidayWindow.ShowDialog();
        }
    }
}
