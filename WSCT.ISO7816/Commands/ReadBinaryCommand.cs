using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// Wrapper for ISO/IEC 7816 READ BINARY C-APDU
    /// </summary>
    public class ReadBinaryCommand : CommandAPDU
    {
        #region >> Properties

        /// <summary>
        /// SFI (Short File Identifier) on 5 bits
        /// </summary>
        public Byte sfi
        {
            set
            {
                if ((ins & 0x01) == 0x00)
                {
                    // If bit 1 of INS is set to 0 and bit 8 of P1 to 1, then bits 7 and 6 of P1 are set to 00 (RFU), bits 5 to 1 of P1 encode
                    // a short EF identifier and P2 (eight bits) encodes an offset from zero to 255. 
                    p1 = (Byte)(0x80 | value);
                }
                else
                {
                    // If bit 1 of INS is set to 1, then P1-P2 shall identify an EF.
                    // If the first eleven bits of P1-P2 are set to 0 and if bits 5 to 1 of P2 are not all equal and if the card and / or the EF supports selection by short EF identifier, 
                    // then bits 5 to 1 of P2 encode a short EF identifier (a number from one to thirty). Otherwise, P1-P2 is a file identifier.
                    p1 = 0x00;
                    p2 = (Byte)value;
                }
            }
            get
            {
                if ((ins & 0x01) == 0x00)
                {
                    return (Byte)(p1 & 0x1F);
                }
                else
                {
                    return (Byte)(p2 & 0x1F);
                }
            }
        }

        /// <summary>
        /// FID (File Identifier) on 2 bytes
        /// </summary>
        public UInt32 fid
        {
            set
            {
                ins |= 0x01;
                p1 = (Byte)(value / 0x100);
                p2 = (Byte)(value % 0x100);
            }
            get
            {
                if ((ins & 0x01) == 0x00)
                {
                    throw new Exception("With INS:{0:X2}, no file identifier is given");
                }
                else
                {
                    return (uint)(p1 * 0x100 + p2);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public uint offset
        {
            set
            {
                if ((ins & 0x01) == 0x00)
                {
                    p2 = (Byte)value;
                }
                else
                {
                    throw new Exception("With INS:{0:X2}, offset shall be present in the offset data object with tag 54 in UDC");
                }
            }
            get
            {
                if ((ins & 0x01) == 0x00)
                {
                    return p2;
                }
                else
                {
                    throw new Exception("With INS:{0:X2}, offset shall be present in the offset data object with tag 54 in UDC");
                }
            }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReadBinaryCommand()
            : base()
        {
            ins = 0xB0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="le"></param>
        public ReadBinaryCommand(UInt32 le)
            : this()
        {
            this.le = le;
        }

        #endregion
    }
}
