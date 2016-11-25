using System;
using System.Windows.Data;
using Stugo.Wpf.Bindings;
using Stugo.Wpf.Localisation;

namespace Stugo.Wpf.ValueConverters
{
    public sealed class PluralLocalisedStringConverter : IValueConverter
    {
        private readonly LocalisationBinding binding;


        public PluralLocalisedStringConverter() : this(null)
        {
        }


        internal PluralLocalisedStringConverter(LocalisationBinding binding)
        {
            this.binding = binding;
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var key = value.ToString();
                var n = System.Convert.ToInt32(binding?.Plural ?? parameter);
                value = LocalisationManager.Current.GetPlural(key, n) ?? key;
            }

            return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
