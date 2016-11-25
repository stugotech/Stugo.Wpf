using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Stugo.Wpf.Bindings
{
    public class SwitchBinding : Binding
    {
        public SwitchBinding()
        {
            Initialize();
        }


        public SwitchBinding(string path)
            : base(path)
        {
            Initialize();
        }


        public SwitchBinding(string path, object trueValue, object falseValue)
            : base(path)
        {
            Initialize();
            this.TrueValue = trueValue;
            this.FalseValue = falseValue;
        }


        private void Initialize()
        {
            this.TrueValue = Binding.DoNothing;
            this.FalseValue = Binding.DoNothing;
            this.Mode = BindingMode.OneWay;
            this.Converter = new SwitchConverter(this);
        }


        [ConstructorArgument("trueValue")]
        public object TrueValue { get; set; }

        [ConstructorArgument("falseValue")]
        public object FalseValue { get; set; }


        private class SwitchConverter : IValueConverter
        {
            private readonly SwitchBinding binding;


            public SwitchConverter(SwitchBinding binding)
            {
                this.binding = binding;
            }


            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    return value.IsTruthy() ? binding.TrueValue : binding.FalseValue;
                }
                catch
                {
                    return DependencyProperty.UnsetValue;
                }
            }


            object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }
    }
}
