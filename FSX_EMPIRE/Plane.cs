using System.Runtime.InteropServices;

namespace FSX_EMPIRE
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    class Plane
    {
        //todo add more variables 
        /// <summary>percent</summary>
        public double throttleLeverPos1 = 0;
        /// <summary>percent</summary>
        public double throttleLeverPos2 = 0;
        /// <summary>percent</summary>
        public double propLeverPos1 = 0;
        /// <summary>percent</summary>
        public double propLeverPos2 = 0;
        /// <summary>percent</summary>
        public double cowlFlapLeverPos1 = 0;
        /// <summary>percent</summary>
        public double cowlFlapLeverPos2 = 0;

        public void UpdateValues(SCoPilot si)
        {
            throttleLeverPos1 = si.GENERAL_ENG_THROTTLE_LEVER_POSITION_1;
            throttleLeverPos2 = si.GENERAL_ENG_THROTTLE_LEVER_POSITION_2;
            propLeverPos1 = si.GENERAL_ENG_PROPELLER_LEVER_POSITION_1;
            propLeverPos2 = si.GENERAL_ENG_PROPELLER_LEVER_POSITION_2;
            cowlFlapLeverPos1 = si.RECIP_ENG_COWL_FLAP_POSITION_1;
            cowlFlapLeverPos2 = si.RECIP_ENG_COWL_FLAP_POSITION_2;
        }
    }
}
