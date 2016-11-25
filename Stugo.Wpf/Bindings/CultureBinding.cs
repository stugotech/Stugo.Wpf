using System.Globalization;
using System.Windows.Data;

namespace Stugo.Wpf.Bindings
{
    public class CultureBinding : Binding
    {
        public CultureBinding()
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }

        public CultureBinding(string path)
            : base(path)
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }
    }
}
