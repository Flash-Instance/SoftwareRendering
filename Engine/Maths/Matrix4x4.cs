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
            //float x = a.X * b.m[0, 0] + a.Y * b.m[1, 0] + a.Z * b.m[2, 0] + b.m[3, 0];
            //float y = a.X * b.m[0, 1] + a.Y * b.m[1, 1] + a.Z * b.m[2, 1] + b.m[3, 1];
            //float z = a.X * b.m[0, 2] + a.Y * b.m[1, 2] + a.Z * b.m[2, 2] + b.m[3, 2];
            //float w = a.X * b.m[0, 3] + a.Y * b.m[1, 3] + a.Z * b.m[2, 3] + b.m[3, 3];

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
            //float m00 = a.m[0, 0] * b.m[0, 0] + a.m[0, 1] * b.m[1, 0] + a.m[0, 2] * b.m[2, 0] + a.m[0, 3] * b.m[3, 0];
            //float m10 = a.m[0, 0] * b.m[0, 1] + a.m[0, 1] * b.m[1, 1] + a.m[0, 2] * b.m[2, 1] + a.m[0, 3] * b.m[3, 1];
            //float m20 = a.m[0, 0] * b.m[0, 2] + a.m[0, 1] * b.m[1, 2] + a.m[0, 2] * b.m[2, 2] + a.m[0, 3] * b.m[3, 2];
            //float m30 = a.m[0, 0] * b.m[0, 3] + a.m[0, 1] * b.m[1, 3] + a.m[0, 2] * b.m[2, 3] + a.m[0, 3] * b.m[3, 3];

            //float m01 = a.m[1, 0] * b.m[0, 0] + a.m[1, 1] * b.m[1, 0] + a.m[1, 2] * b.m[2, 0] + a.m[1, 3] * b.m[3, 0];
            //float m11 = a.m[1, 0] * b.m[0, 1] + a.m[1, 1] * b.m[1, 1] + a.m[1, 2] * b.m[2, 1] + a.m[1, 3] * b.m[3, 1];
            //float m21 = a.m[1, 0] * b.m[0, 2] + a.m[1, 1] * b.m[1, 2] + a.m[1, 2] * b.m[2, 2] + a.m[1, 3] * b.m[3, 2];
            //float m31 = a.m[1, 0] * b.m[0, 3] + a.m[1, 1] * b.m[1, 3] + a.m[1, 2] * b.m[2, 3] + a.m[1, 3] * b.m[3, 3];

            //float m02 = a.m[2, 0] * b.m[0, 0] + a.m[2, 1] * b.m[1, 0] + a.m[2, 2] * b.m[2, 0] + a.m[2, 3] * b.m[3, 0];
            //float m12 = a.m[2, 0] * b.m[0, 1] + a.m[2, 1] * b.m[1, 1] + a.m[2, 2] * b.m[2, 1] + a.m[2, 3] * b.m[3, 1];
            //float m22 = a.m[2, 0] * b.m[0, 2] + a.m[2, 1] * b.m[1, 2] + a.m[2, 2] * b.m[2, 2] + a.m[2, 3] * b.m[3, 2];
            //float m32 = a.m[2, 0] * b.m[0, 3] + a.m[2, 1] * b.m[1, 3] + a.m[2, 2] * b.m[2, 3] + a.m[2, 3] * b.m[3, 3];

            //float m03 = a.m[3, 0] * b.m[0, 0] + a.m[3, 1] * b.m[1, 0] + a.m[3, 2] * b.m[2, 0] + a.m[3, 3] * b.m[3, 0];
            //float m13 = a.m[3, 0] * b.m[0, 1] + a.m[3, 1] * b.m[1, 1] + a.m[3, 2] * b.m[2, 1] + a.m[3, 3] * b.m[3, 1];
            //float m23 = a.m[3, 0] * b.m[0, 2] + a.m[3, 1] * b.m[1, 2] + a.m[3, 2] * b.m[2, 2] + a.m[3, 3] * b.m[3, 2];
            //float m33 = a.m[3, 0] * b.m[0, 3] + a.m[3, 1] * b.m[1, 3] + a.m[3, 2] * b.m[2, 3] + a.m[3, 3] * b.m[3, 3];

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
                { 0f, 0f, far / zDiff, 1f },
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
            float n1 = m[0, 0];
            float n2 = m[0, 1];
            float n3 = m[0, 2];
            float n4 = m[0, 3];
            float n5 = m[1, 0];
            float n6 = m[1, 1];
            float n7 = m[1, 2];
            float n8 = m[1, 3];
            float n9 = m[2, 0];
            float n10 = m[2, 1];
            float n11 = m[2, 2];
            float n12 = m[2, 3];
            float n13 = m[3, 0];
            float n14 = m[3, 1];
            float n15 = m[3, 2];
            float n16 = m[3, 3];
            float n17 = n11 * n16 - n12 * n15;
            float n18 = n10 * n16 - n12 * n14;
            float n19 = n10 * n15 - n11 * n14;
            float n20 = n9 * n16 - n12 * n13;
            float n21 = n9 * n15 - n11 * n13;
            float n22 = n9 * n14 - n10 * n13;
            float n23 = n6 * n17 - n7 * n18 + n8 * n19;
            float n24 = -(n5 * n17 - n7 * n20 + n8 * n21);
            float n25 = n5 * n18 - n6 * n20 + n8 * n22;
            float n26 = -(n5 * n19 - n6 * n21 + n7 * n22);
            float n27 = 1f / (n1 * n23 + n2 * n24 + n3 * n25 + n4 * n26);
            float n28 = n7 * n16 - n8 * n15;
            float n29 = n6 * n16 - n8 * n14;
            float n30 = n6 * n15 - n7 * n14;
            float n31 = n5 * n16 - n8 * n13;
            float n32 = n5 * n15 - n7 * n13;
            float n33 = n5 * n14 - n6 * n13;
            float n34 = n7 * n12 - n8 * n11;
            float n35 = n6 * n12 - n8 * n10;
            float n36 = n6 * n11 - n7 * n10;
            float n37 = n5 * n12 - n8 * n9;
            float n38 = n5 * n11 - n7 * n9;
            float n39 = n5 * n10 - n6 * n9;

            Matrix4x4 r = new Matrix4x4(new float[4, 4]);
            r.m[0, 0] = n23 * n27;
            r.m[1, 0] = n24 * n27;
            r.m[2, 0] = n25 * n27;
            r.m[3, 0] = n26 * n27;
            r.m[0, 1] = -(n2 * n17 - n3 * n18 + n4 * n19) * n27;
            r.m[1, 1] = (n1 * n17 - n3 * n20 + n4 * n21) * n27;
            r.m[2, 1] = -(n1 * n18 - n2 * n20 + n4 * n22) * n27;
            r.m[3, 1] = (n1 * n19 - n2 * n21 + n3 * n22) * n27;
            r.m[0, 2] = (n2 * n28 - n3 * n29 + n4 * n30) * n27;
            r.m[1, 2] = -(n1 * n28 - n3 * n31 + n4 * n32) * n27;
            r.m[2, 2] = (n1 * n29 - n2 * n31 + n4 * n33) * n27;
            r.m[3, 2] = -(n1 * n30 - n2 * n32 + n3 * n33) * n27;
            r.m[0, 3] = -(n2 * n34 - n3 * n35 + n4 * n36) * n27;
            r.m[1, 3] = (n1 * n34 - n3 * n37 + n4 * n38) * n27;
            r.m[2, 3] = -(n1 * n35 - n2 * n37 + n4 * n39) * n27;
            r.m[3, 3] = (n1 * n36 - n2 * n38 + n3 * n39) * n27;

            return r;
        }

    }
}