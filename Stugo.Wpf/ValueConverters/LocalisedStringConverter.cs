using System;
using System.Windows.Data;
using Stugo.Wpf.Localisation;

namespace Stugo.Wpf.ValueConverters
{
    public sealed class LocalisedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var key = value.ToString();

                if (parameter != null)
                    key = parameter + key;

                value = LocalisationManager.Current.GetString(key) ?? key;
            }

            return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
