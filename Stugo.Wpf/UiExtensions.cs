using System.Windows;
using System.Windows.Media;

namespace Stugo.Wpf
{
    public static class UiExtensions
    {
        /// <summary>
        /// Gets the first ancestor of type T.
        /// </summary>
        public static T GetVisualParent<T>(this DependencyObject element)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(element);

            while (parent != null && !(parent is T))
                parent = VisualTreeHelper.GetParent(parent);

            return (T)parent;
        }


        /// <summary>
        /// Get the containing window of the element.
        /// </summary>
        public static Window GetWindow(this DependencyObject element)
        {
            return Window.GetWindow(element);
        }
    }
}
