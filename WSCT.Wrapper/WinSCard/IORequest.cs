using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.WinSCard
{
    class IoRequest : AbstractIoRequest
    {
        #region >> Properties

        public override UInt32 protocol
        {
            get
            {
                return scIoRequest.protocol;
            }
            set
            {
                scIoRequest.protocol = value;
            }
        }

        public override UInt32 pciLength
        {
            get
            {
                return scIoRequest.pciLength;
            }
            set
            {
                scIoRequest.pciLength = value;
            }
        }

        public SCARD_IO_REQUEST scIoRequest;

        #endregion

        #region >> Constructors

        public IoRequest()
        {
            this.scIoRequest = new SCARD_IO_REQUEST();
        }

        public IoRequest(UInt32 protocol)
            : this()
        {
            this.protocol = protocol;
            this.pciLength = (uint)Marshal.SizeOf(scIoRequest);
        }

        #endregion
    }
}
