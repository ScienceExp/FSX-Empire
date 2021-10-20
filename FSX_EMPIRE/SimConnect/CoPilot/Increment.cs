namespace Sim.CoPilot
{
    /// <summary> How much to increment the Hold values by </summary>
    public class Increment
    {
        /// <summary>Amount to adjust throttle to keep airspeed</summary>
        public double AirSpeed = 1;
        /// <summary>Amount to adjust Manifold to keep pressure</summary>
        public double Manifold = 1;
        /// <summary>Amount to adjust propeller RPM</summary>
        public double PropRPM = 1;
    }
}
