using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Stugo.Wpf.Interop;

namespace Stugo.Wpf.Behaviours
{
    /// <summary>
    /// When enabled for a <see cref="System.Windows.Window" />, hooks up a WndProc to the window
    /// which handles the WM_GETMINMAXINFO in order to support maximise/restore on a window which
    /// has ResizeMode set to NoResize. 
    /// </summary>
    public static class WindowMaximiseBehaviour
    {
        private const int WM_GETMINMAXINFO = 0x0024;
        private static readonly HwndSourceHook WindowProcDelegate = WindowProc;


        public static readonly DependencyProperty EnabledProperty =
               DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(WindowMaximiseBehaviour), new PropertyMetadata(false, EnabledChanged));


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
            var window = target as Window;

            if (window != null)
            {
                var windowHandle = new WindowInteropHelper(window).Handle;
                var hwndSource = HwndSource.FromHwnd(windowHandle);

                if (hwndSource != null) 
                {
                    hwndSource.RemoveHook(WindowProcDelegate);
                

                    if (GetEnabled(target))
                        hwndSource.AddHook(WindowProcDelegate);
                }
            }
        }


        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_GETMINMAXINFO:
                    var minMaxInfo = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
                    var screen = Screen.FromHWnd(hwnd);
                    minMaxInfo.ptMaxPosition.x = (int)screen.ScreenRelativeWorkingArea.Left;
                    minMaxInfo.ptMaxPosition.y = (int)screen.ScreenRelativeWorkingArea.Top;
                    minMaxInfo.ptMaxSize.x = (int)screen.ScreenRelativeWorkingArea.Width;
                    minMaxInfo.ptMaxSize.y = (int)screen.ScreenRelativeWorkingArea.Height;
                    Marshal.StructureToPtr(minMaxInfo, lParam, true);
                    break;
            }

            return IntPtr.Zero;
        }
    }
}
