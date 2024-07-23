using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace LydWPFTemplateFramework01.Views.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityRevertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value is bool v && v) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value is Visibility.Collapsed;
        }
    }
}
