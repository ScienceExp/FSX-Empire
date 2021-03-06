using System;
using System.IO;
using System.Windows.Forms;

namespace FSX_EMPIRE
{
    public partial class FrmMain : Form
    {
        #region Form
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitializeMyEventsHandlers();
            LoadSettings();

            G.speechSynth = new Speech();
            G.speechRecognition = new SpeechRecognition();
            //speach.CoPilot("Welcome to FSX Empire!");

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //speach.Crew("Till next time!", false);
            CloseAll();
            if (webCapture1 != null)
                webCapture1.Closeinterfaces();
        }
        #endregion

        #region Close All
        void CloseAll()
        {
            TimerSimConnectRequest.Enabled = false;
            _ = SimConnect.GoogleEarth.ServerStop();
            SimConnect.CloseConnection();
            G.speechSynth.Dispose();
            G.speechRecognition.Dispose();
        }
        #endregion

        #region My Events
        void InitializeMyEventsHandlers()
        {
            SimConnect.GoogleEarth.OnServerStopped += new Sim.Google.Earth.ServerStopped(this.ServerStopped);
            SimConnect.GoogleEarth.OnServerStarted += new Sim.Google.Earth.ServerStarted(this.ServerStarted);
            SimConnect.GoogleEarth.OnServerWaitingForRequest += new Sim.Google.Earth.ServerWaitingForRequest(this.ServerWaitingForRequest);
            SimConnect.OnConnectionOpen += new Sim.Connect.ConnectionOpen(this.FsxConnectionOpen);
            SimConnect.OnConnectionClosed += new Sim.Connect.ConnectionClosed(this.FsxConnectionClosed);
            SimConnect.SystemEvent.OnPaused += new Sim.SystemEvent.Paused(this.FsxPaused);
            SimConnect.SystemEvent.OnUnpaused += new Sim.SystemEvent.Unpaused(this.FsxUnpaused);
            SimConnect.SystemEvent.OnSimStart += new Sim.SystemEvent.SimStart(this.FsxSimStart);
            SimConnect.SystemEvent.OnSimStop += new Sim.SystemEvent.SimStop(this.FsxSimStop);
        }

        #region GoogleEarth Events
        private void ServerStarted()
        {
            picGoogleEarthConnect.Image = Properties.Resources.Green_Light;
        }

        private void ServerWaitingForRequest()
        {
            // Happens many times, so no real need to update status.
        }

        private void ServerStopped()
        {
            picGoogleEarthConnect.Image = Properties.Resources.Red_Light;
        }
        #endregion 

        #region FSX_SimConnect events
        private void FsxSimStop()
        {
            Console.WriteLine("Fsx sim stopped");
        }

        private void FsxSimStart()
        {
            Console.WriteLine("Fsx sim started");
        }

        private void FsxUnpaused()
        {
            Console.WriteLine("Fsx unpaused");
        }

        private void FsxPaused()
        {
            Console.WriteLine("Fsx paused");
        }

        private void FsxConnectionClosed()
        {
            Console.WriteLine("Fsx connection closed");
            TimerSimConnectRequest.Enabled = false;
            TimerUpdateCamera.Enabled = false;
        }

        private void FsxConnectionOpen()
        {
            Console.WriteLine("Fsx connection opened");
            if (SimConnect.CoPilot.IsEnabled || SimConnect.GoogleEarth.ServerEnabled)
                TimerSimConnectRequest.Enabled = true;
            if (webCapture1.IsEnabled)
            {
                SimConnect.CoPilot.ZOOM_1X(); 
                TimerUpdateCamera.Enabled = true;
            }
        }
        #endregion
        #endregion

        #region Read/Write INI Settings
        /// <summary> Writes default settings if the settings.ini file does not exist</summary>
        void WriteSettings()
        {
            string path = G.settingsPath;// AppDomain.CurrentDomain.BaseDirectory + "settings.ini";

            if (File.Exists(path))  //don't overwrite existing file
                return;

            SimConnect.WriteINI(path);
            webCapture1.WriteINI(path);
        }

        /// <summary> Loads settings from the settings.ini file. </summary>
        void LoadSettings()
        {
            WriteSettings(); //make sure file exists
            string path = G.settingsPath;// AppDomain.CurrentDomain.BaseDirectory + "settings.ini";

            SimConnect.ReadINI(path);
            TimerSimConnectRequest.Interval = SimConnect.RequestRateMilliSeconds;

            webCapture1.ReadINI(path);

            chkEnableGoogleEarth.Checked = SimConnect.GoogleEarth.ServerEnabled;
        }
        #endregion

