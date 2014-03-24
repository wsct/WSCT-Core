using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.WinSCard
{
    [StructLayout(LayoutKind.Sequential)]
    struct ScardIoRequest
    {
        /// <summary>
        /// Protocol (see <see cref="Protocol"/>).
        /// </summary>
        public UInt32 protocol;
        /// <summary>
        /// PCI length.
        /// </summary>
        public UInt32 pciLength;
    }
}
