using System.Collections.Generic;

namespace Sim
{
    /// <summary>Handles everything to do with SimConnect System Events. <br></br>
    /// Add events to the "Subscribe" function.<br></br> Then handle the events in the CheckEvents() function.<br></br>
    /// <seealso href="https://msdn.microsoft.com/en-us/library/cc526983.aspx#SimConnect_SubscribeToSystemEvent">Documentation</seealso></summary>
    public class SystemEvent
    {
        /// <summary>Holds the subscribed events for easy unsubscribe</summary>
        List<SystemEventID> SubscribedEvents;

        /// <summary> Holds the paused state of FSX </summary>
        public static bool IsPaused = false;

        #region Constructor
        public SystemEvent()
        {
            SubscribedEvents = new List<SystemEventID>();
        }
        #endregion

        #region Subscribe & Unsubscribe
        /// <summary>The SubscribeToSystemEvent function is used to request that a specific system event is notified to the client.
        /// <seealso href="https://msdn.microsoft.com/en-us/library/cc526983.aspx#SimConnect_SubscribeToSystemEvent">Documentation</seealso></summary>
        public void Subscribe()
        {
            SubscribeToSystemEvent(SystemEventID.SimStart);
            SubscribeToSystemEvent(SystemEventID.SimStop);
            SubscribeToSystemEvent(SystemEventID.Paused);
            SubscribeToSystemEvent(SystemEventID.Unpaused);
        }

        /// <summary>The SubscribeToSystemEvent function is used to request that a specific system event is notified to the client.
        /// <seealso href="https://msdn.microsoft.com/en-us/library/cc526983.aspx#SimConnect_SubscribeToSystemEvent">Documentation</seealso></summary>
        void SubscribeToSystemEvent(SystemEventID eventID)
        {
            SubscribedEvents.Add(eventID);  // Keep track of eventID's for Unsubscribe

            G.simConnect.SubscribeToSystemEvent(
                /*[in]  Specifies the client-defined event ID.*/
                eventID,
                /*[in]  The string name for the requested system event, which should be one from the following table 
                 * (note that the event names are not case-sensitive). Unless otherwise stated in the Description, 
                 * notifications of the event are returned in a SIMCONNECT_RECV_EVENT structure 
                 * (identify the event from the EventID given with this function).*/
                eventID.ToString());
        }

        /// <summary>The UnsubscribeFromSystemEvent function is used to request that notifications are no longer received for the specified system event.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)?redirectedfrom=MSDN#simconnect_unsubscribefromsystemevent">Documentation</seealso></summary>
        public void UnsubscribeAll()
        {
            foreach (SystemEventID eventID in SubscribedEvents)
            {
                G.simConnect.UnsubscribeFromSystemEvent(eventID);
            }
            SubscribedEvents.Clear();
        }
        #endregion

        #region HandleEvents
        /// <summary>Checks a SIMCONNECT_RECV_EVENT.uEventID to see if it is a FSX System Event. 
        /// If so, it handles the event and returns true. Otherwise it returns false.</summary>
        public bool OnRecvEvent(uint uEventID)
        {
            switch (uEventID)
            {
                case (uint)SystemEventID.SimStart:
                    {
                        OnSimStart();
                        return true;
                    }
                case (uint)SystemEventID.SimStop:
                    {
                        OnSimStop();
                        return true;
                    }
                case (uint)SystemEventID.Paused:
                    {
                        IsPaused = true;
                        OnPaused();
                        return true;
                    }
                case (uint)SystemEventID.Unpaused:
                    {
                        IsPaused = false;
                        OnUnpaused();
                        return true;
                    }
            }
            return false;
        }
        #endregion

        #region My Events (Tried to keep same naming as SimConnect)
        /// <summary>Fired when FSX is paused</summary>
        public delegate void Paused();
        /// <summary>Fired when FSX is paused</summary>
        public event Paused OnPaused;

        /// <summary>Fired when FSX is unpaused</summary>
        public delegate void Unpaused();
        /// <summary>Fired when FSX is unpaused</summary>
        public event Unpaused OnUnpaused;

        /// <summary>Fired when FSX starts</summary>
        public delegate void SimStart();
        /// <summary>Fired when FSX starts</summary>
        public event SimStart OnSimStart;

        /// <summary>Fired when FSX stops</summary>
        public delegate void SimStop();
        /// <summary>Fired when FSX stops</summary>
        public event SimStop OnSimStop;
        #endregion
    }
}