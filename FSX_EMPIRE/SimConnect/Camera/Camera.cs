using ExtensionMethods;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;

namespace Sim
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class Camera
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

        #region Adjust camera heading, pitch
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

        /// <summary>Pitches camera up by incº (-)</summary>
        public void PitchUp(float degree)
        {
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

        #region Calculate combined plane camera rotation
        /// <summary>Adds the plane and camera rotations together so result can be used in google earth</summary>
        public Attitude CalculteRotations(Sim.Google.Earth.DataDefinition fsx)
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
            attitude.Heading = (fsx.PLANE_HEADING_DEGREES_TRUE + HeadingDeg).Normalize360();

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

        Enum myGroupID;
        Enum myInputID;
        public Camera(Enum groupID, Enum inputID)
        {
            myGroupID = groupID;
            myInputID = inputID;
        }

        public void ReadINI(string path)
        {
            KeyLeft = IniFile.ReadKey(path, "CameraLeftKey", "SimConnect");
            KeyRight = IniFile.ReadKey(path, "CameraRightKey", "SimConnect");
            KeyUp = IniFile.ReadKey(path, "CameraUpKey", "SimConnect");
            KeyDown = IniFile.ReadKey(path, "CameraDownKey", "SimConnect");
        }

        #region Initialize
        /// <summary>MapClientEventToSimEvent, AddClientEventToNotificationGroup, SetNotificationGroupPriority, MapInputEventToClientEvent</summary>
        public void Initialize()
        {
            MapClientEventToSimEvent(EventID.PAN_LEFT);
            MapClientEventToSimEvent(EventID.PAN_RIGHT);
            MapClientEventToSimEvent(EventID.PAN_UP);
            MapClientEventToSimEvent(EventID.PAN_DOWN);
            //G.simConnect.MapClientEventToSimEvent(EventID.PAN_LEFT, "PAN_LEFT");
            //G.simConnect.MapClientEventToSimEvent(EventID.PAN_RIGHT, "PAN_RIGHT");
            //G.simConnect.MapClientEventToSimEvent(EventID.PAN_UP, "PAN_UP");
            //G.simConnect.MapClientEventToSimEvent(EventID.PAN_DOWN, "PAN_DOWN");

            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.PAN_LEFT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.PAN_RIGHT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.PAN_UP, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.PAN_DOWN, false);

            G.simConnect.SetNotificationGroupPriority(myGroupID, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);

            G.simConnect.MapInputEventToClientEvent(myInputID, KeyLeft, EventID.PAN_LEFT, 0, (EventID)SimConnect.SIMCONNECT_UNUSED, 0, false);
            G.simConnect.MapInputEventToClientEvent(myInputID, KeyRight, EventID.PAN_RIGHT, 0, (EventID)SimConnect.SIMCONNECT_UNUSED, 0, false);
            G.simConnect.MapInputEventToClientEvent(myInputID, KeyUp, EventID.PAN_UP, 0, (EventID)SimConnect.SIMCONNECT_UNUSED, 0, false);
            G.simConnect.MapInputEventToClientEvent(myInputID, KeyDown, EventID.PAN_DOWN, 0, (EventID)SimConnect.SIMCONNECT_UNUSED, 0, false);
        }
        #endregion 

        #region ResetFSXView
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

        #region MapClientEventToSimEvent
        /// <summary>The SimConnect_MapClientEventToSimEvent function associates a client defined event ID with a ESP event name.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_mapclienteventtosimevent">Documentation</seealso></summary>
        void MapClientEventToSimEvent(EventID ID)
        {
            G.simConnect.MapClientEventToSimEvent(
                //Specifies the ID of the client event.
                ID,
                /* Specifies the ESP event name. Refer to the Event IDs document for a list of event names (listed under String Name).
                 * If the event name includes one or more periods (such as "Custom.Event" in the example below) then they are 
                 * custom events specified by the client, and will only be recognized by another client (and not ESP) that has been 
                 * coded to receive such events. No ESP events include periods. If no entry is made for this parameter, the event 
                 * is private to the client.
                 * Alternatively enter a decimal number in the format "#nnnn" or a hex number in the format "#0xnnnn",
                 * where these numbers are in the range THIRD_PARTY_EVENT_ID_MIN and THIRD_PARTY_EVENT_ID_MAX, in order to receive
                 * events from third-party add-ons to ESP.*/
                ID.ToString());
        }
        #endregion

        #region OnRecvEvent
        /// <summary>Checks a SIMCONNECT_RECV_EVENT.uEventID to see if it is a FSX System Event. 
        /// If so, it handles the event and returns true. Otherwise it returns false.</summary>
        public bool OnRecvEvent(uint uEventID)
        {
            switch (uEventID)
            {
                case (uint)EventID.PAN_LEFT:
                    {
                        RotateLeft();
                        return true;
                    }
                case (uint)EventID.PAN_RIGHT:
                    {
                        RotateRight();
                        return true;
                    }
                case (uint)EventID.PAN_UP:
                    {
                        PitchUp();
                        return true;
                    }
                case (uint)EventID.PAN_DOWN:
                    {
                        PitchDown();
                        return true;
                    }
            }
            return false;
        }
        #endregion 
    }
}
