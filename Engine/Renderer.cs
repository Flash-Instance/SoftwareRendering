using System;
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
            depthBuffer = new float[Width, Height];

            Clear();
        }

        public void Clear()
        {
            Clear(Color.Black);
        }

        public void Clear(Color color)
        {
            ctx.Clear(color);

            for(int y = 0;y < depthBuffer.GetLength(1);y++)
            {
                for(int x = 0;x < depthBuffer.GetLength(0);x++)
                {
                    depthBuffer[x, y] = float.MaxValue;
                }
            }
        }
        
        public void DrawMeshSolid(Mesh mesh, Matrix4x4 worldMatrix, Matrix4x4 projectionMatrix)
        {
            Vector resolution = new Vector(Width, Height) / 2f;
            resolution.Z = 1f;

            Vector one = Vector.One;
            one.Z = 0f;

            Matrix4x4 mp = projectionMatrix * worldMatrix;

            for(int i = 0;i < mesh.TriangleCount;i++)
            {
                Vector v1 = mesh[i * 3].Position;
                Vector v2 = mesh[i * 3 + 1].Position;
                Vector v3 = mesh[i * 3 + 2].Position;

                // Calculate normal from world space vertex positions
                Vector a = (v2 - v1) * worldMatrix;
                Vector b = (v3 - v1) * worldMatrix;
                Vector normal = Vector.Cross(a, b).Normalized;

                // Project vertices onto view plane and normalize to screen space
                v1 *= mp;
                v2 *= mp;
                v3 *= mp;

                // Extract depth of each vertex in normalized clip space
                float vD1 = (v1.Z + 1f) / 2f;
                float vD2 = (v2.Z + 1f) / 2f;
                float vD3 = (v3.Z + 1f) / 2f;

                v1 += one;
                v2 += one;
                v3 += one;
                v1 *= resolution;
                v2 *= resolution;
                v3 *= resolution;

                // Calculate bounding box for the projected triangle
                int minX = (int)MathUtils.Min(v1.X, v2.X, v3.X);
                int minY = (int)MathUtils.Min(v1.Y, v2.Y, v3.Y);
                int maxX = (int)MathUtils.Max(v1.X, v2.X, v3.X);
                int maxY = (int)MathUtils.Max(v1.Y, v2.Y, v3.Y);

                // Clip against the edges of the screen
                minX = Math.Max(minX, 0);
                minY = Math.Max(minY, 0);
                maxX = Math.Min(maxX, Width - 1);
                maxY = Math.Min(maxY, Height - 1);

                // Set up the triangle edges
                int a1 = (int)(v1.Y - v2.Y) + 1;
                int b1 = (int)(v2.X - v1.X) + 1;

                int a2 = (int)(v2.Y - v3.Y) + 1;
                int b2 = (int)(v3.X - v2.X) + 1;

                int a3 = (int)(v3.Y - v1.Y) + 1;
                int b3 = (int)(v1.X - v3.X) + 1;

                // Calculate barycentric coordinates at the minimum X and Y corners
                Vector p = new Vector(minX, minY);
                int w1_row = (int)Vector.Barycentric(v2, v3, p);
                int w2_row = (int)Vector.Barycentric(v3, v1, p);
                int w3_row = (int)Vector.Barycentric(v1, v2, p);

                // Perform rasterization
                for(p.Y = minY;p.Y <= maxY;p.Y++)
                {
                    // Store the barycentric coordinates for the start of this row
                    int w1 = w1_row;
                    int w2 = w2_row;
                    int w3 = w3_row;

                    for(p.X = minX;p.X <= maxX;p.X++)
                    {
                        // Check if p is on or inside of the triangle edges
                        if(w1 >= 0f && w2 >= 0f && w3 >= 0f)
                        {
                            // Ready to render the pixel
                            int pX = (int)p.X;
                            int pY = (int)p.Y;

                            // Perform interpolation between the 3 vertices using barycentric coordinates
                            float pW1 = (v2.Y - v3.Y) * (pX - v3.X) + (v3.X - v2.X) * (pY - v3.Y);
                            pW1 /= (v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y);

                            float pW2 = (v3.Y - v1.Y) * (pX - v3.X) + (v1.X - v3.X) * (pY - v3.Y);
                            pW2 /= (v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y);

                            float pW3 = 1f - pW1 - pW2;
                            float pWSum = pW1 + pW2 + pW3;

                            // Interpolate the depth value
                            float depth = (vD1 * pW1 + vD2 * pW2 * vD3 * pW3) / pWSum;

                            // Check the depth value agains the depth buffer
                            if(depth >= 0f && depth < depthBuffer[pX, pY])
                            {
                                // Store the depth
                                depthBuffer[pX, pY] = depth;

                                // Interpolate colors
                                int red = MathUtils.Clamp(0, 255, (int)((255f * pW2) / pWSum));
                                int green = MathUtils.Clamp(0, 255, (int)((255f * pW1) / pWSum));
                                int blue = 128;

                                // Draw the pixel color
                                bitmap.SetPixel((int)p.X, (int)p.Y, Color.FromArgb(red, green, blue));
                            }
                        }

                        // Step once to the right
                        w1 += a2;
                        w2 += a3;
                        w3 += a1;
                    }

                    // Step once down
                    w1_row += b2;
                    w2_row += b3;
                    w3_row += b1;
                }
            }
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