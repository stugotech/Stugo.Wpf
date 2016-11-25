namespace Stugo.Wpf.Behaviours
{
    public enum ResizeType
    {
        // these values give the magnitude of change for each axis, as an sbyte
        // 0xXXYY - so 0xFF01 is x=-1 and y=1, i.e. bottom left corner
        None = 0,
        Left = 0xFF00,
        Right = 0x0100,
        Top = 0x00FF,
        Bottom = 0x0001,
        TopLeft = Top | Left,
        TopRight = Top | Right,
        BottomRight = Bottom | Right,
        BottomLeft = Bottom | Left
    }
}
