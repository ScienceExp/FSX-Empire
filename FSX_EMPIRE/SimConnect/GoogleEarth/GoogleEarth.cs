using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;

namespace Sim.Google
{
    /// <summary>Server to handle Google Earth Requests</summary>
    public class Earth
    {
        #region MyEvents
        public delegate void ServerStopped();
        public event ServerStopped OnServerStopped;

        public delegate void ServerStarted();
        public event ServerStarted OnServerStarted;

        public delegate void ServerWaitingForRequest();
        public event ServerWaitingForRequest OnServerWaitingForRequest;
        #endregion

        #region declerations
        /// <summary> To prevent 2 threads from accessing the same data </summary>
        static object lockObject = new object();

        /// <summary> Used to see if we should enable the server</summary>
        public bool ServerEnabled = true;

        /// <summary>Port to use with local server</summary>
        public int ServerPort = 7890;

        /// <summary>How fast Google Earth will request updates. 0.016 = 60 times per second</summary>
        public double RequestRate = 0.016;

        /// <summary> Holds a handle to google earth so we can close it in program if we wish. </summary>
        public Process ProcessGoogleEarth = null;

        /// <summary> Set 'ServerStop' to true and then call the server page to stop. </summary>
        bool stopServer = false;

        /// <summary> Used to tell if server is currently running before we try and start it. </summary>
        bool serverRunning = false;

        Enum myDefinitionID;
        Enum myRequestID;
        #endregion

        #region variables sent to google earth
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        /// <summary>in meters</summary
        public double Altitude { get; set; }
        /// <summary> Values range from 0 to 360 degrees. </summary>
        public double Heading { get; set; }
        /// <summary>Values range from 0(Down) to 180(up) [clamped]</summary>
        public double Tilt { get; set; }
        /// <summary>Values range from −180 to +180 degrees</summary>
        public double Roll { get; set; }
        #endregion

        #region Constructor
        public Earth(Enum DefinitionID, Enum RequestID)
        {
            myDefinitionID = DefinitionID;
            myRequestID = RequestID;
        }
        #endregion

        #region Read/Write INI Settings
        public void WriteINI(string path)
        {
            IniFile.WriteKey(path, "ServerEnabled", true.ToString(), "GoogleEarth");
            IniFile.WriteKey(path, "ServerPort", "7890", "GoogleEarth");
            IniFile.WriteKey(path, "RequestRateSeconds", "0.016", "GoogleEarth");
        }

        public void ReadINI(string path)
        {
            _ = bool.TryParse(IniFile.ReadKey(path, "ServerEnabled", "GoogleEarth"), out ServerEnabled);
            _ = int.TryParse(IniFile.ReadKey(path, "ServerPort", "GoogleEarth"), out ServerPort);
            _ = double.TryParse(IniFile.ReadKey(path, "RequestRateSeconds", "GoogleEarth"), out RequestRate);
        }
        #endregion 

        #region local server
        /// <summary> Starts the local server and sends the kml data on request.
        async Task StartServerAsync()
        {
            if (!ServerEnabled)
                return;

            if (serverRunning)
                return;

            serverRunning = true;

            try
            {
                stopServer = false;
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:" + ServerPort + "/FSX/");
                listener.Start();
                OnServerStarted();

                while (!stopServer)
                {
                    //Raise Event so we can get status updates on main form
                    OnServerWaitingForRequest();

                    //Server will wait on the next line until request is made
                    HttpListenerContext context = await listener.GetContextAsync();

                    //Build KML: https://developers.google.com/kml/documentation/kml_tut#network_links
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
                    sb.Append("<kml xmlns=\"http://www.opengis.net/kml/2.2\"\n");
                    sb.Append("xmlns:gx=\"http://www.google.com/kml/ext/2.2\">\n");
                    sb.Append("<Placemark>\n");
                    sb.Append("<name>Updated File</name>\n");
                    sb.Append("<Camera id=\"ID\">\n");

                    //lock so variables are not accessed on another thread
                    lock (lockObject)
                    {
                        sb.Append("<longitude>" + Longitude.ToString() + "</longitude>\n");
                        sb.Append("<latitude>" + Latitude.ToString() + "</latitude>\n");
                        sb.Append("<altitude>" + Altitude.ToString() + "</altitude>\n");
                        sb.Append("<heading>" + Heading.ToString() + "</heading>\n");
                        sb.Append("<tilt>" + Tilt.ToString() + "</tilt>\n");
                        sb.Append("<roll>" + Roll.ToString() + "</roll>\n");
                    }

                    sb.Append("<altitudeMode>absolute</altitudeMode>\n");
                    sb.Append("</Camera>\n");
                    sb.Append("</Placemark>\n");
                    sb.Append("</kml>\n");

                    string kml = sb.ToString();
                    context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(kml);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                    using (Stream stream = context.Response.OutputStream)
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            // Send kml to Google Earth
                            await writer.WriteAsync(kml);
                        }
                    }
                }

