using Scheduler.Models;
using Scheduler.Models.DTOs;
using Scheduler.ViewModels;
using Scheduler.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TeamsViewModel mainViewModel = DataContext as TeamsViewModel;

            if (mainViewModel != null)
            {
                EditTeamDTO selectedTeam = mainViewModel.GetTeamToEdit();

                EditTeamWindow editTeamWindow = new EditTeamWindow(selectedTeam);
                editTeamWindow.ShowDialog();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TeamsViewModel mainViewModel = DataContext as TeamsViewModel;
            EditTeamDTO emptyTeam = mainViewModel.GetEmptyTeam();
            EditTeamWindow editTeamWindow = new EditTeamWindow(emptyTeam);
            editTeamWindow.ShowDialog();
        }
    }
}