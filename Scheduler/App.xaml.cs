using System.Configuration;
using System.Data;
using System.Windows;

namespace Scheduler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();

            base.OnStartup(e);
        }
    }

}
