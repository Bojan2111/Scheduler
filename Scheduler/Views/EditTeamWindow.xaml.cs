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
    /// Interaction logic for EditTeamWindow.xaml
    /// </summary>
    public partial class EditTeamWindow : Window
    {
        private EditTeamViewModel _viewModel;

        public EditTeamWindow(EditTeamDTO teamToEdit)
        {
            if (teamToEdit == null)
            {
                throw new ArgumentNullException(nameof(teamToEdit));
            }

            InitializeComponent();

            _viewModel = new EditTeamViewModel();
            _viewModel.SetItem(teamToEdit);

            DataContext = _viewModel;
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateTeamClick(object sender, RoutedEventArgs e)
        {
            EditTeamViewModel editedTeamVM = (EditTeamViewModel) this.DataContext;
            var editedTeam = editedTeamVM.ItemToEdit.Team;


            if (editedTeam != null)
            {
                _viewModel.UpdateItem(editedTeam);
            }
            else
            {
                throw new ArgumentNullException("Team", "Data for Team Object expected");
            }
            this.Close();
        }
    }
}
