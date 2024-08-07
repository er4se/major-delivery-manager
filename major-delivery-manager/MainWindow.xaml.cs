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
using major_delivery_manager.Interfaces;
using major_delivery_manager.ViewModels;
using major_delivery_manager.Views;

namespace major_delivery_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindowsCodeBehind
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MenuViewModel vm = new MenuViewModel();
            vm.CodeBehind = this;
            this.DataContext = vm;

            LoadView(ViewType.MAIN);
        }

        public void LoadView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.MAIN:
                    MainView mainView = new MainView();
                    MainViewModel mainVM = new MainViewModel(this);
                    mainView.DataContext = mainVM;
                    this.OutputView.Content = mainView;
                    break;

                case ViewType.CREATE:
                    CreateRequestView createView = new CreateRequestView();
                    CreateRequestViewModel createVM = new CreateRequestViewModel(this);
                    createView.DataContext = createVM;
                    this.OutputView.Content = createView;
                    break;

                case ViewType.UPDATE:
                    //UpdateRequestView updateView = new UpdateRequestView();
                    //UpdateRequestViewModel updateVM = new UpdateRequestViewModel(this);
                    //updateView.DataContext = updateVM;
                    //this.OutputView.Content = updateView;
                    break;

                case ViewType.REGISTRY:
                    RegistryControlView registryView = new RegistryControlView();
                    RegistryControlViewModel registryVM = new RegistryControlViewModel(this);
                    registryView.DataContext = registryVM;
                    this.OutputView.Content = registryView;
                    break;
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}