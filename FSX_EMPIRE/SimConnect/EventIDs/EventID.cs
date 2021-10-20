namespace Sim
{
    /// <summary>https://docs.microsoft.com/en-us/previous-versions/microsoft-esp/cc526980(v=msdn.10)</summary>
    public enum EventID : uint
    {
        #region Aircraft Engine
        /// <summary>Set throttles max</summary>
        THROTTLE_FULL,
        /// <summary>Increment throttles</summary>
        THROTTLE_INCR,
        /// <summary>Increment throttles small</summary>
        THROTTLE_INCR_SMALL,
        /// <summary>Decrement throttles</summary>
        THROTTLE_DECR,
        /// <summary>Decrease throttles small</summary>
        THROTTLE_DECR_SMALL,
        /// <summary>Set throttles to idle</summary>
        THROTTLE_CUT,
        /// <summary>Increment throttles</summary>
        INCREASE_THROTTLE,
        /// <summary>Decrement throttles</summary>
        DECREASE_THROTTLE,
        /// <summary>Set throttles exactly (0- 16383)</summary>
        THROTTLE_SET,
        /// <summary>Set throttles (0- 16383)</summary>
        AXIS_THROTTLE_SET,
        /// <summary>Set throttle 1 exactly (0 to 16383)</summary>
        THROTTLE1_SET,
        /// <summary>Set throttle 2 exactly (0 to 16383)</summary>
        THROTTLE2_SET,
        /// <summary>Set throttle 3 exactly (0 to 16383)</summary>
        THROTTLE3_SET,
        /// <summary>Set throttle 4 exactly (0 to 16383)</summary>
        THROTTLE4_SET,
        /// <summary>Set throttle 1 max</summary>
        THROTTLE1_FULL,
        /// <summary>Increment throttle 1</summary>
        THROTTLE1_INCR,
        /// <summary>Increment throttle 1 small</summary>
        THROTTLE1_INCR_SMALL,
        /// <summary>Decrement throttle 1</summary>
        THROTTLE1_DECR,
        /// <summary>Set throttle 1 to idle</summary>
        THROTTLE1_CUT,
        /// <summary>Set throttle 2 max</summary>
        THROTTLE2_FULL,
        /// <summary>Increment throttle 2</summary>
        THROTTLE2_INCR,
        /// <summary>Increment throttle 2 small</summary>
        THROTTLE2_INCR_SMALL,
        /// <summary>Decrement throttle 2</summary>
        THROTTLE2_DECR,
        /// <summary>Set throttle 2 to idle</summary>
        THROTTLE2_CUT,
        /// <summary>Set throttle 3 max</summary>
        THROTTLE3_FULL,
        /// <summary>Increment throttle 3</summary>
        THROTTLE3_INCR,
        /// <summary>Increment throttle 3 small</summary>
        THROTTLE3_INCR_SMALL,
        /// <summary>Decrement throttle 3</summary>
        THROTTLE3_DECR,
        /// <summary>Set throttle 3 to idle</summary>
        THROTTLE3_CUT,
        /// <summary>Set throttle 1 max</summary>
        THROTTLE4_FULL,
        /// <summary>Increment throttle 4</summary>
        THROTTLE4_INCR,
        /// <summary>Increment throttle 4 small</summary>
        THROTTLE4_INCR_SMALL,
        /// <summary>Decrement throttle 4</summary>
        THROTTLE4_DECR,
        /// <summary>Set throttle 4 to idle</summary>
        THROTTLE4_CUT,
        /// <summary>Set throttles to 10%</summary>
        THROTTLE_10,
        /// <summary>Set throttles to 20%</summary>
        THROTTLE_20,
        /// <summary>Set throttles to 30%</summary>
        THROTTLE_30,
        /// <summary>Set throttles to 40%</summary>
        THROTTLE_40,
        /// <summary>Set throttles to 50%</summary>
        THROTTLE_50,
        /// <summary>Set throttles to 60%</summary>
        THROTTLE_60,
        /// <summary>Set throttles to 70%</summary>
        THROTTLE_70,
        /// <summary>Set throttles to 80%</summary>
        THROTTLE_80,
        /// <summary>Set throttles to 90%</summary>
        THROTTLE_90,
        /// <summary>Set throttle 1 exactly (-16383 - +16383)</summary>
        AXIS_THROTTLE1_SET,
        /// <summary>Set throttle 2 exactly (-16383 - +16383)</summary>
        AXIS_THROTTLE2_SET,
        /// <summary>Set throttle 3 exactly (-16383 - +16383)</summary>
        AXIS_THROTTLE3_SET,
        /// <summary>Set throttle 4 exactly (-16383 - +16383)</summary>
        AXIS_THROTTLE4_SET,
        /// <summary>Decrease throttle 1 small</summary>
        THROTTLE1_DECR_SMALL,
        /// <summary>Decrease throttle 2 small</summary>
        THROTTLE2_DECR_SMALL,
        /// <summary>Decrease throttle 3 small</summary>
        THROTTLE3_DECR_SMALL,
        /// <summary>Decrease throttle 4 small</summary>
        THROTTLE4_DECR_SMALL,
        /// <summary>Decrease prop levers small</summary>
        PROP_PITCH_DECR_SMALL,
        /// <summary>Decrease prop lever 1 small</summary>
        PROP_PITCH1_DECR_SMALL,
        /// <summary>Decrease prop lever 2 small</summary>
        PROP_PITCH2_DECR_SMALL,
        /// <summary>Decrease prop lever 3 small</summary>
        PROP_PITCH3_DECR_SMALL,
        /// <summary>Decrease prop lever 4 small</summary>
        PROP_PITCH4_DECR_SMALL,
        /// <summary>Set mixture lever 1 to max rich</summary>
        MIXTURE1_RICH,
        /// <summary>Increment mixture lever 1</summary>
        MIXTURE1_INCR,
        /// <summary>Increment mixture lever 1 small</summary>
        MIXTURE1_INCR_SMALL,
        /// <summary>Decrement mixture lever 1</summary>
        MIXTURE1_DECR,
        /// <summary>Set mixture lever 1 to max lean</summary>
        MIXTURE1_LEAN,
        /// <summary>Set mixture lever 2 to max rich</summary>
        MIXTURE2_RICH,
        /// <summary>Increment mixture lever 2</summary>
        MIXTURE2_INCR,
        /// <summary>Increment mixture lever 2 small</summary>
        MIXTURE2_INCR_SMALL,
        /// <summary>Decrement mixture lever 2</summary>
        MIXTURE2_DECR,
        /// <summary>Set mixture lever 2 to max lean</summary>
        MIXTURE2_LEAN,
        /// <summary>Set mixture lever 3 to max rich</summary>
        MIXTURE3_RICH,
        /// <summary>Increment mixture lever 3</summary>
        MIXTURE3_INCR,
        /// <summary>Increment mixture lever 3 small</summary>
        MIXTURE3_INCR_SMALL,
        /// <summary>Decrement mixture lever 3</summary>
        MIXTURE3_DECR,
        /// <summary>Set mixture lever 3 to max lean</summary>
        MIXTURE3_LEAN,
        /// <summary>Set mixture lever 4 to max rich</summary>
        MIXTURE4_RICH,
        /// <summary>Increment mixture lever 4</summary>
        MIXTURE4_INCR,
        /// <summary>Increment mixture lever 4 small</summary>
        MIXTURE4_INCR_SMALL,
        /// <summary>Decrement mixture lever 4</summary>
        MIXTURE4_DECR,
        /// <summary>Set mixture lever 4 to max lean</summary>
        MIXTURE4_LEAN,
        /// <summary>Set mixture levers to exact value (0 to 16383)</summary>
        MIXTURE_SET,
        /// <summary>Set mixture levers to max rich</summary>
        MIXTURE_RICH,
        /// <summary>Increment mixture levers</summary>
        MIXTURE_INCR,
        /// <summary>Increment mixture levers small</summary>
        MIXTURE_INCR_SMALL,
        /// <summary>Decrement mixture levers</summary>
        MIXTURE_DECR,
        /// <summary>Set mixture levers to max lean</summary>
        MIXTURE_LEAN,
        /// <summary>Set mixture lever 1 exact value (0 to 16383)</summary>
        MIXTURE1_SET,
        /// <summary>Set mixture lever 2 exact value (0 to 16383)</summary>
        MIXTURE2_SET,
        /// <summary>Set mixture lever 3 exact value (0 to 16383)</summary>
        MIXTURE3_SET,
        /// <summary>Set mixture lever 4 exact value (0 to 16383)</summary>
        MIXTURE4_SET,
        /// <summary>Set mixture lever 1 exact value (-16383 to +16383)</summary>
        AXIS_MIXTURE_SET,
        /// <summary>Set mixture lever 1 exact value (-16383 to +16383)</summary>
        AXIS_MIXTURE1_SET,
        /// <summary>Set mixture lever 2 exact value (-16383 to +16383)</summary>
        AXIS_MIXTURE2_SET,
        /// <summary>Set mixture lever 3 exact value (-16383 to +16383)</summary>
        AXIS_MIXTURE3_SET,
        /// <summary>Set mixture lever 4 exact value (-16383 to +16383)</summary>
        AXIS_MIXTURE4_SET,
        /// <summary>Set mixture levers to current best power setting</summary>
        MIXTURE_SET_BEST,
        /// <summary>Decrement mixture levers small</summary>
        MIXTURE_DECR_SMALL,
        /// <summary>Decrement mixture lever 1 small</summary>
        MIXTURE1_DECR_SMALL,
        /// <summary>Decrement mixture lever 4 small</summary>
        MIXTURE2_DECR_SMALL,
        /// <summary>Decrement mixture lever 4 small</summary>
        MIXTURE3_DECR_SMALL,
        /// <summary>Decrement mixture lever 4 small</summary>
        MIXTURE4_DECR_SMALL,
        /// <summary>Set prop pitch levers (0 to 16383)</summary>
        PROP_PITCH_SET,
        /// <summary>Set prop pitch levers max (lo pitch)</summary>
        PROP_PITCH_LO,
        /// <summary>Increment prop pitch levers</summary>
        PROP_PITCH_INCR,
        /// <summary>Increment prop pitch levers small</summary>
        PROP_PITCH_INCR_SMALL,
        /// <summary>Decrement prop pitch levers</summary>
        PROP_PITCH_DECR,
        /// <summary>Set prop pitch levers min (hi pitch)</summary>
        PROP_PITCH_HI,
        /// <summary>Set prop pitch lever 1 exact value (0 to 16383)</summary>
        PROP_PITCH1_SET,
        /// <summary>Set prop pitch lever 2 exact value (0 to 16383)</summary>
        PROP_PITCH2_SET,
        /// <summary>Set prop pitch lever 3 exact value (0 to 16383)</summary>
        PROP_PITCH3_SET,
        /// <summary>Set prop pitch lever 4 exact value (0 to 16383)</summary>
        PROP_PITCH4_SET,
        /// <summary>Set prop pitch lever 1 max (lo pitch)</summary>
        PROP_PITCH1_LO,
        /// <summary>Increment prop pitch lever 1</summary>
        PROP_PITCH1_INCR,
        /// <summary>Increment prop pitch lever 1 small</summary>
        PROP_PITCH1_INCR_SMALL,
        /// <summary>Decrement prop pitch lever 1</summary>
        PROP_PITCH1_DECR,
        /// <summary>Set prop pitch lever 1 min (hi pitch)</summary>
        PROP_PITCH1_HI,
        /// <summary>Set prop pitch lever 2 max (lo pitch)</summary>
        PROP_PITCH2_LO,
        /// <summary>Increment prop pitch lever 2</summary>
        PROP_PITCH2_INCR,
        /// <summary>Increment prop pitch lever 2 small</summary>
        PROP_PITCH2_INCR_SMALL,
        /// <summary>Decrement prop pitch lever 2</summary>
        PROP_PITCH2_DECR,
        /// <summary>Set prop pitch lever 2 min (hi pitch)</summary>
        PROP_PITCH2_HI,
        /// <summary>Set prop pitch lever 3 max (lo pitch)</summary>
        PROP_PITCH3_LO,
        /// <summary>Increment prop pitch lever 3</summary>
        PROP_PITCH3_INCR,
        /// <summary>Increment prop pitch lever 3 small</summary>
        PROP_PITCH3_INCR_SMALL,
        /// <summary>Decrement prop pitch lever 3</summary>
        PROP_PITCH3_DECR,
        /// <summary>Set prop pitch lever 3 min (hi pitch)</summary>
        PROP_PITCH3_HI,
        /// <summary>Set prop pitch lever 4 max (lo pitch)</summary>
        PROP_PITCH4_LO,
        /// <summary>Increment prop pitch lever 4</summary>
        PROP_PITCH4_INCR,
        /// <summary>Increment prop pitch lever 4 small</summary>
        PROP_PITCH4_INCR_SMALL,
        /// <summary>Decrement prop pitch lever 4</summary>
        PROP_PITCH4_DECR,
        /// <summary>Set prop pitch lever 4 min (hi pitch)</summary>
        PROP_PITCH4_HI,
        /// <summary>Set propeller levers exact value (-16383 to +16383)</summary>
        AXIS_PROPELLER_SET,
        /// <summary>Set propeller lever 1 exact value (-16383 to +16383)</summary>
        AXIS_PROPELLER1_SET,
        /// <summary>Set propeller lever 2 exact value (-16383 to +16383)</summary>
        AXIS_PROPELLER2_SET,
        /// <summary>Set propeller lever 3 exact value (-16383 to +16383)</summary>
        AXIS_PROPELLER3_SET,
        /// <summary>Set propeller lever 4 exact value (-16383 to +16383)</summary>
        AXIS_PROPELLER4_SET,
        /// <summary>Selects jet engine starter (for +/- sequence)</summary>
        JET_STARTER,
        /// <summary>"Sets magnetos (0</summary>
        MAGNETO_SET,
        /// <summary>Toggle starter 1</summary>
        TOGGLE_STARTER1,
        /// <summary>Toggle starter 2</summary>
        TOGGLE_STARTER2,
        /// <summary>Toggle starter 3</summary>
        TOGGLE_STARTER3,
        /// <summary>Toggle starter 4</summary>
        TOGGLE_STARTER4,
        /// <summary>Toggle starters</summary>
        TOGGLE_ALL_STARTERS,
        /// <summary>Triggers auto-start</summary>
        ENGINE_AUTO_START,
        /// <summary>Triggers auto-shutdown</summary>
        ENGINE_AUTO_SHUTDOWN,
        /// <summary>Selects magnetos (for +/- sequence)</summary>
        MAGNETO,
        /// <summary>Decrease magneto switches positions</summary>
        MAGNETO_DECR,
        /// <summary>Increase magneto switches positions</summary>
        MAGNETO_INCR,
        /// <summary>Set engine 1 magnetos off</summary>
        MAGNETO1_OFF,
        /// <summary>Toggle engine 1 right magneto</summary>
        MAGNETO1_RIGHT,
        /// <summary>Toggle engine 1 left magneto</summary>
        MAGNETO1_LEFT,
        /// <summary>Set engine 1 magnetos on</summary>
        MAGNETO1_BOTH,
        /// <summary>Set engine 1 magnetos on and toggle starter</summary>
        MAGNETO1_START,
        /// <summary>Set engine 2 magnetos off</summary>
        MAGNETO2_OFF,
        /// <summary>Toggle engine 2 right magneto</summary>
        MAGNETO2_RIGHT,
        /// <summary>Toggle engine 2 left magneto</summary>
        MAGNETO2_LEFT,
        /// <summary>Set engine 2 magnetos on</summary>
        MAGNETO2_BOTH,
        /// <summary>Set engine 2 magnetos on and toggle starter</summary>
        MAGNETO2_START,
        /// <summary>Set engine 3 magnetos off</summary>
        MAGNETO3_OFF,
        /// <summary>Toggle engine 3 right magneto</summary>
        MAGNETO3_RIGHT,
        /// <summary>Toggle engine 3 left magneto</summary>
        MAGNETO3_LEFT,
        /// <summary>Set engine 3 magnetos on</summary>
        MAGNETO3_BOTH,
        /// <summary>Set engine 3 magnetos on and toggle starter</summary>
        MAGNETO3_START,
        /// <summary>Set engine 4 magnetos off</summary>
        MAGNETO4_OFF,
        /// <summary>Toggle engine 4 right magneto</summary>
        MAGNETO4_RIGHT,
        /// <summary>Toggle engine 4 left magneto</summary>
        MAGNETO4_LEFT,
        /// <summary>Set engine 4 magnetos on</summary>
        MAGNETO4_BOTH,
        /// <summary>Set engine 4 magnetos on and toggle starter</summary>
        MAGNETO4_START,
        /// <summary>Set engine magnetos off</summary>
        MAGNETO_OFF,
        /// <summary>Set engine right magnetos on</summary>
        MAGNETO_RIGHT,
        /// <summary>Set engine left magnetos on</summary>
        MAGNETO_LEFT,
        /// <summary>Set engine magnetos on</summary>
        MAGNETO_BOTH,
        /// <summary>Set engine magnetos on and toggle starters</summary>
        MAGNETO_START,
        /// <summary>Decrease engine 1 magneto switch position</summary>
        MAGNETO1_DECR,
        /// <summary>Increase engine 1 magneto switch position</summary>
        MAGNETO1_INCR,
        /// <summary>Decrease engine 2 magneto switch position</summary>
        MAGNETO2_DECR,
        /// <summary>Increase engine 2 magneto switch position</summary>
        MAGNETO2_INCR,
        /// <summary>Decrease engine 3 magneto switch position</summary>
        MAGNETO3_DECR,
        /// <summary>Increase engine 3 magneto switch position</summary>
        MAGNETO3_INCR,
        /// <summary>Decrease engine 4 magneto switch position</summary>
        MAGNETO4_DECR,
        /// <summary>Increase engine 4 magneto switch position</summary>
        MAGNETO4_INCR,
        /// <summary>Set engine 1 magneto switch</summary>
        MAGNETO1_SET,
        /// <summary>Set engine 2 magneto switch</summary>
        MAGNETO2_SET,
        /// <summary>Set engine 3 magneto switch</summary>
        MAGNETO3_SET,
        /// <summary>Set engine 4 magneto switch</summary>
        MAGNETO4_SET,
        /// <summary>Sets anti-ice switches on</summary>
        ANTI_ICE_ON,
        /// <summary>Sets anti-ice switches off</summary>
        ANTI_ICE_OFF,
        /// <summary>"Sets anti-ice switches from argument (0</summary>
        ANTI_ICE_SET,
        /// <summary>Toggle anti-ice switches</summary>
        ANTI_ICE_TOGGLE,
        /// <summary>Toggle engine 1 anti-ice switch</summary>
        ANTI_ICE_TOGGLE_ENG1,
        /// <summary>Toggle engine 2 anti-ice switch</summary>
        ANTI_ICE_TOGGLE_ENG2,
        /// <summary>Toggle engine 3 anti-ice switch</summary>
        ANTI_ICE_TOGGLE_ENG3,
        /// <summary>Toggle engine 4 anti-ice switch</summary>
        ANTI_ICE_TOGGLE_ENG4,
        /// <summary>"Sets engine 1 anti-ice switch (0</summary>
        ANTI_ICE_SET_ENG1,
        /// <summary>"Sets engine 2 anti-ice switch (0</summary>
        ANTI_ICE_SET_ENG2,
        /// <summary>"Sets engine 3 anti-ice switch (0</summary>
        ANTI_ICE_SET_ENG3,
        /// <summary>"Sets engine 4 anti-ice switch (0</summary>
        ANTI_ICE_SET_ENG4,
        /// <summary>Toggle engine fuel valves</summary>
        TOGGLE_FUEL_VALVE_ALL,
        /// <summary>Toggle engine 1 fuel valve</summary>
        TOGGLE_FUEL_VALVE_ENG1,
        /// <summary>Toggle engine 2 fuel valve</summary>
        TOGGLE_FUEL_VALVE_ENG2,
        /// <summary>Toggle engine 3 fuel valve</summary>
        TOGGLE_FUEL_VALVE_ENG3,
        /// <summary>Toggle engine 4 fuel valve</summary>
        TOGGLE_FUEL_VALVE_ENG4,
        /// <summary>Sets engine 1 cowl flap lever position (0 to 16383)</summary>
        COWLFLAP1_SET,
        /// <summary>Sets engine 2 cowl flap lever position (0 to 16383)</summary>
        COWLFLAP2_SET,
        /// <summary>Sets engine 3 cowl flap lever position (0 to 16383)</summary>
        COWLFLAP3_SET,
        /// <summary>Sets engine 4 cowl flap lever position (0 to 16383)</summary>
        COWLFLAP4_SET,
        /// <summary>Increment cowl flap levers</summary>
        INC_COWL_FLAPS,
        /// <summary>Decrement cowl flap levers</summary>
        DEC_COWL_FLAPS,
        /// <summary>Increment engine 1 cowl flap lever</summary>
        INC_COWL_FLAPS1,
        /// <summary>Decrement engine 1 cowl flap lever</summary>
        DEC_COWL_FLAPS1,
        /// <summary>Increment engine 2 cowl flap lever</summary>
        INC_COWL_FLAPS2,
        /// <summary>Decrement engine 2 cowl flap lever</summary>
        DEC_COWL_FLAPS2,
        /// <summary>Increment engine 3 cowl flap lever</summary>
        INC_COWL_FLAPS3,
        /// <summary>Decrement engine 3 cowl flap lever</summary>
        DEC_COWL_FLAPS3,
        /// <summary>Increment engine 4 cowl flap lever</summary>
        INC_COWL_FLAPS4,
        /// <summary>Decrement engine 4 cowl flap lever</summary>
        DEC_COWL_FLAPS4,
        /// <summary>Toggle electric fuel pumps</summary>
        FUEL_PUMP,
        /// <summary>Toggle electric fuel pumps</summary>
        TOGGLE_ELECT_FUEL_PUMP,
        /// <summary>Toggle engine 1 electric fuel pump</summary>
        TOGGLE_ELECT_FUEL_PUMP1,
        /// <summary>Toggle engine 2 electric fuel pump</summary>
        TOGGLE_ELECT_FUEL_PUMP2,
        /// <summary>Toggle engine 3 electric fuel pump</summary>
        TOGGLE_ELECT_FUEL_PUMP3,
        /// <summary>Toggle engine 4 electric fuel pump</summary>
        TOGGLE_ELECT_FUEL_PUMP4,
        /// <summary>Trigger engine primers</summary>
        ENGINE_PRIMER,
        /// <summary>Trigger engine primers</summary>
        TOGGLE_PRIMER,
        /// <summary>Trigger engine 1 primer</summary>
        TOGGLE_PRIMER1,
        /// <summary>Trigger engine 2 primer</summary>
        TOGGLE_PRIMER2,
        /// <summary>Trigger engine 3 primer</summary>
        TOGGLE_PRIMER3,
        /// <summary>Trigger engine 4 primer</summary>
        TOGGLE_PRIMER4,
        /// <summary>Trigger propeller switches</summary>
        TOGGLE_FEATHER_SWITCHES,
        /// <summary>Trigger propeller 1 switch</summary>
        TOGGLE_FEATHER_SWITCH_1,
        /// <summary>Trigger propeller 2 switch</summary>
        TOGGLE_FEATHER_SWITCH_2,
        /// <summary>Trigger propeller 3 switch</summary>
        TOGGLE_FEATHER_SWITCH_3,
        /// <summary>Trigger propeller 4 switch</summary>
        TOGGLE_FEATHER_SWITCH_4,
        /// <summary>Turns propeller synchronization switch on</summary>
        TOGGLE_PROPELLER_SYNC,
        /// <summary>Turns auto-feather arming switch on.</summary>
        TOGGLE_AUTOFEATHER_ARM,
        /// <summary>Toggles afterburners</summary>
        TOGGLE_AFTERBURNER,
        /// <summary>Toggles engine 1 afterburner</summary>
        TOGGLE_AFTERBURNER1,
        /// <summary>Toggles engine 2 afterburner</summary>
        TOGGLE_AFTERBURNER2,
        /// <summary>Toggles engine 3 afterburner</summary>
        TOGGLE_AFTERBURNER3,
        /// <summary>Toggles engine 4 afterburner</summary>
        TOGGLE_AFTERBURNER4,
        /// <summary>"Sets engines for 1</summary>
        ENGINE,
        #endregion

        #region Aircraft Flight Controls
        /// <summary>Toggles spoiler handle </summary>
        SPOILERS_TOGGLE,
        /// <summary>Sets flap handle to full retract position</summary>
        FLAPS_UP,
        /// <summary>Sets flap handle to first extension position</summary>
        FLAPS_1,
        /// <summary>Sets flap handle to second extension position</summary>
        FLAPS_2,
        /// <summary>Sets flap handle to third extension position</summary>
        FLAPS_3,
        /// <summary>Sets flap handle to full extension position</summary>
        FLAPS_DOWN,
        /// <summary>Increments elevator trim down</summary>
        ELEV_TRIM_DN,
        /// <summary>Increments elevator down</summary>
        ELEV_DOWN,
        /// <summary>Increments ailerons left</summary>
        AILERONS_LEFT,
        /// <summary>Centers aileron and rudder positions</summary>
        CENTER_AILER_RUDDER,
        /// <summary>Increments ailerons right</summary>
        AILERONS_RIGHT,
        /// <summary>Increment elevator trim up</summary>
        ELEV_TRIM_UP,
        /// <summary>Increments elevator up</summary>
        ELEV_UP,
        /// <summary>Increments rudder left</summary>
        RUDDER_LEFT,
        /// <summary>Centers rudder position</summary>
        RUDDER_CENTER,
        /// <summary>Increments rudder right</summary>
        RUDDER_RIGHT,
        /// <summary>Sets elevator position (-16383 - +16383)</summary>
        ELEVATOR_SET,
        /// <summary>Sets aileron position (-16383 - +16383)</summary>
        AILERON_SET,
        /// <summary>Sets rudder position (-16383 - +16383)</summary>
        RUDDER_SET,
        /// <summary>Increments flap handle position</summary>
        FLAPS_INCR,
        /// <summary>Decrements flap handle position</summary>
        FLAPS_DECR,
        /// <summary>Sets elevator position (-16383 - +16383)</summary>
        AXIS_ELEVATOR_SET,
        /// <summary>Sets aileron position (-16383 - +16383)</summary>
        AXIS_AILERONS_SET,
        /// <summary>Sets rudder position (-16383 - +16383)</summary>
        AXIS_RUDDER_SET,
        /// <summary>Sets elevator trim position (-16383 - +16383)</summary>
        AXIS_ELEV_TRIM_SET,
        /// <summary>Sets spoiler handle position (0 to 16383)</summary>
        SPOILERS_SET,
        /// <summary>Toggles arming of auto-spoilers</summary>
        SPOILERS_ARM_TOGGLE,
        /// <summary>Sets spoiler handle to full extend position</summary>
        SPOILERS_ON,
        /// <summary>Sets spoiler handle to full retract position</summary>
        SPOILERS_OFF,
        /// <summary>Sets auto-spoiler arming on</summary>
        SPOILERS_ARM_ON,
        /// <summary>Sets auto-spoiler arming off</summary>
        SPOILERS_ARM_OFF,
        /// <summary>"Sets auto-spoiler arming (0</summary>
        SPOILERS_ARM_SET,
        /// <summary>Increments aileron trim left</summary>
        AILERON_TRIM_LEFT,
        /// <summary>Increments aileron trim right</summary>
        AILERON_TRIM_RIGHT,
        /// <summary>Increments rudder trim left</summary>
        RUDDER_TRIM_LEFT,
        /// <summary>Increments aileron trim right</summary>
        RUDDER_TRIM_RIGHT,
        /// <summary>Sets spoiler handle position (-16383 - +16383)</summary>
        AXIS_SPOILER_SET,
        /// <summary>Sets flap handle to closest increment (0 to 16383)</summary>
        FLAPS_SET,
        /// <summary>Sets elevator trim position (0 to 16383)</summary>
        ELEVATOR_TRIM_SET,
        /// <summary>Sets flap handle to closest increment (-16383 - +16383)</summary>
        AXIS_FLAPS_SET,
        #endregion

        #region Aircraft Automatic Flight Systems / Autopilot
        /// <summary>Toggles AP on/off</summary>
        AP_MASTER,
        /// <summary>Turns AP off</summary>
        AUTOPILOT_OFF,
        /// <summary>Turns AP on</summary>
        AUTOPILOT_ON,
        /// <summary>Toggles yaw damper on/off</summary>
        YAW_DAMPER_TOGGLE,
        /// <summary>Toggles heading hold mode on/off</summary>
        AP_PANEL_HEADING_HOLD,
        /// <summary>Toggles altitude hold mode on/off</summary>
        AP_PANEL_ALTITUDE_HOLD,
        /// <summary>Turns on AP wing leveler and pitch hold mode</summary>
        AP_ATT_HOLD_ON,
        /// <summary>Turns AP localizer hold on/armed and glide-slope hold mode off</summary>
        AP_LOC_HOLD_ON,
        /// <summary>Turns both AP localizer and glide-slope modes on/armed</summary>
        AP_APR_HOLD_ON,
        /// <summary>Turns heading hold mode on</summary>
        AP_HDG_HOLD_ON,
        /// <summary>Turns altitude hold mode on</summary>
        AP_ALT_HOLD_ON,
        /// <summary>Turns wing leveler mode on</summary>
        AP_WING_LEVELER_ON,
        /// <summary>Turns localizer back course hold mode on/armed</summary>
        AP_BC_HOLD_ON,
        /// <summary>Turns lateral hold mode on </summary>
        AP_NAV1_HOLD_ON,
        /// <summary>Turns off attitude hold mode</summary>
        AP_ATT_HOLD_OFF,
        /// <summary>Turns off localizer hold mode</summary>
        AP_LOC_HOLD_OFF,
        /// <summary>Turns off approach hold mode</summary>
        AP_APR_HOLD_OFF,
        /// <summary>Turns off heading hold mode</summary>
        AP_HDG_HOLD_OFF,
        /// <summary>Turns off altitude hold mode</summary>
        AP_ALT_HOLD_OFF,
        /// <summary>Turns off wing leveler mode</summary>
        AP_WING_LEVELER_OFF,
        /// <summary>Turns off backcourse mode for localizer hold</summary>
        AP_BC_HOLD_OFF,
        /// <summary>Turns off nav hold mode</summary>
        AP_NAV1_HOLD_OFF,
        /// <summary>Toggles airspeed hold mode</summary>
        AP_AIRSPEED_HOLD,
        /// <summary>Toggles autothrottle arming mode</summary>
        AUTO_THROTTLE_ARM,
        /// <summary>Toggles Takeoff/Go Around mode</summary>
        AUTO_THROTTLE_TO_GA,
        /// <summary>Increments heading hold reference bug</summary>
        HEADING_BUG_INC,
        /// <summary>Decrements heading hold reference bug</summary>
        HEADING_BUG_DEC,
        /// <summary>Set heading hold reference bug (degrees)</summary>
        HEADING_BUG_SET,
        /// <summary>Toggles airspeed hold mode</summary>
        AP_PANEL_SPEED_HOLD,
        /// <summary>Increments reference altitude</summary>
        AP_ALT_VAR_INC,
        /// <summary>Decrements reference altitude</summary>
        AP_ALT_VAR_DEC,
        /// <summary>Increments vertical speed reference</summary>
        AP_VS_VAR_INC,
        /// <summary>Decrements vertical speed reference</summary>
        AP_VS_VAR_DEC,
        /// <summary>Increments airspeed hold reference</summary>
        AP_SPD_VAR_INC,
        /// <summary>Decrements airspeed hold reference</summary>
        AP_SPD_VAR_DEC,
        /// <summary>Toggles mach hold</summary>
        AP_PANEL_MACH_HOLD,
        /// <summary>Increments reference mach</summary>
        AP_MACH_VAR_INC,
        /// <summary>Decrements reference mach</summary>
        AP_MACH_VAR_DEC,
        /// <summary>Toggles mach hold</summary>
        AP_MACH_HOLD,
        /// <summary>Sets reference altitude in meters</summary>
        AP_ALT_VAR_SET_METRIC,
        /// <summary>Sets reference vertical speed in feet per minute</summary>
        AP_VS_VAR_SET_ENGLISH,
        /// <summary>Sets airspeed reference in knots</summary>
        AP_SPD_VAR_SET,
        /// <summary>Sets mach reference</summary>
        AP_MACH_VAR_SET,
        /// <summary>Turns yaw damper on</summary>
        YAW_DAMPER_ON,
        /// <summary>Turns yaw damper off</summary>
        YAW_DAMPER_OFF,
        /// <summary>"Sets yaw damper on/off (1</summary>
        YAW_DAMPER_SET,
        /// <summary>Turns airspeed hold on</summary>
        AP_AIRSPEED_ON,
        /// <summary>Turns airspeed hold off</summary>
        AP_AIRSPEED_OFF,
        /// <summary>"Sets airspeed hold on/off (1</summary>
        AP_AIRSPEED_SET,
        /// <summary>Turns mach hold on</summary>
        AP_MACH_ON,
        /// <summary>Turns mach hold off</summary>
        AP_MACH_OFF,
        /// <summary>"Sets mach hold on/off (1</summary>
        AP_MACH_SET,
        /// <summary>Turns altitude hold mode on (without capturing current altitude)</summary>
        AP_PANEL_ALTITUDE_ON,
        /// <summary>Turns altitude hold mode off</summary>
        AP_PANEL_ALTITUDE_OFF,
        /// <summary>"Sets altitude hold mode on/off (1</summary>
        AP_PANEL_ALTITUDE_SET,
        /// <summary>Turns heading mode on (without capturing current heading)</summary>
        AP_PANEL_HEADING_ON,
        /// <summary>Turns heading mode off</summary>
        AP_PANEL_HEADING_OFF,
        /// <summary>"Set heading mode on/off (1</summary>
        AP_PANEL_HEADING_SET,
        /// <summary>Turns on mach hold</summary>
        AP_PANEL_MACH_ON,
        /// <summary>Turns off mach hold</summary>
        AP_PANEL_MACH_OFF,
        /// <summary>"Sets mach hold on/off (1</summary>
        AP_PANEL_MACH_SET,
        /// <summary>Turns on speed hold mode</summary>
        AP_PANEL_SPEED_ON,
        /// <summary>Turns off speed hold mode</summary>
        AP_PANEL_SPEED_OFF,
        /// <summary>"Set speed hold mode on/off (1</summary>
        AP_PANEL_SPEED_SET,
        /// <summary>Sets altitude reference in feet</summary>
        AP_ALT_VAR_SET_ENGLISH,
        /// <summary>Sets vertical speed reference in meters per minute</summary>
        AP_VS_VAR_SET_METRIC,
        /// <summary>Toggles flight director on/off</summary>
        TOGGLE_FLIGHT_DIRECTOR,
        /// <summary>Synchronizes flight director pitch with current aircraft pitch</summary>
        SYNC_FLIGHT_DIRECTOR_PITCH,
        /// <summary>Increments autobrake level</summary>
        INCREASE_AUTOBRAKE_CONTROL,
        /// <summary>Decrements autobrake level</summary>
        DECREASE_AUTOBRAKE_CONTROL,
        /// <summary>Turns airspeed hold mode on with current airspeed</summary>
        AP_PANEL_SPEED_HOLD_TOGGLE,
        /// <summary>Sets mach hold reference to current mach</summary>
        AP_PANEL_MACH_HOLD_TOGGLE,
        /// <summary>Sets the nav (1 or 2) which is used by the Nav hold modes</summary>
        AP_NAV_SELECT_SET,
        /// <summary>Selects the heading bug for use with +/-</summary>
        HEADING_BUG_SELECT,
        /// <summary>Selects the altitude reference for use with +/-</summary>
        ALTITUDE_BUG_SELECT,
        /// <summary>Selects the vertical speed reference for use with +/-</summary>
        VSI_BUG_SELECT,
        /// <summary>Selects the airspeed reference for use with +/-</summary>
        AIRSPEED_BUG_SELECT,
        /// <summary>Increments the pitch reference for pitch hold mode</summary>
        AP_PITCH_REF_INC_UP,
        /// <summary>Decrements the pitch reference for pitch hold mode</summary>
        AP_PITCH_REF_INC_DN,
        /// <summary>Selects pitch reference for use with +/-</summary>
        AP_PITCH_REF_SELECT,
        /// <summary>Toggle attitude hold mode</summary>
        AP_ATT_HOLD,
        /// <summary>Toggles localizer (only) hold mode</summary>
        AP_LOC_HOLD,
        /// <summary>Toggles approach hold (localizer and glide-slope)</summary>
        AP_APR_HOLD,
        /// <summary>Toggles heading hold mode</summary>
        AP_HDG_HOLD,
        /// <summary>Toggles altitude hold mode</summary>
        AP_ALT_HOLD,
        /// <summary>Toggles wing leveler mode</summary>
        AP_WING_LEVELER,
        /// <summary>Toggles the backcourse mode for the localizer hold</summary>
        AP_BC_HOLD,
        /// <summary>Toggles the nav hold mode</summary>
        AP_NAV1_HOLD,
        /// <summary>Autopilot max bank angle increment. </summary>
        AP_MAX_BANK_INC,
        /// <summary>Autopilot max bank angle decrement. </summary>
        AP_MAX_BANK_DEC,
        /// <summary>"Autopilot</summary>
        AP_N1_HOLD,
        /// <summary>Increment the autopilot N1 reference. </summary>
        AP_N1_REF_INC,
        /// <summary>Decrement the autopilot N1 reference. </summary>
        AP_N1_REF_DEC,
        /// <summary>Sets the autopilot N1 reference. </summary>
        AP_N1_REF_SET,
        /// <summary>Turn on or off the fly by wire Elevators and Ailerons computer.</summary>
        FLY_BY_WIRE_ELAC_TOGGLE,
        /// <summary>Turn on or off the fly by wire Flight Augmentation computer. </summary>
        FLY_BY_WIRE_FAC_TOGGLE,
        /// <summary>Turn on or off the fly by wire Spoilers and Elevators computer.</summary>
        FLY_BY_WIRE_SEC_TOGGLE,
        /// <summary>The primary flight display (PFD) should display its current flight plan. </summary>
        G1000_PFD_FLIGHTPLAN_BUTTON,
        /// <summary>Turn to the Procedure page. </summary>
        G1000_PFD_PROCEDURE_BUTTON,
        /// <summary>Zoom in on the current map. </summary>
        G1000_PFD_ZOOMIN_BUTTON,
        /// <summary>Zoom out on the current map. </summary>
        G1000_PFD_ZOOMOUT_BUTTON,
        /// <summary>Turn to the Direct To page. </summary>
        G1000_PFD_DIRECTTO_BUTTON,
        /// <summary>"If a segmented flight plan is highlighted</summary>
        G1000_PFD_MENU_BUTTON,
        /// <summary>Clears the current input. </summary>
        G1000_PFD_CLEAR_BUTTON,
        /// <summary>Enters the current input. </summary>
        G1000_PFD_ENTER_BUTTON,
        /// <summary>Turns on or off a screen cursor. </summary>
        G1000_PFD_CURSOR_BUTTON,
        /// <summary>Step up through the page groups. </summary>
        G1000_PFD_GROUP_KNOB_INC,
        /// <summary>Step down through the page groups. </summary>
        G1000_PFD_GROUP_KNOB_DEC,
        /// <summary>Step up through the individual pages. </summary>
        G1000_PFD_PAGE_KNOB_INC,
        /// <summary>Step down through the individual pages. </summary>
        G1000_PFD_PAGE_KNOB_DEC,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY1,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY2,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY3,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY4,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY5,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY6,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY7,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY8,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY9,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY10,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY11,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_PFD_SOFTKEY12,
        /// <summary>The multifunction display (MFD) should display its current flight plan. </summary>
        G1000_MFD_FLIGHTPLAN_BUTTON,
        /// <summary>Turn to the Procedure page.</summary>
        G1000_MFD_PROCEDURE_BUTTON,
        /// <summary>Zoom in on the current map. </summary>
        G1000_MFD_ZOOMIN_BUTTON,
        /// <summary>Zoom out on the current map. </summary>
        G1000_MFD_ZOOMOUT_BUTTON,
        /// <summary>Turn to the Direct To page. </summary>
        G1000_MFD_DIRECTTO_BUTTON,
        /// <summary>"If a segmented flight plan is highlighted</summary>
        G1000_MFD_MENU_BUTTON,
        /// <summary>Clears the current input. </summary>
        G1000_MFD_CLEAR_BUTTON,
        /// <summary>Enters the current input. </summary>
        G1000_MFD_ENTER_BUTTON,
        /// <summary>Turns on or off a screen cursor. </summary>
        G1000_MFD_CURSOR_BUTTON,
        /// <summary>Step up through the page groups. </summary>
        G1000_MFD_GROUP_KNOB_INC,
        /// <summary>Step down through the page groups. </summary>
        G1000_MFD_GROUP_KNOB_DEC,
        /// <summary>Step up through the individual pages. </summary>
        G1000_MFD_PAGE_KNOB_INC,
        /// <summary>Step down through the individual pages. </summary>
        G1000_MFD_PAGE_KNOB_DEC,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY1,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY2,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY3,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY4,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY5,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY6,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY7,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY8,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY9,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY10,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY11,
        /// <summary>Initiate the action for the icon displayed in the softkey position. </summary>
        G1000_MFD_SOFTKEY12,
        #endregion

        #region Aircraft Fuel System
        /// <summary>Turns selector 1 to OFF position</summary>
        FUEL_SELECTOR_OFF,
        /// <summary>Turns selector 1 to ALL position</summary>
        FUEL_SELECTOR_ALL,
        /// <summary>Turns selector 1 to LEFT position (burns from tip then aux then main)</summary>
        FUEL_SELECTOR_LEFT,
        /// <summary>Turns selector 1 to RIGHT position (burns from tip then aux then main)</summary>
        FUEL_SELECTOR_RIGHT,
        /// <summary>Turns selector 1 to LEFT AUX position</summary>
        FUEL_SELECTOR_LEFT_AUX,
        /// <summary>Turns selector 1 to RIGHT AUX position</summary>
        FUEL_SELECTOR_RIGHT_AUX,
        /// <summary>Turns selector 1 to CENTER position</summary>
        FUEL_SELECTOR_CENTER,
        /// <summary>Sets selector 1 position (see code list below)</summary>
        FUEL_SELECTOR_SET,
        /// <summary>Turns selector 2 to OFF position</summary>
        FUEL_SELECTOR_2_OFF,
        /// <summary>Turns selector 2 to ALL position</summary>
        FUEL_SELECTOR_2_ALL,
        /// <summary>Turns selector 2 to LEFT position (burns from tip then aux then main)</summary>
        FUEL_SELECTOR_2_LEFT,
        /// <summary>Turns selector 2 to RIGHT position (burns from tip then aux then main)</summary>
        FUEL_SELECTOR_2_RIGHT,
        /// <summary>Turns selector 2 to LEFT AUX position</summary>
        FUEL_SELECTOR_2_LEFT_AUX,
        /// <summary>Turns selector 2 to RIGHT AUX position</summary>
        FUEL_SELECTOR_2_RIGHT_AUX,
        /// <summary>Turns selector 2 to CENTER position</summary>
        FUEL_SELECTOR_2_CENTER,
        /// <summary>Sets selector 2 position (see code list below)</summary>
        FUEL_SELECTOR_2_SET,
        /// <summary>Turns selector 3 to OFF position</summary>
        FUEL_SELECTOR_3_OFF,
        /// <summary>Turns selector 3 to ALL position</summary>
        FUEL_SELECTOR_3_ALL,
        /// <summary>Turns selector 3 to LEFT position (burns from tip then aux then main)</summary>
        FUEL_SELECTOR_3_LEFT,
        /// <summary>Turns selector 3 to RIGHT position (burns from tip then aux then main)</summary>
        FUEL_SELECTOR_3_RIGHT,
        /// <summary>Turns selector 3 to LEFT AUX position</summary>
        FUEL_SELECTOR_3_LEFT_AUX,
        /// <summary>Turns selector 3 to RIGHT AUX position</summary>
        FUEL_SELECTOR_3_RIGHT_AUX,
        /// <summary>Turns selector 3 to CENTER position</summary>
        FUEL_SELECTOR_3_CENTER,
        /// <summary>Sets selector 3 position (see code list below)</summary>
        FUEL_SELECTOR_3_SET,
        /// <summary>Turns selector 4 to OFF position</summary>
        FUEL_SELECTOR_4_OFF,
        /// <summary>Turns selector 4 to ALL position</summary>
        FUEL_SELECTOR_4_ALL,
        /// <summary>Turns selector 4 to LEFT position (burns from tip then aux then main)</summary>
        FUEL_SELECTOR_4_LEFT,
        /// <summary>Turns selector 4 to RIGHT position (burns from tip then aux then main)</summary>
        FUEL_SELECTOR_4_RIGHT,
        /// <summary>Turns selector 4 to LEFT AUX position</summary>
        FUEL_SELECTOR_4_LEFT_AUX,
        /// <summary>Turns selector 4 to RIGHT AUX position</summary>
        FUEL_SELECTOR_4_RIGHT_AUX,
        /// <summary>Turns selector 4 to CENTER position</summary>
        FUEL_SELECTOR_4_CENTER,
        /// <summary>Sets selector 4 position (see code list below)</summary>
        FUEL_SELECTOR_4_SET,
        /// <summary>"Opens cross feed valve (when used in conjunction with ""isolate"" tank)"</summary>
        CROSS_FEED_OPEN,
        /// <summary>"Toggles crossfeed valve (when used in conjunction with ""isolate"" tank)"</summary>
        CROSS_FEED_TOGGLE,
        /// <summary>"Closes crossfeed valve (when used in conjunction with ""isolate"" tank)"</summary>
        CROSS_FEED_OFF,
        /// <summary>"Set to True or False. The switch can only be set to True if fuel_dump_rate is specified in the aircraft configuration file</summary>
        FUEL_DUMP_SWITCH_SET,
        /// <summary>"Toggle the antidetonation valve. Pass a value to determine which tank</summary>
        ANTIDETONATION_TANK_VALVE_TOGGLE,
        /// <summary>"Toggle the nitrous valve. Pass a value to determine which tank</summary>
        NITROUS_TANK_VALVE_TOGGLE,
        /// <summary>Fully repair and refuel the user aircraft. Ignored if flight realism is enforced.</summary>
        REPAIR_AND_REFUEL,
        /// <summary>Turns on or off the fuel dump switch.</summary>
        FUEL_DUMP_TOGGLE,
        /// <summary>Request a fuel truck. The aircraft must be in a parking spot for this to be successful.</summary>
        REQUEST_FUEL_KEY,
        /// <summary>"Sets the fuel selector. Fuel will be taken in the order left tip</summary>
        FUEL_SELECTOR_LEFT_MAIN,
        /// <summary>Sets the fuel selector for engine 2.</summary>
        FUEL_SELECTOR_2_LEFT_MAIN,
        /// <summary>Sets the fuel selector for engine 3.</summary>
        FUEL_SELECTOR_3_LEFT_MAIN,
        /// <summary>Sets the fuel selector for engine 4.</summary>
        FUEL_SELECTOR_4_LEFT_MAIN,
        /// <summary>"Sets the fuel selector. Fuel will be taken in the order right tip</summary>
        FUEL_SELECTOR_RIGHT_MAIN,
        /// <summary>Sets the fuel selector for engine 2.</summary>
        FUEL_SELECTOR_2_RIGHT_MAIN,
        /// <summary>Sets the fuel selector for engine 3.</summary>
        FUEL_SELECTOR_3_RIGHT_MAIN,
        /// <summary>Sets the fuel selector for engine 4.</summary>
        FUEL_SELECTOR_4_RIGHT_MAIN,
        #endregion

        #region Aircraft Avionics
        /// <summary>Sequentially selects the transponder digits for use with +/-.</summary>
        XPNDR,
        /// <summary>Sequentially selects the ADF tuner digits for use with +/-. Follow by KEY_SELECT_2 for ADF 2.</summary>
        ADF,
        /// <summary>Selects the DME for use with +/-</summary>
        DME,
        /// <summary>Sequentially selects the COM tuner digits for use with +/-. Follow by KEY_SELECT_2 for COM 2.</summary>
        COM_RADIO,
        /// <summary>Sequentially selects the VOR OBS for use with +/-. Follow by KEY_SELECT_2 for VOR 2.</summary>
        VOR_OBS,
        /// <summary>Sequentially selects the NAV tuner digits for use with +/-. Follow by KEY_SELECT_2 for NAV 2.</summary>
        NAV_RADIO,
        /// <summary>Decrements COM by one MHz</summary>
        COM_RADIO_WHOLE_DEC,
        /// <summary>Increments COM by one MHz</summary>
        COM_RADIO_WHOLE_INC,
        /// <summary>Decrements COM by 25 KHz</summary>
        COM_RADIO_FRACT_DEC,
        /// <summary>Increments COM by 25 KHz</summary>
        COM_RADIO_FRACT_INC,
        /// <summary>Decrements Nav 1 by one MHz</summary>
        NAV1_RADIO_WHOLE_DEC,
        /// <summary>Increments Nav 1 by one MHz</summary>
        NAV1_RADIO_WHOLE_INC,
        /// <summary>Decrements Nav 1 by 25 KHz</summary>
        NAV1_RADIO_FRACT_DEC,
        /// <summary>Increments Nav 1 by 25 KHz</summary>
        NAV1_RADIO_FRACT_INC,
        /// <summary>Decrements Nav 2 by one MHz</summary>
        NAV2_RADIO_WHOLE_DEC,
        /// <summary>Increments Nav 2 by one MHz</summary>
        NAV2_RADIO_WHOLE_INC,
        /// <summary>Decrements Nav 2 by 25 KHz</summary>
        NAV2_RADIO_FRACT_DEC,
        /// <summary>Increments Nav 2 by 25 KHz</summary>
        NAV2_RADIO_FRACT_INC,
        /// <summary>Increments ADF by 100 KHz</summary>
        ADF_100_INC,
        /// <summary>Increments ADF by 10 KHz</summary>
        ADF_10_INC,
        /// <summary>Increments ADF by 1 KHz</summary>
        ADF_1_INC,
        /// <summary>Increments first digit of transponder</summary>
        XPNDR_1000_INC,
        /// <summary>Increments second digit of transponder</summary>
        XPNDR_100_INC,
        /// <summary>Increments third digit of transponder</summary>
        XPNDR_10_INC,
        /// <summary>Increments fourth digit of transponder</summary>
        XPNDR_1_INC,
        /// <summary>Decrements the VOR 1 OBS setting</summary>
        VOR1_OBI_DEC,
        /// <summary>Increments the VOR 1 OBS setting</summary>
        VOR1_OBI_INC,
        /// <summary>Decrements the VOR 2 OBS setting</summary>
        VOR2_OBI_DEC,
        /// <summary>Increments the VOR 2 OBS setting</summary>
        VOR2_OBI_INC,
        /// <summary>Decrements ADF by 100 KHz</summary>
        ADF_100_DEC,
        /// <summary>Decrements ADF by 10 KHz</summary>
        ADF_10_DEC,
        /// <summary>Decrements ADF by 1 KHz</summary>
        ADF_1_DEC,
        /// <summary>Sets COM frequency (BCD Hz)</summary>
        COM_RADIO_SET,
        /// <summary>Sets NAV 1 frequency (BCD Hz)</summary>
        NAV1_RADIO_SET,
        /// <summary>Sets NAV 2 frequency (BCD Hz)</summary>
        NAV2_RADIO_SET,
        /// <summary>Sets ADF frequency (BCD Hz)</summary>
        ADF_SET,
        /// <summary>Sets transponder code (BCD)</summary>
        XPNDR_SET,
        /// <summary>Sets OBS 1 (0 to 360)</summary>
        VOR1_SET,
        /// <summary>Sets OBS 2 (0 to 360)</summary>
        VOR2_SET,
        /// <summary>Sets DME display to Nav 1</summary>
        DME1_TOGGLE,
        /// <summary>Sets DME display to Nav 2</summary>
        DME2_TOGGLE,
        /// <summary>Turns NAV 1 ID off</summary>
        RADIO_VOR1_IDENT_DISABLE,
        /// <summary>Turns NAV 2 ID off</summary>
        RADIO_VOR2_IDENT_DISABLE,
        /// <summary>Turns DME 1 ID off</summary>
        RADIO_DME1_IDENT_DISABLE,
        /// <summary>Turns DME 2 ID off</summary>
        RADIO_DME2_IDENT_DISABLE,
        /// <summary>Turns ADF 1 ID off</summary>
        RADIO_ADF_IDENT_DISABLE,
        /// <summary>Turns NAV 1 ID on</summary>
        RADIO_VOR1_IDENT_ENABLE,
        /// <summary>Turns NAV 2 ID on</summary>
        RADIO_VOR2_IDENT_ENABLE,
        /// <summary>Turns DME 1 ID on</summary>
        RADIO_DME1_IDENT_ENABLE,
        /// <summary>Turns DME 2 ID on</summary>
        RADIO_DME2_IDENT_ENABLE,
        /// <summary>Turns ADF 1 ID on</summary>
        RADIO_ADF_IDENT_ENABLE,
        /// <summary>Toggles NAV 1 ID</summary>
        RADIO_VOR1_IDENT_TOGGLE,
        /// <summary>Toggles NAV 2 ID</summary>
        RADIO_VOR2_IDENT_TOGGLE,
        /// <summary>Toggles DME 1 ID</summary>
        RADIO_DME1_IDENT_TOGGLE,
        /// <summary>Toggles DME 2 ID</summary>
        RADIO_DME2_IDENT_TOGGLE,
        /// <summary>Toggles ADF 1 ID</summary>
        RADIO_ADF_IDENT_TOGGLE,
        /// <summary>Sets NAV 1 ID (on/off)</summary>
        RADIO_VOR1_IDENT_SET,
        /// <summary>Sets NAV 2 ID (on/off)</summary>
        RADIO_VOR2_IDENT_SET,
        /// <summary>Sets DME 1 ID (on/off)</summary>
        RADIO_DME1_IDENT_SET,
        /// <summary>Sets DME 2 ID (on/off)</summary>
        RADIO_DME2_IDENT_SET,
        /// <summary>Sets ADF 1 ID (on/off)</summary>
        RADIO_ADF_IDENT_SET,
        /// <summary>Increments ADF card</summary>
        ADF_CARD_INC,
        /// <summary>Decrements ADF card</summary>
        ADF_CARD_DEC,
        /// <summary>Sets ADF card (0-360)</summary>
        ADF_CARD_SET,
        /// <summary>Toggles between NAV 1 and NAV 2</summary>
        TOGGLE_DME,
        /// <summary>Sets the avionics master switch</summary>
        AVIONICS_MASTER_SET,
        /// <summary>Toggles the avionics master switch</summary>
        TOGGLE_AVIONICS_MASTER,
        /// <summary>Sets COM 1 standby frequency (BCD Hz)</summary>
        COM_STBY_RADIO_SET,
        /// <summary>Swaps COM 1 frequency with standby</summary>
        COM_STBY_RADIO_SWAP,
        /// <summary>"Decrement COM 1 frequency by 25 KHz</summary>
        COM_RADIO_FRACT_DEC_CARRY,
        /// <summary>"Increment COM 1 frequency by 25 KHz</summary>
        COM_RADIO_FRACT_INC_CARRY,
        /// <summary>"Decrement COM 2 frequency by 1 MHz</summary>
        COM2_RADIO_WHOLE_DEC,
        /// <summary>"Increment COM 2 frequency by 1 MHz</summary>
        COM2_RADIO_WHOLE_INC,
        /// <summary>"Decrement COM 2 frequency by 25 KHz</summary>
        COM2_RADIO_FRACT_DEC,
        /// <summary>"Decrement COM 2 frequency by 25 KHz</summary>
        COM2_RADIO_FRACT_DEC_CARRY,
        /// <summary>"Increment COM 2 frequency by 25 KHz</summary>
        COM2_RADIO_FRACT_INC,
        /// <summary>"Increment COM 2 frequency by 25 KHz</summary>
        COM2_RADIO_FRACT_INC_CARRY,
        /// <summary>Sets COM 2 frequency (BCD Hz)</summary>
        COM2_RADIO_SET,
        /// <summary>Sets COM 2 standby frequency (BCD Hz)</summary>
        COM2_STBY_RADIO_SET,
        /// <summary>Swaps COM 2 frequency with standby</summary>
        COM2_RADIO_SWAP,
        /// <summary>"Decrement NAV 1 frequency by 50 KHz</summary>
        NAV1_RADIO_FRACT_DEC_CARRY,
        /// <summary>"Increment NAV 1 frequency by 50 KHz</summary>
        NAV1_RADIO_FRACT_INC_CARRY,
        /// <summary>Sets NAV 1 standby frequency (BCD Hz)</summary>
        NAV1_STBY_SET,
        /// <summary>Swaps NAV 1 frequency with standby</summary>
        NAV1_RADIO_SWAP,
        /// <summary>"Decrement NAV 2 frequency by 50 KHz</summary>
        NAV2_RADIO_FRACT_DEC_CARRY,
        /// <summary>"Increment NAV 2 frequency by 50 KHz</summary>
        NAV2_RADIO_FRACT_INC_CARRY,
        /// <summary>Sets NAV 2 standby frequency (BCD Hz)</summary>
        NAV2_STBY_SET,
        /// <summary>Swaps NAV 2 frequency with standby</summary>
        NAV2_RADIO_SWAP,
        /// <summary>Decrements ADF 1 by 0.1 KHz.</summary>
        ADF1_RADIO_TENTHS_DEC,
        /// <summary>Increments ADF 1 by 0.1 KHz.</summary>
        ADF1_RADIO_TENTHS_INC,
        /// <summary>Decrements first digit of transponder</summary>
        XPNDR_1000_DEC,
        /// <summary>Decrements second digit of transponder</summary>
        XPNDR_100_DEC,
        /// <summary>Decrements third digit of transponder</summary>
        XPNDR_10_DEC,
        /// <summary>Decrements fourth digit of transponder</summary>
        XPNDR_1_DEC,
        /// <summary>"Decrements fourth digit of transponder</summary>
        XPNDR_DEC_CARRY,
        /// <summary>"Increments fourth digit of transponder</summary>
        XPNDR_INC_CARRY,
        /// <summary>"Decrements ADF 1 frequency by 0.1 KHz</summary>
        ADF_FRACT_DEC_CARRY,
        /// <summary>"Increments ADF 1 frequency by 0.1 KHz</summary>
        ADF_FRACT_INC_CARRY,
        /// <summary>Selects COM 1 to transmit</summary>
        COM1_TRANSMIT_SELECT,
        /// <summary>Selects COM 2 to transmit</summary>
        COM2_TRANSMIT_SELECT,
        /// <summary>Toggles all COM radios to receive on</summary>
        COM_RECEIVE_ALL_TOGGLE,
        /// <summary>"Sets whether to receive on all COM radios (1</summary>
        COM_RECEIVE_ALL_SET,
        /// <summary>Toggles marker beacon sound on/off</summary>
        MARKER_SOUND_TOGGLE,
        /// <summary>Sets ADF 1 frequency (BCD Hz)</summary>
        ADF_COMPLETE_SET,
        /// <summary>"Increments ADF 1 by 1 KHz</summary>
        ADF1_WHOLE_INC,
        /// <summary>"Decrements ADF 1 by 1 KHz</summary>
        ADF1_WHOLE_DEC,
        /// <summary>"Increments the ADF 2 frequency 100 digit</summary>
        ADF2_100_INC,
        /// <summary>"Increments the ADF 2 frequency 10 digit</summary>
        ADF2_10_INC,
        /// <summary>"Increments the ADF 2 frequency 1 digit</summary>
        ADF2_1_INC,
        /// <summary>"Increments ADF 2 frequency 1/10 digit</summary>
        ADF2_RADIO_TENTHS_INC,
        /// <summary>"Decrements the ADF 2 frequency 100 digit</summary>
        ADF2_100_DEC,
        /// <summary>"Decrements the ADF 2 frequency 10 digit</summary>
        ADF2_10_DEC,
        /// <summary>"Decrements the ADF 2 frequency 1 digit</summary>
        ADF2_1_DEC,
        /// <summary>"Decrements ADF 2 frequency 1/10 digit</summary>
        ADF2_RADIO_TENTHS_DEC,
        /// <summary>"Increments ADF 2 by 1 KHz</summary>
        ADF2_WHOLE_INC,
        /// <summary>"Decrements ADF 2 by 1 KHz</summary>
        ADF2_WHOLE_DEC,
        /// <summary>"Decrements ADF 2 frequency by 0.1 KHz</summary>
        ADF2_FRACT_DEC_CARRY,
        /// <summary>"Increments ADF 2 frequency by 0.1 KHz</summary>
        ADF2_FRACT_INC_CARRY,
        /// <summary>Sets ADF 1 frequency (BCD Hz)</summary>
        ADF2_COMPLETE_SET,
        /// <summary>Turns ADF 2 ID off</summary>
        RADIO_ADF2_IDENT_DISABLE,
        /// <summary>Turns ADF 2 ID on</summary>
        RADIO_ADF2_IDENT_ENABLE,
        /// <summary>Toggles ADF 2 ID</summary>
        RADIO_ADF2_IDENT_TOGGLE,
        /// <summary>"Sets ADF 2 ID on/off (1</summary>
        RADIO_ADF2_IDENT_SET,
        /// <summary>Swaps frequency with standby on whichever NAV or COM radio is selected.</summary>
        FREQUENCY_SWAP,
        /// <summary>Toggles between GPS and NAV 1 driving NAV 1 OBS display (and AP)</summary>
        TOGGLE_GPS_DRIVES_NAV1,
        /// <summary>Toggles power button</summary>
        GPS_POWER_BUTTON,
        /// <summary>Selects Nearest Airport Page</summary>
        GPS_NEAREST_BUTTON,
        /// <summary>Toggles automatic sequencing of waypoints</summary>
        GPS_OBS_BUTTON,
        /// <summary>Toggles the Message Page</summary>
        GPS_MSG_BUTTON,
        /// <summary>Triggers the pressing of the message button.</summary>
        GPS_MSG_BUTTON_DOWN,
        /// <summary>Triggers the release of the message button</summary>
        GPS_MSG_BUTTON_UP,
        /// <summary>Displays the programmed flightplan.</summary>
        GPS_FLIGHTPLAN_BUTTON,
        /// <summary>Displays terrain information on default display</summary>
        GPS_TERRAIN_BUTTON,
        /// <summary>Displays the approach procedure page.</summary>
        GPS_PROCEDURE_BUTTON,
        /// <summary>Zooms in default display</summary>
        GPS_ZOOMIN_BUTTON,
        /// <summary>Zooms out default display</summary>
        GPS_ZOOMOUT_BUTTON,
        /// <summary>"Brings up the ""Direct To"" page"</summary>
        GPS_DIRECTTO_BUTTON,
        /// <summary>Brings up page to select active legs in a flightplan.</summary>
        GPS_MENU_BUTTON,
        /// <summary>Clears entered data on a page</summary>
        GPS_CLEAR_BUTTON,
        /// <summary>Clears all data immediately</summary>
        GPS_CLEAR_ALL_BUTTON,
        /// <summary>Triggers the pressing of the Clear button</summary>
        GPS_CLEAR_BUTTON_DOWN,
        /// <summary>Triggers the release of the Clear button.</summary>
        GPS_CLEAR_BUTTON_UP,
        /// <summary>Approves entered data.</summary>
        GPS_ENTER_BUTTON,
        /// <summary>Selects GPS cursor</summary>
        GPS_CURSOR_BUTTON,
        /// <summary>Increments cursor</summary>
        GPS_GROUP_KNOB_INC,
        /// <summary>Decrements cursor</summary>
        GPS_GROUP_KNOB_DEC,
        /// <summary>Increments through pages</summary>
        GPS_PAGE_KNOB_INC,
        /// <summary>Decrements through pages</summary>
        GPS_PAGE_KNOB_DEC,
        /// <summary>"Selects one of the two DME systems (1</summary>
        DME_SELECT,
        /// <summary>Turns on the identification sound for the selected DME.</summary>
        RADIO_SELECTED_DME_IDENT_ENABLE,
        /// <summary>Turns off the identification sound for the selected DME.</summary>
        RADIO_SELECTED_DME_IDENT_DISABLE,
        /// <summary>Sets the DME identification sound to the given filename.</summary>
        RADIO_SELECTED_DME_IDENT_SET,
        /// <summary>Turns on or off the identification sound for the selected DME.</summary>
        RADIO_SELECTED_DME_IDENT_TOGGLE,

        #endregion

        #region Aircraft Instruments
        /// <summary>Selects EGT bug for +/-</summary>
        EGT,
        /// <summary>Increments EGT bugs</summary>
        EGT_INC,
        /// <summary>Decrements EGT bugs</summary>
        EGT_DEC,
        /// <summary>Sets EGT bugs (0 to 32767)</summary>
        EGT_SET,
        /// <summary>"Syncs altimeter setting to sea level pressure</summary>
        BAROMETRIC,
        /// <summary>Increments heading indicator</summary>
        GYRO_DRIFT_INC,
        /// <summary>Decrements heading indicator</summary>
        GYRO_DRIFT_DEC,
        /// <summary>Increments altimeter setting</summary>
        KOHLSMAN_INC,
        /// <summary>Decrements altimeter setting</summary>
        KOHLSMAN_DEC,
        /// <summary>Sets altimeter setting (Millibars * 16)</summary>
        KOHLSMAN_SET,
        /// <summary>Increments airspeed indicators true airspeed reference card</summary>
        TRUE_AIRSPEED_CAL_INC,
        /// <summary>Decrements airspeed indicators true airspeed reference card</summary>
        TRUE_AIRSPEED_CAL_DEC,
        /// <summary>"Sets airspeed indicators true airspeed reference card (degrees</summary>
        TRUE_AIRSPEED_CAL_SET,
        /// <summary>Increments EGT bug 1</summary>
        EGT1_INC,
        /// <summary>Decrements EGT bug 1</summary>
        EGT1_DEC,
        /// <summary>Sets EGT bug 1 (0 to 32767)</summary>
        EGT1_SET,
        /// <summary>Increments EGT bug 2</summary>
        EGT2_INC,
        /// <summary>Decrements EGT bug 2</summary>
        EGT2_DEC,
        /// <summary>Sets EGT bug 2 (0 to 32767)</summary>
        EGT2_SET,
        /// <summary>Increments EGT bug 3</summary>
        EGT3_INC,
        /// <summary>Decrements EGT bug 3</summary>
        EGT3_DEC,
        /// <summary>Sets EGT bug 3 (0 to 32767)</summary>
        EGT3_SET,
        /// <summary>Increments EGT bug 4</summary>
        EGT4_INC,
        /// <summary>Decrements EGT bug 4</summary>
        EGT4_DEC,
        /// <summary>Sets EGT bug 4 (0 to 32767)</summary>
        EGT4_SET,
        /// <summary>Increments attitude indicator pitch reference bars</summary>
        ATTITUDE_BARS_POSITION_UP,
        /// <summary>Decrements attitude indicator pitch reference bars</summary>
        ATTITUDE_BARS_POSITION_DOWN,
        /// <summary>Cages attitude indicator at 0 pitch and bank</summary>
        ATTITUDE_CAGE_BUTTON,
        /// <summary>Resets max/min indicated G force to 1.0.</summary>
        RESET_G_FORCE_INDICATOR,
        /// <summary>Reset max indicated engine rpm to 0.</summary>
        RESET_MAX_RPM_INDICATOR,
        /// <summary>Sets heading indicator to 0 drift error.</summary>
        HEADING_GYRO_SET,
        /// <summary>Sets heading indicator drift angle (degrees).</summary>
        GYRO_DRIFT_SET,

        #endregion

        #region Aircraft Lights
        /// <summary>Toggle strobe lights </summary>
        STROBES_TOGGLE,
        /// <summary>Toggle all lights</summary>
        ALL_LIGHTS_TOGGLE,
        /// <summary>Toggle panel lights</summary>
        PANEL_LIGHTS_TOGGLE,
        /// <summary>Toggle landing lights</summary>
        LANDING_LIGHTS_TOGGLE,
        /// <summary>Rotate landing light up</summary>
        LANDING_LIGHT_UP,
        /// <summary>Rotate landing light down</summary>
        LANDING_LIGHT_DOWN,
        /// <summary>Rotate landing light left</summary>
        LANDING_LIGHT_LEFT,
        /// <summary>Rotate landing light right</summary>
        LANDING_LIGHT_RIGHT,
        /// <summary>Return landing light to default position</summary>
        LANDING_LIGHT_HOME,
        /// <summary>Turn strobe lights on</summary>
        STROBES_ON,
        /// <summary>Turn strobe light off</summary>
        STROBES_OFF,
        /// <summary>"Set strobe lights on/off (1</summary>
        STROBES_SET,
        /// <summary>Turn panel lights on</summary>
        PANEL_LIGHTS_ON,
        /// <summary>Turn panel lights off</summary>
        PANEL_LIGHTS_OFF,
        /// <summary>"Set panel lights on/off (1</summary>
        PANEL_LIGHTS_SET,
        /// <summary>Turn landing lights on</summary>
        LANDING_LIGHTS_ON,
        /// <summary>Turn landing lights off</summary>
        LANDING_LIGHTS_OFF,
        /// <summary>"Set landing lights on/off (1</summary>
        LANDING_LIGHTS_SET,
        /// <summary>Toggle beacon lights</summary>
        TOGGLE_BEACON_LIGHTS,
        /// <summary>Toggle taxi lights</summary>
        TOGGLE_TAXI_LIGHTS,
        /// <summary>Toggle logo lights</summary>
        TOGGLE_LOGO_LIGHTS,
        /// <summary>Toggle recognition lights</summary>
        TOGGLE_RECOGNITION_LIGHTS,
        /// <summary>Toggle wing lights</summary>
        TOGGLE_WING_LIGHTS,
        /// <summary>Toggle navigation lights</summary>
        TOGGLE_NAV_LIGHTS,
        /// <summary>Toggle cockpit/cabin lights</summary>
        TOGGLE_CABIN_LIGHTS,
        #endregion

        #region Aircraft Failures
        /// <summary>Toggle vacuum system failure</summary>
        TOGGLE_VACUUM_FAILURE,
        /// <summary>Toggle electrical system failure</summary>
        TOGGLE_ELECTRICAL_FAILURE,
        /// <summary>Toggles blocked pitot tube</summary>
        TOGGLE_PITOT_BLOCKAGE,
        /// <summary> Toggles blocked static port</summary>
        TOGGLE_STATIC_PORT_BLOCKAGE,
        /// <summary>Toggles hydraulic system failure</summary>
        TOGGLE_HYDRAULIC_FAILURE,
        /// <summary>Toggles brake failure (both)</summary>
        TOGGLE_TOTAL_BRAKE_FAILURE,
        /// <summary>Toggles left brake failure</summary>
        TOGGLE_LEFT_BRAKE_FAILURE,
        /// <summary>Toggles right brake failure</summary>
        TOGGLE_RIGHT_BRAKE_FAILURE,
        /// <summary>Toggle engine 1 failure</summary>
        TOGGLE_ENGINE1_FAILURE,
        /// <summary>Toggle engine 2 failure</summary>
        TOGGLE_ENGINE2_FAILURE,
        /// <summary>Toggle engine 3 failure</summary>
        TOGGLE_ENGINE3_FAILURE,
        /// <summary>Toggle engine 4 failure</summary>
        TOGGLE_ENGINE4_FAILURE,
        #endregion

        #region Aircraft Miscellaneous Systems
        /// <summary>Toggle smoke system switch</summary>
        SMOKE_TOGGLE,
        /// <summary>Toggle gear handle</summary>
        GEAR_TOGGLE,
        /// <summary>Increment brake pressure </summary>
        BRAKES,
        /// <summary>"Sets gear handle position up/down (0</summary>
        GEAR_SET,
        /// <summary>Increments left brake pressure</summary>
        BRAKES_LEFT,
        /// <summary>Increments right brake pressure</summary>
        BRAKES_RIGHT,
        /// <summary>Toggles parking brake on/off</summary>
        PARKING_BRAKES,
        /// <summary>Increments emergency gear extension</summary>
        GEAR_PUMP,
        /// <summary>Toggles pitot heat switch</summary>
        PITOT_HEAT_TOGGLE,
        /// <summary>Turns smoke system on</summary>
        SMOKE_ON,
        /// <summary>Turns smoke system off</summary>
        SMOKE_OFF,
        /// <summary>"Sets smoke system on/off (1</summary>
        SMOKE_SET,
        /// <summary>Turns pitot heat switch on</summary>
        PITOT_HEAT_ON,
        /// <summary>Turns pitot heat switch off</summary>
        PITOT_HEAT_OFF,
        /// <summary>"Sets pitot heat switch on/off (1</summary>
        PITOT_HEAT_SET,
        /// <summary>Sets gear handle in UP position</summary>
        GEAR_UP,
        /// <summary>Sets gear handle in DOWN position</summary>
        GEAR_DOWN,
        /// <summary>Toggles main battery switch</summary>
        TOGGLE_MASTER_BATTERY,
        /// <summary>Toggles main alternator/generator switch</summary>
        TOGGLE_MASTER_ALTERNATOR,
        /// <summary>Toggles backup electric vacuum pump</summary>
        TOGGLE_ELECTRIC_VACUUM_PUMP,
        /// <summary>Toggles alternate static pressure port</summary>
        TOGGLE_ALTERNATE_STATIC,
        /// <summary>Decrements decision height reference</summary>
        DECREASE_DECISION_HEIGHT,
        /// <summary>Increments decision height reference</summary>
        INCREASE_DECISION_HEIGHT,
        /// <summary>Toggles structural deice switch</summary>
        TOGGLE_STRUCTURAL_DEICE,
        /// <summary>Toggles propeller deice switch</summary>
        TOGGLE_PROPELLER_DEICE,
        /// <summary>Toggles alternator/generator 1 switch</summary>
        TOGGLE_ALTERNATOR1,
        /// <summary>Toggles alternator/generator 2 switch</summary>
        TOGGLE_ALTERNATOR2,
        /// <summary>Toggles alternator/generator 3 switch</summary>
        TOGGLE_ALTERNATOR3,
        /// <summary>Toggles alternator/generator 4 switch</summary>
        TOGGLE_ALTERNATOR4,
        /// <summary>Toggles master battery and alternator switch</summary>
        TOGGLE_MASTER_BATTERY_ALTERNATOR,
        /// <summary>Sets left brake position from axis controller (e.g. joystick). -16383 (0 brakes) to +16383 (max brakes)</summary>
        AXIS_LEFT_BRAKE_SET,
        /// <summary>Sets right brake position from axis controller (e.g. joystick). -16383 (0 brakes) to +16383 (max brakes)</summary>
        AXIS_RIGHT_BRAKE_SET,
        /// <summary>"Toggles primary door open/close. Follow by KEY_SELECT_2</summary>
        TOGGLE_AIRCRAFT_EXIT,
        /// <summary>Toggles wing folding</summary>
        TOGGLE_WING_FOLD,
        /// <summary>"Sets the wings into the folded position suitable for storage, typically on a carrier. Takes a value: 1 - fold wings, 0 - unfold wings</summary>
        SET_WING_FOLD,
        /// <summary>Sets the tail hook handle. Takes a value: 1 - set tail hook, 0 - retract tail hook</summary>
        TOGGLE_TAIL_HOOK_HANDLE,
        /// <summary>"Sets the tail hook handle. Takes a value:</summary>
        SET_TAIL_HOOK_HANDLE,
        /// <summary>Toggles water rudders</summary>
        TOGGLE_WATER_RUDDER,
        /// <summary>Toggles pushback.</summary>
        TOGGLE_PUSHBACK,
        /// <summary>"Triggers tug and sets the desired heading. The units are a 32 bit integer (0 to 4294967295) which represent 0 to 360 degrees. To set a 45 degree angle</summary>
        KEY_TUG_HEADING,
        /// <summary>"Triggers tug</summary>
        KEY_TUG_SPEED,
        /// <summary>Disables tug</summary>
        TUG_DISABLE,
        /// <summary>Toggles master ignition switch</summary>
        TOGGLE_MASTER_IGNITION_SWITCH,
        /// <summary>Toggles tail wheel lock</summary>
        TOGGLE_TAILWHEEL_LOCK,
        /// <summary>"Adds fuel to the aircraft</summary>
        ADD_FUEL_QUANTITY,
        /// <summary>"Release a towed aircraft</summary>
        TOW_PLANE_RELEASE,
        /// <summary>"Request a tow plane. The user aircraft must be tow-able</summary>
        TOW_PLANE_REQUEST,
        /// <summary>Release one droppable object. Multiple key events will release multiple objects.</summary>
        RELEASE_DROPPABLE_OBJECTS,
        /// <summary>"If the plane has retractable floats</summary>
        RETRACT_FLOAT_SWITCH_DEC,
        /// <summary>"If the plane has retractable floats</summary>
        RETRACT_FLOAT_SWITCH_INC,
        /// <summary>Turn the water ballast valve on or off.</summary>
        TOGGLE_WATER_BALLAST_VALVE,
        /// <summary>Turn the variometer on or off.</summary>
        TOGGLE_VARIOMETER_SWITCH,
        /// <summary>Turn the turn indicator on or off.</summary>
        TOGGLE_TURN_INDICATOR_SWITCH,
        /// <summary>Start up the auxiliary power unit (APU).</summary>
        APU_STARTER,
        /// <summary>Turn the APU off.</summary>
        APU_OFF_SWITCH,
        /// <summary>Turn the auxiliary generator on or off.</summary>
        APU_GENERATOR_SWITCH_TOGGLE,
        /// <summary>"Set the auxiliary generator switch (0</summary>
        APU_GENERATOR_SWITCH_SET,
        /// <summary>"Takes a two digit argument.  The first digit represents the fire extinguisher index</summary>
        EXTINGUISH_ENGINE_FIRE,
        /// <summary>Turn the hydraulic switch on or off.</summary>
        HYDRAULIC_SWITCH_TOGGLE,
        /// <summary>Increases the bleed air source control.</summary>
        BLEED_AIR_SOURCE_CONTROL_INC,
        /// <summary>Decreases the bleed air source control.</summary>
        BLEED_AIR_SOURCE_CONTROL_DEC,
        /// <summary>"Set to one of:</summary>
        BLEED_AIR_SOURCE_CONTROL_SET,
        /// <summary>Turn the turbine ignition switch on or off.</summary>
        TURBINE_IGNITION_SWITCH_TOGGLE,
        /// <summary>"Turn the ""No smoking"" alert on or off."</summary>
        CABIN_NO_SMOKING_ALERT_SWITCH_TOGGLE,
        /// <summary>"Turn the ""Fasten seatbelts"" alert on or off."</summary>
        CABIN_SEATBELTS_ALERT_SWITCH_TOGGLE,
        /// <summary>Turn the anti-skid braking system on or off.</summary>
        ANTISKID_BRAKES_TOGGLE,
        /// <summary>Turn the g round proximity warning system (GPWS) on or off.</summary>
        GPWS_SWITCH_TOGGLE,
        /// <summary>Activate the manual fuel pressure pump.</summary>
        MANUAL_FUEL_PRESSURE_PUMP,
        /// <summary>Increments the nose wheel steering position by 5 percent.</summary>
        STEERING_INC,
        /// <summary>Decrements the nose wheel steering position by 5 percent.</summary>
        STEERING_DEC,
        /// <summary>"Sets the value of the nose wheel steering position. Zero is straight ahead (-16383</summary>
        STEERING_SET,
        /// <summary>Increases the altitude that the cabin is pressurized to.</summary>
        KEY_PRESSURIZATION_PRESSURE_ALT_INC,
        /// <summary>Decreases the altitude that the cabin is pressurized to.</summary>
        KEY_PRESSURIZATION_PRESSURE_ALT_DEC,
        /// <summary>Sets the rate at which cabin pressurization is increased.</summary>
        PRESSURIZATION_CLIMB_RATE_INC,
        /// <summary>Sets the rate at which cabin pressurization is decreased.</summary>
        PRESSURIZATION_CLIMB_RATE_DEC,
        /// <summary>Sets the cabin pressure to the outside air pressure.</summary>
        PRESSURIZATION_PRESSURE_DUMP_SWTICH,
        /// <summary>Deploy or remove the assist arm. Refer to the document Notes on Aircraft Systems.</summary>
        TAKEOFF_ASSIST_ARM_TOGGLE,
        /// <summary>"Value:</summary>
        TAKEOFF_ASSIST_ARM_SET,
        /// <summary>If everything is set up correctly. Launch from the catapult.</summary>
        TAKEOFF_ASSIST_FIRE,
        /// <summary>Toggle the request for the launch bar to be installed or removed.</summary>
        TOGGLE_LAUNCH_BAR_SWITCH,
        /// <summary>"Value: TRUE request set, FALSE request unset</summary>
        SET_LAUNCH_BAR_SWITCH,
        #endregion

        #region Helicopter Specific Systems
        /// <summary>Triggers rotor braking input</summary>
        ROTOR_BRAKE,
        /// <summary>Toggles on electric rotor clutch switch</summary>
        ROTOR_CLUTCH_SWITCH_TOGGLE,
        /// <summary>"Sets electric rotor clutch switch on/off (1</summary>
        ROTOR_CLUTCH_SWITCH_SET,
        /// <summary>Toggles the electric rotor governor switch</summary>
        ROTOR_GOV_SWITCH_TOGGLE,
        /// <summary>"Sets the electric rotor governor switch on/off (1</summary>
        ROTOR_GOV_SWITCH_SET,
        /// <summary>Increments the lateral (right) rotor trim</summary>
        ROTOR_LATERAL_TRIM_INC,
        /// <summary>Decrements the lateral (right) rotor trim</summary>
        ROTOR_LATERAL_TRIM_DEC,
        /// <summary>Sets the lateral (right) rotor trim (0 to 16383)</summary>
        ROTOR_LATERAL_TRIM_SET,
        /// <summary>Toggle between pickup and release mode. Hold mode is automatic and cannot be selected. Refer to the document Notes on Aircraft Systems.</summary>
        SLING_PICKUP_RELEASE,
        /// <summary>The rate at which a hoist cable extends is set in the Aircraft Configuration File.</summary>
        HOIST_SWITCH_EXTEND,
        /// <summary>The rate at which a hoist cable retracts is set in the Aircraft Configuration File.</summary>
        HOIST_SWITCH_RETRACT,
        /// <summary>"The data value should be set to one of:</summary>
        HOIST_SWITCH_SET,
        /// <summary>"Toggles the hoist arm switch</summary>
        HOIST_DEPLOY_TOGGLE,
        /// <summary>"The data value should be set to:</summary>
        HOIST_DEPLOY_SET,
        #endregion

        #region Aircraft Slew System
        /// <summary>Toggles slew on/off</summary>
        SLEW_TOGGLE,
        /// <summary>Turns slew off</summary>
        SLEW_OFF,
        /// <summary>Turns slew on</summary>
        SLEW_ON,
        /// <summary>"Sets slew on/off (1</summary>
        SLEW_SET,
        /// <summary>"Stop slew and reset pitch</summary>
        SLEW_RESET,
        /// <summary>Slew upward fast</summary>
        SLEW_ALTIT_UP_FAST,
        /// <summary>Slew upward slow</summary>
        SLEW_ALTIT_UP_SLOW,
        /// <summary>Stop vertical slew</summary>
        SLEW_ALTIT_FREEZE,
        /// <summary>Slew downward slow</summary>
        SLEW_ALTIT_DN_SLOW,
        /// <summary>Slew downward fast</summary>
        SLEW_ALTIT_DN_FAST,
        /// <summary>Increase upward slew</summary>
        SLEW_ALTIT_PLUS,
        /// <summary>Decrease upward slewÂ </summary>
        SLEW_ALTIT_MINUS,
        /// <summary>Slew pitch downward fast</summary>
        SLEW_PITCH_DN_FAST,
        /// <summary>Slew pitch downward slow</summary>
        SLEW_PITCH_DN_SLOW,
        /// <summary>Stop pitch slew</summary>
        SLEW_PITCH_FREEZE,
        /// <summary>Slew pitch up slow</summary>
        SLEW_PITCH_UP_SLOW,
        /// <summary>Slew pitch upward fast</summary>
        SLEW_PITCH_UP_FAST,
        /// <summary>Increase pitch up slew</summary>
        SLEW_PITCH_PLUS,
        /// <summary>Decrease pitch up slew</summary>
        SLEW_PITCH_MINUS,
        /// <summary>Increase left bank slew</summary>
        SLEW_BANK_MINUS,
        /// <summary>Increase forward slew</summary>
        SLEW_AHEAD_PLUS,
        /// <summary>Increase right bank slew</summary>
        SLEW_BANK_PLUS,
        /// <summary>Slew to the left</summary>
        SLEW_LEFT,
        /// <summary>Stop all slew</summary>
        SLEW_FREEZE,
        /// <summary>Slew to the right</summary>
        SLEW_RIGHT,
        /// <summary>Increase slew heading to the left</summary>
        SLEW_HEADING_MINUS,
        /// <summary>Decrease forward slew</summary>
        SLEW_AHEAD_MINUS,
        /// <summary>Increase slew heading to the right</summary>
        SLEW_HEADING_PLUS,
        /// <summary>Sets forward slew (+/- 16383)</summary>
        AXIS_SLEW_AHEAD_SET,
        /// <summary>Sets sideways slew (+/- 16383)</summary>
        AXIS_SLEW_SIDEWAYS_SET,
        /// <summary>Sets heading slew (+/- 16383)</summary>
        AXIS_SLEW_HEADING_SET,
        /// <summary>Sets vertical slew (+/- 16383)</summary>
        AXIS_SLEW_ALT_SET,
        /// <summary>Sets roll slew (+/- 16383)</summary>
        AXIS_SLEW_BANK_SET,
        /// <summary>Sets pitch slew (+/- 16383)</summary>
        AXIS_SLEW_PITCH_SET,
        #endregion

        #region View System
        /// <summary>Selects next view</summary>
        VIEW_MODE,
        /// <summary>Sets active window to front</summary>
        VIEW_WINDOW_TO_FRONT,
        /// <summary>Resets the view to the default</summary>
        VIEW_RESET,
        /// <summary>Â </summary>
        VIEW_ALWAYS_PAN_UP,
        /// <summary>Â </summary>
        VIEW_ALWAYS_PAN_DOWN,
        /// <summary>Â </summary>
        NEXT_SUB_VIEW,
        /// <summary>Â </summary>
        PREV_SUB_VIEW,
        /// <summary>Â </summary>
        VIEW_TRACK_PAN_TOGGLE,
        /// <summary>Â </summary>
        VIEW_PREVIOUS_TOGGLE,
        /// <summary>Â </summary>
        VIEW_CAMERA_SELECT_START,
        /// <summary>Â </summary>
        PANEL_HUD_NEXT,
        /// <summary>Â </summary>
        PANEL_HUD_PREVIOUS,
        /// <summary>Zooms view in</summary>
        ZOOM_IN,
        /// <summary>Zooms view out</summary>
        ZOOM_OUT,
        /// <summary>Fine zoom in map view</summary>
        MAP_ZOOM_FINE_IN,
        /// <summary>Pans view left</summary>
        PAN_LEFT,
        /// <summary>Pans view right</summary>
        PAN_RIGHT,
        /// <summary>Fine zoom out in map view</summary>
        MAP_ZOOM_FINE_OUT,
        /// <summary>Sets view direction forward</summary>
        VIEW_FORWARD,
        /// <summary>Sets view direction forward and right</summary>
        VIEW_FORWARD_RIGHT,
        /// <summary>Sets view direction to the right</summary>
        VIEW_RIGHT,
        /// <summary>Sets view direction to the rear and right</summary>
        VIEW_REAR_RIGHT,
        /// <summary>Sets view direction to the rear</summary>
        VIEW_REAR,
        /// <summary>Sets view direction to the rear and left</summary>
        VIEW_REAR_LEFT,
        /// <summary>Sets view direction to the left</summary>
        VIEW_LEFT,
        /// <summary>Sets view direction forward and left</summary>
        VIEW_FORWARD_LEFT,
        /// <summary>Sets view direction down</summary>
        VIEW_DOWN,
        /// <summary>Decreases zoom</summary>
        ZOOM_MINUS,
        /// <summary>Increase zoom</summary>
        ZOOM_PLUS,
        /// <summary>Pan view up</summary>
        PAN_UP,
        /// <summary>Pan view down</summary>
        PAN_DOWN,
        /// <summary>Reverse view cycle</summary>
        VIEW_MODE_REV,
        /// <summary>Zoom in fine</summary>
        ZOOM_IN_FINE,
        /// <summary>Zoom out fine</summary>
        ZOOM_OUT_FINE,
        /// <summary>Close current view</summary>
        CLOSE_VIEW,
        /// <summary>Open new view</summary>
        NEW_VIEW,
        /// <summary>Select next view</summary>
        NEXT_VIEW,
        /// <summary>Select previous view</summary>
        PREV_VIEW,
        /// <summary>Pan view left</summary>
        PAN_LEFT_UP,
        /// <summary>Pan view left and down</summary>
        PAN_LEFT_DOWN,
        /// <summary>Pan view right and up</summary>
        PAN_RIGHT_UP,
        /// <summary>Pan view right and down</summary>
        PAN_RIGHT_DOWN,
        /// <summary>Tilt view left</summary>
        PAN_TILT_LEFT,
        /// <summary>Tilt view right</summary>
        PAN_TILT_RIGHT,
        /// <summary>Reset view to forward</summary>
        PAN_RESET,
        /// <summary>Sets view forward and up</summary>
        VIEW_FORWARD_UP,
        /// <summary>"Sets view forward</summary>
        VIEW_FORWARD_RIGHT_UP,
        /// <summary>Sets view right and up</summary>
        VIEW_RIGHT_UP,
        /// <summary>"Sets view rear</summary>
        VIEW_REAR_RIGHT_UP,
        /// <summary>Sets view rear and up</summary>
        VIEW_REAR_UP,
        /// <summary>Sets view rear left and up</summary>
        VIEW_REAR_LEFT_UP,
        /// <summary>Sets view left and up</summary>
        VIEW_LEFT_UP,
        /// <summary>Sets view forward left and up</summary>
        VIEW_FORWARD_LEFT_UP,
        /// <summary>Sets view up</summary>
        VIEW_UP,
        /// <summary>"Reset panning to forward</summary>
        PAN_RESET_COCKPIT,
        /// <summary>Cycle view to next target</summary>
        KEY_CHASE_VIEW_NEXT,
        /// <summary>Cycle view to previous target</summary>
        KEY_CHASE_VIEW_PREV,
        /// <summary>Toggles chase view on/off</summary>
        CHASE_VIEW_TOGGLE,
        /// <summary>Move eyepoint up</summary>
        EYEPOINT_UP,
        /// <summary>Move eyepoint down</summary>
        EYEPOINT_DOWN,
        /// <summary>Move eyepoint right</summary>
        EYEPOINT_RIGHT,
        /// <summary>Move eyepoint left</summary>
        EYEPOINT_LEFT,
        /// <summary>Move eyepoint forward</summary>
        EYEPOINT_FORWARD,
        /// <summary>Move eyepoint backward</summary>
        EYEPOINT_BACK,
        /// <summary>Move eyepoint to default position</summary>
        EYEPOINT_RESET,
        /// <summary>Opens new map view</summary>
        NEW_MAP,
        /// <summary>"Switch immediately to the forward view</summary>
        VIEW_COCKPIT_FORWARD,
        /// <summary>"Switch immediately to the forward view</summary>
        VIEW_VIRTUAL_COCKPIT_FORWARD,
        /// <summary>"Sets the alpha-blending value for the panel. Takes a parameter in the range 0 to 255. The alpha-blending can be changed from the keyboard using Ctrl-Shift-T</summary>
        VIEW_PANEL_ALPHA_SET,
        /// <summary>"Sets the mode to change the alpha-blending</summary>
        VIEW_PANEL_ALPHA_SELECT,
        /// <summary>Increment alpha-blending for the panel.</summary>
        VIEW_PANEL_ALPHA_INC,
        /// <summary>Decrement alpha-blending for the panel.</summary>
        VIEW_PANEL_ALPHA_DEC,
        /// <summary>"Links all the views from one camera together</summary>
        VIEW_LINKING_SET,
        /// <summary>Turns view linking on or off.</summary>
        VIEW_LINKING_TOGGLE,
        /// <summary>"Increments the distance of the view camera from the chase object (such as in Spot Plane view</summary>
        VIEW_CHASE_DISTANCE_ADD,
        /// <summary>Decrements the distance of the view camera from the chase object.</summary>
        VIEW_CHASE_DISTANCE_SUB,
        #endregion

        #region Miscellaneous Events
        /// <summary>Toggles pause on/off</summary>
        PAUSE_TOGGLE,
        /// <summary>Turns pause on</summary>
        PAUSE_ON,
        /// <summary>Turns pause off</summary>
        PAUSE_OFF,
        /// <summary>"Sets pause on/off (1</summary>
        PAUSE_SET,
        /// <summary>Stops demo system playback</summary>
        DEMO_STOP,
        /// <summary>"Sets ""selected"" index (for other events) to 1"</summary>
        SELECT_1,
        /// <summary>"Sets ""selected"" index (for other events) to 2"</summary>
        SELECT_2,
        /// <summary>"Sets ""selected"" index (for other events) to 3"</summary>
        SELECT_3,
        /// <summary>"Sets ""selected"" index (for other events) to 4"</summary>
        SELECT_4,
        /// <summary>"Used in conjunction with ""selected"" parameters to decrease their value (e.g.</summary>
        MINUS,
        /// <summary>"Used in conjunction with ""selected"" parameters to increase their value (e.g.</summary>
        PLUS,
        /// <summary>Sets zoom level to 1</summary>
        ZOOM_1X,
        /// <summary>Toggles sound on/off</summary>
        SOUND_TOGGLE,
        /// <summary>"Selects simulation rate (use KEY_MINUS</summary>
        SIM_RATE,
        /// <summary>Toggles joystick on/off</summary>
        JOYSTICK_CALIBRATE,
        /// <summary>Saves flight situation</summary>
        SITUATION_SAVE,
        /// <summary>Resets flight situation</summary>
        SITUATION_RESET,
        /// <summary>"Sets sound on/off (1</summary>
        SOUND_SET,
        /// <summary>Quit ESP with a message</summary>
        EXIT,
        /// <summary>Quit ESP without a message</summary>
        ABORT,
        /// <summary>Cycle through information readouts while in slew</summary>
        READOUTS_SLEW,
        /// <summary>Cycle through information readouts</summary>
        READOUTS_FLIGHT,
        /// <summary>Used with other events</summary>
        MINUS_SHIFT,
        /// <summary>Used with other events</summary>
        PLUS_SHIFT,
        /// <summary>Increase sim rate</summary>
        SIM_RATE_INCR,
        /// <summary>Decrease sim rate</summary>
        SIM_RATE_DECR,
        /// <summary>Toggles kneeboard</summary>
        KNEEBOARD_VIEW,
        /// <summary>Toggles panel 1</summary>
        PANEL_1,
        /// <summary>Toggles panel 2</summary>
        PANEL_2,
        /// <summary>Toggles panel 3</summary>
        PANEL_3,
        /// <summary>Toggles panel 4</summary>
        PANEL_4,
        /// <summary>Toggles panel 5</summary>
        PANEL_5,
        /// <summary>Toggles panel 6</summary>
        PANEL_6,
        /// <summary>Toggles panel 7</summary>
        PANEL_7,
        /// <summary>Toggles panel 8</summary>
        PANEL_8,
        /// <summary>Toggles panel 9</summary>
        PANEL_9,
        /// <summary>Turns sound on</summary>
        SOUND_ON,
        /// <summary>Turns sound off</summary>
        SOUND_OFF,
        /// <summary>Brings up Help system</summary>
        INVOKE_HELP,
        /// <summary>Toggles aircraft labels</summary>
        TOGGLE_AIRCRAFT_LABELS,
        /// <summary>Brings up flight map</summary>
        FLIGHT_MAP,
        /// <summary>Reload panel data</summary>
        RELOAD_PANELS,
        /// <summary>Toggles indexed panel (1 to 9)</summary>
        PANEL_ID_TOGGLE,
        /// <summary>Opens indexed panel (1 to 9)</summary>
        PANEL_ID_OPEN,
        /// <summary>Closes indexed panel (1 to 9)</summary>
        PANEL_ID_CLOSE,
        /// <summary>"Reloads the user aircraft data (from cache if same type loaded as an AI</summary>
        RELOAD_USER_AIRCRAFT,
        /// <summary>Resets aircraft state</summary>
        SIM_RESET,
        /// <summary>Turns Flying Tips on/off</summary>
        VIRTUAL_COPILOT_TOGGLE,
        /// <summary>"Sets Flying Tips on/off (1</summary>
        VIRTUAL_COPILOT_SET,
        /// <summary>Triggers action noted in Flying Tips</summary>
        VIRTUAL_COPILOT_ACTION,
        /// <summary>Reloads scenery</summary>
        REFRESH_SCENERY,
        /// <summary>Decrements time by hours</summary>
        CLOCK_HOURS_DEC,
        /// <summary>Increments time by hours</summary>
        CLOCK_HOURS_INC,
        /// <summary>Decrements time by minutes</summary>
        CLOCK_MINUTES_DEC,
        /// <summary>Increments time by minutes</summary>
        CLOCK_MINUTES_INC,
        /// <summary>Zeros seconds</summary>
        CLOCK_SECONDS_ZERO,
        /// <summary>Sets hour of day</summary>
        CLOCK_HOURS_SET,
        /// <summary>Sets minutes of the hour</summary>
        CLOCK_MINUTES_SET,
        /// <summary>"Sets hours</summary>
        ZULU_HOURS_SET,
        /// <summary>"Sets minutes</summary>
        ZULU_MINUTES_SET,
        /// <summary>"Sets day</summary>
        ZULU_DAY_SET,
        /// <summary>"Sets year</summary>
        ZULU_YEAR_SET,
        /// <summary>"Enables a keystroke to be sent to a gauge that is in focus. The keystrokes can only be in the range 0 to 9</summary>
        GAUGE_KEYSTROKE,
        /// <summary>Display the ATC window.</summary>
        SIMUI_WINDOW_HIDESHOW,
        /// <summary>Turn window titles on or off.</summary>
        VIEW_WINDOW_TITLES_TOGGLE,
        /// <summary>Sets the pitch of the axis. Requires an angle.</summary>
        AXIS_PAN_PITCH,
        /// <summary>Sets the heading of the axis. Requires an angle.</summary>
        AXIS_PAN_HEADING,
        /// <summary>Sets the tilt of the axis. Requires an angle.</summary>
        AXIS_PAN_TILT,
        /// <summary>Step through the view axes.</summary>
        VIEW_AXIS_INDICATOR_CYCLE,
        /// <summary>Step through the map orientations.</summary>
        VIEW_MAP_ORIENTATION_CYCLE,
        /// <summary>"Requests a jetway</summary>
        TOGGLE_JETWAY,
        /// <summary>"Turn on or off the video recording feature. This records uncompressed AVI format files to:</summary>
        VIDEO_RECORD_TOGGLE,
        /// <summary>Turn on or off the airport name.</summary>
        TOGGLE_AIRPORT_NAME_DISPLAY,
        /// <summary>"Capture the current view as a screenshot. Which will be saved to a bmp file in:</summary>
        CAPTURE_SCREENSHOT,
        /// <summary>"Switch Mouse Look mode on or off. Mouse Look mode enables a user to control their view using the mouse</summary>
        MOUSE_LOOK_TOGGLE,
        /// <summary>Switch inversion of Y axis controls on or off.</summary>
        YAXIS_INVERT_TOGGLE,
        /// <summary>Turn the automatic rudder control feature on or off.</summary>
        AUTORUDDER_TOGGLE,
        /// <summary>"Turns the freezing of the lat/lon position of the aircraft (either user or AI controlled) on or off. If this key event is set, it means that the latitude and longitude of the aircraft are not being controlled by ESP. </summary>
        FREEZE_LATITUDE_LONGITUDE_TOGGLE,
        /// <summary>Freezes the lat/lon position of the aircraft.</summary>
        FREEZE_LATITUDE_LONGITUDE_SET,
        /// <summary>Turns the freezing of the altitude of the aircraft on or off.</summary>
        FREEZE_ALTITUDE_TOGGLE,
        /// <summary>Freezes the altitude of the aircraft..</summary>
        FREEZE_ALTITUDE_SET,
        /// <summary>"Turns the freezing of the attitude (pitch</summary>
        FREEZE_ATTITUDE_TOGGLE,
        /// <summary>"Freezes the attitude (pitch</summary>
        FREEZE_ATTITUDE_SET,
        /// <summary>Turn the point-of-interest indicator (often a light beam) on or off. Refer to the Missions system documentation.</summary>
        POINT_OF_INTEREST_TOGGLE_POINTER,
        /// <summary>Change the current point-of-interest to the previous point-of-interest.</summary>
        POINT_OF_INTEREST_CYCLE_PREVIOUS,
        /// <summary>Change the current point-of-interest to the next point-of-interest.</summary>
        POINT_OF_INTEREST_CYCLE_NEXT,
        #endregion

        #region ATC
        /// <summary>Activates ATC window</summary>
        ATC,
        /// <summary>Selects ATC option 1</summary>
        ATC_MENU_1,
        /// <summary>Selects ATC option 2</summary>
        ATC_MENU_2,
        /// <summary>Selects ATC option 3</summary>
        ATC_MENU_3,
        /// <summary>Selects ATC option 4</summary>
        ATC_MENU_4,
        /// <summary>Selects ATC option 5</summary>
        ATC_MENU_5,
        /// <summary>Selects ATC option 6</summary>
        ATC_MENU_6,
        /// <summary>Selects ATC option 7</summary>
        ATC_MENU_7,
        /// <summary>Selects ATC option 8</summary>
        ATC_MENU_8,
        /// <summary>Selects ATC option 9</summary>
        ATC_MENU_9,
        /// <summary>Selects ATC option 10</summary>
        ATC_MENU_0,
        #endregion

        #region Multiplayer
        /// <summary>Toggle to the next player to track</summary>
        MP_TRANSFER_CONTROL,
        /// <summary>Cycle through the current user aircraft.</summary>
        MP_PLAYER_CYCLE,
        /// <summary>Set the view to follow the selected user aircraft.</summary>
        MP_PLAYER_FOLLOW,
        /// <summary>Toggles chat window visible/invisible</summary>
        MP_CHAT,
        /// <summary>Activates chat window</summary>
        MP_ACTIVATE_CHAT,
        /// <summary>Start capturing audio from the users computer and transmitting it to all other players in the multiplayer session who are turned to the same radio frequency.</summary>
        MP_VOICE_CAPTURE_START,
        /// <summary>Stop capturing radio audio.</summary>
        MP_VOICE_CAPTURE_STOP,
        /// <summary>Start capturing audio from the users computer and transmitting it to all other players in the multiplayer session.</summary>
        MP_BROADCAST_VOICE_CAPTURE_START,
        /// <summary>Stop capturing broadcast audio.</summary>
        MP_BROADCAST_VOICE_CAPTURE_STOP,
        /// <summary>Show or hide multi-player race results.</summary>
        TOGGLE_RACERESULTS_WINDOW,
        #endregion
    }
}
