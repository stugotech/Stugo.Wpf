using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Stugo.Wpf.Behaviours
{
    /// <summary>
    /// When enabled for a <see cref="System.Windows.Window" />, attaches relevant behaviours to
    /// template elements if present.
    /// </summary>
    public static class FancyWindowBehaviour
    {
        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(FancyWindowBehaviour), new PropertyMetadata(false, EnabledChanged));


        public static bool GetEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledProperty);
        }


        public static void SetEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledProperty, value);
        }


        public static readonly DependencyProperty IsResizeEnabledProperty =
            DependencyProperty.RegisterAttached("IsResizeEnabled", typeof(bool), 
                typeof(FancyWindowBehaviour), new PropertyMetadata(false, IsResizeEnabledChanged));


        public static bool GetIsResizeEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsResizeEnabledProperty);
        }


        public static void SetIsResizeEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsResizeEnabledProperty, value);
        }

        
        private static void EnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var window = target as Window;

            if (window != null)
            {
                window.Loaded -= OnWindowLoaded;
                var enabled = GetEnabled(target);

                if (enabled)
                    window.Loaded += OnWindowLoaded;

                if (window.IsLoaded)
                    EnableBehaviour(window, enabled);
            }
        }


        private static void OnWindowLoaded(object sender, System.EventArgs e)
        {
            Debug.Print("Loaded");
            var window = sender as Window;

            if (window != null)
            {
                EnableBehaviour(window, GetEnabled(window));
                EnableResizeBehaviour(window, GetIsResizeEnabled(window));
            }
        }


        private static void EnableBehaviour(Window window, bool enable)
        {
            var template = window.Template;

            if (template != null)
            {
                var closeButton = template.FindName("PART_CloseButton", window) as Button;
                if (closeButton != null)
                    CloseButtonBehaviour.SetEnabled(closeButton, enable);

                var minimiseButton = template.FindName("PART_MinimiseButton", window) as Button;
                if (minimiseButton != null)
                    MinimiseButtonBehaviour.SetEnabled(minimiseButton, enable);

                var maxmiseButton = template.FindName("PART_MaximiseButton", window) as Button;
                if (maxmiseButton != null)
                    MaximiseButtonBehaviour.SetEnabled(maxmiseButton, enable);

                var headerThumb = template.FindName("PART_HeaderThumb", window) as Thumb;
                if (headerThumb != null)
                    WindowHeaderBehaviour.SetEnabled(headerThumb, enable);

                EnableResizeBehaviour(window, enable);
            }
        }


        private static void IsResizeEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var window = target as Window;

            if (window?.IsLoaded == true)
                EnableResizeBehaviour(window, GetIsResizeEnabled(window));
        }


        private static void EnableResizeBehaviour(Window window, bool enable)
        {
            var template = window.Template;
            WindowMaximiseBehaviour.SetEnabled(window, enable);
            var types = Enum.GetValues(typeof(ResizeType)).OfType<ResizeType>().Where(x => x != ResizeType.None);

            foreach (var type in types)
            {
                var thumb = template.FindName($"PART_Resize{type}", window) as Thumb;
                if (thumb != null)
                    WindowResizeBehaviour.SetType(thumb, enable ? type : ResizeType.None);
            }
        }
    }
}
