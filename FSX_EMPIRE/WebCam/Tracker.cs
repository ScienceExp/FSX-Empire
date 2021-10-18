using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WebCam
{
    public class Tracker
    {
        public class BoundingBox
        {
            public int left;
            public int right;
            public int top;
            public int bottom;
        }
        public BoundingBox boundingBox;
        #region Declerations
        /// <summary>Class to hold tracker matches</summary>
        public class Match
        {
            public Point point;
            public float score;

            public Match(int x, int y, float Score)
            {
                point = new Point(x, y);
                score = Score;
            }
        }

        /// <summary>1 = white, 0 = black</summary>
        public float[][] pixelIntensity;

        /// <summary>Each element holds the sum of all elements left and above</summary>
        readonly float[][] integral;

        /// <summary>Width of pixel Intensity Image</summary>
        readonly int width;

        /// <summary>Height of pixel Intensity Image</summary>
        readonly int height;

        const float white = 255f/256f;          // 1 = white (full white or black was causing errors)
        const float black = 0.000001f/256f;     // 0 = black

        /// <summary>This is the final marker locations once all processing is done</summary>
        readonly Point[] Markers;
        /// <summary>Size of the marker that is being generated and searched for</summary>
        public int markerSize;
        /// <summary>How close the sum of pixels needs to match the tracker to be considered a possablitly</summary>
        public float minimumPositive = 0.3f;
        /// <summary>Holds all the locations that give a value above minimumPositive</summary>
        public List<Match> PossibleMarkers;

        /// <summary>Gives the center of the marker locations</summary>
        public Point[] MarkerLocations
        {
            get
            {
                int offset = markerSize/2;
                Point[] p = new Point[Markers.Length];
                for (int i = 0; i < Markers.Length; i++)
                {
                    p[i].X = Markers[i].X-offset;
                    p[i].Y = Markers[i].Y-offset;
                }
                return p;
            }
        }
        #endregion

        #region Constructor
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tracker(int Width, int Height,int MarkerSize)
        {
            boundingBox = new BoundingBox();
            width = Width;
            height = Height;
            markerSize = MarkerSize;

            pixelIntensity = new float[width][];
            for (int i = 0; i < width; i++)
                pixelIntensity[i] = new float[height];

            integral = new float[width][];
            for (int i = 0; i < width; i++)
                integral[i] = new float[height];

            Markers = new Point[2]; 
        }
        #endregion

        #region Public function called to start finding the Markers
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Find(ImageBuffer buffer)
        {
            PossibleMarkers = new List<Match>();

            if (boundingBox.top > boundingBox.bottom)
                CroppedImageBufferToIntensity(buffer);
            else
                ImageBufferToIntensity(buffer);
        }
        #endregion

        #region Convert from to ImageBuffer
        /// <summary>Gets the pixel's intenstity value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ImageBufferToIntensity(ImageBuffer buffer)
        {
            int xy;
            int y1;
            int x1;
            for (int y = 0; y < height; y++)
            {
                y1 = y * width * 3;
                for (int x = 0; x < width; x++)
                {
                    x1 = x * 3;
                    xy = x1 + y1;
                    float gray = (float)(buffer.image[xy + 2] * .21 + buffer.image[xy + 1] * .71 + buffer.image[xy] * .071);
                    //pixelIntensity[x][y] = (float)((255f - gray) / 256f); //this way inverts color
                    pixelIntensity[x][y] = (float)((gray) / 256f);
                }
            }
            CreateIntegralArray();
        }

        /// <summary>Gets the pixel's intenstity value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CroppedImageBufferToIntensity(ImageBuffer buffer)
        {
            int xy;
            int y1;
            int x1;
            //for (int y = 0; y < height; y++)
            for (int y = boundingBox.bottom; y < boundingBox.top; y++)
            {
                y1 = y * width * 3;
                //for (int x = 0; x < width; x++)
                for (int x = boundingBox.right; x < boundingBox.left; x++)
                {
                    x1 = x * 3;
                    xy = x1 + y1;
                    float gray = (float)(buffer.image[xy + 2] * .21 + buffer.image[xy + 1] * .71 + buffer.image[xy] * .071);
                    //pixelIntensity[x][y] = (float)((255f - gray) / 256f); //this way inverts color
                    pixelIntensity[x][y] = (float)((gray) / 256f);
                }
            }
            CroppedCreateIntegralArray();
        }

        /// <summary>Converts IntensityImage To ImageBuffer</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IntensityToImageBuffer(ref ImageBuffer buffer)
        {
            int xy;
            int y1;
            int x1;
            for (int y = 0; y < height; y++)
            {
                y1 = y * width * 3;

                for (int x = 0; x < width; x++)
                {
                    x1 = x * 3;
                    xy = x1 + y1;
                    buffer.image[xy] = buffer.image[xy + 1] = buffer.image[xy + 2] = (byte)(pixelIntensity[x][y] * 256);
                }
            }
        }
        #endregion

        #region Create Integral Array
        /// <summary>Creates an array where each point in the array holds the sum of all values left and up in the array. Including itself.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CreateIntegralArray()
        {
            float sum = 0;
            for (int x = 0; x < width; x++) // sum for 1st row
            {
                sum += pixelIntensity[x][0];
                integral[x][0] = sum;
            }

            for (int y = 1; y < height; y++)
            {
                sum = 0;                    // Reset sum for each row
                for (int x = 0; x < width; x++)
                {
                    sum += pixelIntensity[x][y];
                    integral[x][y] = integral[x][y - 1] + sum;
                }
            }
            FindCheckerMarker();
        }

        /// <summary>Creates an array where each point in the array holds the sum of all values left and up in the array. Including itself.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CroppedCreateIntegralArray()
        {
            float sum = 0;
            //for (int x = 0; x < width; x++) // sum for 1st row
            for (int x = boundingBox.right; x < boundingBox.left; x++) // sum for 1st row
            {
                sum += pixelIntensity[x][0];
                integral[x][0] = sum;
            }

            //for (int y = 1; y < height; y++)
            for (int y = boundingBox.bottom + 1; y < boundingBox.top; y++)
            {
                sum = 0;                    // Reset sum for each row
                                            //for (int x = 0; x < width; x++)
                for (int x = boundingBox.right; x < boundingBox.left; x++)
                {
                    sum += pixelIntensity[x][y];
                    integral[x][y] = integral[x][y - 1] + sum;
                }
            }
            CroppedFindCheckerMarker();
        }
        #endregion

        #region Find all possible Marker matches
        /// <summary>Search the image for possible marker locations</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void FindCheckerMarker()
        {
            int hX = markerSize / 2;
            int hY = markerSize / 2;
            float div = hX * hY * 2;

            for (int y = markerSize; y < height; y++)
            {
                for (int x = markerSize; x < width; x++)
                {
                    float sumW = GetSum(x, y, hX, hY);
                    sumW += GetSum(x - hX, y - hY, hX, hY); //left bottom
                    sumW /= div;

                    float sumB = GetSum(x - hX, y, hX, hY);
                    sumB += GetSum(x, y - hY, hX, hY);      //right bottom
                    sumB /= div;

                    float sumTotal = sumW - sumB;

                    //Keep track of all the places that are possible tracker locations
                    if (sumTotal > minimumPositive)
                    {
                        PossibleMarkers.Add(new Match(x, y, sumTotal));
                    }
                }
            }
            ProcessPossibleMarkers();
        }

        /// <summary>Search the image for possible marker locations</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CroppedFindCheckerMarker()
        {
            int hX = markerSize / 2;
            int hY = markerSize / 2;
            float div = hX * hY * 2;

            for (int y = boundingBox.bottom + markerSize; y < boundingBox.top; y++)
            {
                for (int x =boundingBox.right + markerSize; x < boundingBox.left ; x++)
                {
                    float sumW = GetSum(x, y, hX, hY);
                    sumW += GetSum(x - hX, y - hY, hX, hY); //left bottom
                    sumW /= div;

                    float sumB = GetSum(x - hX, y, hX, hY);
                    sumB += GetSum(x, y - hY, hX, hY);      //right bottom
                    sumB /= div;

                    float sumTotal = sumW - sumB;

                    //Keep track of all the places that are possible tracker locations
                    if (sumTotal > minimumPositive)
                    {
                        PossibleMarkers.Add(new Match(x, y, sumTotal));
                    }
                }
            }
            ProcessPossibleMarkers();
        }

        /// <summary>Gets the sum of all the pixels in the bounding box</summary>
        /// <returns>Sum</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float GetSum(int x, int y, int width, int height)
        {
            return integral[x][y] - integral[x][y - height] - integral[x - width][y] + integral[x - width][y - height];
        }
        #endregion

        #region Get top 2 Marker matches and put top in Marker[0] and bottom in Marker[1]
        /// <summary>Find the best 2 Markers. Put the top parker in Marker[0] and the bottom in Marker[1]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ProcessPossibleMarkers()
        {
            int distance = markerSize/2; 
            int Marker1 = -1;
            int Marker2 = -1;
            if (PossibleMarkers.Count > 0)
            {
                List<Point> p = new List<Point>();
                List<float> total = new List<float>();
                List<int> count = new List<int>();
                p.Add(PossibleMarkers[0].point);
                total.Add(PossibleMarkers[0].score);
                count.Add(1);
                for (int i = 1; i < PossibleMarkers.Count; i++)
                {
                    bool bMatch = false;
                    for (int j = 0; j < p.Count; j++)
                    {
                        if (Math.Abs(p[j].X - PossibleMarkers[i].point.X) < distance)
                        {
                            if (Math.Abs(p[j].Y - PossibleMarkers[i].point.Y) < distance)
                            {
                                bMatch = true;
                                total[j] += PossibleMarkers[i].score;

                                //Get the best x,y position
                                if (PossibleMarkers[i].score>(total[j]/count[j]))
                                    p[j] = new Point(PossibleMarkers[i].point.X, PossibleMarkers[i].point.Y);

                                count[j] += 1;
                                break;

                            }
                        }
                    }
                    if (!bMatch)
                    {
                        p.Add(PossibleMarkers[i].point);
                        total.Add(PossibleMarkers[i].score);
                        count.Add(1);
                    }
                }

                //Holds the highest match value
                float max = -1;
                //Holds the 2nd highest match value
                float secondMax = -1;

                for (int j = 0; j < p.Count; j++)
                {
                    if (total[j] > max)
                    {
                        secondMax = max;
                        Marker2 = Marker1;

                        max = total[j];
                        Marker1 = j;
                    }
                    else if (total[j] > secondMax && total[j] < max)
                    {
                        secondMax = total[j];
                        Marker2 = j;
                    }
                }

                if (Marker1 != -1)
                {
                    if (Marker2 != -1)
                    {
                        if (p[Marker1].Y > p[Marker2].Y)
                        {
                            Markers[0] = p[Marker1];
                            Markers[1] = p[Marker2];
                        }
                        else
                        {
                            Markers[1] = p[Marker1];
                            Markers[0] = p[Marker2];
                        }
                    }
                    else //only one marker detected
                    {
                        var dist1 = Math.Sqrt((Math.Pow(Markers[0].X  - p[Marker1].X, 2) + Math.Pow(Markers[0].Y - p[Marker1].Y, 2)));
                        var dist2 = Math.Sqrt((Math.Pow(Markers[1].X - p[Marker1].X, 2) + Math.Pow(Markers[1].Y - p[Marker1].Y, 2)));

                        if (Math.Abs(dist1 - dist2) > distance) // Mostly to fix tracker going out of screen
                        {
                            if (dist1 < dist2)
                                Markers[0] = p[Marker1];
                            else
                                Markers[1] = p[Marker1];
                        }
                    }
                }
            }
        }
        #endregion

        #region Draw the marker being searched for (for debug)
        /// <summary>Draws the Marker being looked for in the bottom left corner. For debug purposes</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawMarker()
        {
            int hX = markerSize / 2;
            int hY = markerSize / 2;
            int xoffset = 1;
            int yoffset = 1;

            //bottom left
            for (int y = 0; y < hY; y++)
                for (int x = 0; x < hX; x++)
                    pixelIntensity[xoffset + x][yoffset + y] = white;

            //top left
            for (int y = hY; y < markerSize; y++)
                for (int x = 0; x < hX; x++)
                    pixelIntensity[xoffset + x][yoffset + y] = black;

            //bottom right
            for (int y = 0; y < hY; y++)
                for (int x = hX; x < markerSize; x++)
                    pixelIntensity[xoffset + x][yoffset + y] = black;

            //top Right
            for (int y = hY; y < markerSize; y++)
                for (int x = hX; x < markerSize; x++)
                    pixelIntensity[xoffset + x][yoffset + y] = white;
        }
        #endregion
    }
}
