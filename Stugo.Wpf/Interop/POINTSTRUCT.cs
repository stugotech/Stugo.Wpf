using System.Runtime.InteropServices;

namespace Stugo.Wpf.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINTSTRUCT
    {
        public int x;
        public int y;


        public POINTSTRUCT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