                listener.Stop();
                listener.Close();
                serverRunning = false;
                OnServerStopped();
            }
            catch (HttpListenerException e)
            {
                stopServer = true;
                MessageBox.Show(e.Message.ToString());
            }
        }

        /// <summary> Writes main KML NetworkLink file that will be opened in Google Earth. </summary>
        public void LoadKmlInGoogleEarth()
        {
            if (!ServerEnabled)
                return;

            string kmlFile = AppDomain.CurrentDomain.BaseDirectory + "NetworkLink.kml";

            //Build KML: https://developers.google.com/kml/documentation/kml_tut#network_links
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            sb.Append("<kml xmlns=\"http://www.opengis.net/kml/2.2\">\n");

            sb.Append("<Folder>\n");
            sb.Append("<name>Flight Sim</name>\n");
            sb.Append("<visibility>1</visibility>\n");
            sb.Append("<open>1</open>\n");
            sb.Append("<NetworkLink>\n");
            sb.Append("<name>Refresher</name>\n");
            sb.Append("<visibility>1</visibility>\n");
            sb.Append("<open>1</open>\n");
            sb.Append("<refreshVisibility>1</refreshVisibility>\n");
            sb.Append("<flyToView>1</flyToView>\n");
            sb.Append("<Link>\n");
            sb.Append("<href>" + "http://localhost:" + ServerPort + "/FSX/" + "</href>\n");
            sb.Append("<refreshMode>onInterval</refreshMode>\n");
            sb.Append("<refreshInterval>" + RequestRate.ToString() + "</refreshInterval>\n");
            //sb.Append("<minRefreshPeriod>" + RequestRate.ToString() + "</minRefreshPeriod>\n");
            sb.Append("</Link>\n");
            sb.Append("</NetworkLink>\n");
            sb.Append("</Folder>\n");
            sb.Append("</kml>\n");

            using (TextWriter tw = new StreamWriter(kmlFile))
            {
                // Write kml file to file on hard drive.
                tw.WriteAsync(sb.ToString());
            }

            // Open saved kml file in google earth
            ProcessGoogleEarth = Process.Start(kmlFile);

            _ = StartServerAsync();
        }

        /// <summary>Sets 'stopServer' to true and makes one last call to the server</summary>
        public async Task ServerStop()
        {
            if (!ServerEnabled)
                return;

            if (stopServer)
                return;

            stopServer = true;

            // The server needs a call to get past listener.GetContextAsync()
            WebRequest webRequest = WebRequest.Create("http://localhost:" + ServerPort + "/FSX/");
            webRequest.Method = "GET";

            int timeout = 1000; // add a timeout so it does not get stuck if server has closed already. [milliseconds]
            var task = webRequest.GetResponseAsync();
            _ = await Task.WhenAny(task, Task.Delay(timeout)) == task;
        }
        #endregion

        #region AddToDataDefinition
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct DataDefinition
        {
            public double PLANE_LATITUDE;
            public double PLANE_LONGITUDE;
            public double PLANE_ALTITUDE;
            public double PLANE_HEADING_DEGREES_TRUE;
            public double PLANE_PITCH_DEGREES;
            public double PLANE_BANK_DEGREES;
        }

        /// <summary>The AddToDataDefinition function is used to add a ESP simulation variable name to a client defined object definition.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_addtodatadefinition">Documentation</seealso></summary>
        public void AddToDataDefinition()
        {
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE LATITUDE", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE LONGITUDE", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE ALTITUDE", "Meters", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE HEADING DEGREES TRUE", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE PITCH DEGREES", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.GOOGLE_EARTH, "PLANE BANK DEGREES", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            G.simConnect.RegisterDataDefineStruct<DataDefinition>(DEFINITION.GOOGLE_EARTH);
        }
        #endregion 

        #region Request/recieve data from SimConnect
        /// <summary>The RequestDataOnSimObjectType function is used to retrieve information about 
        /// simulation objects of a given type that are within a specified radius of the user's aircraft.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_requestdataonsimobjecttype">Documentation</seealso></summary>
        public void RequestDataOnSimObjectType()
        {
            if (G.simConnect != null)
            {
                G.simConnect.RequestDataOnSimObjectType(
                    /*Specifies the ID of the client defined request. This is used later by the client to identify 
                     * which data has been received. This value should be unique for each request, 
                     * re-using a RequestID will overwrite any previous request using the same ID.*/
                    myRequestID,
                    /* Specifies the ID of the client defined data definition.*/
                    myDefinitionID,
                    /*Double word containing the radius in meters. If this is set to zero only information on the user
                     * aircraft will be returned, although this value is ignored if type is set to SIMCONNECT_SIMOBJECT_TYPE_USER.
                     * The error SIMCONNECT_EXCEPTION_OUT_OF_BOUNDS will be returned if a radius is given and it exceeds 
                     * the maximum allowed (200000 meters, or 200 Km).*/
                    0,
                    /*Specifies the type of object to receive information on. One member of the SIMCONNECT_SIMOBJECT_TYPE enumeration type.*/
                    SIMCONNECT_SIMOBJECT_TYPE.USER);
            }
        }

        /// <summary>Handles the data that has been revieved by a request and then sends back CoPilot data if needed.</summary>
        public void OnRecvSimobjectDataBytype(SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data, Camera camera)
        {
            DataDefinition sge = (DataDefinition)data.dwData[0];
            Attitude attitude = camera.CalculteRotations(sge);

            lock (lockObject)
            {
                Longitude = sge.PLANE_LONGITUDE;
                Latitude = sge.PLANE_LATITUDE;
                Altitude = sge.PLANE_ALTITUDE;

                Heading = attitude.Heading;
                Tilt = attitude.Pitch;
                Roll = attitude.Bank;
            }
        }
        #endregion
    }
}
