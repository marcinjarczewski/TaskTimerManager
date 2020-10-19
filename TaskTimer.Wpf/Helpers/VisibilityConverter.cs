using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TaskTimer.Wpf.Helpers
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolvalue = (bool)value;
            if(boolvalue)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibilityValue = (Visibility)value;
            return visibilityValue == Visibility.Visible;
        }
    }
}
