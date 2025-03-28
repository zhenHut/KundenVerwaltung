using System.Windows;
using KundenVerwaltung.ViewModel;

namespace KundenVerwaltung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext =  viewModel;        
        }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}