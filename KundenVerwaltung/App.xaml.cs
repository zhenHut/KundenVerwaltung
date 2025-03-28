using ERPManager.Interfaces;
using KundenVerwaltung.Configuration;
using System.Windows;

namespace KundenVerwaltung
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public static IGlobalLoadingService? GlobalLoadingService { get; private set; }

        public App()
        {
            _serviceProvider = Bootstrapper.ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            GlobalLoadingService = (IGlobalLoadingService?)_serviceProvider.GetService(typeof(IGlobalLoadingService));

            Resources["GlobalLoadingService"] = GlobalLoadingService;

            var mainWindow = (MainWindow?)_serviceProvider.GetService(typeof(MainWindow));
            mainWindow?.Show();
        }
    }

}
