using System;
using System.Windows.Forms;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;

namespace Sim
{
    #region Enums
    enum DEFINITION : uint
    {
        COPILOT,
        CoPilotReadOnly,
        GOOGLE_EARTH,
    }

    enum REQUEST : uint
    {
        COPILOT,
        CoPilotReadOnly,
        GOOGLE_EARTH,
    };

    enum NotificationGroup : uint
    {
        COPILOT,
        SIMRATE,
        CAMERA,
    }

    enum InputGroup : uint
    {
        KEYS,
    }

    /// <summary>Added this because 'MapInputEventToClientEvent' needed an enum type</summary>
    enum simconnect : uint
    {
        unused = 4294967295,
    }
    #endregion

    /// <summary>See: https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526981(v=msdn.10) </summary>
    public partial class Connect : UserControl
    {
        #region Properties in the UserControl properties box
        [Category("SimConnect"),
        Description("Image to show when connected")]
        public Image bRunning { get; set; } = new Bitmap(32, 32);

        [Category("SimConnect"),
        Description("Image to show when disconnected")]
        public Image bStopped { get; set; } = new Bitmap(32, 32);
        #endregion 

        #region My Event Delegates
        public delegate void ConnectionOpen();
        public event ConnectionOpen OnConnectionOpen;

        public delegate void ConnectionClosed();
        public event ConnectionClosed OnConnectionClosed;
        #endregion

        #region Declerations
        /// <summary>How fast we want the update data called</summary>
        public int RequestRateMilliSeconds = 1000;

        public SystemEvent SystemEvent;
        public CoPilot.MyCoPilot CoPilot;
        public Google.Earth GoogleEarth;
        public Camera camera;
        /// <summary> User-defined win32 event. </summary>
        const int WM_USER_SIMCONNECT = 0x0402;
        #endregion

        #region Read/Write INI Settings
        public void WriteINI(string path)
        {
            IniFile.WriteKey(path, "RequestRateMilliSeconds", "16", "SimConnect");
            GoogleEarth.WriteINI(path);
            camera.WriteINI(path);
        }
        public void ReadINI(string path)
        {
            if (int.TryParse(IniFile.ReadKey(path, "RequestRateMilliSeconds", "SimConnect"), out int i))
                RequestRateMilliSeconds = i;

            GoogleEarth.ReadINI(path);
            camera.ReadINI(path);
        }
        #endregion

        #region Constructor
        /// <summary> Constructor </summary>
        public Connect()
        {
            InitializeComponent();
            SystemEvent = new Sim.SystemEvent();
            CoPilot = new CoPilot.MyCoPilot();
            GoogleEarth = new Google.Earth(DEFINITION.GOOGLE_EARTH, REQUEST.GOOGLE_EARTH);
            camera = new Camera(NotificationGroup.CAMERA ,InputGroup.KEYS );
            //Console.WriteLine("unused " + (uint)SimConnect.SIMCONNECT_UNUSED); // = 4294967295
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

        #region Initialize
        void Initialize()
        {
            try
            {
                InitializeEventHandlers();
                MapClientEventToSimEvent();
                CoPilot.AddSettableDefinitions();
                CoPilot.AddReadonlyDefinitions();
                GoogleEarth.AddToDataDefinition();
                SystemEvent.Subscribe();

                camera.Initialize();
                G.simConnect.SetInputGroupState(InputGroup.KEYS, (uint)SIMCONNECT_STATE.ON);
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
                EventID.SIM_RATE_INCR,
                "SIM_RATE_INCR");

            G.simConnect.MapClientEventToSimEvent(
                EventID.SIM_RATE_DECR,
                "SIM_RATE_DECR");
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
            G.simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_Exceptions.Simconnect_OnRecvException);
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
                    if (SystemEvent.IsPaused == false)
                        GoogleEarth.OnRecvSimobjectDataBytype(data, camera);
                    break;

                case REQUEST.COPILOT:
                    if (SystemEvent.IsPaused == false)
                        CoPilot.OnRecvSettableData(data);
                    break;
                case REQUEST.CoPilotReadOnly:
                    if (SystemEvent.IsPaused == false)
                        CoPilot.OnRecvReadOnlyData(data);
                    break;
                default:
                    MessageBox.Show("Unknown REQUEST ID: " + data.dwRequestID);
                    break;
            }
        }

        void Simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            picSimConnect.Image = bRunning;                                         // Update image so user knows their connected
            camera.ResetFSXView();                                                // Make FSX view match Google Earth View
            OnConnectionOpen();                                                     // Fire event in case we want to do something else on FrmMain
        }

        void Simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            CloseConnection();
        }

        void Simconnect_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT data)
        {
            if (SystemEvent.OnRecvEvent(data.uEventID))
                return;

            if (camera.OnRecvEvent(data))
                return;

            Console.WriteLine("UNKNOWN EVENT");
        }
    
        #endregion

        #region Public Functions (Called from FrmMain)
        #region Check if fsx is running
        /// <summary>checks to see if fsx process is running</summary>
        public bool IsRunning()
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
            if (IsRunning() == false)
            {
                //Todo, could start it automatically....
                switch (MessageBox.Show("FSX does not appear to be running.", "FSX Error", MessageBoxButtons.AbortRetryIgnore))
                {
                    case DialogResult.None:
                        return false;
                    case DialogResult.Abort:
                        return false;
                    case DialogResult.Retry:
                        if (IsRunning() == false)
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
                SystemEvent.UnsubscribeAll();               
                G.simConnect.Dispose();       // Dispose serves the same purpose as SimConnect_Close()
                G.simConnect = null;
                picSimConnect.Image = bStopped; //FSX_EMPIRE.Properties.Resources.Red_Light;
                OnConnectionClosed();
            }
        }
        #endregion

        #region sim rate
        public void SimRate_Inc()
        {
            if (G.simConnect != null)
            {
                G.simConnect.TransmitClientEvent(
                    (uint)SimConnect.SIMCONNECT_OBJECT_ID_USER,
                    EventID.SIM_RATE_INCR,
                    (uint)0,
                    NotificationGroup.SIMRATE,
                    SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY
                    );

                CoPilot.Hold.AirSpeed *= 2;
            }
        }

        public void SimRate_Dec()
        {
            if (G.simConnect != null)
            {
                G.simConnect.TransmitClientEvent(
                (uint)SimConnect.SIMCONNECT_OBJECT_ID_USER,
                EventID.SIM_RATE_DECR,
                (uint)0,
                NotificationGroup.SIMRATE,
                SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY
                );

                CoPilot.Increment.AirSpeed /= 2;
            }
        }
        #endregion
        #endregion
    }
}
