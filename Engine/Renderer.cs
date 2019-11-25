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
            ctx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void Clear(Color color)
        {
            ctx.Clear(color);
        }

        public void FillRect(float x, float y, float width, float height, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                ctx.FillRectangle(brush, new RectangleF(x, y, width, height));
            }
        }

        public void FillCircle(Vector pos, float radius, Color color)
        {
            FillCircle(pos.X, pos.Y, radius, color);
        }

        public void FillCircle(float x, float y, float radius, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                ctx.FillEllipse(brush, new RectangleF(x - radius, y - radius, radius * 2, radius * 2));
            }
        }

    }
}