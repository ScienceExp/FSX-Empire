using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim
{
    enum SystemEventID : uint
    {
        #region General events
        /// <summary>Request a notification every second. string = "1sec"</summary>
        OneSec,
        /// <summary>Request a notification every four seconds. string = "4sec"</summary>
        FourSec,
        /// <summary>Request notifications six times per second. This is the same rate that joystick movement events are transmitted. string = "6Hz"</summary>
        SixHz,
        /// <summary>Request a notification when the aircraft flight dynamics file is changed. These files have a .AIR extension. The filename is returned in a SIMCONNECT_RECV_EVENT_FILENAME structure.</summary>
        AircraftLoaded,
        /// <summary>Request a notification if the user aircraft crashes.</summary>
        Crashed,
        /// <summary>Request a notification when the crash cut-scene has completed.</summary>
        CrashReset,
        /// <summary>"Request a notification when a flight is loaded. Note that when a flight is ended</summary>
        FlightLoaded,
        /// <summary>Request a notification when a flight is saved correctly. The filename of the flight saved is returned in a SIMCONNECT_RECV_EVENT_FILENAME structure.</summary>
        FlightSaved,
        /// <summary>Request a notification when a new flight plan is activated. The filename of the activated flight plan is returned in a SIMCONNECT_RECV_EVENT_FILENAME structure.</summary>
        FlightPlanActivated,
        /// <summary>Request a notification when the active flight plan is de-activated.</summary>
        FlightPlanDeactivated,
        /// <summary>Request notifications every visual frame. Information is returned in a SIMCONNECT_RECV_EVENT_FRAME structure.</summary>
        Frame,
        /// <summary>"Request notifications when the flight is paused or unpaused</summary>
        Pause,
        /// <summary>Request a notification when the flight is paused.</summary>
        Paused,
        /// <summary>Request notifications for every visual frame that the simulation is paused. Information is returned in a SIMCONNECT_RECV_EVENT_FRAME structure.</summary>
        PauseFrame,
        /// <summary>Request a notification when the user changes the position of their aircraft through a dialog.</summary>
        PositionChanged,
        /// <summary>"Request notifications when the flight is running or not</summary>
        Sim,
        /// <summary>"The simulator is running. Typically the user is actively controlling the aircraft on the ground or in the air. However</summary>
        SimStart,
        /// <summary>"The simulator is not running. Typically the user is loading a flight</summary>
        SimStop,
        /// <summary>"Requests a notification when the master sound switch is changed. This request will also return the current state of the master sound switch immediately. A flag is returned in the dwData parameter</summary>
        Sound,
        /// <summary>Request a notification when the flight is un-paused.</summary>
        Unpaused,
        /// <summary>"Requests a notification when the user aircraft view is changed. This request will also return the current view immediately. A flag is returned in the dwData parameter</summary>
        View,
        /// <summary>Request a notification when the weather mode is changed.</summary>
        WeatherModeChanged,
        #endregion

        #region AI Specific events
        /// <summary>Request a notification when an AI object is added to the simulation. Refer also to the SIMCONNECT_RECV_EVENT_OBJECT_ADDREMOVE structure.</summary>
        ObjectAdded,
        /// <summary>Request a notification when an AI object is removed from the simulation. Refer also to the SIMCONNECT_RECV_EVENT_OBJECT_ADDREMOVE structure.</summary>
        ObjectRemoved,
        #endregion

        #region Mission Specific events
        /// <summary>Request a notification when the user has completed a mission. Refer also to the SIMCONNECT_MISSION_END enum.</summary>
        MissionCompleted,
        /// <summary>Request a notification when a mission action has been executed.Refer also to the SimConnect_CompleteCustomMissionAction function.</summary>
        CustomMissionActionExecuted,
        #endregion

        #region Multi-player Racing Events
        /// <summary>Used by a client to request a notification that they have successfully joined a multiplayer race. The event is returned as a SIMCONNECT_RECV_EVENT_MULTIPLAYER_CLIENT_STARTED structure. This event is only sent to the client, not the host of the session.</summary>
        MultiplayerClientStarted,
        /// <summary>Used by a host of a multiplayer race to request a notification when the race is open to other players in the lobby. The event is returned in a SIMCONNECT_RECV_EVENT_MULTIPLAYER_SERVER_STARTED structure.</summary>
        MultiplayerServerStarted,
        /// <summary>Request a notification when the mutliplayer race session is terminated. The event is returned in a SIMCONNECT_RECV_EVENT_MULTIPLAYER_SESSION_ENDED structure. If a client player leaves a race, this event wil be returned just to the client. If a host leaves or terminates the session, then all players will receive this event. This is the only event that will be broadcast to all players.</summary>
        MultiplayerSessionEnded,
        /// <summary>Request a notification of the race results for each racer. The results will be returned in SIMCONNECT_RECV_EVENT_RACE_END structures, one for each player.</summary>
        RaceEnd,
        /// <summary>Request a notification of the race results for each racer. The results will be returned in SIMCONNECT_RECV_EVENT_RACE_LAP structures, one for each player.</summary>
        RaceLap,
        #endregion
    }
}
