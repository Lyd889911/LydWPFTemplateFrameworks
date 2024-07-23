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
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Tray.Controls;
using LydWPFTemplatework01.ViewModels;
using LydWPFTemplatework01.ViewModels.Pages;
using LydWPFTemplatework01.ViewModels.Windows;

namespace LydWPFTemplatework01.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow, IWindow
    {
        private bool _isUserClosedPane;
        private bool _isPaneOpenedOrClosedFromCode;
        private MainWindowViewModel _vm;
        private SettingsPageViewModel _svm;
        public MainWindow(MainWindowViewModel vm, NotifyIconViewModel nvm,SettingsPageViewModel svm, INavigationService navigationService)
        {
            _vm = vm;
            _svm = svm;
            DataContext = _vm;
            InitializeComponent();
            NotifyIconMenu.DataContext = nvm;
            navigationService.SetNavigationControl(RootNavigation);
        }

        //托盘图标双击
        private void OnNotifyIconLeftDoubleClick(NotifyIcon sender, RoutedEventArgs e)
        {
            App.GetService<NotifyIconViewModel>()?.ShowOrHide();
        }

        //窗体大小改变
        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_svm.IsAutoChangedMenu)
                return;

            if (_isUserClosedPane)
            {
                return;
            }

            _isPaneOpenedOrClosedFromCode = true;
            RootNavigation.SetCurrentValue(NavigationView.IsPaneOpenProperty, e.NewSize.Width > 1000);
            _isPaneOpenedOrClosedFromCode = false;
        }

        private void NavigationView_OnPaneOpened(NavigationView sender, RoutedEventArgs args)
        {
            if (_isPaneOpenedOrClosedFromCode)
            {
                return;
            }

            _isUserClosedPane = false;
        }

        private void NavigationView_OnPaneClosed(NavigationView sender, RoutedEventArgs args)
        {
            if (_isPaneOpenedOrClosedFromCode)
            {
                return;
            }
            _isUserClosedPane = true;
        }
    }
    public interface IWindow
    {
        void Show();
    }

}