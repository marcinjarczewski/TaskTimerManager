using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TaskTimer.Wpf.Helpers
{
    [ValueConversion(typeof(int), typeof(double))]
    public class DayWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            //{
            //    return 100;
            //}
            var val = double.Parse(value.ToString());
            int margin = 2 * 7 +7;
            return (val - margin) / 7;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
