using System;

namespace Engine
{
    public struct Matrix4x4
    {

        public Matrix4x4 Inversed
        {
            get
            {
                return Inverse();
            }
        }

        private readonly float[,] m;

        public Matrix4x4(float[,] m)
        {
            if(m == null || m.GetLength(0) != 4 && m.GetLength(1) != 4)
            {
                throw new Exception("Expected a 4x4 array");
            }

            this.m = m;
        }

        #region OPERATORS
        public static Vector operator *(Vector a, Matrix4x4 b)
        {
            float x = a.X * b.m[0, 0] + a.Y * b.m[0, 1] + a.Z * b.m[0, 2] + b.m[0, 3];
            float y = a.X * b.m[1, 0] + a.Y * b.m[1, 1] + a.Z * b.m[1, 2] + b.m[1, 3];
            float z = a.X * b.m[2, 0] + a.Y * b.m[2, 1] + a.Z * b.m[2, 2] + b.m[2, 3];
            float w = a.X * b.m[3, 0] + a.Y * b.m[3, 1] + a.Z * b.m[3, 2] + b.m[3, 3];

            if(w != 0f)
            {
                x /= w;
                y /= w;
                z /= w;
            }

            return new Vector(x, y, z, w);
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            float m00 = a.m[0, 0] * b.m[0, 0] + a.m[1, 0] * b.m[0, 1] + a.m[2, 0] * b.m[0, 2] + a.m[3, 0] * b.m[0, 3];
            float m10 = a.m[0, 0] * b.m[1, 0] + a.m[1, 0] * b.m[1, 1] + a.m[2, 0] * b.m[1, 2] + a.m[3, 0] * b.m[1, 3];
            float m20 = a.m[0, 0] * b.m[2, 0] + a.m[1, 0] * b.m[2, 1] + a.m[2, 0] * b.m[2, 2] + a.m[3, 0] * b.m[2, 3];
            float m30 = a.m[0, 0] * b.m[3, 0] + a.m[1, 0] * b.m[3, 1] + a.m[2, 0] * b.m[3, 2] + a.m[3, 0] * b.m[3, 3];

            float m01 = a.m[0, 1] * b.m[0, 0] + a.m[1, 1] * b.m[0, 1] + a.m[2, 1] * b.m[0, 2] + a.m[3, 1] * b.m[0, 3];
            float m11 = a.m[0, 1] * b.m[1, 0] + a.m[1, 1] * b.m[1, 1] + a.m[2, 1] * b.m[1, 2] + a.m[3, 1] * b.m[1, 3];
            float m21 = a.m[0, 1] * b.m[2, 0] + a.m[1, 1] * b.m[2, 1] + a.m[2, 1] * b.m[2, 2] + a.m[3, 1] * b.m[2, 3];
            float m31 = a.m[0, 1] * b.m[3, 0] + a.m[1, 1] * b.m[3, 1] + a.m[2, 1] * b.m[3, 2] + a.m[3, 1] * b.m[3, 3];

            float m02 = a.m[0, 2] * b.m[0, 0] + a.m[1, 2] * b.m[0, 1] + a.m[2, 2] * b.m[0, 2] + a.m[3, 2] * b.m[0, 3];
            float m12 = a.m[0, 2] * b.m[1, 0] + a.m[1, 2] * b.m[1, 1] + a.m[2, 2] * b.m[1, 2] + a.m[3, 2] * b.m[1, 3];
            float m22 = a.m[0, 2] * b.m[2, 0] + a.m[1, 2] * b.m[2, 1] + a.m[2, 2] * b.m[2, 2] + a.m[3, 2] * b.m[2, 3];
            float m32 = a.m[0, 2] * b.m[3, 0] + a.m[1, 2] * b.m[3, 1] + a.m[2, 2] * b.m[3, 2] + a.m[3, 2] * b.m[3, 3];

            float m03 = a.m[0, 3] * b.m[0, 0] + a.m[1, 3] * b.m[0, 1] + a.m[2, 3] * b.m[0, 2] + a.m[3, 3] * b.m[0, 3];
            float m13 = a.m[0, 3] * b.m[1, 0] + a.m[1, 3] * b.m[1, 1] + a.m[2, 3] * b.m[1, 2] + a.m[3, 3] * b.m[1, 3];
            float m23 = a.m[0, 3] * b.m[2, 0] + a.m[1, 3] * b.m[2, 1] + a.m[2, 3] * b.m[2, 2] + a.m[3, 3] * b.m[2, 3];
            float m33 = a.m[0, 3] * b.m[3, 0] + a.m[1, 3] * b.m[3, 1] + a.m[2, 3] * b.m[3, 2] + a.m[3, 3] * b.m[3, 3];

            return new Matrix4x4(new float[4, 4]
            {
                { m00, m10, m20, m30 },
                { m01, m11, m21, m31 },
                { m02, m12, m22, m32 },
                { m03, m13, m23, m33 }
            });
        }
        #endregion

