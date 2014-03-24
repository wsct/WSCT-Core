using System;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Abstract IoRequest structure to be implemented by PC/SC wrapper.
    /// </summary>
    public abstract class AbstractIoRequest
    {
        /// <summary>
        /// Protocol (see <see cref="Wrapper.Protocol"/>).
        /// </summary>
        public abstract UInt32 Protocol { get; set; }

        /// <summary>
        /// PCI length.
        /// </summary>
        public abstract UInt32 PciLength { get; set; }
    }
}
