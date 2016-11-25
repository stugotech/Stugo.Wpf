using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Stugo.Wpf.Behaviours
{
    /// <summary>
    /// When enabled for a <see cref="System.Windows.Controls.Primitives.Thumb" />, causes the
    /// thumb to move the window when dragged, and maximise/restore the window when double-clicked.
    /// </summary>
    public static class WindowHeaderBehaviour
    {
        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(WindowHeaderBehaviour), new PropertyMetadata(false, EnabledChanged));


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
            var thumb = target as Thumb;

            if (thumb != null)
            {
                thumb.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
                thumb.PreviewMouseMove -= OnPreviewMouseMove;
                thumb.DragDelta -= OnDragDelta;

                if (GetEnabled(target))
                {
                    thumb.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
                    thumb.PreviewMouseMove += OnPreviewMouseMove;
                    thumb.DragDelta += OnDragDelta;
                }
            }
        }


        private static void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                (sender as UIElement)?.GetWindow()?.DragMove();
        }


        private static void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var window = (sender as UIElement)?.GetWindow();

            if (window != null && window.WindowState == WindowState.Maximized)
            {
                var windowLocation = window.PointToScreen(new Point());

                // restore window due to moving it
                window.WindowState = WindowState.Normal;

                window.Left = windowLocation.X + e.HorizontalChange;
                window.Top = windowLocation.Y + e.VerticalChange;
            }
        }


        private static void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = (sender as UIElement)?.GetWindow();

            if (window != null && e.ClickCount == 2)
            {
                if (window.WindowState == WindowState.Maximized)
                    window.WindowState = WindowState.Normal;
                else if (window.WindowState == WindowState.Normal)
                    window.WindowState = WindowState.Maximized;

                e.Handled = true;
            }
        }
    }
}
