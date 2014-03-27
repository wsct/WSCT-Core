using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.PCSCLite64
{
    internal sealed class IoRequest : AbstractIoRequest
    {
        #region >> Properties

        public ScardIoRequest ScIoRequest;

        /// <inheritdoc />
        public override UInt32 Protocol
        {
            get { return (uint)ScIoRequest.protocol; }
            set { ScIoRequest.protocol = value; }
        }

        /// <inheritdoc />
        public override UInt32 PciLength
        {
            get { return (uint)ScIoRequest.pciLength; }
            set { ScIoRequest.pciLength = value; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new <see cref="IoRequest"/> instance.
        /// </summary>
        public IoRequest()
        {
            ScIoRequest = new ScardIoRequest();
        }

        /// <summary>
        /// Initializes a new <see cref="IoRequest"/> instance.
        /// </summary>
        /// <param name="protocol">Transmission protocol.</param>
        public IoRequest(UInt32 protocol)
            : this()
        {
            Protocol = protocol;
            PciLength = (uint)Marshal.SizeOf(ScIoRequest);
        }

        #endregion
    }
}