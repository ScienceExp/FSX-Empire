using System;

namespace WebCam
{
    public static partial class ByteExtensions
    {
        public static Byte Max(this Byte val1, Byte val2)
        {
            return Math.Max(val1, val2);
        }
    }
}
