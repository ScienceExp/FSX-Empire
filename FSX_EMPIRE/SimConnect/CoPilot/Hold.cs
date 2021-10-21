namespace Sim.CoPilot
{
    /// <summary> Values to 'hold'. -1 means co-pilot will not adjust. Changing these will cause the coPilot to act.</summary>
    public class Hold
    {
        /// <summary> -1 means don't worry about airspeed. Other value is the speed to keep in mph. (Do not use with Hold.ManifolPressure) </summary>
        public float AirSpeed = -1;

        /// <summary> -1 means don't worry about manifold pressure. (Do not use with Hold.Airspeed) </summary>
        public float ManifoldPressure = -1;

        /// <summary> -1 means don't worry about prop RPM. Other value is the rpm of the propeller. </summary>
        public float PropRPM = -1;

        /// <summary> -1 means don't adjust. Other value is % cowl flaps are opened. </summary>
        public float CowlFlap = -1;
    }
}
