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
    [ValueConversion(typeof(bool), typeof(Style))]
    public class TimerStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolvalue = (bool)value;
            if(boolvalue)
            {
                return Application.Current.FindResource("TaskItemGridActive") as Style; 
            }

            return Application.Current.FindResource("TaskItemGridPaused") as Style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
