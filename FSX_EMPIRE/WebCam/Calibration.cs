using System;
using System.Drawing;
using System.IO;
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

        #region Declerations
        /// <summary>This is the outer bounds of the calibration area. Where the markers were found.</summary>
        public BoundingBox boundingBox;
        public int boundingBoxPadding = 100;

        /// <summary>Max degrees left or right the camera will rotate</summary>
        float xRotationMultiplier = 90;
        /// <summary>Max degrees up or down the camera will rotate</summary>
        float yRotationMultiplier = 60;

        /// <summary>If rotation goes beyond calibrated areas it will set the new max to the beyond value</summary>
        bool enableContinualCalibration = false;

        /// <summary>How far tracker must move in the x or y direction to be considered an actual move</summary>
        float deadZone = 2;

        /// <summary>The x offset of the 2 markers while in a neutral head position</summary>
        float offsetx = 0;
        /// <summary>The y offset of the 2 markers while in a neutral head position</summary>
        float offsety = 0;

        /// <summary>This is the total distance between markers in the x direction. Value will be negative.</summary>
        float maxLeft = 0;
        /// <summary>This is the total distance between markers in the x direction. Value will be positive</summary>
        float maxRight = 0;
        /// <summary>This is the total distance between markers in the y direction. Value will be smaller than MaxDown.</summary>
        float maxUp = 0;
        /// <summary>This is the total distance between markers in the y direction. Value will be larger than MaxUp</summary>
        float maxDown = 0;

        /// <summary>This is the amount of pixels that make up the 'left' rotation. It takes into account the offset.</summary>
        float amountLeft;
        /// <summary>This is the amount of pixels that make up the 'right' rotation. It takes into account the offset.</summary>
        float amountRight;
        /// <summary>This is the amount of pixels that make up the 'up' rotation. It takes into account the offset.</summary>
        float amountUp;
        /// <summary>This is the amount of pixels that make up the 'down' rotation. It takes into account the offset.</summary>
        float amountDown;
        /// <summary>holds half the width of the tracker marker size when calibrated</summary>
        public float scale;
        /// <summary>used to calculate if we are in zoom mode or not</summary>
        public float scaleFactor =0.95f;
        #endregion

        #region Read/Write Calibration.ini
        readonly string path = AppDomain.CurrentDomain.BaseDirectory + "Calibration.ini";

        /// <summary>Writes values to ini if the file does not exist</summary>
        public void WriteINI(bool force = false)
        {
            if (File.Exists(path)&&!force)  //don't overwrite existing file
                return;

            IniFile.WriteKey(path, "scale", scale.ToString(), "Tracker");
            IniFile.WriteKey(path, "scaleFactor", scaleFactor.ToString(), "Tracker");
            IniFile.WriteKey(path, "offsetx", offsetx.ToString(), "Tracker");
            IniFile.WriteKey(path, "offsety", offsety.ToString(), "Tracker");
            IniFile.WriteKey(path, "maxLeft", maxLeft.ToString(), "Tracker");
            IniFile.WriteKey(path, "amountLeft", amountLeft.ToString(), "Tracker");
            IniFile.WriteKey(path, "maxRight", maxRight.ToString(), "Tracker");
            IniFile.WriteKey(path, "amountRight", amountRight.ToString(), "Tracker");
            IniFile.WriteKey(path, "maxUp", maxUp.ToString(), "Tracker");
            IniFile.WriteKey(path, "amountUp", amountUp.ToString(), "Tracker");
            IniFile.WriteKey(path, "maxDown", maxDown.ToString(), "Tracker");
            IniFile.WriteKey(path, "amountDown", amountDown.ToString(), "Tracker");
            IniFile.WriteKey(path, "enableContinualCalibration", enableContinualCalibration.ToString(), "Tracker");
            IniFile.WriteKey(path, "boundingBoxPadding", boundingBoxPadding.ToString(), "Tracker");

            IniFile.WriteKey(path, "boundingBoxleft", boundingBox.left.ToString(), "Tracker");
            IniFile.WriteKey(path, "boundingBoxright", boundingBox.right.ToString(), "Tracker");
            IniFile.WriteKey(path, "boundingBoxtop", boundingBox.top.ToString(), "Tracker");
            IniFile.WriteKey(path, "boundingBoxbottom", boundingBox.bottom.ToString(), "Tracker");

            IniFile.WriteKey(path, "xRotationMultiplier", "60", "Tracker");
            IniFile.WriteKey(path, "yRotationMultiplier", "45", "Tracker");
            IniFile.WriteKey(path, "cameraLerpSpeed", cameraLerpSpeed.ToString(), "Tracker");
            IniFile.WriteKey(path, "deadZone", "2", "Tracker");
        }

        /// <summary>Reads values from the ini file</summary>
        public void ReadINI()
        {
            WriteINI(); // Make sure file exists
            _ = float.TryParse(IniFile.ReadKey(path, "scale", "Tracker"), out scale);
            _ = float.TryParse(IniFile.ReadKey(path, "scaleFactor", "Tracker"), out scaleFactor);
            _ = float.TryParse(IniFile.ReadKey(path, "offsetx", "Tracker"), out offsetx);
            _ = float.TryParse(IniFile.ReadKey(path, "offsety", "Tracker"), out offsety);
            _ = float.TryParse(IniFile.ReadKey(path, "maxLeft", "Tracker"), out maxLeft);
            _ = float.TryParse(IniFile.ReadKey(path, "amountLeft", "Tracker"), out amountLeft);
            _ = float.TryParse(IniFile.ReadKey(path, "maxRight", "Tracker"), out maxRight);
            _ = float.TryParse(IniFile.ReadKey(path, "amountRight", "Tracker"), out amountRight);
            _ = float.TryParse(IniFile.ReadKey(path, "maxUp", "Tracker"), out maxUp);
            _ = float.TryParse(IniFile.ReadKey(path, "amountUp", "Tracker"), out amountUp);
            _ = float.TryParse(IniFile.ReadKey(path, "maxDown", "Tracker"), out maxDown);
            _ = float.TryParse(IniFile.ReadKey(path, "amountDown", "Tracker"), out amountDown);
            _ = bool.TryParse(IniFile.ReadKey(path, "enableContinualCalibration", "Tracker"), out enableContinualCalibration);
             _ = int.TryParse(IniFile.ReadKey(path, "boundingBoxPadding", "Tracker"), out boundingBoxPadding);

             _ = int.TryParse(IniFile.ReadKey(path, "boundingBoxleft", "Tracker"), out boundingBox.left);
             _ = int.TryParse(IniFile.ReadKey(path, "boundingBoxright", "Tracker"), out boundingBox.right);
             _ = int.TryParse(IniFile.ReadKey(path, "boundingBoxtop", "Tracker"), out boundingBox.top);
             _ = int.TryParse(IniFile.ReadKey(path, "boundingBoxbottom", "Tracker"), out boundingBox.bottom);

             _ = float.TryParse(IniFile.ReadKey(path, "xRotationMultiplier", "Tracker"), out xRotationMultiplier);
             _ = float.TryParse(IniFile.ReadKey(path, "yRotationMultiplier", "Tracker"), out yRotationMultiplier);
             _ = float.TryParse(IniFile.ReadKey(path, "cameraLerpSpeed", "Tracker"), out cameraLerpSpeed);
             _ = float.TryParse(IniFile.ReadKey(path, "deadZone", "Tracker"), out deadZone);
        }
        #endregion 

        #region Constructor
        public Calibration()
        {
            lastTopMarker = new Point(0, 0);
            lastBottomMarker = new Point(0, 0);
            boundingBox = new BoundingBox
            {
                left = 10,
                bottom = int.MaxValue,
                right = int.MaxValue,
                top = 10
            };
        }
        #endregion 

        #region Calibrate

        public void DoCalibration(ref Tracker tracker)       
        {
            if (MessageBox.Show("Look straight ahead at your screen.\n Click 'OK' to capture orientation.", "Calibration Step 1", MessageBoxButtons.OK) == DialogResult.OK)
            {
                SetCenter(tracker.MarkerLocations[0], tracker.MarkerLocations[1]);
                scale = tracker.Scale;
            }
            if (MessageBox.Show("Look as far left as you normally would in game.\n Make sure trackers are still being read. \nClick 'OK' to capture orientation.", "Calibration Step 2", MessageBoxButtons.OK) == DialogResult.OK)
                SetMaxLeftRotation(tracker.MarkerLocations[0], tracker.MarkerLocations[1]);

            if (MessageBox.Show("Look as far right as you normally would in game.\n Make sure trackers are still being read. \nClick 'OK' to capture orientation.", "Calibration Step 3", MessageBoxButtons.OK) == DialogResult.OK)
                SetMaxRightRotation(tracker.MarkerLocations[0], tracker.MarkerLocations[1]);

            if (MessageBox.Show("Look as far up as you normally would in game.\n Make sure both trackers are still being read. \nClick 'OK' to capture orientation.", "Calibration Step 4", MessageBoxButtons.OK) == DialogResult.OK)
                SetMaxUpRotation(tracker.MarkerLocations[0], tracker.MarkerLocations[1]);

            if (MessageBox.Show("Look as far down as you normally would in game.\n Make sure both trackers are still being read. \nClick 'OK' to capture orientation.", "Calibration Step 5", MessageBoxButtons.OK) == DialogResult.OK)
                SetMaxDownRotation(tracker.MarkerLocations[0], tracker.MarkerLocations[1]);

            if (MessageBox.Show("Would you like to enable auto updating calibration?\n If you rotate more than the calibration max a new max will be set.", "Calibration Step 6", MessageBoxButtons.YesNo) == DialogResult.Yes)
                enableContinualCalibration = true;
            else
                enableContinualCalibration = false;

            WriteINI(true); 
        }

        /// <summary>Sets the offsets while the head is in a neutral position</summary>
        public void SetCenter(Point topMarker, Point bottomMarker)
        {
            offsetx = topMarker.X - bottomMarker.X;
            offsety = topMarker.Y - bottomMarker.Y;
            IniFile.WriteKey(path, "offsetx", offsetx.ToString(), "Tracker");
            IniFile.WriteKey(path, "offsety", offsety.ToString(), "Tracker");
            //Console.WriteLine("offsetX: " + offsetx + " offsetY: " + offsety);
        }

        public void SetMaxLeftRotation(Point topMarker, Point bottomMarker)
        {
            if (topMarker.X > boundingBox.left)
                boundingBox.left = topMarker.X;
            if (bottomMarker.X > boundingBox.left)
                boundingBox.left = bottomMarker.X;

            maxLeft = topMarker.X - bottomMarker.X; //should return a negative number
            amountLeft = maxLeft - offsetx;

            IniFile.WriteKey(path, "maxLeft", maxLeft.ToString(), "Tracker");
            IniFile.WriteKey(path, "amountLeft", amountLeft.ToString(), "Tracker");
            IniFile.WriteKey(path, "boundingBox.left", boundingBox.left.ToString(), "Tracker");

            //Console.WriteLine("MaxLeft: " + MaxLeft);
        }

        public void SetMaxRightRotation(Point topMarker, Point bottomMarker)
        {
            if (topMarker.X < boundingBox.right)
                boundingBox.right = topMarker.X;
            if (bottomMarker.X < boundingBox.right)
                boundingBox.right = bottomMarker.X;

            maxRight = topMarker.X - bottomMarker.X; //should return a positive number
            amountRight = maxRight - offsetx;

            IniFile.WriteKey(path, "maxRight", maxRight.ToString(), "Tracker");
            IniFile.WriteKey(path, "amountRight", amountRight.ToString(), "Tracker");
            IniFile.WriteKey(path, "boundingBox.right", boundingBox.right.ToString(), "Tracker");
            //Console.WriteLine("MaxRight: " + MaxRight);
        }

        public void SetMaxUpRotation(Point topMarker, Point bottomMarker)
        {
            if (topMarker.Y > boundingBox.top)
                boundingBox.top = topMarker.Y;
            if (bottomMarker.Y > boundingBox.top)
                boundingBox.top = bottomMarker.Y;

            maxUp = topMarker.Y - bottomMarker.Y; //should return a positive number (but smaller than MaxDown)
            amountUp = Math.Abs(offsety) - maxUp;

            IniFile.WriteKey(path, "maxUp", maxUp.ToString(), "Tracker");
            IniFile.WriteKey(path, "amountUp", amountUp.ToString(), "Tracker");
            IniFile.WriteKey(path, "boundingBox.top", boundingBox.top.ToString(), "Tracker");
            //Console.WriteLine("MaxUp: " + MaxUp);
        }

        public void SetMaxDownRotation(Point topMarker, Point bottomMarker)
        {
            if (topMarker.Y < boundingBox.bottom)
                boundingBox.bottom = topMarker.Y;
            if (bottomMarker.Y < boundingBox.bottom)
                boundingBox.bottom = bottomMarker.Y;

            maxDown = topMarker.Y - bottomMarker.Y; //should return a positive number
            amountDown = maxDown - Math.Abs(offsety);

            IniFile.WriteKey(path, "maxDown", maxDown.ToString(), "Tracker");
            IniFile.WriteKey(path, "amountDown", amountDown.ToString(), "Tracker");
            IniFile.WriteKey(path, "boundingBox.bottom", boundingBox.bottom.ToString(), "Tracker");

            //Console.WriteLine("MaxDown: " + MaxDown);
        }
        #endregion

        #region Get rotations based on marker positions
        float lastX = 0;
        float oldReturnX = 0;
        Point lastTopMarker;
        Point lastBottomMarker;
        /// <returns>The percent of maximum rotation. (1 = 100%) * xRotationMultiplier</returns>
        public float GetRotationLeftRight(Point topMarker, Point bottomMarker)
        {
            if (Math.Abs(lastTopMarker.X - topMarker.X) > deadZone || Math.Abs(lastBottomMarker.X - bottomMarker.X) > deadZone)
            {
                lastTopMarker.X = topMarker.X;
                lastBottomMarker.X = bottomMarker.X;

                float currentDistanceX = (topMarker.X - bottomMarker.X);

                #region Continual Calibration
                if (enableContinualCalibration)
                {
                    if (currentDistanceX < maxLeft)
                    {
                        maxLeft = currentDistanceX;
                        amountLeft = maxLeft - offsetx;
                    }
                    else if (currentDistanceX > maxRight)
                    {
                        maxRight = currentDistanceX;
                        amountRight = maxRight - offsetx;
                    }
                }
                #endregion

                currentDistanceX -= offsetx;
                if (currentDistanceX < 0)
                    lastX = (currentDistanceX / amountLeft) * -1f;
                else
                    lastX = (currentDistanceX / amountRight);
            }
            
            oldReturnX = Lerp(oldReturnX, lastX);
            return oldReturnX * xRotationMultiplier;
        }

        float lastY = 0;
        float oldReturnY = 0;
        /// <returns>The percent of maximum rotation.(1 = 100%) * yRotationMultiplier</returns>
        public float GetRotationUpDown(Point topMarker, Point bottomMarker)
        {
            if (Math.Abs(lastTopMarker.Y - topMarker.Y) > deadZone || Math.Abs(lastBottomMarker.Y - bottomMarker.Y) > deadZone)
            {
                lastTopMarker.Y = topMarker.Y;
                lastBottomMarker.Y = bottomMarker.Y;

                float offset = Math.Abs(offsety);
                float currentDistanceY = (topMarker.Y - bottomMarker.Y);

                #region Continual Calibration
                if (enableContinualCalibration)
                {
                    if (currentDistanceY < maxUp)
                    {
                        maxUp = currentDistanceY; //should return a positive number (but smaller than MaxDown)
                        amountUp = offset - maxUp;
                    }
                    else if (currentDistanceY > maxDown)
                    {
                        maxDown = currentDistanceY; //should return a positive number
                        amountDown = maxDown - offset;
                    }
                }
                #endregion

                currentDistanceY -= offset;
                if (currentDistanceY < 0)
                    lastY = currentDistanceY / amountUp;
                else
                    lastY = currentDistanceY / amountDown;
            }
            oldReturnY = Lerp(oldReturnY, lastY);
            return oldReturnY * yRotationMultiplier;
        }
        #endregion 

        #region Lerp
        /// <summary>Speed of camera transition from one position to another. (Range = 0 to 1)</summary>
        float cameraLerpSpeed = 0.1f;
        /// <summary>Lerps between 2 values. (Used to smooth camera movement.)</summary>
        float Lerp(float startValue, float endValue)
        {
            if (cameraLerpSpeed < 0f)
                cameraLerpSpeed = 0f;
            else if (cameraLerpSpeed > 1f)
                cameraLerpSpeed = 1f;

            return startValue + (endValue - startValue) * cameraLerpSpeed;
        }
        #endregion
    }
}