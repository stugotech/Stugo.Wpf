using System.Windows.Data;

namespace Stugo.Wpf.Bindings
{
    public class ReadOnlyBinding : CultureBinding
    {
        public ReadOnlyBinding()
        {
            Mode = BindingMode.OneWay;
        }


        public ReadOnlyBinding(string path)
            : base(path)
        {
            Mode = BindingMode.OneWay;
        }
    }
}
