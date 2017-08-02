using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Stugo.Wpf.Interop;

namespace Stugo.Wpf
{
    /// <summary>
    /// Represents a monitor attached to the device.  Attempts to replicate
    /// <see cref="System.Windows.Forms.Screen"/> API.
    /// </summary>
    public class Screen
    {
        /// <summary>
        /// Get the screen marked as primary (the one that the taskbar is on)
        /// </summary>
        public static Screen GetPrimaryScreen()
        {
            return new Screen(User32WindowFunctions.MonitorFromPoint(new POINTSTRUCT(), MONITOR_DEFAULTTO.PRIMARY));
        }


        /// <summary>
        /// Get a list of all screens attached to the device.
        /// </summary>
        /// <returns></returns>
        public static Screen[] GetAllScreens()
        {
            var screens = new List<Screen>();

            User32WindowFunctions.EnumDisplayMonitorsCallback callback =
                (monitor, hdc, lprcMonitor, lparam) =>
                {
                    screens.Add(new Screen(monitor));
                    return true;
                };

            User32WindowFunctions.EnumDisplayMonitors(User32WindowFunctions.NullHandleRef, null, callback, IntPtr.Zero);
            return screens.ToArray();
        }


        /// <summary>
        /// Get the screen which has most of the window referred to by <paramref name="hwnd"/> on
        /// it.
        /// </summary>
        public static Screen FromHWnd(IntPtr hwnd)
        {
            return new Screen(User32WindowFunctions.MonitorFromWindow(new HandleRef(null, hwnd), MONITOR_DEFAULTTO.NEAREST));
        }


        /// <summary>
        /// Get the screen referred to by the given win32 monitor handle.
        /// </summary>
        public static Screen FromMonitorHandle(IntPtr handle)
        {
            return new Screen(handle);
        }


        /// <summary>
        /// Get the screen containing the given point.
        /// </summary>
        public static Screen FromPoint(Point point)
        {
            var nativePoint = new POINTSTRUCT((int)point.X, (int)point.Y);
            return new Screen(User32WindowFunctions.MonitorFromPoint(nativePoint, MONITOR_DEFAULTTO.NEAREST));
        }


        /// <summary>
        /// Get the screen containing most of the given rectangle.
        /// </summary>
        public static Screen FromRect(Rect rect)
        {
            var nativeRect = new RECT((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);
            return new Screen(User32WindowFunctions.MonitorFromRect(ref nativeRect, MONITOR_DEFAULTTO.NEAREST));
        }


        /// <summary>
        /// Get the screen containing most of the given WPF window.
        /// </summary>
        public static Screen FromWindow(Window window)
        {
            return new Screen(new WindowInteropHelper(window).Handle);
        }


        /// <summary>
        /// Get the screen containing most of the given WPF control.
        /// </summary>
        public static Screen FromControl(FrameworkElement control)
        {
            var layoutRect = new Rect(control.PointToScreen(new Point()), control.RenderSize);
            return FromRect(layoutRect);
        }


        /// <summary>
        /// Create a new instance.
        /// </summary>
        private Screen(IntPtr monitorHandle)
        {
            var info = new MONITORINFOEX();
            User32WindowFunctions.GetMonitorInfo(new HandleRef(null, monitorHandle), info);
            Bounds = new Rect(info.rcMonitor.left, info.rcMonitor.top, info.rcMonitor.width, info.rcMonitor.height);
            WorkingArea = new Rect(info.rcWork.left, info.rcWork.top, info.rcWork.width, info.rcWork.height);
            Primary = (info.dwFlags & MONITORINFOEX.FLAGS_PRIMARY) != 0;
            DeviceName = new string(info.szDevice).TrimEnd((char)0);

            var workingLocation = new Point(WorkingArea.Left - Bounds.Left, WorkingArea.Y - Bounds.Y);
            ScreenRelativeWorkingArea = new Rect(workingLocation, WorkingArea.Size);
        }


        /// <summary>
        /// Get the bounds of the screen in global coordinate space.
        /// </summary>
        public Rect Bounds { get; private set; }

        /// <summary>
        /// Get the working area of the screen in global coordinate space.
        /// </summary>
        public Rect WorkingArea { get; private set; }

        /// <summary>
        /// Get the working area of the screen in the screen coordinate space.
        /// </summary>
        public Rect ScreenRelativeWorkingArea { get; private set; }

        /// <summary>
        /// Get a value indicating if this is the primary screen.
        /// </summary>
        public bool Primary { get; private set; }

        /// <summary>
        /// Get the name of the monitor device.
        /// </summary>
        public string DeviceName { get; private set; }
    }
}
