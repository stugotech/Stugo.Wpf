using System.Runtime.InteropServices;

namespace Stugo.Wpf.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal class MINMAXINFO
    {
        public POINT ptReserved = null;
        public POINT ptMaxSize = null;
        public POINT ptMaxPosition = null;
        public POINT ptMinTrackSize = null;
        public POINT ptMaxTrackSize = null;
    }
}
