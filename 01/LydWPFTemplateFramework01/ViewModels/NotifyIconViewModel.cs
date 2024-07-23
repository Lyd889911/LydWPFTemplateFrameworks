using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LydWPFTemplateFramework01.ViewModels
{
    public partial class NotifyIconViewModel: ObservableObject
    {
        [RelayCommand]
        private void CheckUpdate()
        {

        }

        [RelayCommand]
        private void Exit()
        {
            //TODO 保存配置

            Application.Current.Shutdown();
        }

        [RelayCommand]
        public void ShowOrHide()
        {
            if (Application.Current.MainWindow.Visibility == Visibility.Visible)
            {
                Application.Current.MainWindow.Hide();
            }
            else
            {
                Application.Current.MainWindow.Activate();
                Application.Current.MainWindow.Focus();
                Application.Current.MainWindow.Show();
            }
        }
    }
}
