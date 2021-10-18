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
            webCapture1.Start();

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
            _ = Google.ServerStop();
            FSX_SimConnect1.CloseConnection();
            G.speechSynth.Dispose();
            G.speechRecognition.Dispose();
        }
        #endregion

        #region My Events
        void InitializeMyEventsHandlers()
        {
            Google.OnServerStopped += new Google.ServerStopped(this.ServerStopped);
            Google.OnServerStarted += new Google.ServerStarted(this.ServerStarted);
            Google.OnServerWaitingForRequest += new Google.ServerWaitingForRequest(this.ServerWaitingForRequest);
            FSX_SimConnect1.OnFsxConnectionOpen += new FSX_SimConnect.FsxConnectionOpen(this.FsxConnectionOpen);
            FSX_SimConnect1.OnFsxConnectionClosed += new FSX_SimConnect.FsxConnectionClosed(this.FsxConnectionClosed);
            FSX_SimConnect1.OnFsxPaused += new FSX_SimConnect.FsxPaused(this.FsxPaused);
            FSX_SimConnect1.OnFsxUnpaused += new FSX_SimConnect.FsxUnpaused(this.FsxUnpaused);
            FSX_SimConnect1.OnFsxSimStart += new FSX_SimConnect.FsxSimStart(this.FsxSimStart);
            FSX_SimConnect1.OnFsxSimStop += new FSX_SimConnect.FsxSimStop(this.FsxSimStop);
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
            TimerSimConnectRequest.Enabled = true;
            if (webCapture1.isEnabled)
                TimerUpdateCamera.Enabled = true;
        }
        #endregion
        #endregion

        #region Settings.ini
        /// <summary> Writes default settings if the settings.ini file does not exist</summary>
        void WriteSettings()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";

            if (File.Exists(path))  //don't overwrite existing file
                return;

            #region Write GoogleEarth
            IniFile.WriteKey(path, "ServerEnabled", true.ToString(), "GoogleEarth");
            IniFile.WriteKey(path, "ServerPort", "7890", "GoogleEarth");
            IniFile.WriteKey(path, "RequestRateSeconds", "0.016", "GoogleEarth");
            #endregion

            #region Write SimConnect
            IniFile.WriteKey(path, "RequestRateMilliSeconds", "16", "SimConnect");
            IniFile.WriteKey(path, "CameraLeftKey", "VK_COMMA", "SimConnect");
            IniFile.WriteKey(path, "CameraRightKey", "VK_PERIOD", "SimConnect");
            IniFile.WriteKey(path, "CameraUpKey", "M", "SimConnect");
            IniFile.WriteKey(path, "CameraDownKey", "N", "SimConnect");
            #endregion

            #region Write WebCamTracker
            IniFile.WriteKey(path, "CameraEnabled", true.ToString(), "WebCamTracker");
            IniFile.WriteKey(path, "FPS", "30", "WebCamTracker");
            IniFile.WriteKey(path, "VerticalResoltion", "720", "WebCamTracker");
            IniFile.WriteKey(path, "HorizontalResoltion", "1280", "WebCamTracker");
            IniFile.WriteKey(path, "ShowDebug", false.ToString(), "WebCamTracker");
            IniFile.WriteKey(path, "MarkerSize", "32", "WebCamTracker");
            IniFile.WriteKey(path, "MarkerMinMatch", "0.3", "WebCamTracker");
            #endregion 
        }
        /// <summary> Loads settings from the settings.ini file. </summary>
        void LoadSettings()
        {
            WriteSettings(); //make sure file exists
            string path = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";

            #region Read GoogleEarth
            _ = bool.TryParse(IniFile.ReadKey(path, "ServerEnabled", "GoogleEarth"), out Google.ServerEnabled);
            _ = int.TryParse(IniFile.ReadKey(path, "ServerPort", "GoogleEarth"), out Google.ServerPort);
            _ = double.TryParse(IniFile.ReadKey(path, "RequestRateSeconds", "GoogleEarth"), out Google.RequestRate);
            #endregion 

            #region Read SimConnect
            if (int.TryParse(IniFile.ReadKey(path, "RequestRateMilliSeconds", "SimConnect"), out int i))
                TimerSimConnectRequest.Interval = i;

            G.camera.KeyLeft = IniFile.ReadKey(path, "CameraLeftKey", "SimConnect");
            G.camera.KeyRight = IniFile.ReadKey(path, "CameraRightKey", "SimConnect");
            G.camera.KeyUp = IniFile.ReadKey(path, "CameraUpKey", "SimConnect");
            G.camera.KeyDown = IniFile.ReadKey(path, "CameraDownKey", "SimConnect");
            #endregion

            #region Read WebCamTracker
            _ = bool.TryParse(IniFile.ReadKey(path, "CameraEnabled", "WebCamTracker"), out bool isEnabled);
            webCapture1.isEnabled = isEnabled;
            _ = int.TryParse(IniFile.ReadKey(path, "FPS", "WebCamTracker"), out int FPS);
            webCapture1.CaptureFPS = FPS;
            _ = int.TryParse(IniFile.ReadKey(path, "VerticalResoltion", "WebCamTracker"), out int verticalResolution);
            webCapture1.VerticalResoltion = verticalResolution;
            _ = int.TryParse(IniFile.ReadKey(path, "HorizontalResoltion", "WebCamTracker"), out int horizontalResolution);
            webCapture1.HorizontalResoltion = horizontalResolution;
            _ = bool.TryParse(IniFile.ReadKey(path, "ShowDebug", "WebCamTracker"), out bool showDebug);
            webCapture1.ShowDebug = showDebug;
            _ = int.TryParse(IniFile.ReadKey(path, "MarkerSize", "WebCamTracker"), out int markerSize);
            webCapture1.MarkerSize = markerSize;
            _ = float.TryParse(IniFile.ReadKey(path, "MarkerMinMatch", "WebCamTracker"), out float markerMinMatch);
            webCapture1.MarkerMinMatch = markerMinMatch;
            #endregion
        }
        #endregion

        #region Connect & Disconnect Buttons
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (FSX_SimConnect1.OpenConnection())       //todo:make auto connect
                if (Google.ServerEnabled)
                    Google.LoadKmlInGoogleEarth();
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            CloseAll();
        }
        #endregion 

        private void TimerSimConnectRequest_Tick(object sender, EventArgs e)
        {
            FSX_SimConnect1.RequestDataForCoPilot();
            FSX_SimConnect1.RequestDataForGoogleEarth();
        }

        #region CoPilot Buttons
        private void BtnAirSpeedSet_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtAirSpeed.Text, out double v))
                CoPilot.Hold.AirSpeed = v;
        }

        private void BtnAirSpeedOff_Click(object sender, EventArgs e)
        {
            txtAirSpeed.Text = "-1";
            CoPilot.Hold.AirSpeed = -1.0;
        }

        private void BtnSimRateInc_Click(object sender, EventArgs e)
        {
            FSX_SimConnect1.SimRate_Inc();
        }

        private void BtnSimRateDec_Click(object sender, EventArgs e)
        {
            FSX_SimConnect1.SimRate_Dec();
        }

        private void BtnManifoldPressureSet_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtManifoldPressure.Text, out double v))
                CoPilot.Hold.ManifoldPressure = v;
        }

        private void BtnManifoldPressureOff_Click(object sender, EventArgs e)
        {
            txtManifoldPressure.Text = "-1";
            CoPilot.Hold.ManifoldPressure = -1;
        }

        private void BtnPropellerRPMSet_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPropellerRPM.Text, out double v))
                CoPilot.Hold.PropRPM = v;
        }

        private void BtnPropellerRPMOff_Click(object sender, EventArgs e)
        {
            txtPropellerRPM.Text = "-1";
            CoPilot.Hold.PropRPM = -1;
        }

        private void BtnCowlFlapsSet_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtCowlFlaps.Text, out double v))
                CoPilot.Hold.CowlFlap = v;
        }

        private void BtnCowlFlapsOff_Click(object sender, EventArgs e)
        {
            txtCowlFlaps.Text = "-1";
            CoPilot.Hold.CowlFlap = -1;
        }
        #endregion

        #region WebCam
        //bool CalibrationComplete = false;
        private void BtnCalibrate_Click(object sender, EventArgs e)
        {
            webCapture1.DoCalibration();
            //todo probably save calibration? (More accurte to do each time though)
            //CalibrationComplete = true;
        }
        #endregion

        private void TimerUpdateCamera_Tick(object sender, EventArgs e)
        {
            //if (CalibrationComplete)
            //{
            //Console.WriteLine(webCapture1.TopMarker.X + " " + webCapture1.TopMarker.Y);
            //Console.WriteLine(webCapture1.BottomMarker.X + " " + webCapture1.BottomMarker.Y);
            float x = webCapture1.calibration.GetRotationLeftRight(webCapture1.TopMarker, webCapture1.BottomMarker);
                float y = webCapture1.calibration.GetRotationUpDown(webCapture1.TopMarker, webCapture1.BottomMarker);
                G.camera.SetPitchAndRotation(x, y);
            Console.WriteLine(x + " " + y);
            //}
        }
    }
}
