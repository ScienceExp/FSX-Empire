using Microsoft.FlightSimulator.SimConnect;

namespace FSX_EMPIRE
{
    /// <summary> How much to increment the Hold values by </summary>
    public class CInc
    {
        /// <summary>Amount to adjust throttle to keep airspeed</summary>
        public double AirSpeed = 1;
        public double Manifold = 1;

        /// <summary>Amount to adjust propeller RPM</summary>
        public double PropRPM = 1;
    }

    /// <summary> Values to 'hold'. -1 means co-pilot will not adjust</summary>
    public class CHold
    {
        /// <summary> -1 means don't worry about airspeed. Other value is the speed to keep in mph. (Do not use with Hold.ManifolPressure) </summary>
        public double AirSpeed = -1;

        /// <summary> -1 means don't worry about manifold pressure. (Do not use with Hold.Airspeed) </summary>
        public double ManifoldPressure = -1;

        /// <summary> -1 means don't worry about prop RPM. Other value is the rpm of the propeller. </summary>
        public double PropRPM = -1;

        /// <summary> -1 means don't adjust. Other value is % cowl flaps are opened. </summary>
        public double CowlFlap = -1;
    }

    public static class CoPilot
    {
        /// <summary> Where CoPilot should try and keep values at. '-1' means ignore </summary>
        public static CHold Hold = new CHold();

