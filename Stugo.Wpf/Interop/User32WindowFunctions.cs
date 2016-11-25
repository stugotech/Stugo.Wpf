using System;
using System.Runtime.InteropServices;

namespace Stugo.Wpf.Interop
{
    /// <summary>
    /// Interop entry points to user32.dll functions.  These delegates and associated structs were
    /// mostly blagged from the System.Windows.Forms reference source.
    /// </summary>
    internal static class User32WindowFunctions
    {
        public delegate bool EnumDisplayMonitorsCallback(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);


        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern bool EnumDisplayMonitors(HandleRef hdc, COMRECT rcClip, EnumDisplayMonitorsCallback lpfnEnum, IntPtr dwData);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out]MONITORINFOEX info);
        
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern IntPtr MonitorFromPoint(POINTSTRUCT pt, MONITOR_DEFAULTTO flags);

        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern IntPtr MonitorFromRect(ref RECT rect, MONITOR_DEFAULTTO flags);

        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern IntPtr MonitorFromWindow(HandleRef handle, MONITOR_DEFAULTTO flags);


        public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);
    }
}
