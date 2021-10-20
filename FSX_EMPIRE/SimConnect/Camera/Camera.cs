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
        public double HatSwitchSpeed = 1;

        Enum myGroupID;
        Enum myInputID;
        #endregion

        #region Adjust camera heading, pitch
        /// <summary>Pitches camera up by incº</summary>
        public void PitchUp(double increment)
        {
            PitchDeg = (PitchDeg - increment).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                PitchDeg.ToFloat(),
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD);
        }

        /// <summary>Pitches camera down by incº</summary>
        public void PitchDown(double increment)
        {
            PitchDeg = (PitchDeg + increment).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                PitchDeg.ToFloat(),
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD);
        }

        /// <summary>Rotates camera right by incº</summary>
        public void RotateRight(double increment)
        {
            HeadingDeg = (HeadingDeg + increment).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                HeadingDeg.ToFloat());
        }

        /// <summary>Rotates camera left by incº</summary>
        public void RotateLeft(double increment)
        {
            HeadingDeg = (HeadingDeg - increment).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                HeadingDeg.ToFloat());
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

        public Camera(Enum groupID, Enum inputID)
        {
            myGroupID = groupID;
            myInputID = inputID;
        }

        #region Read/Write INI Settings
        public void WriteINI(string path)
        {
            IniFile.WriteKey(path, "HatSwitchSpeed", HatSwitchSpeed.ToString(), "SimConnect");
        }

        public void ReadINI(string path)
        {
            _ = double.TryParse(IniFile.ReadKey(path, "HatSwitchSpeed", "SimConnect"), out HatSwitchSpeed);
        }
        #endregion

        enum Joystick
        {
            one,
            two,
        }

        #region Initialize
        /// <summary>This initializes the elements to get the Hat Switch working to send values to Google Earth</summary>
        public void Initialize()
        {
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_FORWARD, "VIEW_FORWARD");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_RIGHT, "VIEW_RIGHT");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_LEFT, "VIEW_LEFT");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_REAR, "VIEW_REAR");

            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_FORWARD, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_RIGHT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_LEFT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_REAR, false);

            G.simConnect.MapInputEventToClientEvent(myInputID, "joystick:0:POV:0", EventID.VIEW_FORWARD, 0, simconnect.unused, 0, false);
            G.simConnect.MapInputEventToClientEvent(myInputID, "joystick:1:POV:0", EventID.VIEW_RIGHT, 0, simconnect.unused, 0, false);

            G.simConnect.SetNotificationGroupPriority(myGroupID, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);

            G.simConnect.SetInputGroupState(myInputID, (uint)SIMCONNECT_STATE.ON);
            G.simConnect.SetInputGroupPriority(myInputID, (uint)SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);
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

        #region OnRecvEvent
        /// <summary>Checks a SIMCONNECT_RECV_EVENT.uEventID to see if it is a FSX System Event. 
        /// If so, it handles the event and returns true. Otherwise it returns false.</summary>
        public bool OnRecvEvent(SIMCONNECT_RECV_EVENT data)
        {
            Console.WriteLine(data.dwData);
            switch (data.uEventID)
            {
                case (uint)EventID.VIEW_FORWARD:
                    {
                        PitchUp(HatSwitchSpeed);
                        return true;
                    }
                case (uint)EventID.VIEW_REAR:
                    {
                        PitchDown(HatSwitchSpeed);
                        return true;
                    }
                case (uint)EventID.VIEW_RIGHT:
                    {
                        RotateRight(HatSwitchSpeed);
                        return true;
                    }
                case (uint)EventID.VIEW_LEFT:
                    {
                        RotateLeft(HatSwitchSpeed);
                        return true;
                    }
            }
            return false;
        }
        #endregion 

        #region Old Junk
        #region MapClientEventToSimEvent
        /// <summary>The MapClientEventToSimEvent function associates a client defined event ID with a ESP event name.
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

        #region AddClientEventToNotificationGroup
        /// <summary>The SimConnect_AddClientEventToNotificationGroup function is used to add an individual client defined event to a notification group.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_addclienteventtonotificationgroup">Documentation</seealso></summary>
        void AddClientEventToNotificationGroup(EventID ID)
        {
            G.simConnect.AddClientEventToNotificationGroup(
                //Specifies the ID of the client defined group.
                myGroupID,
                //Specifies the ID of the client defined event.
                ID,
                /*[in, optional]  Boolean, True indicates that the event will be masked by this client and will not be 
                 * transmitted to any more clients, possibly including ESP itself (if the priority of the client exceeds that of ESP).
                 * False is the default. See the explanation of SimConnect Priorities.*/
                false);
        }
        #endregion

        #region MapInputEventToClientEvent
        /// <summary>The SimConnect_MapInputEventToClientEvent function is used to connect input events (such as keystrokes, joystick or mouse movements) with the sending of appropriate event notifications.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_mapinputeventtoclientevent">Documentation</seealso></summary>
        void MapInputEventToClientEvent(EventID ID, string input)
        {
            G.simConnect.MapInputEventToClientEvent(
                // Specifies the ID of the client defined input group that the input event is to be added to.
                myInputID,
                /*Pointer to a null-terminated string containing the definition of the input events 
                 * (keyboard keys, mouse or joystick events, for example). 
                 * See the Remarks and example below for a range of possibilities. */
                input,
                /*Specifies the ID of the down, and default, event. This is the client defined event that is 
                 * triggered when the input event occurs. If only an up event is required, set this to SIMCONNECT_UNUSED.*/
                ID,
                //Specifies an optional numeric value, which will be returned when the down event occurs.
                0,
                /*Specifies the ID of the up event. This is the client defined event that is triggered when the up action occurs.*/
                simconnect.unused,
                //Specifies an optional numeric value, which will be returned when the up event occurs.
                0,
                /* If set to true, specifies that the client will mask the event, and no other lower priority clients will receive it.
                 * The default is false.*/
                false);
        }
        #endregion
        #endregion 
    }
}
