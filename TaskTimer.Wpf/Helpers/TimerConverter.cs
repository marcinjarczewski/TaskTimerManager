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
    [ValueConversion(typeof(int), typeof(string))]
    public class TimerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (int)value;
            string hours = ((int)time / 3600).ToString("00");
            int modTime = time % 3600;
            string minutes = ((int)modTime / 60).ToString("00");
            string seconds = (modTime % 60).ToString("00");
            return string.Format("{0}:{1}:{2}", hours, minutes, seconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timer = (string)value;
            int res = 0;
            int.TryParse(timer.Substring(0,2), out res);
            int total = res * 60 * 60;
            int.TryParse(timer.Substring(3, 2), out res);
            total += res * 60;
            int.TryParse(timer.Substring(6, 2), out res);
            return total + res;
        }
    }
}
