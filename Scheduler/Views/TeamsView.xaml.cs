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
    /// Interaction logic for TeamsView.xaml
    /// </summary>
    public partial class TeamsView : UserControl
    {
        public TeamsView()
        {
            InitializeComponent();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TeamsViewModel teamViewModel = DataContext as TeamsViewModel;

            if (teamViewModel != null)
            {
                EditTeamDTO selectedTeam = teamViewModel.GetTeamToEdit();

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
