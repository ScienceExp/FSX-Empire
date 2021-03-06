using Microsoft.FlightSimulator.SimConnect;
using Microsoft.Win32;
using System;
/// <summary> Global variables that can be accessed by any class </summary>
class G
{
    public static FSX_EMPIRE.Speech speechSynth;
    public static FSX_EMPIRE.SpeechRecognition speechRecognition;

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
    /// <summary>Path to Settings.INI</summary>
    public static string settingsPath = AppDomain.CurrentDomain.BaseDirectory + "Settings.ini";

    /// <summary>Path to CoPilotCommands</summary>
    public static string CoPilotCommandsPath = AppDomain.CurrentDomain.BaseDirectory + "CoPilotCommands.ini";

    #endregion

    #region aircrft.cfg
    public static string FSXAircraftCFGFile { get; set; }
    public static string FSXPlaneName { get; set; }
    #endregion
}
