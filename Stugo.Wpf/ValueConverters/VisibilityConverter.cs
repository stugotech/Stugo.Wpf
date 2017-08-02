using System;
using System.Globalization;
using System.Windows.Data;
using Stugo.Wpf.Bindings;

namespace Stugo.Wpf.ValueConverters
{
    public class VisibilityConverter : IValueConverter
    {
        private readonly VisibilityBinding binding;

        public VisibilityConverter() : this(null)
        {
        }


        internal VisibilityConverter(VisibilityBinding binding)
        {
            this.binding = binding;
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var invert = binding != null ? binding.Invert : ((parameter as bool?) ?? false);
            return (value.IsTruthy() == !invert).ToVisibility();
        }


        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
