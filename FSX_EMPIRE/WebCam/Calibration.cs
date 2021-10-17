using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebCam
{
    public class Calibration
    {
        public class BoundingBox
        {
            public int left;
            public int right;
            public int top;
            public int bottom;
        }
        #region declerations
        public bool EnableContinualCalibration = true;
        /// <summary>How far tracker must move in the x or y direction to be considered an actual move</summary>
        public float deadZone = 2;

        /// <summary>The x offset of the 2 markers while in a neutral head position</summary>
        float offsetx = 0;
        /// <summary>The y offset of the 2 markers while in a neutral head position</summary>
        float offsety = 0;

        /// <summary>This is the total distance between markers in the x direction. Value will be negative.</summary>
        float MaxLeft = 0;
        /// <summary>This is the total distance between markers in the x direction. Value will be positive</summary>
        float MaxRight = 0;
        /// <summary>This is the total distance between markers in the y direction. Value will be smaller than MaxDown.</summary>
        float MaxUp = 0;
        /// <summary>This is the total distance between markers in the y direction. Value will be larger than MaxUp</summary>
        float MaxDown = 0;

        /// <summary>This is the amount of pixels that make up the 'left' rotation. It takes into account the offset.</summary>
        float amountLeft;
        /// <summary>This is the amount of pixels that make up the 'right' rotation. It takes into account the offset.</summary>
        float amountRight;
        /// <summary>This is the amount of pixels that make up the 'up' rotation. It takes into account the offset.</summary>
        float amountUp;
        /// <summary>This is the amount of pixels that make up the 'down' rotation. It takes into account the offset.</summary>
        float amountDown;

        public BoundingBox boundingBox;
        Point lastTopMarker;
        Point lastBottomMarker;

        #endregion 

        public Calibration()
        {
            lastTopMarker = new Point(0, 0);
            lastBottomMarker = new Point(0, 0);
            boundingBox = new BoundingBox();
            boundingBox.left = 10;
            boundingBox.bottom = int.MaxValue;
            boundingBox.right = int.MaxValue;
            boundingBox.top = 10;
        }
        /// <summary>Sets the offsets while the head is in a neutral position</summary>
        public void SetCenter(Point topMarker, Point bottomMarker)
        {
            offsetx = topMarker.X - bottomMarker.X;
            offsety = topMarker.Y - bottomMarker.Y;
            Console.WriteLine("offsetX: " + offsetx + " offsetY: " + offsety);
        }

        public void SetMaxLeftRotation(Point topMarker, Point bottomMarker)
        {
            if (topMarker.X > boundingBox.left)
                boundingBox.left = topMarker.X;
            if (bottomMarker.X > boundingBox.left)
                boundingBox.left = bottomMarker.X;

            MaxLeft = topMarker.X - bottomMarker.X; //should return a negative number
            amountLeft = MaxLeft - offsetx;

            Console.WriteLine("MaxLeft: " + MaxLeft);
        }

        public void SetMaxRightRotation(Point topMarker, Point bottomMarker)
        {
            if (topMarker.X < boundingBox.right)
                boundingBox.right = topMarker.X;
            if (bottomMarker.X < boundingBox.right)
                boundingBox.right = bottomMarker.X;

            MaxRight = topMarker.X - bottomMarker.X; //should return a positive number
            amountRight = MaxRight - offsetx;
            Console.WriteLine("MaxRight: " + MaxRight);
        }

        public void SetMaxUpRotation(Point topMarker, Point bottomMarker)
        {
            if (topMarker.Y > boundingBox.top)
                boundingBox.top = topMarker.Y;
            if (bottomMarker.Y > boundingBox.top)
                boundingBox.top = bottomMarker.Y;

            MaxUp = topMarker.Y - bottomMarker.Y; //should return a positive number (but smaller than MaxDown)
            amountUp = Math.Abs(offsety) - MaxUp;
            Console.WriteLine("MaxUp: " + MaxUp);
        }

        public void SetMaxDownRotation(Point topMarker, Point bottomMarker)
        {
            if (topMarker.Y < boundingBox.bottom)
                boundingBox.bottom = topMarker.Y;
            if (bottomMarker.Y < boundingBox.bottom)
                boundingBox.bottom = bottomMarker.Y;

            MaxDown = topMarker.Y - bottomMarker.Y; //should return a positive number
            amountDown = MaxDown - Math.Abs(offsety);
            Console.WriteLine("MaxDown: " + MaxDown);
        }

        float lastX=0;
        float oldReturnX = 0;
        /// <returns>The percent of maximum rotation. (1 = 100%)</returns>
        public float GetRotationLeftRight(Point topMarker, Point bottomMarker)
        {
            if (Math.Abs(lastTopMarker.X - topMarker.X) > deadZone || Math.Abs(lastBottomMarker.X - bottomMarker.X) > deadZone)
            {
                lastTopMarker.X = topMarker.X;
                lastBottomMarker.X = bottomMarker.X;

                float currentDistanceX = (topMarker.X - bottomMarker.X);

                #region Continual Calibration
                if (EnableContinualCalibration)
                {
                    if (currentDistanceX < MaxLeft)
                    {
                        MaxLeft = currentDistanceX;
                        amountLeft = MaxLeft - offsetx;
                    }
                    else if (currentDistanceX > MaxRight)
                    {
                        MaxRight = currentDistanceX;
                        amountRight = MaxRight - offsetx;
                    }
                }
                #endregion

                currentDistanceX -= offsetx;
                if (currentDistanceX < 0)
                {
                    lastX = (currentDistanceX / amountLeft) * -1f;
                }
                else
                {
                    lastX = (currentDistanceX / amountRight);
                }
            }
            oldReturnX = Lerp(oldReturnX, lastX, LerpSpeed);
            return oldReturnX;
        }

        float lastY=0;
        float oldReturnY = 0;
        /// <returns>The percent of maximum rotation.(1 = 100%)</returns>
        public float GetRotationUpDown(Point topMarker, Point bottomMarker)
        {
            if (Math.Abs(lastTopMarker.Y - topMarker.Y) > deadZone || Math.Abs(lastBottomMarker.Y - bottomMarker.Y) > deadZone)
            {
                lastTopMarker.Y = topMarker.Y;
                lastBottomMarker.Y = bottomMarker.Y;

                float offset = Math.Abs(offsety);
                float currentDistanceY = (topMarker.Y - bottomMarker.Y);

                #region Continual Calibration
                if (EnableContinualCalibration)
                {
                    if (currentDistanceY < MaxUp)
                    {
                        MaxUp = currentDistanceY; //should return a positive number (but smaller than MaxDown)
                        amountUp = offset - MaxUp;
                    }
                    else if (currentDistanceY > MaxDown)
                    {
                        MaxDown = currentDistanceY; //should return a positive number
                        amountDown = MaxDown - offset;
                    }
                }
                #endregion

                currentDistanceY -= offset;
                if (currentDistanceY < 0)
                {
                    lastY= currentDistanceY / amountUp;
                }
                else
                {
                    lastY= currentDistanceY / amountDown;
                }
            }
            oldReturnY= Lerp(oldReturnY, lastY, LerpSpeed);
            return oldReturnY;
        }

        #region Lerp
        public float LerpSpeed = 0.2f;

        float Lerp(float startValue, float endValue, float speed)
        {
            if (speed < 0f)
                speed = 0f;
            else if (speed > 1f)
                speed = 1f;

            return startValue + (endValue - startValue) * speed;
        }
        #endregion
    }
}