        /// <summary> Amount CoPilot will adjust values by </summary>
        public static CInc Inc = new CInc();

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts throttle. </summary>
        public static void CheckAirSpeed(SCoPilot si)
        {
            double throttle = si.GENERAL_ENG_THROTTLE_LEVER_POSITION_1;
            double airSpeed = si.AIRSPEED_INDICATED;

            if (Hold.AirSpeed != -1)
            {
                if (airSpeed < Hold.AirSpeed)
                {
                    throttle += Inc.AirSpeed;
                    if (throttle > 100.0)
                        throttle = 100.0;
                }
                else if (airSpeed > Hold.AirSpeed)
                {
                    throttle -= Inc.AirSpeed;
                    if (throttle < 0.0)
                        throttle = 0.0;
                }
                else    //no need to update
                {
                    return;
                }

                G.simConnect.SetDataOnSimObject(
                    DEFINITION.ThrottlePos_1,                  //Specifies the ID of the client defined data definition.
                    SimConnect.SIMCONNECT_OBJECT_ID_USER,   //SIMCONNECT_OBJECT_ID_USER (to specify the user's aircraft)
                    SIMCONNECT_DATA_SET_FLAG.DEFAULT,       //Not SIMCONNECT_DATA_SET_FLAG_TAGGED
                    throttle);


                G.simConnect.SetDataOnSimObject(
                    DEFINITION.ThrottlePos_2,
                    SimConnect.SIMCONNECT_OBJECT_ID_USER,
                    SIMCONNECT_DATA_SET_FLAG.DEFAULT,
                    throttle);

                G.myPlane.throttleLeverPos1 = throttle;
                G.myPlane.throttleLeverPos2 = throttle;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts throttle. </summary>
        public static void CheckManifoldPressure(SCoPilot si)
        {
            double throttle = si.GENERAL_ENG_THROTTLE_LEVER_POSITION_1;
            double manifoldPressure = si.ENG_MANIFOLD_PRESSURE_1;

            if (Hold.ManifoldPressure != -1)
            {
                if (manifoldPressure < Hold.ManifoldPressure)
                {
                    throttle += Inc.Manifold;
                    if (throttle > 100.0)
                        throttle = 100.0;
                }
                else if (manifoldPressure > Hold.ManifoldPressure)
                {
                    throttle -= Inc.Manifold;
                    if (throttle < 0.0)
                        throttle = 0.0;
                }
                else    //no need to update
                {
                    return;
                }

                G.simConnect.SetDataOnSimObject(
                    DEFINITION.ThrottlePos_1,                  //Specifies the ID of the client defined data definition.
                    SimConnect.SIMCONNECT_OBJECT_ID_USER,   //SIMCONNECT_OBJECT_ID_USER (to specify the user's aircraft)
                    SIMCONNECT_DATA_SET_FLAG.DEFAULT,       //Not SIMCONNECT_DATA_SET_FLAG_TAGGED
                    throttle);

                G.simConnect.SetDataOnSimObject(
                    DEFINITION.ThrottlePos_2,
                    SimConnect.SIMCONNECT_OBJECT_ID_USER,
                    SIMCONNECT_DATA_SET_FLAG.DEFAULT,
                    throttle);

                G.myPlane.throttleLeverPos1 = throttle;
                G.myPlane.throttleLeverPos2 = throttle;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts propeller throttle. </summary>
        public static void CheckPropellerRPM(SCoPilot si)
        {
            double propRPM = si.PROP_RPM_1;
            double propLeverPos = si.GENERAL_ENG_PROPELLER_LEVER_POSITION_1;

            if (Hold.PropRPM != -1)
            {
                if (propRPM < Hold.PropRPM)
                {
                    propLeverPos += Inc.PropRPM;
                    if (propLeverPos > 100.0)
                        propLeverPos = 100.0;
                }
                else if (propRPM > Hold.PropRPM)
                {
                    propLeverPos -= Inc.PropRPM;
                    if (propLeverPos < 0.0)
                        propLeverPos = 0.0;
                }
                else    //no need to update
                {
                    return;
                }

                G.simConnect.SetDataOnSimObject(
                    DEFINITION.PropLeverPos_1,                         // Specifies the ID of the client defined data definition.
                    SimConnect.SIMCONNECT_OBJECT_ID_USER,           // SIMCONNECT_OBJECT_ID_USER (to specify the user's aircraft)
                    SIMCONNECT_DATA_SET_FLAG.DEFAULT,               // Not SIMCONNECT_DATA_SET_FLAG_TAGGED
                    propLeverPos);

                G.simConnect.SetDataOnSimObject(
                    DEFINITION.PropLeverPos_2,                         //Specifies the ID of the client defined data definition.
                    SimConnect.SIMCONNECT_OBJECT_ID_USER,           //SIMCONNECT_OBJECT_ID_USER (to specify the user's aircraft)
                    SIMCONNECT_DATA_SET_FLAG.DEFAULT,               //Not SIMCONNECT_DATA_SET_FLAG_TAGGED
                    propLeverPos);

                G.myPlane.propLeverPos1 = propLeverPos;
                G.myPlane.propLeverPos2 = propLeverPos;
            }
        }

        /// <summary> Checks to see if copilot should adjust. If 'yes' then adjusts cowl flap lever position. </summary>
        public static void CheckCowlFlaps(SCoPilot si)
        {
            double cowlFlapPos = si.RECIP_ENG_COWL_FLAP_POSITION_1;

            if (Hold.CowlFlap != -1)
            {
                if (cowlFlapPos != Hold.CowlFlap)
                {
                    cowlFlapPos = Hold.CowlFlap;
                    G.simConnect.SetDataOnSimObject(
                        DEFINITION.CowlFlapPos_1,                          //Specifies the ID of the client defined data definition.
                        SimConnect.SIMCONNECT_OBJECT_ID_USER,           //SIMCONNECT_OBJECT_ID_USER (to specify the user's aircraft)
                        SIMCONNECT_DATA_SET_FLAG.DEFAULT,               //Not SIMCONNECT_DATA_SET_FLAG_TAGGED
                        cowlFlapPos);

                    G.simConnect.SetDataOnSimObject(
                        DEFINITION.CowlFlapPos_2,
                        SimConnect.SIMCONNECT_OBJECT_ID_USER,
                        SIMCONNECT_DATA_SET_FLAG.DEFAULT,
                        cowlFlapPos);

                    G.myPlane.cowlFlapLeverPos1 = cowlFlapPos;
                    G.myPlane.cowlFlapLeverPos2  = cowlFlapPos;
                }
            }

        }
    }
}
