using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;

namespace Sim.CoPilot
{
    /// <summary>These are the functions that are called if the copilot is to adjust something</summary>
    public class MyCoPilot
    {
        /// <summary> Where CoPilot should try and keep values at. '-1' means ignore </summary>
        public Hold Hold = new Hold();

        /// <summary> Amount CoPilot will adjust values by </summary>
        public Increment Increment;

        /// <summary>Should we SetDataOnSimObject</summary>
        bool SendData = false;

        /// <summary>The Definition ID associated with this class</summary>
        Enum myDefinitionID;
        Enum myRequestID;
        DataDefinition myData;

        public MyCoPilot(Enum DefinitionID , Enum RequestID)
        {
            myDefinitionID = DefinitionID;
            myRequestID = RequestID;
            Increment = new Increment();
            myData = new DataDefinition();
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct DataDefinition
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

        /// <summary>The AddToDataDefinition function is used to add a ESP simulation variable name to a client defined object definition.
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_addtodatadefinition">Documentation</seealso></summary>
        public void AddToDataDefinition()
        {
            G.simConnect.AddToDataDefinition(myDefinitionID, "AIRSPEED INDICATED", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "INDICATED ALTITUDE", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "ENG MANIFOLD PRESSURE:1", "inHG", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "ENG MANIFOLD PRESSURE:2", "inHG", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "GENERAL ENG THROTTLE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "GENERAL ENG THROTTLE LEVER POSITION:2", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "PROP RPM:1", "Rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "PROP RPM:2", "Rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "GENERAL ENG PROPELLER LEVER POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "GENERAL ENG PROPELLER LEVER POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "RECIP ENG COWL FLAP POSITION:1", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            G.simConnect.AddToDataDefinition(myDefinitionID, "RECIP ENG COWL FLAP POSITION:2", "percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            G.simConnect.RegisterDataDefineStruct<DataDefinition>(myDefinitionID);
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts throttle. </summary>
        public void CheckAirSpeed(DataDefinition data)
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

                data.GENERAL_ENG_THROTTLE_LEVER_POSITION_1 = Throttle;
                data.GENERAL_ENG_THROTTLE_LEVER_POSITION_2 = Throttle;
                SendData = true;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts throttle. </summary>
        public void CheckManifoldPressure(DataDefinition data)
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

                data.GENERAL_ENG_THROTTLE_LEVER_POSITION_1 = Throttle;
                data.GENERAL_ENG_THROTTLE_LEVER_POSITION_2 = Throttle;
                SendData = true;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts propeller throttle. </summary>
        public void CheckPropellerRPM(DataDefinition data)
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

                data.GENERAL_ENG_PROPELLER_LEVER_POSITION_1 = LeverPos;
                data.GENERAL_ENG_PROPELLER_LEVER_POSITION_2 = LeverPos;
                SendData = true;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts cowl flap lever position. </summary>
        public void CheckCowlFlaps(DataDefinition data)
        {
            if (Hold.CowlFlap != -1)
            {
                double FlapPos = data.RECIP_ENG_COWL_FLAP_POSITION_1;
                if (FlapPos != Hold.CowlFlap)
                {
                    FlapPos = Hold.CowlFlap;

                    data.RECIP_ENG_COWL_FLAP_POSITION_1 = FlapPos;
                    data.RECIP_ENG_COWL_FLAP_POSITION_2 = FlapPos;
                    SendData = true;
                }
            }

        }

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
    public void OnRecvSimobjectDataBytype(SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
    {
        DataDefinition d = (DataDefinition)data.dwData[0];
        CheckAirSpeed(d);
        CheckManifoldPressure(d);
        CheckPropellerRPM(d);
        CheckCowlFlaps(d);

        if (SendData)
        {
            SendData = false;
            SetDataOnSimObject();
        }
    }

    /// <summary>The SetDataOnSimObject function is used to make changes to the data properties of an object.
    /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526983(v=msdn.10)#simconnect_setdataonsimobject">Documentation</seealso></summary>
    void SetDataOnSimObject()
    {
        G.simConnect.SetDataOnSimObject(
            /*Specifies the ID of the client defined data definition.*/
                myDefinitionID,
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

    }
}
