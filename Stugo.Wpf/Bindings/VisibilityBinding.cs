using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Stugo.Wpf.ValueConverters;

namespace Stugo.Wpf.Bindings
{
    public class VisibilityBinding : Binding
    {
        public VisibilityBinding()
            : this(null, false)
        {
        }


        public VisibilityBinding(string path)
            : this(path, false)
        {
        }


        public VisibilityBinding(string path, bool invert)
            : base(path)
        {
            Invert = invert;
            Converter = new VisibilityConverter(this);
        }


        [ConstructorArgument("invert")]
        public bool Invert { get; set; }
    }
}
