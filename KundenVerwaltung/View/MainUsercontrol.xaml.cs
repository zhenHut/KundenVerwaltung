using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
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
            _viewModel = viewModel;            
        }

        #endregion

        #region Fields

        private readonly MainViewModel _viewModel;

        #endregion

        #region Properties

        #endregion

        #region Methods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Init();
            
        }


        #endregion
    }
}