        #region Connect & Disconnect Buttons
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (SimConnect.OpenConnection())       //todo:make auto connect
                if (SimConnect.GoogleEarth.ServerEnabled)
                    SimConnect.GoogleEarth.LoadKmlInGoogleEarth();
            chkEnableGoogleEarth.Enabled = false; 
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            CloseAll();
            chkEnableGoogleEarth.Enabled = true;
        }
        #endregion 

        private void TimerSimConnectRequest_Tick(object sender, EventArgs e)
        {
            if (SimConnect.CoPilot.IsEnabled)
            {
                SimConnect.CoPilot.RequestSettableData();
                SimConnect.CoPilot.RequestReadOnlyData();
            }

            if (SimConnect.GoogleEarth.ServerEnabled)
            {
                SimConnect.GoogleEarth.RequestDataOnSimObjectType();
            }
        }

        #region CoPilot Buttons
        private void BtnAirSpeedSet_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtAirSpeed.Text, out float v))
                SimConnect.CoPilot.Hold.AirSpeed = v;
        }

        private void BtnAirSpeedOff_Click(object sender, EventArgs e)
        {
            txtAirSpeed.Text = "-1";
            SimConnect.CoPilot.Hold.AirSpeed = -1.0f;
        }

        private void BtnSimRateInc_Click(object sender, EventArgs e)
        {
            SimConnect.SimRate_Inc();
        }

        private void BtnSimRateDec_Click(object sender, EventArgs e)
        {
            SimConnect.SimRate_Dec();
        }

        private void BtnManifoldPressureSet_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtManifoldPressure.Text, out float v))
                SimConnect.CoPilot.Hold.ManifoldPressure = v;
        }

        private void BtnManifoldPressureOff_Click(object sender, EventArgs e)
        {
            txtManifoldPressure.Text = "-1";
            SimConnect.CoPilot.Hold.ManifoldPressure = -1;
        }

        private void BtnPropellerRPMSet_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtPropellerRPM.Text, out float v))
                SimConnect.CoPilot.Hold.PropRPM = v;
        }

        private void BtnPropellerRPMOff_Click(object sender, EventArgs e)
        {
            txtPropellerRPM.Text = "-1";
            SimConnect.CoPilot.Hold.PropRPM = -1;
        }

        private void BtnCowlFlapsSet_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtCowlFlaps.Text, out float v))
                SimConnect.CoPilot.Hold.CowlFlap = v;
        }

        private void BtnCowlFlapsOff_Click(object sender, EventArgs e)
        {
            txtCowlFlaps.Text = "-1";
            SimConnect.CoPilot.Hold.CowlFlap = -1;
        }
        #endregion

        #region WebCam
        private void BtnCalibrate_Click(object sender, EventArgs e)
        {
            webCapture1.DoCalibration();
        }

        int zoomlvl = 0;
        int zoomMax = 4;
        private void TimerUpdateCamera_Tick(object sender, EventArgs e)
        {
            float x = webCapture1.calibration.GetRotationLeftRight(webCapture1.TopMarker, webCapture1.BottomMarker);
            float y = webCapture1.calibration.GetRotationUpDown(webCapture1.TopMarker, webCapture1.BottomMarker);
            SimConnect.camera.SetPitchAndRotation(x, y);
            float Scale = webCapture1.calibration.scale / webCapture1.TrackerScale;
            Console.WriteLine(webCapture1.calibration.scale + " " + webCapture1.TrackerScale);
            if (Scale < webCapture1.calibration.scaleFactor)
            {
                if (zoomlvl < zoomMax)
                {
                    SimConnect.CoPilot.ZOOM_OUT();
                    zoomlvl++;
                }
            }
            else
            {
                if (zoomlvl > 0)
                {
                    SimConnect.CoPilot.ZOOM_IN();
                    zoomlvl--;
                }
                //if (webCapture1.TrackerScale< webCapture1.calibration.scale)
                //{
                //     webCapture1.calibration.scale= webCapture1.TrackerScale;
                //}
            }
        }
        #endregion

        private void btnStartTracker_Click(object sender, EventArgs e)
        {
            webCapture1.IsEnabled = !webCapture1.IsEnabled;
            if (webCapture1.IsEnabled)
            {
                btnCalibrate.Enabled = true;
                btnStartTracker.Text = "Stop tracker";
            }
            else
            {
                btnCalibrate.Enabled = false;
                btnStartTracker.Text = "Start tracker";
            }
        }

        private void chkEnableGoogleEarth_CheckedChanged(object sender, EventArgs e)
        {
            SimConnect.GoogleEarth.ServerEnabled = chkEnableGoogleEarth.Checked;
            IniFile.WriteKey(G.settingsPath, "ServerEnabled", SimConnect.GoogleEarth.ServerEnabled.ToString(), "GoogleEarth");
        }

        private void chkEnableCoPilot_CheckedChanged(object sender, EventArgs e)
        {
            SimConnect.CoPilot.IsEnabled = chkEnableCoPilot.Checked;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //SimConnect.CoPilot.TransmitEvent(Sim.EventID.AP_ALT_VAR_INC);
            SimConnect.CoPilot.myReadOnlyData.AUTOPILOT_ALTITUDE_LOCK_VAR = 6000;
        }
    }
}
