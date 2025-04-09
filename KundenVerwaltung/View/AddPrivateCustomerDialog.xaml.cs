using KundenVerwaltung.ViewModel;
using System.Windows;


namespace KundenVerwaltung.View
{
    /// <summary>
    /// Interaktionslogik für AddCustomerDialog.xaml
    /// </summary>
    public partial class AddPrivateCustomerDialog : Window
    {
        public AddPrivateCustomerDialog(AddCustomerDialogViewModel addCustomerDialogViewModel)
        {
            InitializeComponent();
            DataContext = addCustomerDialogViewModel;
        }


    }
}
