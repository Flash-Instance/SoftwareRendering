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
        }

        public void DrawMeshWireframe(Mesh mesh, Matrix4x4 matrix, Color color, float thickness = 1f)
        {
            Vector resolution = new Vector(Width, Height) / 2f;
            resolution.Z = 1f;

            Vector one = Vector.One;
            one.Z = 0f;

            using (Pen pen = new Pen(color, thickness))
            {
                for (int i = 0; i < mesh.TriangleCount; i++)
                {
                    Vector v1 = mesh[i * 3].Position * matrix;
                    Vector v2 = mesh[i * 3 + 1].Position * matrix;
                    Vector v3 = mesh[i * 3 + 2].Position * matrix;

                    // Calculate normal from projected vertex positions
                    Vector a = v2 - v1;
                    Vector b = v3 - v1;
                    Vector normal = Vector.Cross(a, b).Normalized;

                    // Perform backface culling
                    float dot = Vector.Dot(normal, v1.Normalized);
                    if (dot > 0)
                    {
                        // Normalize to screen space
                        v1 += one;
                        v2 += one;
                        v3 += one;
                        v1 *= resolution;
                        v2 *= resolution;
                        v3 *= resolution;
                            
                        ctx.DrawLine(pen, v1.X, v1.Y, v2.X, v2.Y);
                        ctx.DrawLine(pen, v2.X, v2.Y, v3.X, v3.Y);
                        ctx.DrawLine(pen, v3.X, v3.Y, v1.X, v1.Y);
                    }
                }
            }
        }

    }
}