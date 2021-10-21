using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Sim.CoPilot
{
    /// <summary>This is where the CoPilot does his work</summary>
    public class MyCoPilot
    {
        #region Declerations
        public bool IsEnabled { get; set; }

        /// <summary> Where CoPilot should try and keep values at. '-1' means ignore </summary>
        public Hold Hold = new Hold();

        /// <summary> Amount CoPilot will adjust values by </summary>
        public Increment Increment;

        Dictionary<string, Action<float>> SettableCommands;
        Dictionary<int, Action<DataDefinition>> SettableFunctions;
        #endregion

        #region Constructor
        public MyCoPilot()
        {
            Increment = new Increment();
            myData = new DataDefinition();
            myReadOnlyData = new ReadOnlyDefinition();

            SettableFunctions = new Dictionary<int, Action<DataDefinition>>();
            SettableCommands = new Dictionary<string, Action<float>>();
        }
        #endregion 

        #region Read Only and Transmit Data
        #region Setup
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct ReadOnlyDefinition
        {
            public double AUTOPILOT_ALTITUDE_LOCK_VAR;
        }

        public ReadOnlyDefinition myReadOnlyData;

        void MapClientEventToSimEvent(EventID ID)
        {
            G.simConnect.MapClientEventToSimEvent(
                ID,
                ID.ToString());
        }

        public void AddReadonlyDefinitions()
        {
            MapClientEventToSimEvent(EventID.AUTOPILOT_ON);
            MapClientEventToSimEvent(EventID.AUTOPILOT_OFF);
            MapClientEventToSimEvent(EventID.AP_PANEL_ALTITUDE_ON);
            MapClientEventToSimEvent(EventID.AP_PANEL_ALTITUDE_OFF);
            MapClientEventToSimEvent(EventID.AP_ALT_VAR_INC);
            MapClientEventToSimEvent(EventID.AP_ALT_VAR_DEC);
            MapClientEventToSimEvent(EventID.ZOOM_IN);
            MapClientEventToSimEvent(EventID.ZOOM_OUT);
            MapClientEventToSimEvent(EventID.ZOOM_1X);

            G.simConnect.AddToDataDefinition(DEFINITION.CoPilotReadOnly, "AUTOPILOT ALTITUDE LOCK VAR", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            G.simConnect.RegisterDataDefineStruct<ReadOnlyDefinition>(DEFINITION.CoPilotReadOnly);
        }

        #endregion 

        #region Request, Recieve, Transmit, Data
        /// <summary>The RequestDataOnSimObjectType function is used to retrieve information about 
        /// simulation objects of a given type that are within a specified radius of the user's aircraft.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_requestdataonsimobjecttype">Documentation</seealso></summary>
        public void RequestReadOnlyData()
        {
            if (G.simConnect != null)
            {
                G.simConnect.RequestDataOnSimObjectType(
                /*Specifies the ID of the client defined request. This is used later by the client to identify 
                 * which data has been received. This value should be unique for each request, 
                 * re-using a RequestID will overwrite any previous request using the same ID.*/
                REQUEST.CoPilotReadOnly,
                /* Specifies the ID of the client defined data definition.*/
                DEFINITION.CoPilotReadOnly,
                /*Double word containing the radius in meters. If this is set to zero only information on the user
                 * aircraft will be returned, although this value is ignored if type is set to SIMCONNECT_SIMOBJECT_TYPE_USER.
                 * The error SIMCONNECT_EXCEPTION_OUT_OF_BOUNDS will be returned if a radius is given and it exceeds 
                 * the maximum allowed (200000 meters, or 200 Km).*/
                0,
                /*Specifies the type of object to receive information on. One member of the SIMCONNECT_SIMOBJECT_TYPE enumeration type.*/
                SIMCONNECT_SIMOBJECT_TYPE.USER);
            }
        }

        /// <summary>Even though this data is read only data is still transmitted to adjust it</summary>
        public void OnRecvReadOnlyData(SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
                ReadOnlyDefinition r = (ReadOnlyDefinition)data.dwData[0];
                AUTOPILOT_ALTITUDE_LOCK_VAR(r);
        }

        public void TransmitEvent(EventID ID, uint dwData = 0)
        {
            if (G.simConnect != null)
            {
                G.simConnect.TransmitClientEvent(
                SimConnect.SIMCONNECT_OBJECT_ID_USER,
                ID,
                dwData,
                NotificationGroup.COPILOT,
                SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY
                );
            }
        }
        #endregion 

        #region CoPilot Adjust Functions
        void AUTOPILOT_ALTITUDE_LOCK_VAR(ReadOnlyDefinition data)
        {
            if (myReadOnlyData.AUTOPILOT_ALTITUDE_LOCK_VAR == data.AUTOPILOT_ALTITUDE_LOCK_VAR)
                return;

            if (myReadOnlyData.AUTOPILOT_ALTITUDE_LOCK_VAR > data.AUTOPILOT_ALTITUDE_LOCK_VAR)
            {
                TransmitEvent(EventID.AP_ALT_VAR_INC);
            }
            else
            {
                TransmitEvent(EventID.AP_ALT_VAR_DEC);
            }
        }

        public void ZOOM_IN()
        {

            TransmitEvent(EventID.ZOOM_IN);
        }

        public void ZOOM_OUT()
        {

            TransmitEvent(EventID.ZOOM_OUT);
        }


        public void ZOOM_1X()
        {

            TransmitEvent(EventID.ZOOM_1X);
        }

        #endregion
        #endregion

        #region Settable Data

        #region Setup
        /// <summary>Should we SetDataOnSimObject</summary>
        bool SendData = false;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
         struct DataDefinition
        {
            public double AIRSPEED_INDICATED;
            public double INDICATED_ALTITUDE;
            public double ENG_MANIFOLD_PRESSURE_1;
            public double ENG_MANIFOLD_PRESSURE_2;
            public double GENERAL_ENG_THROTTLE_LEVER_POSITION_1;
            public double GENERAL_ENG_THROTTLE_LEVER_POSITION_2;
            public double PROP_RPM_1;
            public double PROP_RPM_2;
            public double GENERAL_ENG_PROPELLER_LEVER_POSITION_1;
            public double GENERAL_ENG_PROPELLER_LEVER_POSITION_2;
            public double RECIP_ENG_COWL_FLAP_POSITION_1;
            public double RECIP_ENG_COWL_FLAP_POSITION_2;
        }

        DataDefinition myData;

        /// <summary>The AddToDataDefinition function is used to add a ESP simulation variable name to a client defined object definition.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_addtodatadefinition">Documentation</seealso></summary>
        public void AddSettableDefinitions()
        {
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "AIRSPEED INDICATED", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "INDICATED ALTITUDE", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "ENG MANIFOLD PRESSURE:1", "inHG", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "ENG MANIFOLD PRESSURE:2", "inHG", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "GENERAL ENG THROTTLE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "GENERAL ENG THROTTLE LEVER POSITION:2", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "PROP RPM:1", "Rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "PROP RPM:2", "Rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "GENERAL ENG PROPELLER LEVER POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "GENERAL ENG PROPELLER LEVER POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "RECIP ENG COWL FLAP POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(DEFINITION.COPILOT, "RECIP ENG COWL FLAP POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);


            G.simConnect.RegisterDataDefineStruct<DataDefinition>(DEFINITION.COPILOT);
        }
        #endregion 

        #region Request, Recieve, Set Data
        /// <summary>The RequestDataOnSimObjectType function is used to retrieve information about 
        /// simulation objects of a given type that are within a specified radius of the user's aircraft.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_requestdataonsimobjecttype">Documentation</seealso></summary>
        public void RequestSettableData()
        {
            if (G.simConnect != null)
            {
                G.simConnect.RequestDataOnSimObjectType(
                /*Specifies the ID of the client defined request. This is used later by the client to identify 
                 * which data has been received. This value should be unique for each request, 
                 * re-using a RequestID will overwrite any previous request using the same ID.*/
                REQUEST.COPILOT,
                /* Specifies the ID of the client defined data definition.*/
                DEFINITION.COPILOT,
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
        public void OnRecvSettableData(SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {

            DataDefinition d = (DataDefinition)data.dwData[0];

            foreach (var item in SettableFunctions )
            {
                SettableFunctions[item.Key](d);
                //Console.WriteLine(item.Key);
            }

            //if data was changed then send the updated Data
            if (SendData)
            {
                SendData = false;
                SetSettableData();
            }
        }

        /// <summary>The SetDataOnSimObject function is used to make changes to the data properties of an object.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_setdataonsimobject">Documentation</seealso></summary>
        void SetSettableData()
        {
            G.simConnect.SetDataOnSimObject(
                    /*Specifies the ID of the client defined data definition.*/
                    DEFINITION.COPILOT,
                    /*Specifies the ID of the ESP object that the data should be about. 
                     * This ID can be SIMCONNECT_OBJECT_ID_USER (to specify the user's aircraft) or obtained from 
                     * a SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE structure after a call to SimConnect_RequestDataOnSimObjectType.
                     * Also refer to the note on multiplayer mode in the remarks for SimConnect_RequestDataOnSimObject.*/
                    SimConnect.SIMCONNECT_OBJECT_ID_USER,
                    /*Null, or one or more of the following flags.*/
                    SIMCONNECT_DATA_SET_FLAG.DEFAULT,
                    /*Pointer to the data that is to be written. If the data is not in tagged format, 
                     * this should point to the block of data. If the data is in tagged format this should point to the 
                     * first tag name (datumID), which is always four bytes long, which should be followed by the data itself.
                     * Any number of tag name/value pairs can be specified this way, the server will use the cbUnitSize parameter 
                     * to determine how much data has been sent.*/
                    myData);
        }
        #endregion

        #region Copilot Adjust Functions
        void SetAirSpeedCommand(float value)
        {
            Hold.AirSpeed = value;
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts throttle. </summary>
        void SetAirSpeed(DataDefinition data)
        {
            if (Hold.AirSpeed != -1)
            {
                double Throttle = data.GENERAL_ENG_THROTTLE_LEVER_POSITION_1;
                double Speed = data.AIRSPEED_INDICATED;
                if (Speed < Hold.AirSpeed)
                {
                    Throttle += Increment.AirSpeed;
                    if (Throttle > 100.0)
                        Throttle = 100.0;
                }
                else if (Speed > Hold.AirSpeed)
                {
                    Throttle -= Increment.AirSpeed;
                    if (Throttle < 0.0)
                        Throttle = 0.0;
                }
                else    //no need to update
                {
                    return;
                }

                myData.GENERAL_ENG_THROTTLE_LEVER_POSITION_1 = Throttle;
                myData.GENERAL_ENG_THROTTLE_LEVER_POSITION_2 = Throttle;
                myData.AIRSPEED_INDICATED = Speed;

                SendData = true;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts throttle. </summary>
        void SetManifoldPressure(DataDefinition data)
        {
            if (Hold.ManifoldPressure != -1)
            {
                double Throttle = data.GENERAL_ENG_THROTTLE_LEVER_POSITION_1;
                double Pressure = data.ENG_MANIFOLD_PRESSURE_1;
                if (Pressure < Hold.ManifoldPressure)
                {
                    Throttle += Increment.Manifold;
                    if (Throttle > 100.0)
                        Throttle = 100.0;
                }
                else if (Pressure > Hold.ManifoldPressure)
                {
                    Throttle -= Increment.Manifold;
                    if (Throttle < 0.0)
                        Throttle = 0.0;
                }
                else    //no need to update
                {
                    return;
                }

                myData.GENERAL_ENG_THROTTLE_LEVER_POSITION_1 = Throttle;
                myData.GENERAL_ENG_THROTTLE_LEVER_POSITION_2 = Throttle;
                SendData = true;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts propeller throttle. </summary>
        void SetPropellerRPM(DataDefinition data)
        {
            if (Hold.PropRPM != -1)
            {
                double RPM = data.PROP_RPM_1;
                double LeverPos = data.GENERAL_ENG_PROPELLER_LEVER_POSITION_1;

                if (RPM < Hold.PropRPM)
                {
                    LeverPos += Increment.PropRPM;
                    if (LeverPos > 100.0)
                        LeverPos = 100.0;
                }
                else if (RPM > Hold.PropRPM)
                {
                    LeverPos -= Increment.PropRPM;
                    if (LeverPos < 0.0)
                        LeverPos = 0.0;
                }
                else    //no need to update
                {
                    return;
                }

                myData.GENERAL_ENG_PROPELLER_LEVER_POSITION_1 = LeverPos;
                myData.GENERAL_ENG_PROPELLER_LEVER_POSITION_2 = LeverPos;
                SendData = true;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts cowl flap lever position. </summary>
        void SetCowlFlaps(DataDefinition data)
        {
            if (Hold.CowlFlap != -1)
            {
                double FlapPos = data.RECIP_ENG_COWL_FLAP_POSITION_1;
                if (FlapPos != Hold.CowlFlap)
                {
                    FlapPos = Hold.CowlFlap;

                    myData.RECIP_ENG_COWL_FLAP_POSITION_1 = FlapPos;
                    myData.RECIP_ENG_COWL_FLAP_POSITION_2 = FlapPos;
                    SendData = true;
                }
            }

        }
        #endregion 

        #endregion

        void WriteINI()
        {
            IniFile.WriteKey(G.CoPilotCommandsPath, "SetAirSpeed", "set air speed", "Commands");

        }

        void ReadINI()
        {
            string s = string.Empty;
            int c = 0;
            WriteINI();

            s = IniFile.ReadKey(G.CoPilotCommandsPath, "SetAirSpeed", "Commands");
            SettableCommands.Add(s, SetAirSpeedCommand); 
            SettableFunctions.Add(c, SetAirSpeed);
            c++;

            //SettableFunctions.Add("set manifold pressure", SetManifoldPressure);
            //SettableFunctions.Add("set propeller rpm", SetPropellerRPM);
            //SettableFunctions.Add("set cowl flaps", SetCowlFlaps);
        }

        //Alpha, Bravo, Charlie, Delta, Echo, Foxtrot, Golf, Hotel, India, Juliet, Kilo, Lima, Mike, November, Oscar, Papa, Quebec, Romeo, Sierra, Tango, Uniform, Victor, Whiskey, X-ray, Yankee, Zulu.
        // The number three (3) is pronounced “tree.”
        // The number five(5) is pronounced “fife.”
        // The number nine(9) is pronounced “niner.” 
    }
}
