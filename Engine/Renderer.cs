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
        private float[,] depthBuffer;

        public Renderer(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            ctx = Graphics.FromImage(this.bitmap);
            ctx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

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

        public void DrawLine(float x1, float y1, float x2, float y2, float thickness, Color color)
        {
            using (Pen pen = new Pen(color, thickness))
            {
                ctx.DrawLine(pen, x1, y1, x2, y2);
            }
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
                Vector v1 = mesh[i * 3].Position;
                Vector v2 = mesh[i * 3 + 1].Position;
                Vector v3 = mesh[i * 3 + 2].Position;
                Vector normal = mesh[i * 3].Normal;

                Vector center = (v1 + v2 + v3) / 3f;
                Vector normalEnd = (center + (normal * 0.05f)) * mp;
                center *= mp;
                center /= center.W;
                center += one;
                center *= resolution;
                normalEnd /= normalEnd.W;
                normalEnd += one;
                normalEnd *= resolution;

                v1 *= mp;
                v2 *= mp;
                v3 *= mp;
                //normal *= mp;

                float dot = Vector.Dot(normal, v1);
                if (dot < 0f)
                {
                    v1 /= v1.W;
                    v2 /= v2.W;
                    v3 /= v3.W;

                    v1 = v1 + one;
                    v2 = v2 + one;
                    v3 = v3 + one;

                    v1 *= resolution;
                    v2 *= resolution;
                    v3 *= resolution;

                    DrawLine(v3.X, v3.Y, v1.X, v1.Y, thickness, color);
                    DrawLine(v1.X, v1.Y, v2.X, v2.Y, thickness, color);
                    DrawLine(v2.X, v2.Y, v3.X, v3.Y, thickness, color);

                    DrawLine(center.X, center.Y, normalEnd.X, normalEnd.Y, 0.5f, Color.Red);
                }
            }
        }

    }
}