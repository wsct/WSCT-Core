using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// Wrapper for ISO/IEC 7816 APPEND RECORD C-APDU
    /// </summary>
    public class AppendRecordCommand : CommandAPDU
    {
        #region >> Properties

        /// <summary>
        /// SFI (Short File Identifier)
        /// </summary>
        public Byte sfi
        {
            get { return (Byte)(p2 >> 3); }
            set { p2 = (Byte)(value << 3); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppendRecordCommand()
            : base()
        {
            ins = 0xE2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfi"></param>
        /// <param name="udc"></param>
        public AppendRecordCommand(Byte sfi, Byte[] udc)
            : this()
        {
            this.p1 = 0x00;
            this.sfi = sfi;
            this.udc = udc;
        }

        #endregion
    }
}
