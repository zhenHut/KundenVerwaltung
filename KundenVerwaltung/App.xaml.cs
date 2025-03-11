using KundenVerwaltung.Configuration;
using KundenVerwaltung.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace KundenVerwaltung
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            _serviceProvider = Bootstrapper.ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = (MainWindow?)_serviceProvider.GetService(typeof(MainWindow));
           
            mainWindow?.Show();
        }
    }

}
