using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.PCSCLite64
{
    /// <summary>
    /// PCSCLite x64 native structure ported for .NET.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ScardIoRequest
    {
        /// <summary>
        /// Protocol (see <see cref="Protocol"/>).
        /// </summary>
        public UInt64 protocol;

        /// <summary>
        /// PCI length.
        /// </summary>
        public UInt64 pciLength;
    }
}