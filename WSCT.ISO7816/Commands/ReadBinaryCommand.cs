using System;

namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// Wrapper for ISO/IEC 7816 READ BINARY C-APDU.
    /// </summary>
    public class ReadBinaryCommand : CommandAPDU
    {
        #region >> Properties

        /// <summary>
        /// SFI (Short File Identifier) on 5 bits.
        /// </summary>
        public byte Sfi
        {
            set
            {
                if ((Ins & 0x01) == 0x00)
                {
                    // If bit 1 of INS is set to 0 and bit 8 of P1 to 1, then bits 7 and 6 of P1 are set to 00 (RFU), bits 5 to 1 of P1 encode
                    // a short EF identifier and P2 (eight bits) encodes an offset from zero to 255. 
                    P1 = (byte)(0x80 | value);
                }
                else
                {
                    // If bit 1 of INS is set to 1, then P1-P2 shall identify an EF.
                    // If the first eleven bits of P1-P2 are set to 0 and if bits 5 to 1 of P2 are not all equal and if the card and / or the EF supports selection by short EF identifier, 
                    // then bits 5 to 1 of P2 encode a short EF identifier (a number from one to thirty). Otherwise, P1-P2 is a file identifier.
                    P1 = 0x00;
                    P2 = value;
                }
            }
            get
            {
                if ((Ins & 0x01) == 0x00)
                {
                    return (byte)(P1 & 0x1F);
                }
                return (byte)(P2 & 0x1F);
            }
        }

        /// <summary>
        /// FID (File Identifier) on 2 bytes.
        /// </summary>
        public UInt32 Fid
        {
            set
            {
                Ins |= 0x01;
                P1 = (byte)(value/0x100);
                P2 = (byte)(value%0x100);
            }
            get
            {
                if ((Ins & 0x01) == 0x00)
                {
                    throw new Exception("With INS:{0:X2}, no file identifier is given");
                }
                return (uint)(P1*0x100 + P2);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public uint Offset
        {
            set
            {
                if ((Ins & 0x01) == 0x00)
                {
                    P2 = (byte)value;
                }
                else
                {
                    throw new Exception("With INS:{0:X2}, offset shall be present in the offset data object with tag 54 in UDC");
                }
            }
            get
            {
                if ((Ins & 0x01) == 0x00)
                {
                    return P2;
                }
                throw new Exception("With INS:{0:X2}, offset shall be present in the offset data object with tag 54 in UDC");
            }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ReadBinaryCommand()
        {
            Ins = 0xB0;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="le"></param>
        public ReadBinaryCommand(UInt32 le)
            : this()
        {
            Le = le;
        }

        #endregion
    }
}