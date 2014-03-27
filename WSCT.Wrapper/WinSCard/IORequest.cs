using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.WinSCard
{
    internal sealed class IoRequest : AbstractIoRequest
    {
        #region >> Properties

        public ScardIoRequest ScIoRequest;

        public override UInt32 Protocol
        {
            get { return ScIoRequest.protocol; }
            set { ScIoRequest.protocol = value; }
        }

        public override UInt32 PciLength
        {
            get { return ScIoRequest.pciLength; }
            set { ScIoRequest.pciLength = value; }
        }

        #endregion

        #region >> Constructors

        public IoRequest()
        {
            ScIoRequest = new ScardIoRequest();
        }

        public IoRequest(UInt32 protocol)
            : this()
        {
            Protocol = protocol;
            PciLength = (uint)Marshal.SizeOf(ScIoRequest);
        }

        #endregion
    }
}