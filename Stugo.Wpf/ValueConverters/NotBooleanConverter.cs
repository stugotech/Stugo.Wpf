using System;
using System.Globalization;
using System.Windows.Data;

namespace Stugo.Wpf.ValueConverters
{
    public sealed class NotBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value.IsTruthy();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && !(bool)value;
        }
    }
}
