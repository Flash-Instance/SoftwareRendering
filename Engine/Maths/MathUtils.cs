using System;

namespace Engine
{
    /// <summary>
    /// Contains different mathematical utilities
    /// </summary>
    public static class MathUtils
    {

        /// <summary>
        /// Convert the given angle in degrees to radians
        /// </summary>
        /// <param name="degrees">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static float DegreesToRadians(float degrees)
        {
            return (float)(degrees * Math.PI / 180f);
        }

    }
}