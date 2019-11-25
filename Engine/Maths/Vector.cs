using System;

namespace Engine
{
    /// <summary>
    /// Represents either a 2D, 3D, or 4D vector
    /// </summary>
    public struct Vector
    {

        /// <summary>
        /// The magnitude / length of the vector
        /// </summary>
        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
            }
        }

        /// <summary>
        /// The normalized version of this vector
        /// </summary>
        public Vector Normalized
        {
            get
            {
                float mag = Magnitude;
                if(mag != 0)
                {
                    return new Vector(X / mag, Y / mag, Z / mag, W / mag);
                }

                return this;
            }
        }

        /// <summary>
        /// The X component of the vector
        /// </summary>
        public float X;

        /// <summary>
        /// The Y component of the vector
        /// </summary>
        public float Y;

        /// <summary>
        /// The Z component of the vector
        /// </summary>
        public float Z;

        /// <summary>
        /// The W component of the vector
        /// </summary>
        public float W;

        #region CONSTANTS
        /// <summary>
        /// Vector with a magnitude of zero
        /// </summary>
        public static readonly Vector Zero = new Vector(0f, 0f, 0f);

        /// <summary>
        /// Vector with a magnitude of one
        /// </summary>
        public static readonly Vector One = new Vector(1f, 1f, 1f);

        /// <summary>
        /// Unit vector pointing up in world space
        /// </summary>
        public static readonly Vector Up = new Vector(0f, 1f, 0f);

        /// <summary>
        /// Unit vector pointing down in world space
        /// </summary>
        public static readonly Vector Down = new Vector(0f, -1f, 0f);

        /// <summary>
        /// Unit vector pointing right in world space
        /// </summary>
        public static readonly Vector Right = new Vector(1f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing left in world space
        /// </summary>
        public static readonly Vector Left = new Vector(-1f, 0f, 0f);

        /// <summary>
        /// Unit vector pointing forward in world space
        /// </summary>
        public static readonly Vector Forward = new Vector(0f, 0f, 1f);

        /// <summary>
        /// Unit vector pointing backwards in world space
        /// </summary>
        public static readonly Vector Backward = new Vector(0f, 0f, -1f);
        #endregion

        /// <summary>
        /// Constructs a new 2D vector
        /// </summary>
        /// <param name="x">The X component of the vector</param>
        /// <param name="y">The Y component of the vector</param>
        public Vector(float x, float y) : this(x, y, 0f) { }

        /// <summary>
        /// Constructs a new 3D vector
        /// </summary>
        /// <param name="x">The X component of the vector</param>
        /// <param name="y">The Y component of the vector</param>
        /// <param name="z">The Z component of the vector</param>
        public Vector(float x, float y, float z) : this(x, y, z, 0f) { }

        /// <summary>
        /// Constructs a new 4D vector
        /// </summary>
        /// <param name="x">The X component of the vector</param>
        /// <param name="y">The Y component of the vector</param>
        /// <param name="z">The Z component of the vector</param>
        /// <param name="w">The W component of the vector</param>
        public Vector(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        #region OPERATORS
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vector operator +(Vector a, float s)
        {
            return new Vector(a.X + s, a.Y + s, a.Z + s, a.W + s);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Vector operator -(Vector a, float s)
        {
            return new Vector(a.X - s, a.Y - s, a.Z - s, a.W - s);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        public static Vector operator *(Vector a, float s)
        {
            return new Vector(a.X * s, a.Y * s, a.Z * s, a.W * s);
        }
        #endregion

        #region STATIC METHODS
        /// <summary>
        /// Calculates the dot product between the given vectors
        /// </summary>
        /// <param name="a">The first vector operand</param>
        /// <param name="b">The second vector operand</param>
        /// <returns>The dot product between vectors A and B</returns>
        public static float Dot(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        /// <summary>
        /// Calculates the cross product between the given vectors
        /// </summary>
        /// <param name="a">The first vector operand</param>
        /// <param name="b">The second vector operand</param>
        /// <returns>The cross product between vectors A and B</returns>
        public static Vector Cross(Vector a, Vector b)
        {
            return new Vector(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        /// <summary>
        /// Calculates the distance between the given vectors
        /// </summary>
        /// <param name="a">The first vector operand</param>
        /// <param name="b">The second vector operand</param>
        /// <returns>The distance between vectors A and B</returns>
        public static float Distance(Vector a, Vector b)
        {
            return (a - b).Magnitude;
        }

        /// <summary>
        /// Calculates a unit vector representing the direction from vector A to vector B
        /// </summary>
        /// <param name="a">The first vector operand</param>
        /// <param name="b">The second vector operand</param>
        /// <returns>The direction between vectors A and B</returns>
        public static Vector Direction(Vector a, Vector b)
        {
            return (b - a).Normalized;
        }

        /// <summary>
        /// Creates a 2D unit vector from the given angle
        /// </summary>
        /// <param name="angleDegrees">The angle in degrees</param>
        /// <returns>Unit vector created from the given angle</returns>
        public static Vector FromAngle(float angleDegrees)
        {
            float angleRadians = MathUtils.DegreesToRadians(angleDegrees);
            return new Vector((float)Math.Cos(angleRadians), (float)Math.Sin(angleRadians));
        }
        #endregion

    }
}