using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WebCam
{
    public class ImageBuffer
    {
        #region Declerations
        /// <summary>Image data in bytes. Series of Blue, Green, Red values. Image[0] = bottom left.</summary>
        public byte[] image;

        /// <summary>width of the image in pixels</summary>
        readonly int width;

        /// <summary>height of the image in pixels</summary>
        readonly int height;

        /// <summary></summary>
        readonly int stride;
        #endregion

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="Width">Width in pixels</param>
        /// <param name="Height">Height in pixels</param>
        /// <param name="Stride">The number of bytes from one row of pixels in memory to the next row of pixels in memory</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ImageBuffer(int Width, int Height, int Stride = 0)
        {
            width = Width;
            height = Height;
            if (Stride == 0)
                stride = width * 3;
            else
                stride = Stride;
            image = new byte[width * 3 * height];
        }
        #endregion

        #region Convert to Bitmap
        /// <summary>Converts the image byte array to a Bitmap</summary>
        /// <returns>Bitmap image</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bitmap ConvertToBitmap()
        {
            IntPtr p = new IntPtr();
            Marshal.Copy(image, 0, p, image.Length);
            Bitmap b = new Bitmap(width, height, stride,
              PixelFormat.Format24bppRgb, p);
            b.RotateFlip(RotateFlipType.Rotate180FlipX);
            return b;
        }
        #endregion

        #region Copy image From/To pointer
        // Could use this instead of 'Marshal.Copy' but need to enable unsafe mode
        //[DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        //private static extern void CopyMemory(IntPtr Destination, IntPtr Source, [MarshalAs(UnmanagedType.U4)] uint Length);

        /// <summary>Copies the data from the pointer to the image byte array</summary>
        /// <param name="pointer">pointer in memory where the byte array will be copied FROM</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyImageFromPointer(IntPtr pointer)
        {
            Marshal.Copy(pointer, image, 0, image.Length);
        }

        /// <summary>Copies the image byte array to the pointer address in memory</summary>
        /// <param name="pointer">pointer in memory where the byte array will be copied TO</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyImageToPointer(IntPtr pointer)
        {
            Marshal.Copy(image, 0, pointer, image.Length);
        }
        #endregion

        #region Get/Set Pixel
        /// <summary>Gets the pixel color at the x,y location</summary>
        /// <param name="x">x position of pixel</param>
        /// <param name="y">y position of pixel</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetPixel(int x, int y)
        {
            x *= 3;
            y *= width * 3;
            return Color.FromArgb(
                image[x + 2 + y],     //Red
                image[x + 1 + y],     //Green
                image[x + y]);        //Blue
        }

        /// <summary>Sets the pixel at x, y to a color</summary>
        /// <param name="x">x coordinate of pixel</param>
        /// <param name="y">y coordinate of pixel</param>
        /// <param name="color">Color to change pixel to</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixel(int x, int y, Color color)
        {
            SetPixel(x, y, color.R, color.G, color.B);
        }

        /// <summary>Sets the pixel at x, y to a color</summary>
        /// <param name="x">x coordinate of pixel</param>
        /// <param name="y">y coordinate of pixel</param>
        /// <param name="R">Red value of pixel</param>
        /// <param name="G">Green value of pixel</param>
        /// <param name="B">Blue value of pixel</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixel(int x, int y, byte R, byte G, byte B)
        {
            x *= 3;
            y *= width * 3;
            int xy = x + y;
            image[xy] = B;
            image[xy + 1] = G;
            image[xy + 2] = R;
        }
        #endregion

        #region Draw Line/Circle
        /// <summary>Draws a line from beginning point to the end point</summary>
        /// <param name="begin">beginning point of the line in pixels</param>
        /// <param name="end">ending point of the line in pixels</param>
        /// <param name="color">color of the line</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawLine(Point begin, Point end, Color color)
        {
            // Does not work with reverse(negative) angles. Todo adgist point to draw from lowest to highest.
            Point nextPoint = begin;
            int deltax = end.X - begin.X;
            int deltay = end.Y - begin.Y;
            int error = deltax / 2;
            int ystep = 1;
            if (end.Y < begin.Y)
                ystep = -1;

            while (nextPoint.X <= end.X)
            {
                int point = (nextPoint.X * 3) + (nextPoint.Y * width * 3);
                image[point] = color.B;
                image[point + 1] = color.G;
                image[point + 2] = color.R;

                nextPoint.X++;

                error -= deltay;

                if (error < 0)
                {
                    nextPoint.Y += ystep;
                    error += deltax;
                }
            }
        }

        /// <summary>Draws a circle</summary>
        /// <param name="center">center position of circle in pixels</param>
        /// <param name="radius">radius of circle in pixels</param>
        /// <param name="color">color of circle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCircle(Point center, int radius, Color color)
        {
            int d = (5 - radius * 4) / 4;
            int x = 0;
            int y = radius;
            int xp, yp, xn;
            int xp3, yp3, xn3;
            do
            {
                xp = center.X + x;
                yp = center.Y + y;
                xn = center.X - x;
                int yn = center.Y - y;

                xp3 = xp * 3;
                yp3 = yp * width * 3;
                xn3 = xn * 3;
                int yn3 = yn * width * 3;

                int p;

                #region SetPixel(center.X + x, center.Y + y) and SetPixel(center.X + x, center.Y - y)
                if (0 <= xp && xp < width)
                {
                    if (0 <= yp && yp < height) //check if in image boundaries
                    {
                        // Set Pixels (top right quadrent)
                        p = xp3 + yp3;
                        image[p] = color.B;
                        image[p + 1] = color.G;
                        image[p + 2] = color.R;
                    }

                    if (0 <= yn && yn < height) //check if in image boundaries
                    {
                        // Set Pixels (bottom right quadrent)
                        p = xp3 + yn3;
                        image[p] = color.B;
                        image[p + 1] = color.G;
                        image[p + 2] = color.R;
                    }
                }
                #endregion

                #region SetPixel(center.X - x, center.Y + y) and SetPixel(center.X - x, center.Y - y)
                if (0 <= xn && xn < width)
                {
                    if (0 <= yp && yp < height) //check if in image boundaries
                    {
                        // SetPixel (top left quadrent)
                        p = xn3 + yp3;
                        image[p] = color.B;
                        image[p + 1] = color.G;
                        image[p + 2] = color.R;
                    }

                    if (0 <= yn && yn < height) //check if in image boundaries
                    {
                        // SetPixel (bottom left quadrent)
                        p = xn3 + yn3;
                        image[p] = color.B;
                        image[p + 1] = color.G;
                        image[p + 2] = color.R;
                    }
                }
                #endregion

                xp = center.X + y;
                yp = center.Y + x;
                xn = center.X - y;
                yn = center.Y - x;

                xp3 = xp * 3;
                yp3 = yp * width * 3;
                xn3 = xn * 3;
                yn3 = yn * width * 3;

                #region SetPixel(center.X + y, center.Y + x) and SetPixel(center.X + y, center.Y - x)
                if (0 <= xp && xp < width)
                {
                    if (0 <= yp && yp < height) //check if in image boundaries
                    {
                        // Set Pixels (top Right quadrent)
                        p = xp3 + yp3;
                        image[p] = color.B;
                        image[p + 1] = color.G;
                        image[p + 2] = color.R;
                    }

                    if (0 <= yn && yn < height) //check if in image boundaries
                    {
                        // Set Pixels (bottom right quadrent)
                        p = xp3 + yn3;
                        image[p] = color.B;
                        image[p + 1] = color.G;
                        image[p + 2] = color.R;
                    }
                }
                #endregion

                #region SetPixel(center.X - x, center.Y + y) and SetPixel(center.X - x, center.Y - y)
                if (0 <= xn && xn < width)
                {
                    if (0 <= yp && yp < height) //check if in image boundaries
                    {
                        // SetPixel (top left quadrent)
                        p = xn3 + yp3;
                        image[p] = color.B;
                        image[p + 1] = color.G;
                        image[p + 2] = color.R;
                    }

                    if (0 <= yn && yn < height) //check if in image boundaries
                    {
                        // SetPixel (bottom left quadrent)
                        p = xn3 + yn3;
                        image[p] = color.B;
                        image[p + 1] = color.G;
                        image[p + 2] = color.R;
                    }
                }
                #endregion

                if (d < 0)
                {
                    d += 2 * x + 1;
                }
                else
                {
                    d += 2 * (x - y) + 1;
                    y--;
                }
                x++;
            } while (x <= y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRect(Point TopLeft, Point BottomRight, Color color)
        {
            for (int x = BottomRight.X; x < TopLeft.X ; x++)
            {
                SetPixel(x, TopLeft.Y, color);
                SetPixel(x, BottomRight.Y, color);
            }
            for (int y = BottomRight.Y ; y < TopLeft.Y; y++)
            {
                SetPixel(BottomRight.X, y, color);
                SetPixel(TopLeft.X, y, color);
            }
        }
        #endregion

        #region Grey Scale
        /// <summary>Converts the image to Grey scale</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ToGreyScale()
        {
            for (int i = 0; i < image.Length; i += 3)
            {
                byte gray = (byte)(image[i + 2] * .21 + image[i + 1] * .71 + image[i] * .071);
                image[i] = image[i + 1] = image[i + 2] = gray;
            }
        }
        #endregion 

        #region Black and White
        /// <summary>Converts the image to Grey scale</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ToBlackAndWhite()
        {
            for (int i = 0; i < image.Length; i += 3)
            {
                byte gray = (byte)(image[i + 2] * .21 + image[i + 1] * .71 + image[i] * .071);
                if (gray < 128)
                    gray = 0;
                else
                    gray = 255;
                image[i] = image[i + 1] = image[i + 2] = gray;
            }
        }


        /// <summary>Converts the image to Grey scale. But any pixel that does not meet the Gray threshold is turned black</summary>
        /// <param name="threshhold">Pixels with greyscale values below this are turned black</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ToGreyScale(byte threshhold)
        {
            for (int i = 0; i < image.Length; i += 3)
            {
                byte grey = (byte)(image[i + 2] * .21 + image[i + 1] * .71 + image[i] * .071);

                if (grey > threshhold)
                    image[i] = image[i + 1] = image[i + 2] = grey;
                else
                    image[i] = image[i + 1] = image[i + 2] = 0;
            }
        }
        #endregion

        #region Flood Fill & Replace Color
        /// <summary>Will flood fill the targetColor with the fillColor</summary>
        /// <param name="startPoint">Point to start filling from</param>
        /// <param name="fillColor">Flood Fill Color</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FloodFill(Point pt, Color fillColor)
        {
            Color targetColor = GetPixel(pt.X, pt.Y);
            if (targetColor.ToArgb().Equals(fillColor.ToArgb()))
                return;

            Stack<Point> pixels = new Stack<Point>();

            pixels.Push(pt);               // put item on top of stack
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop(); // get item on top of the stack
                int y1 = temp.Y;

                while (y1 >= 0 && GetPixel(temp.X, y1) == targetColor)
                    y1--;

                y1++;
                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < height && GetPixel(temp.X, y1) == targetColor)
                {
                    SetPixel(temp.X, y1, fillColor);

                    if (!spanLeft && temp.X > 0 && GetPixel(temp.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && GetPixel(temp.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < width - 1 && GetPixel(temp.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < width - 1 && GetPixel(temp.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }
                    y1++;
                }
            }
        }

        /// <summary>Will flood fill any pixel that is within the distance threashold.\nDistance from black to white is 195075</summary>
        /// <param name="startPoint">Point to start filling from</param>
        /// <param name="targetColor">Target color that ill be replaced</param>
        /// <param name="distance">Distance a color can be from the targetColor and still be considered the targetColor</param>
        /// <param name="fillColor">Flood Fill Color</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FloodFillByDistance(Point startPoint, Color targetColor, int distance, Color fillColor)
        {
            Stack<Point> pixels = new Stack<Point>();

            pixels.Push(startPoint);               // put item on top of stack

            while (pixels.Count != 0)       // While stack not empty
            {
                Point temp = pixels.Pop(); // get item on top of the stack
                int y1 = temp.Y;

                while (y1 >= 0 && targetColor.GetDistance(GetPixel(temp.X, y1)) <= distance)
                    y1--;

                y1++;
                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < height && targetColor.GetDistance(GetPixel(temp.X, y1)) <= distance)
                {
                    SetPixel(temp.X, y1, fillColor);

                    if (!spanLeft && temp.X > 0 && targetColor.GetDistance(GetPixel(temp.X - 1, y1)) < distance)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && targetColor.GetDistance(GetPixel(temp.X - 1, y1)) > distance)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < width - 1 && targetColor.GetDistance(GetPixel(temp.X + 1, y1)) < distance)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < width - 1 && targetColor.GetDistance(GetPixel(temp.X + 1, y1)) > distance)
                    {
                        spanRight = false;
                    }
                    y1++;
                }
            }
        }

        /// <summary>Replaces every pixel of the targetColor with the replacementColor</summary>
        /// <param name="targetColor">Target color to be replaces</param>
        /// <param name="replacementColor">Color that will replace the targetColor</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReplaceColor(Color targetColor, Color replacementColor)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (targetColor==GetPixel(x, y))
                    {
                        SetPixel(x, y, replacementColor);
                    }
                }
            }
        }

        /// <summary>Replaces every color within the distance threashold of the targetColor with the replacementColor</summary>
        /// <param name="targetColor">Target color to be replaces</param>
        /// <param name="distance">How far off the color can be to still be considered the targetColor</param>
        /// <param name="replacementColor">Color that will replace the targetColor</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReplaceColorByDistance(Color targetColor, int distance, Color replacementColor)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (targetColor.GetDistance(GetPixel(x, y)) < distance)
                    {
                        SetPixel(x, y, replacementColor);
                    }
                }
            }
        }
        #endregion

        #region Convolution (Todo: Super slow.... Not optomized at all)
        /// <summary>Will apply a convolution filter to the image</summary>
        /// <param name="matrix">Matrix filter that will be applied to the image</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyFilter(Filters.Matrix matrix)
        {
            double[,] filter = Filters.Get(matrix); 
            // Filters should be square. So square root tells size. width = height
            int size = (int)Math.Sqrt(filter.Length);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Convolve(x, y, filter, size);
                }
            }
        }

        /// <summary>Applies multilpe convolution filters to the original image. Effects are added together. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyFilters(Filters.Matrix[] matrix)
        {
            int size;
            double[,] filter;
            byte[][] b = new byte[matrix.Length][];

            for (int i = 0; i < matrix.Length; i++)
                b[i] = new byte[width * height * 3];

            for (int i = 0; i < matrix.Length; i++)
            {
                filter = Filters.Get(matrix[i]);
                size = (int)Math.Sqrt(filter.Length);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Convolve(x, y, filter, size);
                    }
                }
                b[i] = image.ToArray<byte>();
            }

            int t = 0;
            for (int i = 0; i < image.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    t += b[j][i];
                }
                image[i] = (byte)(t / matrix.Length);
                t = 0;
            }
        }

        /// <summary>Does the convolution process to a pixel</summary>
        /// <param name="px">x position of pixel</param>
        /// <param name="py">y positin of pixel</param>
        /// <param name="filter">convolution filter to apply</param>
        /// <param name="filterSize">size of the filter width/height</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Convolve(int px, int py, double[,] filter, int filterSize)
        {
            double R = 0.0;
            double G = 0.0;
            double B = 0.0;
            double div = 0.0;
            for (int y = 0; y < filterSize; y++) //row
                for (int x = 0; x < filterSize; x++) //col
                {
                    if ((px + x) < width && (py + y) < height)
                    {
                        R += filter[filterSize - 1 - x, filterSize - 1 - y] * GetPixel(px + x, py + y).R;
                        G += filter[filterSize - 1 - x, filterSize - 1 - y] * GetPixel(px + x, py + y).G;
                        B += filter[filterSize - 1 - x, filterSize - 1 - y] * GetPixel(px + x, py + y).B;
                        div += filter[filterSize - 1 - x, filterSize - 1 - y];
                    }
                }
            if (R < 0) R = 0;
            if (G < 0) G = 0;
            if (B < 0) B = 0;
            if (R > 255) R = 255;
            if (G > 255) G = 255;
            if (B > 255) B = 255;
            div = Math.Abs(div);
            if (div != 0)
                SetPixel(px, py, Convert.ToByte(R / div), Convert.ToByte(G / div), Convert.ToByte(B / div));
            else
                SetPixel(px, py, Convert.ToByte(R), Convert.ToByte(G), Convert.ToByte(B));
        }
        #endregion
    }
}
