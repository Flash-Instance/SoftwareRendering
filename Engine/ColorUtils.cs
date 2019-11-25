using System.Drawing;

namespace Engine
{
    public static class ColorUtils
    {

        public static Color Invert(Color color)
        {
            return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
        }

    }
}