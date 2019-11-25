using System;

namespace Engine
{
    public static class MathUtils
    {

        public static float DegreesToRadians(float degrees)
        {
            return (float)(degrees * Math.PI / 180f);
        }

    }
}