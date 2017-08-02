using System.Windows;
using System.Windows.Controls;

namespace Stugo.Wpf.Behaviours
{
    /// <summary>
    /// When enabled for a <see cref="System.Windows.Controls.Button" />, causes the button to
    /// close the parent window when clicked.
    /// </summary>
    public static class CloseButtonBehaviour
    {
        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(CloseButtonBehaviour), new PropertyMetadata(false, EnabledChanged));


        public static bool GetEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledProperty);
        }


        public static void SetEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledProperty, value);
        }


        private static void EnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var button = target as Button;

            if (button != null)
            {
                button.Click -= ButtonOnClick;

                if (GetEnabled(target))
                    button.Click += ButtonOnClick;
            }
        }


        private static void ButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var button = (Button)sender;
            var window = button.GetWindow();

            if (window != null)
                window.Close();
        }
    }
}
