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
        private readonly Font font;

        private float[,] depthBuffer;

        public Renderer(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            ctx = Graphics.FromImage(this.bitmap);
            ctx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            font = SystemFonts.DefaultFont;

            Clear();
        }

        public void Clear()
        {
            Clear(Color.Black);
        }

        public void Clear(Color color)
        {
            ctx.Clear(color);
            depthBuffer = new float[Width, Height];
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

        public void DrawMeshWireframe(Mesh mesh, Matrix4x4 modelMatrix, Matrix4x4 projectionMatrix, Color color, float thickness = 1f)
        {
            Vector resolution = new Vector(Width, Height) / 2f;
            resolution.Z = 1f;

            Vector one = Vector.One;
            one.Z = 0f;

            Matrix4x4 mp = projectionMatrix * modelMatrix;

            for (int i = 0; i < mesh.TriangleCount; i++)
            {
                Vector v1 = mesh[i * 3].Position * mp;
                Vector v2 = mesh[i * 3 + 1].Position * mp;
                Vector v3 = mesh[i * 3 + 2].Position * mp;
                Vector normal = mesh[i * 3].Normal * (new Matrix4x4(new float[4, 4]{
                    { 1f, 1f, 1f, 0f },
                    { 1f, 1f, 1f, 0f },
                    { 1f, 1f, 1f, 0f },
                    { 0f, 0f, 0f, 1f }
                }) * modelMatrix);

                float dot = Vector.Dot(normal.Normalized, mesh[i * 3].Position * modelMatrix);
                if (dot < 0)
                {
                    //v1 *= mp;
                    //v2 *= mp;
                    //v3 *= mp;

                    //v1 /= v1.W;
                    //v2 /= v2.W;
                    //v3 /= v3.W;

                    v1 = v1 + one;
                    v2 = v2 + one;
                    v3 = v3 + one;

                    v1 *= resolution;
                    v2 *= resolution;
                    v3 *= resolution;

                    using (Pen pen = new Pen(color, thickness))
                    {
                        ctx.DrawLine(pen, v1.X, v1.Y, v2.X, v2.Y);
                        ctx.DrawLine(pen, v2.X, v2.Y, v3.X, v3.Y);
                        ctx.DrawLine(pen, v3.X, v3.Y, v1.X, v1.Y);
                    }
                }
            }
        }

    }
}