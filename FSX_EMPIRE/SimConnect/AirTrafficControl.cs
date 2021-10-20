using Microsoft.FlightSimulator.SimConnect;

namespace Sim
{
    class AirTrafficControl
    {
        public void SetupEvents()
        {
            MapClientEventToSimEvent(EventID.ATC);
            MapClientEventToSimEvent(EventID.ATC_MENU_1);
            MapClientEventToSimEvent(EventID.ATC_MENU_2);
            MapClientEventToSimEvent(EventID.ATC_MENU_3);
            MapClientEventToSimEvent(EventID.ATC_MENU_4);
            MapClientEventToSimEvent(EventID.ATC_MENU_5);
            MapClientEventToSimEvent(EventID.ATC_MENU_6);
            MapClientEventToSimEvent(EventID.ATC_MENU_7);
            MapClientEventToSimEvent(EventID.ATC_MENU_8);
            MapClientEventToSimEvent(EventID.ATC_MENU_9);
            MapClientEventToSimEvent(EventID.ATC_MENU_0); //10
        }

        /// <summary>Used to associate a client defined event ID with an ESP event name.</summary>
        void MapClientEventToSimEvent(EventID eventID)
        {
            G.simConnect.MapClientEventToSimEvent(
                //Specifies the ID of the client event.
                eventID,
                //Specifies the ESP event name. Refer to the Event IDs document for a list of event names (listed under String Name).
                eventID.ToString());
        }

        void ATC()
        {
            TransmitClientEvent(EventID.ATC);
        }

        /// <summary>used to request that the ESP server transmit to all SimConnect clients the specified client event.</summary>
        void TransmitClientEvent(EventID eventID)
        {
            G.simConnect.TransmitClientEvent(
                /* Specifies the ID of the server defined object. If this parameter is set to SIMCONNECT_OBJECT_ID_USER,
                 * then the transmitted event will be sent to the other clients in priority order. 
                 * If this parameters contains another object ID, then the event will be sent direct to that sim-object,
                 * and no other clients will receive it.*/
                (uint)SimConnect.SIMCONNECT_OBJECT_ID_USER,
                //Specifies the ID of the client event.
                eventID,
                /* Double word containing any additional number required by the event. This is often zero. 
                 * If the event is a ESP event, then refer to the Event IDs document for information on this additional
                 * value. If the event is a custom event, then any value put in this parameter will be available to 
                 * the clients that receive the event.*/
                (uint)0,
                /* The default behavior is that this specifies the GroupID of the event. The SimConnect server will use 
                 * the priority of this group to send the message to all clients with a lower priority. To receive the 
                 * event notification other SimConnect clients must have subscribed to receive the event.  
                 * See the explanation of SimConnect Priorities. The exception to the default behavior is set by the 
                 * SIMCONNECT_EVENT_FLAG_GROUPID_IS_PRIORITY flag, described below.*/
                SIMCONNECT_GROUP_PRIORITY.DEFAULT,
                /*Indicates to the SimConnect server to treat the GroupID as a priority value. If this flag is set,
                 * and the GroupID parameter is set to SIMCONNECT_GROUP_PRIORITY_HIGHEST then all client notification
                 * groups that have subscribed to the event will receive the notification (unless one of them masks it).
                 * The event will be transmitted to clients starting at the priority specified in the GroupID parameter,
                 * though this can result in the client that transmitted the event, receiving it again.*/
                SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY
                );
        }
    }
}
