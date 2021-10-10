using System.Runtime.InteropServices;

namespace FSX_EMPIRE
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct SCoPilot
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
}
