using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MVVMReactive.Core.MVVM.Core.MVVM.Converter
{
    public class NotNullVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (true.Equals(parameter) || "true".Equals(parameter) || "True".Equals(parameter)) // for inversion
                return value != null ? Visibility.Collapsed : Visibility.Visible;
            else
                return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}