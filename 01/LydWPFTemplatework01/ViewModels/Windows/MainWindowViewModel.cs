using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using LydWPFTemplatework01.Views.Pages;

namespace LydWPFTemplatework01.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        //显示主窗体
        [ObservableProperty]
        private bool _isVisible=true;

        //菜单列表
        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem("1号页面", SymbolRegular.Play24, typeof(P1Page)),
            new NavigationViewItem("2号页面", SymbolRegular.Timer24, typeof(P2Page)),
            new NavigationViewItem("3号页面", SymbolRegular.TaskListSquareLtr24, typeof(P3Page))
        };

        //底部的菜单
        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem("设置", SymbolRegular.Settings24, typeof(SettingsPage))
        };

        [RelayCommand]
        private void OnHide()
        {
            IsVisible = false;
        }
    }
}
