using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WebCam
{
    class Procesor
    {
        #region Declerations
        /// <summary>Holds pointer to the original data. Used to copy data back to memory.</summary>
        IntPtr pMem;
        /// <summary>Image width in pixels</summary>
        int width;
        /// <summary>Image height in pixels</summary>
        int height;
        /// <summary>This is the one to put original data into</summary>
        int last;
        /// <summary>This is the one to put changed data into</summary>
        int current;
        /// <summary>This value keeps track of if data had been put into array for processing</summary>
        bool ready = false;
        /// <summary>Make true to see intensity pixel representation and the tracker marker being searched for</summary>
        public bool ShowDebug = false;
        /// <summary>Byte array that the original image is stored in</summary>
        ImageBuffer[] buffer;
        /// <summary>Finds and keeps track of the marker locations</summary>
        public Tracker tracker;
        #endregion

        /// <summary>Initializes image with a width and height in pixels</summary>
        /// <param name="imageWidth">width of image</param>
        /// <param name="imageHeight">height of image</param>
        /// <param name="markerSize">size (width or height) of the tracker marker</param>
        public Procesor(int imageWidth, int imageHeight, int markerSize)
        {
            width = imageWidth;
            height = imageHeight;

            // Don't really need 2 buffers for tracking, but added just in case I want to compare frames at some later point
            buffer = new ImageBuffer[2];
            buffer[0] = new ImageBuffer(width, height);
            buffer[1] = new ImageBuffer(width, height);

            tracker = new Tracker(width, height, markerSize);

            last = 0;
            current = buffer.Length - 1;
        }

        /// <summary>Copies pointer data into byte array 'raw'</summary>
        /// <param name="pBuffer">pointer</param>
        /// <param name="BufferLen">length of data at pointer location in memory</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StartDraw(IntPtr pBuffer)
        {
            pMem = pBuffer;
            buffer[current].CopyImageFromPointer(pMem);

            tracker.Find(buffer[current]);

            if (ShowDebug)
                tracker.DrawMarker();

            ready = true;
        }

        /// <summary>Copies data back into the pointer memory</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EndDraw()
        {
            if (!ready)
                MessageBox.Show("EndDraw() called before StartDraw()");

            int half = tracker.markerSize / 2;
            #region Draw Debug
            if (ShowDebug)
            {
                // Copies the intensity buffer back into the image buffer
                tracker.IntensityToImageBuffer(ref buffer[current]);

                // Draws every point that could be a possible marker
                foreach (Tracker.match p in tracker.PossibleMarkers)
                    buffer[current].SetPixel(p.point.X - half, p.point.Y - half, Color.Red);
            }
            #endregion

            #region Draw markers on screen 
            if (tracker.MarkerLocations[0].X > 0 && tracker.MarkerLocations[0].Y > 0)
            {
                buffer[current].DrawCircle(tracker.MarkerLocations[0], half, Color.Red);
                buffer[current].SetPixel(tracker.MarkerLocations[0].X, tracker.MarkerLocations[0].Y, Color.Red);
            }
            if (tracker.MarkerLocations[1].X > 0 && tracker.MarkerLocations[1].Y > 0)
            {
                buffer[current].DrawCircle(tracker.MarkerLocations[1], half, Color.Aqua);
                buffer[current].SetPixel(tracker.MarkerLocations[1].X, tracker.MarkerLocations[1].Y, Color.Aqua);
            }
            if (tracker.boundingBox != null)
            {
                if (tracker.boundingBox.left > tracker.boundingBox.right)
                {
                    buffer[current].DrawRect(new Point(tracker.boundingBox.left, tracker.boundingBox.top),
                        new Point(tracker.boundingBox.right,
                        tracker.boundingBox.bottom), Color.Red);
                }
            }

            buffer[current].DrawLine(new Point(0, 0), new Point(100, 200),Color.Green); 
            buffer[current].CopyImageToPointer(pMem);
            #endregion

            #region Swap Buffers
            current = last;
            last++;

            if (last > buffer.Length - 1)
                last = 0;
            #endregion

            ready = false;
        }
    }
}
