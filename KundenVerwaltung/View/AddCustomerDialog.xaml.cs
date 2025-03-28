using KundenVerwaltung.ViewModel;
using System.Windows;


namespace KundenVerwaltung.View
{
    /// <summary>
    /// Interaktionslogik für AddCustomerDialog.xaml
    /// </summary>
    public partial class AddCustomerDialog : Window
    {
        public AddCustomerDialog(AddCustomerDialogViewModel addCustomerDialogViewModel)
        {
            InitializeComponent();
            DataContext = addCustomerDialogViewModel;
        }


    }
}
