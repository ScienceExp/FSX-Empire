using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System;
using Microsoft.FlightSimulator.SimConnect;

namespace FSX_EMPIRE
{
    /// <summary>Server to handle Google Earth Requests</summary>
    class Google
    {
        #region delegates
        public delegate void ServerStopped();
        public static event ServerStopped OnServerStopped;

        public delegate void ServerStarted();
        public static event ServerStarted OnServerStarted;

        public delegate void ServerWaitingForRequest();
        public static event ServerWaitingForRequest OnServerWaitingForRequest;
        #endregion

        #region declerations
        /// <summary> Used to see if we should enable the server</summary>
        public static bool ServerEnabled = true;

        /// <summary>Port to use with local server</summary>
        public static int ServerPort = 7890;

        /// <summary>How fast Google Earth will request updates. 0.016 = 60 times per second</summary>
        public static double RequestRate = 0.016;

        /// <summary> Holds a handle to google earth so we can close it in program if we wish. </summary>
        public static Process ProcessGoogleEarth = null;

        /// <summary> Set 'ServerStop' to true and then call the server page to stop. </summary>
        static bool stopServer = false;

        /// <summary> Used to tell if server is currently running before we try and start it. </summary>
        static bool serverRunning = false;
        #endregion

        #region variables sent to google earth
        public static double Longitude { get; set; }
        public static double Latitude { get; set; }
        /// <summary>in meters</summary
        public static double Altitude { get; set; }
        /// <summary> Values range from 0 to 360 degrees. </summary>
        public static double Heading { get; set; }
        /// <summary>Values range from 0(Down) to 180(up) [clamped]</summary>
        public static double Tilt { get; set; }
        /// <summary>Values range from −180 to +180 degrees</summary>
        public static double Roll { get; set; }
        #endregion

        #region local server
        /// <summary> Starts the local server and sends the kml data on request.
        static async Task StartServerAsync()
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
                    lock (G.lockObject)
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
        public static void LoadKmlInGoogleEarth()
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
        public static async Task ServerStop()
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

        #region update values
        /// <summary>Updates variables that will be sent to google earth</summary>
        public static void UpdateValues(SGoogleEarth sge)
        {
            Attitude attitude = G.camera.CalculteRotations(sge);

            lock (G.lockObject)
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

        #region request data from SimConnect
        /// <summary>RequestDataOnSimObjectType</summary>
        public static void RequestDataOnSimObjectType()
        {
            G.simConnect.RequestDataOnSimObjectType(
                REQUEST.GOOGLE_EARTH,
                DEFINITION.GOOGLE_EARTH,
                0,
                SIMCONNECT_SIMOBJECT_TYPE.USER);
        }
        #endregion 
    }
}
