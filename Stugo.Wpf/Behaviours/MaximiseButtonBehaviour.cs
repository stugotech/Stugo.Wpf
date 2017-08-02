using System.Windows;
using System.Windows.Controls;

namespace Stugo.Wpf.Behaviours
{
    /// <summary>
    /// When enabled for a <see cref="System.Windows.Controls.Button" />, causes the button to
    /// toggle the window state of the parent window when clicked.
    /// </summary>
    public static class MaximiseButtonBehaviour
    {
        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(MaximiseButtonBehaviour), 
                new PropertyMetadata(false, EnabledChanged));


        public static bool GetEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledProperty);
        }


        public static void SetEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledProperty, value);
        }


        private static void EnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var button = target as Button;
            var window = button != null ? button.GetWindow() : null;

            if (window != null)
            {
                button.Click -= ButtonOnClick;

                if (GetEnabled(target))
                    button.Click += ButtonOnClick;
            }
        }

        private static void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var window = button.GetWindow();

            if (window != null)
            {
                if (window.WindowState == WindowState.Maximized)
                    window.WindowState = WindowState.Normal;
                else if (window.WindowState == WindowState.Normal)
                    window.WindowState = WindowState.Maximized;
            }
        }
    }
}
