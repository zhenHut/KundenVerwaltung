using KundenVerwaltung.Commands;
using MVVMStandard.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace KundenVerwaltung.ViewModel
{
    public class AddCustomerDialogViewModel : BaseViewModel
    {
        #region Constructor

        public AddCustomerDialogViewModel()
        {
            
        }

        #endregion

        #region Fields
        private string _customerName;
        private string _email;
        private string _phone;
        
        #endregion

        #region Properties

         public string CustomerName
        {
            get => _customerName;
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        #endregion

        #region Commands

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        #endregion

        #region Methods

        public void Init() 
        {
            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Save()
        {

            MessageBox.Show("Kunde erfolgreich hinzugefügt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseWindow();
        }

        private void Cancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
        #endregion
    }
}


