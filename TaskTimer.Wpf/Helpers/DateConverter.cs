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
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return string.Empty;
            }
            var date = (DateTime)value;
            if(date == default(DateTime))
            {
                return string.Empty;
            }
            if(date.TimeOfDay == new TimeSpan())
            {
                return date.ToString("dd'/'MM'/'yyyy");
            }
            if(parameter != null && (string)parameter == "DateOnly")
            {
                return date.ToString("dd'/'MM'/'yyyy");
            }
            return date.ToString("dd'/'MM'/'yyyy HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date;
            var values = ((string)value).Split('/');
            if(values.Count() == 1)
            {
                values = ((string)value).Split('-');
            }
            values = values.Select(v => v.Trim()).ToArray();
            var formatted = string.Join("/", values);

            var isOk = DateTime.TryParseExact(formatted, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            isOk = DateTime.TryParseExact(formatted, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            isOk = DateTime.TryParseExact(formatted, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            isOk = DateTime.TryParseExact(formatted, "d/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            isOk = DateTime.TryParseExact(formatted, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            isOk = DateTime.TryParseExact(formatted, "d/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            isOk = DateTime.TryParseExact(formatted, "d/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            isOk = DateTime.TryParseExact(formatted, "d/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            isOk = DateTime.TryParseExact(formatted, "d/M/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            if (isOk)
                return date;
            return default(DateTime);
        }
    }
}
