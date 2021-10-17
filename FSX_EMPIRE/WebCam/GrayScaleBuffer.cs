using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebCam
{
    /// <summary>This does not hold the R,G,B values. It has a single byte value for each pixel</summary>
    class GrayScaleBuffer
    {
        #region Declerations
        /// <summary>Image data in bytes. Single byte value for pixel color. Image[0] = bottom left.</summary>
        public byte[][] image;
        /// <summary>width of the image in pixels</summary>
        int width;
        /// <summary>height of the image in pixels</summary>
        int height;
        #endregion

        #region Constructor
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrayScaleBuffer(int Width, int Height)
        {
            width = Width;
            height = Height;

            image = new byte[width][];
            for (int w = 0; w < width; w++)
                image[w] = new byte[height]; 
        }
        #endregion

        /// <summary>Creates a Grey Scale Image from a RGB Image</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CreateFromImageBuffer(ImageBuffer buffer)
        {
            int x = 0;
            int y = 0;
            int xy = 0;

            for (int j = 0; j < height - 1; j++)
            {
                y = j * width * 3;

                for (int i = 0; i < width - 1; i++)
                {
                    x = i * 3;
                    xy = x + y;
                    byte gray = (byte)(
                        buffer.image[xy + 2] * .21 +
                        buffer.image[xy + 1] * .71 +
                        buffer.image[xy] * .071);
                    this.image[i][j] = gray;
                }
            }
        }

        /// <summary>Subtracts the argument image from the internal image</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Subtract(GrayScaleBuffer buffer)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    image[x][y] = (byte)(image[x][y] - buffer.image[x][y]);
                }
            }
        }
    }
}
