using Microsoft.FlightSimulator.SimConnect;
using Microsoft.Win32;

namespace FSX_EMPIRE
{
    /// <summary> Global variables that can be accessed by any class </summary>
    class G
    {
        /// <summary> Class for adjusting and syncing cameras in FSX and Google earth </summary>
        public static Camera camera = new Camera();

        /// <summary> Holds info for the current plane you are flying </summary>
        public static Plane myPlane = new Plane();

        /// <summary> SimConnect object </summary>
        public static SimConnect simConnect = null;

        #region file paths
        /// <summary>FSX path key in registry</summary>
        const string RegKeyFSX = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft Games\\flight simulator\\10.0";

        /// <summary>Main FSX folder</summary>
        public static string FSXPath
        {
            get { return (string)Registry.GetValue(RegKeyFSX, "SetupPath", ""); }
        }

        /// <summary>Path to Aircraft.cfg</summary>
        public static string FSXAircraftPath { get { return FSXPath + @"\SimObjects\Airplanes\"; } }
        #endregion

        #region aircrft.cfg
        public static string FSXAircraftCFGFile { get; set; }
        public static string FSXPlaneName { get; set; }
        #endregion

        /// <summary> To prevent 2 threads from accessing the same data </summary>
        public static object lockObject = new object();
    }
}
