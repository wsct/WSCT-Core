using System;

namespace WSCT.Wrapper.Desktop.Core.SCardControl
{
    public class ControlCode
    {
        /// <summary>
        /// Helper method doing the same work as SCARD_CTL_CODE macro in C.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static uint Get(uint code)
        {
            return Environment.OSVersion.Platform switch
            {
                PlatformID.Unix or PlatformID.MacOSX => 0x42000000 + code,
                _ => 0x00310000 + (code << 2)
            };
        }
    }
}
