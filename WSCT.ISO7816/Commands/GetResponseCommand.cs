using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// Wrapper for ISO/IEC 7816 GET RESPONSE C-APDU
    /// </summary>
    public class GetResponseCommand : CommandAPDU
    {
        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetResponseCommand()
            : base()
        {
            ins = 0xC0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="le"></param>
        public GetResponseCommand(uint le)
            : this()
        {
            this.p1 = 0x00;
            this.p2 = 0x00;
            this.le = le;
        }

        #endregion
    }
}
