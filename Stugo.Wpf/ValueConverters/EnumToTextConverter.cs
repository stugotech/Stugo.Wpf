using System;
using System.Windows.Data;
using Stugo.Wpf.Localisation;

namespace Stugo.Wpf.ValueConverters
{
    public sealed class EnumToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // parameter is used as prefix if set.
            if (value != null)
            {
                var type = value.GetType();

                if (type.IsEnum)
                {
                    var name = $"{parameter ?? type.Name}_{Enum.GetName(type, value)}";
                    value = LocalisationManager.Current.GetString(name) ?? value;
                }
            }

            return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
