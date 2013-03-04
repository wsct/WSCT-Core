using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.PCSCLite32
{
    [StructLayout(LayoutKind.Sequential)]
    struct SCARD_IO_REQUEST
    {
        /// <summary>Protocol (see <see cref="Protocol"/>)</summary>
        public UInt32 protocol;
        /// <summary>PCI length</summary>
        public UInt32 pciLength;
    }
}
