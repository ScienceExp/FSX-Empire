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
        /// <summary>Used to tell if control 0 uses a d-pad or hat-switch as they are handled a little different</summary>
        bool Joystick0IsDpad = true;
        /// <summary>Holds the Group enum ID that can be found in Sim.Connect</summary>
        Enum myGroupID;
        /// <summary>Holds the Input enum ID that can be found in Sim.Connect</summary>
        Enum myInputID;
        #endregion

        #region Adjust camera heading, pitch
        /// <summary>Pitches camera up by incº</summary>
        void PitchUp(double increment)
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
        void PitchDown(double increment)
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
        void RotateRight(double increment)
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
        void RotateLeft(double increment)
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

        /// <summary>Adds pitch and rotation to existing values</summary>
        void AddPitchAndRotation(double headingInc, double pitchInc)
        {
            HeadingDeg = (HeadingDeg - headingInc).Normalize180();
            PitchDeg = (PitchDeg - pitchInc).Normalize180();

            G.simConnect.CameraSetRelative6DOF(
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                PitchDeg.ToFloat(),
                SimConnect.SIMCONNECT_CAMERA_IGNORE_FIELD,
                HeadingDeg.ToFloat());
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

        #region Constructor
        public Camera(Enum groupID, Enum inputID)
        {
            myGroupID = groupID;
            myInputID = inputID;
        }
        #endregion 

        #region Read/Write INI Settings
        public void WriteINI(string path)
        {
            IniFile.WriteKey(path, "HatSwitchSpeed", HatSwitchSpeed.ToString(), "SimConnect");
            IniFile.WriteKey(path, "Joystick0IsDpad", Joystick0IsDpad.ToString(), "SimConnect");
        }

        public void ReadINI(string path)
        {
            _ = double.TryParse(IniFile.ReadKey(path, "HatSwitchSpeed", "SimConnect"), out HatSwitchSpeed);
            _ = bool.TryParse(IniFile.ReadKey(path, "Joystick0IsDpad", "SimConnect"), out Joystick0IsDpad);
        }
        #endregion

        #region Initialize
        /// <summary>This initializes the elements to get the Hat Switch working to send values to Google Earth</summary>
        public void Initialize()
        {
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_FORWARD, "VIEW_FORWARD");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_RIGHT, "VIEW_RIGHT");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_LEFT, "VIEW_LEFT");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_REAR, "VIEW_REAR");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_FORWARD_LEFT, "VIEW_FORWARD_LEFT");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_FORWARD_RIGHT, "VIEW_FORWARD_RIGHT");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_REAR_LEFT, "VIEW_REAR_LEFT");
            G.simConnect.MapClientEventToSimEvent(EventID.VIEW_REAR_RIGHT, "VIEW_REAR_RIGHT");
            G.simConnect.MapClientEventToSimEvent(EventID.PAN_RESET, "PAN_RESET");
            G.simConnect.MapClientEventToSimEvent(EventID.PAN_UP, "PAN_UP");

            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_FORWARD, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_RIGHT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_LEFT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_REAR, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_FORWARD_LEFT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_FORWARD_RIGHT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_REAR_LEFT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.VIEW_REAR_RIGHT, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.PAN_RESET, false);
            G.simConnect.AddClientEventToNotificationGroup(myGroupID, EventID.PAN_UP, false);

            if (Joystick0IsDpad)
            {
                // d-pad
                G.simConnect.MapInputEventToClientEvent(myInputID, "joystick:0:POV:0", EventID.VIEW_FORWARD, 0, simconnect.unused, 0, false);
                // hat-switch
                G.simConnect.MapInputEventToClientEvent(myInputID, "joystick:1:POV:0", EventID.PAN_UP, 0, EventID.PAN_RESET, 0, false);
            }
            else
            {
                // d-pad
                G.simConnect.MapInputEventToClientEvent(myInputID, "joystick:1:POV:0", EventID.VIEW_FORWARD, 0, simconnect.unused, 0, false);
                // hat-switch
                G.simConnect.MapInputEventToClientEvent(myInputID, "joystick:0:POV:0", EventID.PAN_UP, 0, EventID.PAN_RESET, 0, false);
            }

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
            switch (data.uEventID)
            {
                #region These fire when d-pad set to these events
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
                case (uint)EventID.VIEW_FORWARD_LEFT:
                    {
                        AddPitchAndRotation(HatSwitchSpeed, HatSwitchSpeed);
                        return true;
                    }
                case (uint)EventID.VIEW_FORWARD_RIGHT:
                    {
                        AddPitchAndRotation(-HatSwitchSpeed, HatSwitchSpeed);
                        return true;
                    }
                case (uint)EventID.VIEW_REAR_RIGHT:
                    {
                        AddPitchAndRotation(-HatSwitchSpeed, -HatSwitchSpeed);
                        return true;
                    }
                case (uint)EventID.VIEW_REAR_LEFT:
                    {
                        AddPitchAndRotation(HatSwitchSpeed, -HatSwitchSpeed);
                        return true;
                    }
                #endregion 
                #region These only fire once when hat switch set to "hat pan"
                case (uint)EventID.PAN_RESET:
                    {
                        SetPitchAndRotation(0, 0);      //matches how fsx does it
                        return true;
                    }
                case (uint)EventID.PAN_UP:
                    {
                        switch ((uint)data.dwData)
                        {
                            case (uint)27000:               //27000 left
                                {
                                    SetPitchAndRotation(-90, 0);
                                    return true;
                                }
                            case (uint)31500:               //31500 forward left
                                {
                                    SetPitchAndRotation(-45, 0);
                                    return true;
                                }
                            case (uint)18000:               //18000 rear
                                {
                                    SetPitchAndRotation(180, 0);
                                    return true;
                                }
                            case (uint)22500:               //22500 rear left
                                {
                                    SetPitchAndRotation(-135, 0);
                                    return true;
                                }
                            case (uint)9000:                //9000 right
                                {
                                    SetPitchAndRotation(90, 0);
                                    return true;
                                }
                            case (uint)4500:                //4500 forward right
                                {
                                    SetPitchAndRotation(45, 0);
                                    return true;
                                }
                            case (uint)13500:               //13500 rear right
                                {
                                    SetPitchAndRotation(135, 0);
                                    return true;
                                }
                            case (uint)0:                   //0 facing ahead
                                {
                                    SetPitchAndRotation(0, 0);
                                    return true;
                                }
                            default:
                                return true;
                        }
                    }
                    #endregion
            }
            return false;
        }
        #endregion 
    }
}
