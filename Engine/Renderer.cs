using System.Drawing;

namespace Engine
{
    public class Renderer
    {

        public int Width
        {
            get { return bitmap.Width; }
        }

        public int Height
        {
            get { return bitmap.Height; }
        }

        private readonly Bitmap bitmap;
        private readonly Graphics ctx;

        public Renderer(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            ctx = Graphics.FromImage(this.bitmap);
        }

        public void Clear(Color color)
        {
            ctx.Clear(color);
        }

        public void FillRect(int x, int y, int width, int height, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                ctx.FillRectangle(brush, new Rectangle(x, y, width, height));
            }
        }

        public void FillCircle(int x, int y, int radius, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                ctx.FillEllipse(brush, new Rectangle(x - radius, y - radius, radius * 2, radius * 2));
            }
        }

    }
}