        #region STATIC METHODS
        public static Matrix4x4 CreateIdentity()
        {
            return new Matrix4x4(new float[4, 4]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f }
            });
        }

        public static Matrix4x4 CreateProjection(float width, float height, float fov, float near, float far)
        {
            float aspect = height / width;
            float theta = (float)(1f / Math.Tan(MathUtils.DegreesToRadians(fov / 2f)));
            float zDiff = far - near;

            return new Matrix4x4(new float[4, 4]
            {
                { aspect * theta, 0f, 0f, 0f },
                { 0f, theta, 0f, 0f },
                { 0f, 0f, far / zDiff, -1f },
                { 0f, 0f, (-far * near) / zDiff, 0f }
            });
        }

        public static Matrix4x4 CreateTranslation(Vector translation)
        {
            return new Matrix4x4(new float[4, 4]
            {
                { 1f, 0f, 0f, translation.X },
                { 0f, 1f, 0f, translation.Y },
                { 0f, 0f, 1f, translation.Z },
                { 0f, 0f, 0f, 1f }
            });
        }

        public static Matrix4x4 CreateRotation(MathUtils.Axis axis, float angleDegrees)
        {
            float angleRadians = MathUtils.DegreesToRadians(angleDegrees);
            float s = (float)Math.Sin(angleRadians);
            float c = (float)Math.Cos(angleRadians);

            float[,] m = null;
            switch(axis)
            {
                case MathUtils.Axis.X:
                    m = new float[4, 4]
                    {
                        { 1f, 0f, 0f, 0f },
                        { 0f, c, -s, 0f },
                        { 0f, s, c, 0f },
                        { 0f, 0f, 0f, 1f }
                    };
                    break;

                case MathUtils.Axis.Y:
                    m = new float[4, 4]
                    {
                        { c, 0f, s, 0f },
                        { 0f, 1f, 0f, 0f },
                        { -s, 0f, c, 0f },
                        { 0f, 0f, 0f, 1f }
                    };
                    break;

                case MathUtils.Axis.Z:
                    m = new float[4, 4]
                    {
                        { c, -s, 0f, 0f },
                        { s, c, 0f, 0f },
                        { 0f, 0f, 1f, 0f },
                        { 0f, 0f, 0f, 1f }
                    };
                    break;
            }

            return new Matrix4x4(m);
        }

        public static Matrix4x4 CreateScale(Vector scale)
        {
            return new Matrix4x4(new float[4, 4]
            {
                { scale.X, 0f, 0f, 0f },
                { 0f, scale.Y, 0f, 0f },
                { 0f, 0f, scale.Z, 0f },
                { 0f, 0f, 0f, 1f }
            });
        }
        #endregion

        private Matrix4x4 Inverse()
        {
            double n1 = m[0, 0];
            double n2 = m[0, 1];
            double n3 = m[0, 2];
            double n4 = m[0, 3];
            double n5 = m[1, 0];
            double n6 = m[1, 1];
            double n7 = m[1, 2];
            double n8 = m[1, 3];
            double n9 = m[2, 0];
            double n10 = m[2, 1];
            double n11 = m[2, 2];
            double n12 = m[2, 3];
            double n13 = m[3, 0];
            double n14 = m[3, 1];
            double n15 = m[3, 2];
            double n16 = m[3, 3];
            double n17 = (double)n11 * (double)n16 - (double)n12 * (double)n15;
            double n18 = (double)n10 * (double)n16 - (double)n12 * (double)n14;
            double n19 = (double)n10 * (double)n15 - (double)n11 * (double)n14;
            double n20 = (double)n9 * (double)n16 - (double)n12 * (double)n13;
            double n21 = (double)n9 * (double)n15 - (double)n11 * (double)n13;
            double n22 = (double)n9 * (double)n14 - (double)n10 * (double)n13;
            double n23 = (double)n6 * (double)n17 - (double)n7 * (double)n18 + (double)n8 * (double)n19;
            double n24 = -((double)n5 * (double)n17 - (double)n7 * (double)n20 + (double)n8 * (double)n21);
            double n25 = (double)n5 * (double)n18 - (double)n6 * (double)n20 + (double)n8 * (double)n22;
            double n26 = -((double)n5 * (double)n19 - (double)n6 * (double)n21 + (double)n7 * (double)n22);
            double n27 = 1f / ((double)n1 * (double)n23 + (double)n2 * (double)n24 + (double)n3 * (double)n25 + (double)n4 * (double)n26);
            double n28 = (double)n7 * (double)n16 - (double)n8 * (double)n15;
            double n29 = (double)n6 * (double)n16 - (double)n8 * (double)n14;
            double n30 = (double)n6 * (double)n15 - (double)n7 * (double)n14;
            double n31 = (double)n5 * (double)n16 - (double)n8 * (double)n13;
            double n32 = (double)n5 * (double)n15 - (double)n7 * (double)n13;
            double n33 = (double)n5 * (double)n14 - (double)n6 * (double)n13;
            double n34 = (double)n7 * (double)n12 - (double)n8 * (double)n11;
            double n35 = (double)n6 * (double)n12 - (double)n8 * (double)n10;
            double n36 = (double)n6 * (double)n11 - (double)n7 * (double)n10;
            double n37 = (double)n5 * (double)n12 - (double)n8 * (double)n9;
            double n38 = (double)n5 * (double)n11 - (double)n7 * (double)n9;
            double n39 = (double)n5 * (double)n10 - (double)n6 * (double)n9;

            Matrix4x4 r = new Matrix4x4(new float[4, 4]);
            r.m[0, 0] = (float)((double)n23 * (double)n27);
            r.m[1, 0] = (float)((double)n24 * (double)n27);
            r.m[2, 0] = (float)((double)n25 * (double)n27);
            r.m[3, 0] = (float)((double)n26 * (double)n27);
            r.m[0, 1] = (float)-(((double)n2 * (double)n17 - (double)n3 * (double)n18 + (double)n4 * (double)n19) * (double)n27);
            r.m[1, 1] = (float)(((double)n1 * (double)n17 - (double)n3 * (double)n20 + (double)n4 * (double)n21) * (double)n27);
            r.m[2, 1] = (float)-(((double)n1 * (double)n18 - (double)n2 * (double)n20 + (double)n4 * (double)n22) * (double)n27);
            r.m[3, 1] = (float)(((double)n1 * (double)n19 - (double)n2 *(double)n21 + (double)n3 * (double)n22) * (double)n27);
            r.m[0, 2] = (float)(((double)n2 * (double)n28 - (double)n3 * (double)n29 + (double)n4 * (double)n30) * (double)n27);
            r.m[1, 2] = (float)-(((double)n1 * (double)n28 - (double)n3 * (double)n31 + (double)n4 * (double)n32) * (double)n27);
            r.m[2, 2] = (float)(((double)n1 * (double)n29 - (double)n2 * (double)n31 + (double)n4 * (double)n33) * (double)n27);
            r.m[3, 2] = (float)-(((double)n1 * (double)n30 - (double)n2 * (double)n32 + (double)n3 * (double)n33) * (double)n27);
            r.m[0, 3] = (float)-(((double)n2 * (double)n34 - (double)n3 * (double)n35 + (double)n4 * (double)n36) * (double)n27);
            r.m[1, 3] = (float)(((double)n1 * (double)n34 - (double)n3 * (double)n37 + (double)n4 * (double)n38) * (double)n27);
            r.m[2, 3] = (float)-(((double)n1 * (double)n35 - (double)n2 * (double)n37 + (double)n4 * (double)n39) * (double)n27);
            r.m[3, 3] = (float)(((double)n1 * (double)n36 - (double)n2 * (double)n38 + (double)n3 * (double)n39) * (double)n27);

            return r;
        }

    }
}