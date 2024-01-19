using Scheduler.Models;
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
    /// Interaction logic for EditTeamWindow.xaml
    /// </summary>
    public partial class EditTeamWindow : Window
    {
        public EditTeamWindow(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }
            InitializeComponent();
            DataContext = new EditTeamViewModel(team);
            
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateTeamClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
