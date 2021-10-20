using System.Drawing;

namespace WebCam
{
    public static class ColorExtensions
    {
        /// <summary>Distance from black to white is 195075</summary>
        /// <returns>Distance in color value from this.color to argument color</returns>
        public static int GetDistance(this Color c, Color color)
        {
            int redDifference;
            int greenDifference;
            int blueDifference;

            redDifference = c.R - color.R;
            greenDifference = c.G - color.G;
            blueDifference = c.B - color.B;

            return redDifference * redDifference + greenDifference * greenDifference + blueDifference * blueDifference;
        }

        /// <summary>Converts color to Gray Scale</summary>
        public static Color ToGray(this Color color)
        {
            byte b = (byte)(color.B * .21 + color.G * .71 + color.R * .071);
            return Color.FromArgb(color.R, color.G, color.B); 
        }
    }
}
