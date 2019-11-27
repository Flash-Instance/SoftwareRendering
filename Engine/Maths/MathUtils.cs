using System;

namespace Engine
{
    /// <summary>
    /// Contains different mathematical utilities
    /// </summary>
    public static class MathUtils
    {

        /// <summary>
        /// Represents the different axes in the coordinate system
        /// </summary>
        public enum Axis
        {
            /// <summary>
            /// Positive X axis
            /// </summary>
            X = 1,

            /// <summary>
            /// Positive Y axis
            /// </summary>
            Y = 2,

            /// <summary>
            /// Positive Z axis
            /// </summary>
            Z = 3,

            /// <summary>
            /// Positive X axis
            /// </summary>
            Right = 1,

            /// <summary>
            /// Positive Y axis
            /// </summary>
            Up = 2,

            /// <summary>
            /// Positive Z axis
            /// </summary>
            Forward = 3
        }

        /// <summary>
        /// Convert the given angle in degrees to radians
        /// </summary>
        /// <param name="degrees">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static float DegreesToRadians(float degrees)
        {
            return (float)(degrees * Math.PI / 180f);
        }

        public static int Clamp(int min, int max, int value)
        {
            return (int)Math.Min(max, Math.Max(min, value));
        }

        public static float Min(params float[] values)
        {
            float min = float.MaxValue;
            for(int i = 0;i < values.Length;i++)
            {
                min = Math.Min(min, values[i]);
            }

            return min;
        }

        public static float Max(params float[] values)
        {
            float max = float.MinValue;
            for(int i = 0;i < values.Length;i++)
            {
                max = Math.Max(max, values[i]);
            }

            return max;
        }

    }
}