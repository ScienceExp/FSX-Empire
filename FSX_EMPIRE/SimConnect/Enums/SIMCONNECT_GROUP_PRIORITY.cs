using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim
{
    enum SIMCONNECT_GROUP_PRIORITY : uint
    {
        /// <summary>The highest priority.</summary>
        HIGHEST = 1,
        /// <summary>The highest priority that allows events to be masked.</summary>
        HIGHEST_MASKABLE = 10000000,
        /// <summary>The standard priority.</summary>
        STANDARD = 1900000000,
        /// <summary>The default priority.</summary>
        DEFAULT = 2000000000,
        /// <summary>Priorities lower than this will be ignored.</summary>
        LOWEST = 4000000000,
    }
}
