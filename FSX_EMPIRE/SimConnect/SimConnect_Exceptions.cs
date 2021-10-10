namespace FSX_EMPIRE
{
    //Error codes found at https://msdn.microsoft.com/en-us/library/cc526983.aspx#SIMCONNECT_EXCEPTION
    public enum SIMCONNECT_EXCEPTION
    {
        SIMCONNECT_EXCEPTION_NONE = 0,
        SIMCONNECT_EXCEPTION_ERROR = 1,
        SIMCONNECT_EXCEPTION_SIZE_MISMATCH = 2,
        SIMCONNECT_EXCEPTION_UNRECOGNIZED_ID = 3,
        SIMCONNECT_EXCEPTION_UNOPENED = 4,
        SIMCONNECT_EXCEPTION_VERSION_MISMATCH = 5,
        SIMCONNECT_EXCEPTION_TOO_MANY_GROUPS = 6,
        SIMCONNECT_EXCEPTION_NAME_UNRECOGNIZED = 7,
        SIMCONNECT_EXCEPTION_TOO_MANY_EVENT_NAMES = 8,
        SIMCONNECT_EXCEPTION_EVENT_ID_DUPLICATE = 9,
        SIMCONNECT_EXCEPTION_TOO_MANY_MAPS = 10,
        SIMCONNECT_EXCEPTION_TOO_MANY_OBJECTS = 11,
        SIMCONNECT_EXCEPTION_TOO_MANY_REQUESTS = 12,
        SIMCONNECT_EXCEPTION_WEATHER_INVALID_PORT = 13,
        SIMCONNECT_EXCEPTION_WEATHER_INVALID_METAR = 14,
        SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_GET_OBSERVATION = 15,
        SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_CREATE_STATION = 16,
        SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_REMOVE_STATION = 17,
        SIMCONNECT_EXCEPTION_INVALID_DATA_TYPE = 18,
        SIMCONNECT_EXCEPTION_INVALID_DATA_SIZE = 19,
        SIMCONNECT_EXCEPTION_DATA_ERROR = 20,
        SIMCONNECT_EXCEPTION_INVALID_ARRAY = 21,
        SIMCONNECT_EXCEPTION_CREATE_OBJECT_FAILED = 22,
        SIMCONNECT_EXCEPTION_LOAD_FLIGHTPLAN_FAILED = 23,
        SIMCONNECT_EXCEPTION_OPERATION_INVALID_FOR_OJBECT_TYPE = 24,
        SIMCONNECT_EXCEPTION_ILLEGAL_OPERATION = 25,
        SIMCONNECT_EXCEPTION_ALREADY_SUBSCRIBED = 26,
        SIMCONNECT_EXCEPTION_INVALID_ENUM = 27,
        SIMCONNECT_EXCEPTION_DEFINITION_ERROR = 28,
        SIMCONNECT_EXCEPTION_DUPLICATE_ID = 29,
        SIMCONNECT_EXCEPTION_DATUM_ID = 30,
        SIMCONNECT_EXCEPTION_OUT_OF_BOUNDS = 31,
        SIMCONNECT_EXCEPTION_ALREADY_CREATED = 32,
        SIMCONNECT_EXCEPTION_OBJECT_OUTSIDE_REALITY_BUBBLE = 33,
        SIMCONNECT_EXCEPTION_OBJECT_CONTAINER = 34,
        SIMCONNECT_EXCEPTION_OBJECT_AI = 35,
        SIMCONNECT_EXCEPTION_OBJECT_ATC = 36,
        SIMCONNECT_EXCEPTION_OBJECT_SCHEDULE = 37,
    };
    public class SimConnect_Exceptions
    {
        public string GetDescription(SIMCONNECT_EXCEPTION errorCode)
        {
            switch (errorCode)
            {
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_NONE:
                    return "There has not been an error. This value is not currently used. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_ERROR:
                    return "An unspecific error has occurred. This can be from incorrect flag settings, null or incorrect parameters, the need to have at least one up or down event with an input event, failed calls from the SimConnect server to the operating system, among other reasons. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_SIZE_MISMATCH:
                    return "The size of the data provided does not match the size required. This typically occurs when the wrong string length, fixed or variable, is involved.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_UNRECOGNIZED_ID:
                    return "The client event, request ID, data definition ID, or object ID was not recognized. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_UNOPENED:
                    return "Communication with the SimConnect server has not been opened. This error is not currently used.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_VERSION_MISMATCH:
                    return "A versioning error has occurred. Typically this will occur when a client built on a newer version of the SimConnect client dll attempts to work with an older version of the SimConnect server. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_TOO_MANY_GROUPS:
                    return " Specifies that the maximum number of groups allowed has been reached. The maximum is 20.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_NAME_UNRECOGNIZED:
                    return "Specifies that the simulation event name (such as 'brakes') is not recognized.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_TOO_MANY_EVENT_NAMES:
                    return "Specifies that the maximum number of event names allowed has been reached. The maximum is 1000.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_EVENT_ID_DUPLICATE:
                    return "Specifies that the event ID has been used already. This can occur with calls to SimConnect_MapClientEventToSimEvent, or SimConnect_SubscribeToSystemEvent.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_TOO_MANY_MAPS:
                    return "Specifies that the maximum number of mappings allowed has been reached. The maximum is 20.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_TOO_MANY_OBJECTS:
                    return "Specifies that the maximum number of objects allowed has been reached. The maximum is 1000.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_TOO_MANY_REQUESTS:
                    return "Specifies that the maximum number of requests allowed has been reached. The maximum is 1000.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_WEATHER_INVALID_PORT:
                    return "Specifies an invalid port number was requested. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_WEATHER_INVALID_METAR:
                    return "Specifies that the metar data supplied did not match the required format. See the section Metar Data Format for details on the format required.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_GET_OBSERVATION:
                    return "Specifies that the weather observation requested was not available. Refer to the remarks section for SimConnect_WeatherRequestObservationAtStation for some notes on this exception.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_CREATE_STATION:
                    return "Specifies that the weather station could not be created.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_WEATHER_UNABLE_TO_REMOVE_STATION:
                    return "Specifies that the weather station could not be removed. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_INVALID_DATA_TYPE:
                    return " Specifies that the data type requested does not apply to the type of data requested. Typically this occurs with a fixed length string of the wrong length.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_INVALID_DATA_SIZE:
                    return "Specifies that the size of the data provided is not what is expected. This can occur when the size of a structure provided does not match the size given, or a null string entry is made for a menu or sub-menu entry text, or data with a size of zero is added to a data definition. It can also occur with an invalid request to SimConnect_CreateClientData.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_DATA_ERROR:
                    return "Specifies a generic data error. This error is used by the SimConnect_WeatherCreateThermal function to report incorrect parameters, such as negative radii or values greater than the maximum allowed. It is also used by the SimConnect_FlightSave and SimConnect_FlightLoad functions to report incorrect file types. It is also used by other functions to report that flags or reserved parameters have not been set to zero.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_INVALID_ARRAY:
                    return " Specifies an invalid array has been sent to the SimConnect_SetDataOnSimObject function. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_CREATE_OBJECT_FAILED:
                    return "Specifies that the attempt to create an AI object failed.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_LOAD_FLIGHTPLAN_FAILED:
                    return "Specifies that the specified flight plan could not be found, or did not load correctly.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_OPERATION_INVALID_FOR_OJBECT_TYPE:
                    return "Specifies that the operation requested does not apply to the object type, for example trying to set a flight plan on an object that is not an aircraft will result in this error.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_ILLEGAL_OPERATION:
                    return "Specifies that the AI operation requested cannot be completed, such as requesting that an object be removed when the client did not create that object. This error also applies to the Weather system.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_ALREADY_SUBSCRIBED:
                    return "Specifies that the client has already subscribed to that event.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_INVALID_ENUM:
                    return "Specifies that the member of the enumeration provided was not valid. Currently this is only used if an unknown type is provided to SimConnect_RequestDataOnSimObjectType.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_DEFINITION_ERROR:
                    return "Specifies that there is a problem with a data definition. Currently this is only used if a variable length definition is sent with SimConnect_RequestDataOnSimObject.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_DUPLICATE_ID:
                    return "Specifies that the ID has already been used. This can occur with menu IDs, or with the IDs provided to SimConnect_AddToDataDefinition, SimConnect_AddClientEventToNotificationGroup or SimConnect_MapClientDataNameToID.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_DATUM_ID:
                    return "Specifies that the datum ID is not recognized. This currently occurs with a call to the SimConnect_SetDataOnSimObject function. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_OUT_OF_BOUNDS:
                    return "Specifies that the radius given in the SimConnect_RequestDataOnSimObjectType was outside the acceptable range, or with an invalid request to SimConnect_CreateClientData. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_ALREADY_CREATED:
                    return "Specifies that a client data area with the name requested by a call to SimConnect_MapClientDataNameToID has already been created by another addon. Try again with a different name.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_OBJECT_OUTSIDE_REALITY_BUBBLE:
                    return "Specifies that an attempt to create an ATC controlled AI object failed because the location of the object is outside the reality bubble. ";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_OBJECT_CONTAINER:
                    return "Specifies that an attempt to create an AI object failed because of an error with the container system for the object.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_OBJECT_AI:
                    return "Specifies that an attempt to create an AI object failed because of an error with the AI system for the object.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_OBJECT_ATC:
                    return "Specifies that an attempt to create an AI object failed because of an error with the ATC system for the object.";
                case SIMCONNECT_EXCEPTION.SIMCONNECT_EXCEPTION_OBJECT_SCHEDULE:
                    return "Specifies that an attempt to create an AI object failed because of a scheduling problem.";
                default:
                    return "Error code unrecognized.";
            }
        }
    }
}