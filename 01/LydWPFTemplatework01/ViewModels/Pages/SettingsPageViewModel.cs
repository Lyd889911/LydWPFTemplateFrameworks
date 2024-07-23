using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace LydWPFTemplatework01.ViewModels.Pages
{
    public partial class SettingsPageViewModel: ObservableObject
    {
        [ObservableProperty]
        private ApplicationTheme _currentApplicationTheme = ApplicationTheme.Unknown;

        [ObservableProperty]
        private bool _isAutoChangedMenu = true;

        [ObservableProperty]
        private ObservableCollection<string> _applicationThemeItems = new() { "Light", "Dark", "High Contrast" };

        partial void OnCurrentApplicationThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
        {
            ApplicationThemeManager.Apply(newValue);
        }
    }
}
