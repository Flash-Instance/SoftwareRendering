using System;

namespace Engine
{
    public struct Matrix4x4
    {

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

    }
}