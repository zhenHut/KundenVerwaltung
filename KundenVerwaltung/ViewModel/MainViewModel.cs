using ERPManager.Collection;
using ERPManager.Helpers;
using ERPManager.Interfaces;
using ERPManager.ZusatzInhaltManager;
using GeneratedORM;
using KundenVerwaltung.Commands;
using KundenVerwaltung.View;
using KundenVerwaltung.ZusatzInhalt;
using MVVMStandard.ViewModel;
using System.Windows.Input;

namespace KundenVerwaltung.ViewModel
{
    public class MainViewModel : AutoInitializableViewModel
    {
        #region Constructor

        public MainViewModel(INavigationService addCustomerDialog, IGlobalLoadingService loadingService)
        {
            _navigationService = addCustomerDialog;
            _loadingService = loadingService;

            LoadCommands();
        }

        #endregion

        #region Fields

        private string _searchText = "";
        private ViewCollection<Customers>? _customers;
        private Customers? _selectedCustomer;
        private bool _isSelected;

        private readonly IGlobalLoadingService _loadingService;

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

        public ViewCollection<Customers>? Customers
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

        public Customers? SelectedCustomer
        {
            get => Customers?.CurrentItem;
            set
            {
                if (Customers != null)
                {
                    Customers.CurrentItem = value;
                    OnPropertyChanged(nameof(SelectedCustomer));

                    IsSelected = Customers.CurrentItem != null;
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


        public bool IsLoading
        {
            get => _loadingService.IsLoading;
            set
            {
                if (_loadingService.IsLoading == value)
                    return;

                _loadingService.IsLoading = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands

        public ICommand? OpenCustomerViewCommand { get; private set; } 
        public ICommand? EditCustomerCommand { get; private set; }
        public ICommand? DeleteCustomerCommand { get; private set; }

        public ICommand? LoadDataCommand { get; private set; }

        #endregion

        #region Methods

        protected override async Task Initialize()
        {
            using (new LoadingScope(_loadingService))
            {
                EventLoading();
                await LoadCustomers();
            }
        }

        private async Task LoadCustomers()
        {
            string sql = "where 1 =1 order by id";
            Customers = await Task.Run(() => ERPManager.Services.QueryManager.StaticErpAbfrage<Customers>(sql));
        }

        private void LoadCommands()
        {
            OpenCustomerViewCommand = new RelayCommand(_ => _navigationService.ShowDialog<AddPrivateCustomerDialog>());
            EditCustomerCommand = new RelayCommand(_ => EditCustomer(), _ => IsSelected);
            DeleteCustomerCommand = new RelayCommand(_ => DeleteCustomer(), _ => IsSelected);
            LoadDataCommand = new RelayCommand(async _ => await StarteLade());
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
                _customers?.DeleteCurrent();
        }

        private void EventLoading()
        {
            EntityBaseModel.OrmObjectMaterialized -= ERPManager_OrmObjectMaterialized;
            EntityBaseModel.OrmObjectMaterialized += ERPManager_OrmObjectMaterialized;
        }

        private void ERPManager_OrmObjectMaterialized(object? sender, OrmEntityEventArgs args)
        {
            if (args.Entity is Customers entity)
            {
                entity.ZusatzInhalt = new CustomerZusatzInhalt(entity);
            }
        }

        private async Task StarteLade()
        {
            IsLoading = true;
            await Task.Delay(3000); // Simulierte Ladezeit
            IsLoading = false;
        }

        #endregion
    }
}
