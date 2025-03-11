using ERPManager.Collection;
using GeneratedORM;
using KundenVerwaltung.Commands;
using MVVMStandard.ViewModel;
using System.Windows.Input;
using ERPManager.Interfaces;
using KundenVerwaltung.View;


namespace KundenVerwaltung.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor

        public MainViewModel(INavigationService addCustomerDialog)
        {
            _navigationService = addCustomerDialog;

            OpenCustomerViewCommand = new RelayCommand(_ => _navigationService.ShowDialog<AddCustomerDialog>());
            EditCustomerCommand = new RelayCommand(_ => EditCustomer(), _ => CanModifyCustomer());
            DeleteCustomerCommand = new RelayCommand(_ => DeleteCustomer(), _ => CanModifyCustomer());

        }


        #endregion

        #region Fields
        
        private string _searchText = "";
        private ViewCollection<Customers> _customers;
        private Customers _selectedCustomer;
        private bool _isSelected;

        #endregion

        #region Views

        private readonly INavigationService _navigationService;

        #endregion

        #region Properties

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                    return;

                _searchText = value;
                OnPropertyChanged();
            }
        }

        public ViewCollection<Customers> Customers
        {
            get => _customers;
            set
            {
                if (_customers == value)
                    return;

                _customers = value;
                OnPropertyChanged();
            }
        }

        public Customers SelectedCustomer
        {
            get => Customers?.CurrentItem;
            set
            {
                if (Customers != null && Customers.CurrentItem != value)
                {
                    Customers.CurrentItem = value;
                    OnPropertyChanged(nameof(SelectedCustomer));
                }
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand OpenCustomerViewCommand { get; }
        public ICommand EditCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }

        #endregion

        #region Methods

        public void Init()
        {

            LoadCustomers();
        }

        private void LoadCustomers()
        {
            string sql = "where 1 =1";
            Customers = ERPManager.Services.QueryManager.StaticErpAbfrage<Customers>(sql);
        }

        /// <summary>
        /// Öffnet einen Dialog zum Bearbeiten des gewählten Kunden
        /// </summary>
        private void EditCustomer()
        {
            if (SelectedCustomer != null)
            {
                // TODO: Bearbeitungs-Dialog öffnen
            }
        }

        /// <summary>
        /// Löscht den ausgewählten Kunden
        /// </summary>
        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
                _customers.DeleteCurrent();
        }

        private bool CanModifyCustomer()
        {
            return SelectedCustomer != null;
        }

        #endregion
    }
}
