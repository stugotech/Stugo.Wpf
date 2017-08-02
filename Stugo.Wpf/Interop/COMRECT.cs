using System.Runtime.InteropServices;

namespace Stugo.Wpf.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal class COMRECT
    {
        public COMRECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }


        public int left;
        public int top;
        public int right;
        public int bottom;

        public int width { get { return right - left; }}
        public int height { get { return bottom - top; } }
    }
}
