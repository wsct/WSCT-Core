using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WSCT.Core.APDU;
using WSCT.Helpers;

namespace WSCT.ISO7816
{
    /// <summary>
    /// Represents the normalized (ISO7816) C-APDU to be sent to a smart card.
    /// <para>C-APDU: <b>CLA INS P1 P2 (Lc UDC) (Le)</b></para>
    /// </summary>
    /// <remarks>
    /// Only short C-APDU (Le, Lc coded on 1 byte) are now supported.
    /// </remarks>
    [XmlRoot("commandAPDU")]
    public class CommandAPDU : ICardCommand, IXmlSerializable
    {
        #region >> Fields

        private CommandCase _commandCase = CommandCase.CC1;
        private Boolean _hasLc;
        private Boolean _hasLe;

        private UInt32 _lc;
        private UInt32 _le;

        private byte[] _udc;

        #endregion

        #region >> Properties

        /// <summary>
        /// Accessor to the Command Case of the C-APDU.
        /// Write access: <see cref="HasLe"/> and <see cref="HasLc"/> are updated.
        /// </summary>
        public CommandCase CommandCase
        {
            get { return _commandCase; }
            set
            {
                _commandCase = value;
                switch (value)
                {
                    case CommandCase.CC1:
                        _hasLc = false;
                        _hasLe = false;
                        break;
                    case CommandCase.CC2:
                    case CommandCase.CC2E:
                        _hasLc = false;
                        _hasLe = true;
                        break;
                    case CommandCase.CC3:
                    case CommandCase.CC3E:
                        _hasLc = true;
                        _hasLe = false;
                        break;
                    case CommandCase.CC4:
                    case CommandCase.CC4E:
                        _hasLc = true;
                        _hasLe = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Accessor to the CLA byte of the C-APDU.
        /// </summary>
        public byte Cla { get; set; }

        /// <summary>
        /// Accessor to the INS byte of the C-APDU.
        /// </summary>
        public byte Ins { get; set; }

        /// <summary>
        /// Accessor to the P1 byte of the C-APDU.
        /// </summary>
        public byte P1 { get; set; }

        /// <summary>
        /// Accessor to the P2 byte of the C-APDU.
        /// </summary>
        public byte P2 { get; set; }

        /// <summary>
        /// Accessor to the Le value of the C-APDU.
        /// </summary>
        public UInt32 Le
        {
            get { return _le; }
            set
            {
                _le = value;
                HasLe = true;
            }
        }

        /// <summary>
        /// Accessor to the Lc value of the C-APDU.
        /// </summary>
        public UInt32 Lc
        {
            get { return _lc; }
            set
            {
                _lc = value;
                HasLc = true;
            }
        }

        /// <summary>
        /// Accessor to the Ne value of the C-APDU.
        /// Valid values: 0, 1, 2, or 3
        /// </summary>
        public UInt32 Ne
        {
            get
            {
                switch (_commandCase)
                {
                    // Case 1 and 3 do not have expected response length
                    case CommandCase.CC1:
                    case CommandCase.CC3:
                    case CommandCase.CC3E:
                        return 0;
                    
                    // Case 2 and 4 are standard commands with short (1-byte) expected response lengths
                    case CommandCase.CC2:
                    case CommandCase.CC4:
                        return 1;
                    
                    // The expected response length is 3 bytes (0x00, 0xXX, 0XX) when command length Lc is absent
                    case CommandCase.CC2E:
                        return 3;
                    
                    // When command length Lc is present (and extended), expected response length is encoded as to bytes
                    case CommandCase.CC4E:
                        return 2;

                    // If the command case is unknown, the length is unknown.
                    case CommandCase.Unknown:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        /// <summary>
        /// Accessor to the Nc value of the C-APDU.
        /// Valid values: 0, 1, or 3
        /// </summary>
        public UInt32 Nc
        {
            get
            {
                switch (_commandCase)
                {
                    // Case 1 and 2 do not have command data
                    case CommandCase.CC1:
                    case CommandCase.CC2:
                    case CommandCase.CC2E:
                        return 0;
                    
                    // Case 3 and 4 are standard commands with short (1-byte) command data
                    case CommandCase.CC3:
                    case CommandCase.CC4:
                        return 1;
                    
                    // Extended-length commands with response lengths are encoded as three bytes (0x00, 0xXX, 0xXX)
                    case CommandCase.CC3E:
                    case CommandCase.CC4E:
                        return 3;

                    // If the command case is unknown, the length is unknown.
                    case CommandCase.Unknown:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Informs if Le has been parsed.
        /// Write access: <see cref="CommandCase"/> is updated.
        /// </summary>
        public Boolean HasLe
        {
            get { return _hasLe; }
            set
            {
                _hasLe = value;
                if (value)
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC1:
                            _commandCase = CommandCase.CC2;
                            break;
                        case CommandCase.CC3:
                            _commandCase = CommandCase.CC4;
                            break;
                    }
                }
                else
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC2:
                            _commandCase = CommandCase.CC1;
                            break;
                        case CommandCase.CC4:
                            _commandCase = CommandCase.CC3;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Informs if Lc has been parsed.
        /// Write access: <see cref="CommandCase"/> is updated.
        /// </summary>
        public Boolean HasLc
        {
            get { return _hasLc; }
            set
            {
                _hasLc = value;
                if (value)
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC1:
                            _commandCase = CommandCase.CC3;
                            break;
                        case CommandCase.CC2:
                            _commandCase = CommandCase.CC4;
                            break;
                    }
                }
                else
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC3:
                            _commandCase = CommandCase.CC1;
                            break;
                        case CommandCase.CC4:
                            _commandCase = CommandCase.CC2;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Accessor to the UDC of the C-APDU.
        /// Write access: <see cref="Lc"/> is updated.
        /// </summary>
        public byte[] Udc
        {
            get { return (HasLc ? _udc : new byte[0]); }
            set
            {
                _udc = value;
                Lc = (uint)value.Length;
            }
        }

        /// <summary>
        /// Informs if the C-APDU is a Command Case 1.
        /// </summary>
        public Boolean IsCc1
        {
            get { return _commandCase == CommandCase.CC1; }
        }

        /// <summary>
        /// Informs if the C-APDU is a Command Case 2.
        /// </summary>
        public Boolean IsCc2
        {
            get { return _commandCase == CommandCase.CC2; }
        }
        
        /// <summary>
        /// Informs if the C-APDU is a Extended Command Case 2.
        /// </summary>
        public Boolean IsCc2E
        {
            get { return _commandCase == CommandCase.CC2E; }
        }

        /// <summary>
        /// Informs if the C-APDU is a Command Case 3.
        /// </summary>
        public Boolean IsCc3
        {
            get { return _commandCase == CommandCase.CC3; }
        }
        
        /// <summary>
        /// Informs if the C-APDU is a Extended Command Case 3.
        /// </summary>
        public Boolean IsCc3E
        {
            get { return _commandCase == CommandCase.CC3E; }
        }

        /// <summary>
        /// Informs if the C-APDU is a Command Case 4.
        /// </summary>
        public Boolean IsCc4
        {
            get { return _commandCase == CommandCase.CC4; }
        }
        
        /// <summary>
        /// Informs if the C-APDU is a Extended Command Case 4.
        /// </summary>
        public Boolean IsCc4E
        {
            get { return _commandCase == CommandCase.CC4E; }
        }

        /// <summary>
        /// Accessor to the entire C-APDU in byte array format.
        /// </summary>
        public byte[] BinaryCommand
        {
            get
            {
                // All APDUs have a length of at least 4: [CLA] [INS] [P1] [P2]
                var length = 4;

                // If command data is present, allocate space for the data and the length
                if (HasLc)
                {
                    length += Udc.Length + (int)Nc;
                }

                // If expected length is present, allocate space for it as well
                if (HasLe)
                {
                    length += (int)Ne;
                }

                // Write out the binary data
                var data = new byte[length];
                var offset = 0;
                data[offset++] = Cla;
                data[offset++] = Ins;
                data[offset++] = P1;
                data[offset++] = P2;
                
                // Handle Lc
                if (HasLc)
                {
                    // The bit converter produces a bit array of 4 bytes, so we can keep the last X bytes
                    var lcBytes = BitConverter.GetBytes(Lc);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(lcBytes);
                    var lcOffset = lcBytes.Length - (int)Nc;
                    Array.Copy(lcBytes, lcOffset, data, offset, lcBytes.Length - lcOffset);
                    offset += (int)Nc;
                    
                    Array.Copy(Udc, 0, data, offset, Udc.Length);
                    offset += Udc.Length;
                }

                // Handle Le
                if (HasLe)
                {
                    var leBytes = BitConverter.GetBytes(Le);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(leBytes);
                    var leOffset = leBytes.Length - (int)Ne;
                    Array.Copy(leBytes, leOffset, data, offset, leBytes.Length - leOffset);
                }
                return data;
            }
        }

        /// <summary>
        /// Accessor to the entire C-APDU in string format (all bytes are represented in hexa).
        /// </summary>
        public string StringCommand
        {
            get { return BinaryCommand.ToHexa(); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CommandAPDU()
        {
            _hasLe = false;
            P1 = 0x00;
            P2 = 0x00;
            Ins = 0x00;
            Cla = 0x00;
        }

        /// <summary>
        /// Initializes a new instance for CC1 C-APDU.
        /// </summary>
        /// <param name="cla">CLA byte of the C-APDU.</param>
        /// <param name="ins">INS byte of the C-APDU.</param>
        /// <param name="p1">P1 byte of the C-APDU.</param>
        /// <param name="p2">P2 byte of the C-APDU.</param>
        public CommandAPDU(byte cla, byte ins, byte p1, byte p2)
        {
            _hasLe = false;
            Cla = cla;
            Ins = ins;
            P1 = p1;
            P2 = p2;
        }

        /// <summary>
        /// Initializes a new instance for CC2 C-APDU.
        /// </summary>
        /// <param name="cla">CLA byte of the C-APDU.</param>
        /// <param name="ins">INS byte of the C-APDU.</param>
        /// <param name="p1">P1 byte of the C-APDU.</param>
        /// <param name="p2">P2 byte of the C-APDU.</param>
        /// <param name="le">Le value of the C-APDU.</param>
        public CommandAPDU(byte cla, byte ins, byte p1, byte p2, UInt32 le)
        {
            _hasLe = false;
            Cla = cla;
            Ins = ins;
            P1 = p1;
            P2 = p2;
            Le = le;
        }

        /// <summary>
        /// Initializes a new instance for CC2 C-APDU.
        /// </summary>
        /// <param name="cla">CLA byte of the C-APDU.</param>
        /// <param name="ins">INS byte of the C-APDU.</param>
        /// <param name="p1">P1 byte of the C-APDU.</param>
        /// <param name="p2">P2 byte of the C-APDU.</param>
        /// <param name="lc">Lc value of the C-APDU.</param>
        /// <param name="udc">UDC of the C-APDU.</param>
        public CommandAPDU(byte cla, byte ins, byte p1, byte p2, UInt32 lc, byte[] udc)
        {
            _hasLe = false;
            Cla = cla;
            Ins = ins;
            P1 = p1;
            P2 = p2;
            Udc = udc;
            Lc = lc;
        }

        /// <summary>
        /// Initializes a new instance for CC4 C-APDU.
        /// </summary>
        /// <param name="cla">CLA byte of the C-APDU.</param>
        /// <param name="ins">INS byte of the C-APDU.</param>
        /// <param name="p1">P1 byte of the C-APDU.</param>
        /// <param name="p2">P2 byte of the C-APDU.</param>
        /// <param name="lc">Lc value of the C-APDU.</param>
        /// <param name="udc">UDC of the C-APDU.</param>
        /// <param name="le">Le value of the C-APDU.</param>
        public CommandAPDU(byte cla, byte ins, byte p1, byte p2, UInt32 lc, byte[] udc, UInt32 le)
        {
            _hasLe = false;
            Cla = cla;
            Ins = ins;
            P1 = p1;
            P2 = p2;
            Udc = udc;
            Lc = lc;
            HasLc = true;
            Le = le;
            HasLe = true;
        }

        /// <summary>
        /// Initializes a new instance for arbitrary C-APDU.
        /// </summary>
        /// <param name="cAPDU">C-APDU to be assigned as a byte array.</param>
        public CommandAPDU(byte[] cAPDU)
        {
            _hasLe = false;
            P1 = 0x00;
            P2 = 0x00;
            Ins = 0x00;
            Cla = 0x00;
            Parse(cAPDU);
        }

        /// <summary>
        /// Initializes a new instance for arbitrary C-APDU.
        /// </summary>
        /// <param name="cAPDU">C-APDU to be assigned as a string container (each byte is coded in hexa).</param>
        public CommandAPDU(string cAPDU)
        {
            _hasLe = false;
            P1 = 0x00;
            P2 = 0x00;
            Ins = 0x00;
            Cla = 0x00;
            Parse(cAPDU);
        }

        #endregion

        #region >> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="udcPart"></param>
        public void AppendUdc(byte[] udcPart)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region >> Object

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return StringCommand;
        }

        #endregion

        #region >> ICardCommand Members

        /// <inheritdoc />
        public ICardCommand Parse(byte[] cAPDU)
        {
            if (cAPDU.Length < 4)
            {
                throw new Exception("cApdu.Length<4");
            }

            var offset = 0;
            byte[] udcBytes;

            Cla = cAPDU[offset++];
            Ins = cAPDU[offset++];
            P1 = cAPDU[offset++];
            P2 = cAPDU[offset++];
            CommandCase = CommandCase.CC1;
            
            // Is additional data present?
            if (cAPDU.Length <= offset) return this;
            
            // Parse the length
            var lengthByte = cAPDU[offset++];
            if (cAPDU.Length == offset)
            {
                // Case 2, one byte length
                CommandCase = CommandCase.CC2;
                Le = lengthByte;
                return this;
            }
            else if (lengthByte == 0 && cAPDU.Length == offset + 2)
            {
                // Case 2, 3 byte Lc
                CommandCase = CommandCase.CC2E;
                var firstByte = (int)cAPDU[offset++];
                var secondByte = (int)cAPDU[offset++];
                Le = Convert.ToUInt32((firstByte * 256) + secondByte);
                return this;
            }

            // Single-byte Lc
            if (lengthByte > 0)
            {
                if (cAPDU.Length == offset + lengthByte)
                {
                    CommandCase = CommandCase.CC3;
                }
                else
                {
                    CommandCase = CommandCase.CC4;
                    Le = cAPDU[offset + lengthByte];
                }
                udcBytes = new byte[lengthByte];
                Array.Copy(cAPDU, offset, udcBytes, 0, lengthByte);
                Udc = udcBytes;
            }
            else
            {
                // Multi-byte Lc
                int firstByte = cAPDU[offset++];
                int secondByte = cAPDU[offset++];
                Lc = Convert.ToUInt32((firstByte * 256) + secondByte);
                udcBytes = new byte[Lc];
                Array.Copy(cAPDU, offset, udcBytes, 0, Lc);
                Udc = udcBytes;
                
                if (cAPDU.Length == offset + Lc)
                {
                    CommandCase = CommandCase.CC3E;
                }
                else
                {
                    CommandCase = CommandCase.CC4E;
                    firstByte = cAPDU[offset + Lc];
                    secondByte = cAPDU[offset + Lc + 1];
                    Le = Convert.ToUInt32((firstByte * 256) + secondByte);
                }
            }

            return this;
        }

        /// <inheritdoc />
        public ICardCommand Parse(string cAPDU)
        {
            return Parse(cAPDU.FromHexa());
        }

        #endregion

        #region >> IXmlSerializable Members

        /// <inheritdoc />
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <inheritdoc />
        public void ReadXml(XmlReader reader)
        {
            Cla = reader.GetAttribute("cla").FromHexa()[0];
            Ins = reader.GetAttribute("ins").FromHexa()[0];
            P1 = reader.GetAttribute("p1").FromHexa()[0];
            P2 = reader.GetAttribute("p2").FromHexa()[0];
            if (reader.MoveToAttribute("le"))
            {
                Le = reader.ReadContentAsString().FromHexa()[0];
            }
            reader.MoveToElement();
            if (reader.IsEmptyElement)
            {
                // <commandAPDU cla=... ins=... p1=... p2=... (le=...) />
                reader.ReadStartElement();
            }
            else
            {
                // <commandAPDU cla=... ins=... p1=... p2=... (le=...) > udc </commandAPDU>
                reader.ReadStartElement();
                Udc = reader.ReadContentAsString().FromHexa();
                reader.ReadEndElement();
            }
            reader.ReadStartElement();
        }

        /// <inheritdoc />
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("cla", String.Format("{0:X2}", Cla));
            writer.WriteAttributeString("ins", String.Format("{0:X2}", Ins));
            writer.WriteAttributeString("p1", String.Format("{0:X2}", P1));
            writer.WriteAttributeString("p2", String.Format("{0:X2}", P2));
            if (HasLc)
            {
                writer.WriteString(Udc.ToHexa());
            }
            if (HasLe)
            {
                writer.WriteAttributeString("le", String.Format("{0:X2}", Le));
            }
        }

        #endregion
    }
}