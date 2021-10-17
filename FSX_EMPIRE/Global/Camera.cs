using ExtensionMethods;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;

namespace FSX_EMPIRE
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class Camera        // Todo, would like to make this head tracking.
    {
        #region variables
        public double DeltaX;
        public double DeltaY;
        public double DeltaZ;
        /// <summary> Range of allowable values is +90 to -90 degrees. </summary>
        public double PitchDeg;
        /// <summary> Range of allowable values is +180 to -180 degrees. </summary>
        public double BankDeg;
        /// <summary> Range of allowable values is +180 to -180 degrees. </summary>
        public double HeadingDeg;

        /// <summary> Amount to adjust camera heading and pitch by</summary>
        public double inc = 5.0;

        public string KeyLeft = "VK_COMMA";
        public string KeyRight = "VK_PERIOD";
        public string KeyUp = "M";
        public string KeyDown = "N";
        #endregion

        #region adjust camera heading, pitch
        /// <summary>Pitches camera up by incº</summary>
        public void PitchUp()
        {
            PitchDeg = (PitchDeg - inc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                PitchDeg.ToFloat(),
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD);
        }

        /// <summary>Pitches camera down by incº</summary>
        public void PitchDown()
        {
            PitchDeg = (PitchDeg + inc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                PitchDeg.ToFloat(),
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD);
        }

        /// <summary>Rotates camera right by incº</summary>
        public void RotateRight()
        {
            HeadingDeg = (HeadingDeg + inc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                HeadingDeg.ToFloat());
        }

        /// <summary>Rotates camera left by incº</summary>
        public void RotateLeft()
        {
            HeadingDeg = (HeadingDeg - inc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                HeadingDeg.ToFloat());
        }
        #endregion

        #region adjust camera heading, pitch based on degree
        /// <summary>Pitches camera up by incº (-)</summary>
        public void PitchUp(float degree)
        {
            PitchDeg = (PitchDeg - inc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                degree,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD);
            
            PitchDeg = degree;
        }

        /// <summary>Pitches camera down by incº (+)</summary>
        public void PitchDown(float degree)
        {
            PitchDeg = (PitchDeg + inc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                degree,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD);

            PitchDeg = degree;
        }

        /// <summary>Rotates camera right by incº (+)</summary>
        public void RotateRight(float degree)
        {
            HeadingDeg = (HeadingDeg + inc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                degree);

            HeadingDeg = degree;
        }

        /// <summary>Rotates camera left by incº (-) </summary>
        public void RotateLeft(float degree)
        {
            HeadingDeg = (HeadingDeg - inc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                degree);

            HeadingDeg = degree;
        }

        /// <summary>Sets camera heading and pitch at the same time</summary>
        public void SetPitchAndRotation(float headingDeg, float pitchDeg)
        {
            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                pitchDeg,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                headingDeg);

            PitchDeg = pitchDeg;
            HeadingDeg = headingDeg;
        }

        #endregion

        #region calculate combined plane camera rotation
        /// <summary>Adds the plane and camera rotations together so result can be used in google earth</summary>
        public Attitude CalculteRotations(SGoogleEarth fsx)
        {
            //Order of Transformations in FSX
            //<altitude>    - translate along the Z axis to<altitude>

            //<heading>     - rotate around the Z axis.
            //<tilt>        - rotate around the X axis.
            //<roll>        - rotate around the Z axis (again).

            Attitude attitude = new Attitude();
            double cos = Math.Cos(HeadingDeg.Normalize360().ToRad());
            double cos2 = cos * cos;    // square
            if (cos < 0) cos2 *= -1;    // preserve sine
            double sin = Math.Sin(HeadingDeg.Normalize360().ToRad());
            double sin2 = sin * sin;    // spuare 
            if (sin < 0) sin2 *= -1;    // preserve sine

            // Heading rotated first in FSX so no need for trig. Just add Camera and Plane values
            attitude.Heading = (fsx.PLANE_HEADING_DEGREES_TRUE + G.camera.HeadingDeg).Normalize360();

            // Now need to apply plane pitch partly to pitch and partly to bank
            attitude.Pitch =                            // Google rotates in opposite direction so need to add '-'
                -(PitchDeg +                            // Add camera pitch 
                fsx.PLANE_PITCH_DEGREES * cos2 +        // Add plane pitch component
                -fsx.PLANE_BANK_DEGREES * sin2) +       // Add plane bank component
                90.0;                                   // Add 90º because google earth 0º faces down

            attitude.Pitch = attitude.Pitch.Clamp(0, 180); // Google Earth only pitches between 0 an 180

            // Now need to apply plane bank partly to pitch and partly to bank
            attitude.Bank = (
                fsx.PLANE_PITCH_DEGREES * sin2 +        // Pitch
                fsx.PLANE_BANK_DEGREES * cos2           // Bank
            ).Normalize180();

            return attitude;
        }
        #endregion

        #region initialize
        /// <summary>MapClientEventToSimEvent, AddClientEventToNotificationGroup, SetNotificationGroupPriority, MapInputEventToClientEvent</summary>
        public void Initialize()
        {
            G.simConnect.MapClientEventToSimEvent(EVENT.CAMERA_LEFT, "PAN_LEFT");
            G.simConnect.MapClientEventToSimEvent(EVENT.CAMERA_RIGHT, "PAN_RIGHT");
            G.simConnect.MapClientEventToSimEvent(EVENT.CAMERA_UP, "PAN_UP");
            G.simConnect.MapClientEventToSimEvent(EVENT.CAMERA_DOWN, "PAN_DOWN");

            G.simConnect.AddClientEventToNotificationGroup(GROUP.CAMERA, EVENT.CAMERA_LEFT, (bool)false);
            G.simConnect.AddClientEventToNotificationGroup(GROUP.CAMERA, EVENT.CAMERA_RIGHT, (bool)false);
            G.simConnect.AddClientEventToNotificationGroup(GROUP.CAMERA, EVENT.CAMERA_UP, (bool)false);
            G.simConnect.AddClientEventToNotificationGroup(GROUP.CAMERA, EVENT.CAMERA_DOWN, (bool)false);

            G.simConnect.SetNotificationGroupPriority(GROUP.CAMERA, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);

            G.simConnect.MapInputEventToClientEvent(INPUT.KEYS, KeyLeft, EVENT.CAMERA_LEFT, (uint)0, (EVENT)SimConnect.SIMCONNECT_UNUSED, (uint)0, (bool)false);
            G.simConnect.MapInputEventToClientEvent(INPUT.KEYS, KeyRight, EVENT.CAMERA_RIGHT, (uint)0, (EVENT)SimConnect.SIMCONNECT_UNUSED, (uint)0, (bool)false);
            G.simConnect.MapInputEventToClientEvent(INPUT.KEYS, KeyUp, EVENT.CAMERA_UP, (uint)0, (EVENT)SimConnect.SIMCONNECT_UNUSED, (uint)0, (bool)false);
            G.simConnect.MapInputEventToClientEvent(INPUT.KEYS, KeyDown, EVENT.CAMERA_DOWN, (uint)0, (EVENT)SimConnect.SIMCONNECT_UNUSED, (uint)0, (bool)false);
        }
        #endregion 

        #region reset FSX view
        /// <summary>Resets view in FSX so it mathches google earth</summary>
        public void ResetFSXView()
        {
            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                PitchDeg.ToFloat(),
                BankDeg.ToFloat(),
                HeadingDeg.ToFloat());
        }
        #endregion
    }
}
