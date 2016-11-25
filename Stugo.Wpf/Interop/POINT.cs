using System.Runtime.InteropServices;

namespace Stugo.Wpf.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal class POINT
    {
        public int x;
        public int y;


        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
