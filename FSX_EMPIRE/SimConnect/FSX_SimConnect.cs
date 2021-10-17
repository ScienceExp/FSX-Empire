using System;
using System.Windows.Forms;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FSX_EMPIRE
{
    #region Enums
    enum DEFINITION : uint
    {
        ThrottlePos_1,
        ThrottlePos_2,
        PropLeverPos_1,
        PropLeverPos_2,
        CowlFlapPos_1,
        CowlFlapPos_2,
        COPILOT,
        GOOGLE_EARTH,
    }

    enum REQUEST : uint
    {
        COPILOT,
        GOOGLE_EARTH,
    };

    enum GROUP : uint
    {
        SIMRATE,
        CAMERA,
    }

    enum EVENT : uint
    {
        SIM_START,
        SIM_STOP,
        PAUSED,
        UNPAUSED,
        SIM_RATE_INCR,
        SIM_RATE_DECR,
        CAMERA_LEFT,
        CAMERA_RIGHT,
        CAMERA_UP,
        CAMERA_DOWN,
    }

    enum INPUT : uint
    {
        KEYS,
    }
    #endregion

    /// <summary>See: https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526981(v=msdn.10) </summary>
    public partial class FSX_SimConnect : UserControl
    {
        #region My Event Delegates
        public delegate void FsxConnectionOpen();
        public event FsxConnectionOpen OnFsxConnectionOpen;

        public delegate void FsxConnectionClosed();
        public event FsxConnectionClosed OnFsxConnectionClosed;

        public delegate void FsxPaused();
        public event FsxPaused OnFsxPaused;

        public delegate void FsxUnpaused();
        public event FsxUnpaused OnFsxUnpaused;

        public delegate void FsxSimStart();
        public event FsxSimStart OnFsxSimStart;

        public delegate void FsxSimStop();
        public event FsxSimStop OnFsxSimStop;
        #endregion

        #region Declerations
        /// <summary> Variable to know if sim is paused or not. </summary>
        public static bool Paused = false;

        /// <summary> User-defined win32 event. </summary>
        const int WM_USER_SIMCONNECT = 0x0402;
        #endregion 

        #region FSX_SimConnect()
        /// <summary> Constructor </summary>
        public FSX_SimConnect()
        {
            InitializeComponent();
        }
        #endregion

        #region WndProc() [Callback]
        /// <summary> Callback from SimConnect so we can recieve messages</summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_USER_SIMCONNECT)
            {
                if (G.simConnect != null)
                    G.simConnect.ReceiveMessage();
            }
            else
                base.WndProc(ref m);
        }
        #endregion

        #region AddToDataDefinition
        void AddToDataDefinition_CoPilot()
        {
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "AIRSPEED INDICATED", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "INDICATED ALTITUDE", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "ENG MANIFOLD PRESSURE:1", "inHG", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "ENG MANIFOLD PRESSURE:2", "inHG", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "GENERAL ENG THROTTLE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "GENERAL ENG THROTTLE LEVER POSITION:2", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "PROP RPM:1", "Rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "PROP RPM:2", "Rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "GENERAL ENG PROPELLER LEVER POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "GENERAL ENG PROPELLER LEVER POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "RECIP ENG COWL FLAP POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "RECIP ENG COWL FLAP POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            // The following is a douplicate in order to send data
            G.simConnect.AddToDataDefinition(DEFINITION.ThrottlePos_1, "GENERAL ENG THROTTLE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.ThrottlePos_2, "GENERAL ENG THROTTLE LEVER POSITION:2", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.PropLeverPos_1, "GENERAL ENG PROPELLER LEVER POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.PropLeverPos_2, "GENERAL ENG PROPELLER LEVER POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.CowlFlapPos_1, "RECIP ENG COWL FLAP POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.CowlFlapPos_2, "RECIP ENG COWL FLAP POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            G.simConnect.RegisterDataDefineStruct<SCoPilot>(DEFINITION.COPILOT);
            //G.simConnect.RegisterDataDefineStruct<double>(DEFINITION.THROTTLE_1); //struct is just a double so no need to register
            //G.simConnect.RegisterDataDefineStruct<double>(DEFINITION.THROTTLE_2);
            //G.simConnect.RegisterDataDefineStruct<double>(DEFINITION.PROPELLER_1);
            //G.simConnect.RegisterDataDefineStruct<double>(DEFINITION.PROPELLER_2);
            //G.simConnect.RegisterDataDefineStruct<double>(DEFINITION.COWLFLAP_1);
            //G.simConnect.RegisterDataDefineStruct<double>(DEFINITION.COWLFLAP_2);
        }

        void AddToDataDefinition_GoogleEarth()
        {
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE LATITUDE", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE LONGITUDE", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE ALTITUDE", "Meters", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE HEADING DEGREES TRUE", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE PITCH DEGREES", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE BANK DEGREES", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
 
            G.simConnect.RegisterDataDefineStruct<SGoogleEarth>(DEFINITION.GOOGLE_EARTH);
        }
        #endregion 

        #region Initialize
        void Initialize()
        {
            try
            {
                InitializeEventHandlers();
                MapClientEventToSimEvent();
                AddToDataDefinition_CoPilot();
                AddToDataDefinition_GoogleEarth();
                SubscribeToSystemEvents();

                G.camera.Initialize();
                G.simConnect.SetInputGroupState(INPUT.KEYS, (uint)SIMCONNECT_STATE.ON);
            }
            catch (COMException ex)
            {
                MessageBox.Show("SimConnect Setup() Error:" + ex.Message);
            }
        }
        #endregion

        #region MapClientEventToSimEvent
        void MapClientEventToSimEvent()
        {
            G.simConnect.MapClientEventToSimEvent(
                EVENT.SIM_RATE_INCR,
                "SIM_RATE_INCR");

            G.simConnect.MapClientEventToSimEvent(
                EVENT.SIM_RATE_DECR,
                "SIM_RATE_DECR");
        }
        #endregion

        #region SubscribeToSystemEvent & UnsubscribeFromSystemEvent
        void SubscribeToSystemEvents()
        {
            // Request a simulation started and stop event
            //https://msdn.microsoft.com/en-us/library/cc526983.aspx#SimConnect_SubscribeToSystemEvent
            G.simConnect.SubscribeToSystemEvent(
                EVENT.SIM_START,
                "SimStart");

            G.simConnect.SubscribeToSystemEvent(
                EVENT.SIM_STOP,
                "SimStop");

            G.simConnect.SubscribeToSystemEvent(
                EVENT.UNPAUSED,
                "Unpaused");

            G.simConnect.SubscribeToSystemEvent(
                EVENT.PAUSED,
                "Paused");
        }

        void UnsubscribeFromSystemEvents()
        {
            try
            {
                G.simConnect.UnsubscribeFromSystemEvent(EVENT.SIM_START);
                G.simConnect.UnsubscribeFromSystemEvent(EVENT.SIM_STOP);
                G.simConnect.UnsubscribeFromSystemEvent(EVENT.UNPAUSED);
                G.simConnect.UnsubscribeFromSystemEvent(EVENT.PAUSED);
            }
            catch (Exception ex)
            {
                MessageBox.Show("could not Unsubscribe to events. " + ex.Message.ToString());
            }
        }
        #endregion

        #region Initialize SimConnect EventHandlers
        void InitializeEventHandlers()
        {
            /* listen to events - only events that we need for this sample are uncommented. */

            //simconnect.OnRecvAirportList += new SimConnect.RecvAirportListEventHandler(simconnect_OnRecvAirportList);
            //simconnect.OnRecvAssignedObjectId += new SimConnect.RecvAssignedObjectIdEventHandler(simconnect_OnRecvAssignedObjectId);
            //simconnect.OnRecvClientData += new SimConnect.RecvClientDataEventHandler(simconnect_OnRecvClientData);
            //simconnect.OnRecvCustomAction += new SimConnect.RecvCustomActionEventHandler(simconnect_OnRecvCustomAction);
            G.simConnect.OnRecvEvent += new SimConnect.RecvEventEventHandler(Simconnect_OnRecvEvent);
            //simconnect.OnRecvEventFilename += new SimConnect.RecvEventFilenameEventHandler(simconnect_OnRecvEventFilename);
            //simconnect.OnRecvEventFrame += new SimConnect.RecvEventFrameEventHandler(simconnect_OnRecvEventFrame);
            //simconnect.OnRecvEventMultiplayerClientStarted += new SimConnect.RecvEventMultiplayerClientStartedEventHandler(simconnect_OnRecvEventMultiplayerClientStarted);
            //simconnect.OnRecvEventMultiplayerServerStarted += new SimConnect.RecvEventMultiplayerServerStartedEventHandler(simconnect_OnRecvEventMultiplayerServerStarted);
            //simconnect.OnRecvEventMultiplayerSessionEnded += new SimConnect.RecvEventMultiplayerSessionEndedEventHandler(simconnect_OnRecvEventMultiplayerSessionEnded);
            //simconnect.OnRecvEventObjectAddremove += new SimConnect.RecvEventObjectAddremoveEventHandler(simconnect_OnRecvEventObjectAddremove);
            //simconnect.OnRecvEventRaceEnd += new SimConnect.RecvEventRaceEndEventHandler(simconnect_OnRecvEventRaceEnd);
            //simconnect.OnRecvEventRaceLap += new SimConnect.RecvEventRaceLapEventHandler(simconnect_OnRecvEventRaceLap);
            //simconnect.OnRecvEventWeatherMode += new SimConnect.RecvEventWeatherModeEventHandler(simconnect_OnRecvEventWeatherMode);
            G.simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(Simconnect_OnRecvException);
            //simconnect.OnRecvNdbList += new SimConnect.RecvNdbListEventHandler(simconnect_OnRecvNdbList);
            //simconnect.OnRecvNull += new SimConnect.RecvNullEventHandler(simconnect_OnRecvNull);
            G.simConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(Simconnect_OnRecvOpen);
            G.simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(Simconnect_OnRecvQuit);
            //simconnect.OnRecvReservedKey += new SimConnect.RecvReservedKeyEventHandler(simconnect_OnRecvReservedKey); 
            //simconnect.OnRecvSimobjectData += new SimConnect.RecvSimobjectDataEventHandler(simconnect_OnRecvSimobjectData);
            G.simConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(Simconnect_OnRecvSimobjectDataBytype);
            //simconnect.OnRecvSystemState += new SimConnect.RecvSystemStateEventHandler(simconnect_OnRecvSystemState);
            //simconnect.OnRecvVorList += new SimConnect.RecvVorListEventHandler(simconnect_OnRecvVorList);
            //simconnect.OnRecvWaypointList += new SimConnect.RecvWaypointListEventHandler(simconnect_OnRecvWaypointList);
            //simconnect.OnRecvWeatherObservation += new SimConnect.RecvWeatherObservationEventHandler(simconnect_OnRecvWeatherObservation);
        }
        #endregion

        #region SimConnect OnEvents
        void Simconnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            switch ((REQUEST)data.dwRequestID)
            {
                case REQUEST.GOOGLE_EARTH:
                    if (Paused == false)
                    {
                        Google.UpdateValues((SGoogleEarth)data.dwData[0]);
                    }
                    break;

                case REQUEST.COPILOT:
                    SCoPilot si = (SCoPilot)data.dwData[0];
                    if (Paused == false)
                    {
                        G.myPlane.UpdateValues(si);
                        CoPilot.CheckAirSpeed(si);
                        CoPilot.CheckManifoldPressure(si);
                        CoPilot.CheckPropellerRPM(si);
                        CoPilot.CheckCowlFlaps(si); 
                    }
                    break;
                default:
                    MessageBox.Show("Unknown REQUEST ID: " + data.dwRequestID);
                    break;
            }
        }

        void Simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            picSimConnect.Image = Properties.Resources.Green_Light;     // Update image so user knows their connected
            G.camera.ResetFSXView();                                    // Make FSX view match Google Earth View
            OnFsxConnectionOpen();                                      // Fire event in case we want to do something else on FrmMain
        }

        void Simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            CloseConnection();
        }

        void Simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            SimConnect_Exceptions ex = new SimConnect_Exceptions();     // Class interpreting fsx exceptions
            MessageBox.Show(
                "Exception received: " + data.dwException +
                ": " + ex.GetDescription((SIMCONNECT_EXCEPTION)data.dwException));
        }

        void Simconnect_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT data)
        {
            switch (data.uEventID)
            {
                case (uint)EVENT.SIM_START:
                    {
                        OnFsxSimStart();
                        break;
                    }
                case (uint)EVENT.SIM_STOP:
                    {
                        OnFsxSimStop();
                        break;
                    }
                case (uint)EVENT.PAUSED:
                    {
                        Paused = true;
                        OnFsxPaused();
                        break;
                    }
                case (uint)EVENT.UNPAUSED:
                    {
                        Paused = false;
                        OnFsxUnpaused();
                        break;
                    }
                case (uint)EVENT.CAMERA_LEFT:
                    {
                        G.camera.RotateLeft();
                        break;
                    }
                case (uint)EVENT.CAMERA_RIGHT:
                    {
                        G.camera.RotateRight(); 
                        break;
                    }
                case (uint)EVENT.CAMERA_UP:
                    {
                        G.camera.PitchUp();
                        break;
                    }
                case (uint)EVENT.CAMERA_DOWN:
                    {
                        G.camera.PitchDown();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("UNKNOWN EVENT");
                        break;
                    }
            }
        }
        #endregion

        #region Public Functions (Called from FrmMain)
        #region check if fsx is running
        /// <summary>checks to see if fsx process is running</summary>
        public bool IsFsxRunning()
        {
            //Process[] processlist = Process.GetProcesses();
            //foreach (Process theprocess in processlist)
            //    Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);

            Process[] pname = Process.GetProcessesByName("fsx");
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        #endregion

        #region open/close connection
        /// <summary>This will attempt to connect to SimConnect</summary>
        /// <returns>True if successful, False if not</returns>
        public bool OpenConnection()
        {
            if (IsFsxRunning() == false)
            {
                //Todo, could start it automatically....
                switch (MessageBox.Show("FSX does not appear to be running.", "FSX Error", MessageBoxButtons.AbortRetryIgnore))
                {
                    case DialogResult.None:
                        return false;
                    case DialogResult.Abort:
                        return false;
                    case DialogResult.Retry:
                        if (IsFsxRunning() == false)
                        {
                            MessageBox.Show("Still could not find fsx process. Aborting connection attempt.");
                            return false;
                        }
                        break;
                    case DialogResult.Ignore:
                        break;
                }
            }

            if (G.simConnect == null)
            {
                try
                {
                    G.simConnect = new SimConnect("Managed Data Request", Handle, WM_USER_SIMCONNECT, null, 0);
                    Initialize();
                    return true;
                }
                catch (COMException ex)
                {
                    MessageBox.Show("Unable to connect to FSX: " + ex.Message);
                }
            }
            else
                CloseConnection();
            return false;
        }

        /// <summary>Closes the SimConnect connection if it is open.</summary>
        public void CloseConnection()
        {
            if (G.simConnect != null)
            {
                UnsubscribeFromSystemEvents();                
                G.simConnect.Dispose();       // Dispose serves the same purpose as SimConnect_Close()
                G.simConnect = null;
                picSimConnect.Image = Properties.Resources.Red_Light;
                OnFsxConnectionClosed();
            }
        }
        #endregion

        #region requests
        public void RequestDataForCoPilot()
        {
            if (G.simConnect != null)
            {
                G.simConnect.RequestDataOnSimObjectType(
                    REQUEST.COPILOT,
                    DEFINITION.COPILOT,
                    0,
                    SIMCONNECT_SIMOBJECT_TYPE.USER);
            }
        }

        public void RequestDataForGoogleEarth()
        {
            if (G.simConnect != null)
                Google.RequestDataOnSimObjectType(); 
        }
        #endregion

        #region sim rate
        public void SimRate_Inc()
        {
            if (G.simConnect != null)
            {

                G.simConnect.TransmitClientEvent(
                    (uint)SimConnect.SIMCONNECT_OBJECT_ID_USER,
                    EVENT.SIM_RATE_INCR,
                    (uint)0,
                    GROUP.SIMRATE,
                    SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY
                    );

                CoPilot.Inc.AirSpeed *= 2;
            }
        }

        public void SimRate_Dec()
        {
            if (G.simConnect != null)
            {
                G.simConnect.TransmitClientEvent(
                (uint)SimConnect.SIMCONNECT_OBJECT_ID_USER,
                EVENT.SIM_RATE_DECR,
                (uint)0,
                GROUP.SIMRATE,
                SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY
                );

                CoPilot.Inc.AirSpeed /= 2;
            }
        }
        #endregion
        #endregion
    }
}
