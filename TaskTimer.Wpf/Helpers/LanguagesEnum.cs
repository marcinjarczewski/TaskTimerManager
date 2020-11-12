using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TaskTimer.Wpf.Properties;

namespace TaskTimer.Wpf.Helpers
{
    public enum LanguagesEnum
    {
        [Description("pl")]
        Polish = 1,
        [Description("en")]
        English = 2
    }

    public static class LanguagesEnumExtensions
    {
        public static string TranslatedString(this LanguagesEnum lang)
        {
            switch (lang)
            {
                case LanguagesEnum.Polish:
                    return Resources.Polish;
                case LanguagesEnum.English:
                    return Resources.English;
                default:
                    return Resources.Polish;
            }
        }

        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }

    public static class EnumHelper
    {
        public static T GetValueFromDescription<T>(string description)
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }
    }
}

