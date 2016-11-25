using System.Windows.Data;
using System.Windows.Markup;
using Stugo.Wpf.ValueConverters;

namespace Stugo.Wpf.Bindings
{
    public class LocalisationBinding : Binding
    {
        public LocalisationBinding()
            : this(null, null)
        {
        }


        public LocalisationBinding(string path)
            : this(path, null)
        {
        }


        public LocalisationBinding(string path, object plural)
            : base(path)
        {
            Plural = plural;

            if (plural == null)
                Converter = new LocalisedStringConverter();
            else
                Converter = new PluralLocalisedStringConverter(this);
        }


        [ConstructorArgument("plural")]
        public object Plural { get; set; }
    }
}
