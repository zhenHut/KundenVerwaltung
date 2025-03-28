using KundenVerwaltung.View;
using Microsoft.Extensions.DependencyInjection;
using ERPManager.Interfaces;
using System.Windows;

namespace KundenVerwaltung.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowAddCustomerDialog()
        {
            var window = _serviceProvider.GetService<AddCustomerDialog>();
            window?.ShowDialog();
        }

        public void ShowDialog<T>() where T : class
        {

            var window = _serviceProvider.GetService(typeof(T)) as Window;
            if (window != null)
            {
                var mainWindow = _serviceProvider.GetService<MainWindow>();
                window.Owner = mainWindow;

                // Optional: Startposition relativ zum `Owner`
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                window?.ShowDialog();
            }
        }

        public T? ShowDialogWithResult<T>() where T : class, new()
        {
            var window = _serviceProvider.GetService(typeof(T)) as Window;
            return window?.ShowDialog() == true ? window as T : null;
        }
    }
}
