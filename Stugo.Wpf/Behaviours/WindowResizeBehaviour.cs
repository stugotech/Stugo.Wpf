using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Stugo.Wpf.Behaviours
{
    /// <summary>
    /// When enabled for a <see cref="System.Windows.Controls.Primitives.Thumb" />, causes the
    /// thumb to resize the window when dragged, according to the configured ResizeType.
    /// </summary>
    public static class WindowResizeBehaviour
    {
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.RegisterAttached("Type", typeof(ResizeType),
                typeof(WindowResizeBehaviour), new PropertyMetadata(ResizeType.None, OnTypeChanged));


        public static ResizeType GetType(DependencyObject obj)
        {
            return (ResizeType)obj.GetValue(TypeProperty);
        }


        public static void SetType(DependencyObject obj, ResizeType value)
        {
            obj.SetValue(TypeProperty, value);
        }
        

        private static void OnTypeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var thumb = source as Thumb;

            if (thumb != null)
            {
                thumb.DragDelta -= OnDragDelta;

                if (GetType(thumb) != ResizeType.None)
                    thumb.DragDelta += OnDragDelta;
            }
        }


        private static void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            var window = thumb != null ? thumb.GetWindow() : null;

            if (window != null)
            {
                var type = GetType(thumb);

                // clever jigger pokery with carefully chosen enum values to give size directions
                var fx = (sbyte)(((int)type & 0xFF00) >> 8);
                var fy = (sbyte)((int)type & 0xFF);

                var width = Clamp(window.Width + fx * e.HorizontalChange, window.MinWidth, window.MaxWidth);
                var height = Clamp(window.Height + fy * e.VerticalChange, window.MinHeight, window.MaxHeight);

                // reposition the window if sizing from top and/or left so it doesn't grow from
                // the bottom right
                if (fx < 0)
                    window.Left -= width - window.Width;
                if (fy < 0)
                    window.Top -= height - window.Height;

                window.Width = width;
                window.Height = height;
            }
        }


        private static double Clamp(double value, double min, double max)
        {
            return Math.Min(max, Math.Max(min, value));
        }
    }
}
