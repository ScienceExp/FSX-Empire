using System;

namespace ExtensionMethods
{
    /// <summary>Extend variable functions to make code neater</summary>
    public static class MyExtensions
    {
        #region double
        /// <summary>[Degree] value = value * π / 180</summary>
        public static double ToRad(this double value)
        {
            return value * (Math.PI / 180.0);
        }

        /// <summary>[Radian] value = value * 180 / π)</summary>
        public static double ToDeg(this double value)
        {
            return value * (180.0 / Math.PI);
        }

        /// <summary>[Degree] value = between 0 and 360</summary>
        public static double Normalize360(this double value)
        {
            while (value < 0.0) value += 360.0;
            while (value > 360.0) value -= 360.0;
            return value;
        }

        /// <summary>[Degree] value = between -180 and 180</summary>
        public static double Normalize180(this double value)
        {
            while (value < -180.0) value += 360.0;
            while (value > 180.0) value -= 360.0;
            return value;
        }

        /// <summary>Clamp value to a value between min and max</summary>
        public static double Clamp(this double value, double min, double max)
        {
            if (value < min) value = min;
            else if (value > max) value = max;

            return value;
        }
        #endregion

        #region float
        /// <summary>[Degree] value = value * π / 180</summary>
        public static float ToRad(this float value)
        {
            return (float)(value * (Math.PI / 180.0));
        }

        /// <summary>[Radian] value = value * 180 / π)</summary>
        public static float ToDeg(this float value)
        {
            return (float)(value * (180.0 / Math.PI));
        }

        /// <summary>[Degree] value = between 0 and 360</summary>
        public static float Normalize360(this float value)
        {
            while (value < 0.0f) value += 360.0f;
            while (value > 360.0f) value -= 360.0f;
            return value;
        }

        /// <summary>[Degree] value = between -180 and 180</summary>
        public static float Normalize180(this float value)
        {
            while (value < -180.0f) value += 360.0f;
            while (value > 180.0f) value -= 360.0f;
            return value;
        }

        public static float ToFloat(this double value)
        {
            return (float)value;
        }
        #endregion
    }
}
