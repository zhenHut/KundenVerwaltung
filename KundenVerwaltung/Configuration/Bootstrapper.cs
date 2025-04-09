using ERPManager;
using ERPManager.Interfaces;
using KundenVerwaltung.Services;
using KundenVerwaltung.View;
using KundenVerwaltung.ViewModel;
using Microsoft.Extensions.DependencyInjection;


namespace KundenVerwaltung.Configuration
{
    public static class Bootstrapper
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // 🔹 ERPManagerFactory initialisieren (stellt sicher, dass alles geladen ist)
            ERPManagerFactory.Initialize();

            // 🔹 GlobalLoadingService aus der Factory holen
            var globalLoadingService = ERPManagerFactory.GetGlobalLoadingService();

            // 🔹 `IGlobalLoadingService` richtig registrieren
            services.AddSingleton<IGlobalLoadingService>(globalLoadingService);

            // Registrierung des ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<AddCustomerDialogViewModel>();

            // Registrierung der Views
            services.AddSingleton<MainWindow>();
            services.AddTransient<AddPrivateCustomerDialog>();

            // Registrierung des INavigationService
            services.AddSingleton<INavigationService, NavigationService>();
           
            return services.BuildServiceProvider();
        }
    }
}
