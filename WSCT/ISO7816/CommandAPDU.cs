using System;
using System.Linq;
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
    public class CommandAPDU : ICardCommand, IXmlSerializable, IEquatable<CommandAPDU>
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
                if (value > 0xFFFF)
                {
                    throw new Exception("Le exceeds maximum size.");
                }

                CommandCase newCommandCase;
                if (value > 0xFF)
                {
                    newCommandCase = CommandCase switch
                    {
                        CommandCase.CC1 => CommandCase.CC2E,
                        CommandCase.CC2 => CommandCase.CC2E,
                        CommandCase.CC3 => CommandCase.CC4E,
                        CommandCase.CC4 => CommandCase.CC4E,
                        CommandCase.CC2E => CommandCase.CC2E,
                        CommandCase.CC3E => CommandCase.CC4E,
                        CommandCase.CC4E => CommandCase.CC4E,
                        CommandCase.Unknown => CommandCase.CC2E,
                        _ => throw new NotSupportedException($"Command case {CommandCase} is not valid.")
                    };
                }
                else
                {
                    newCommandCase = CommandCase switch
                    {
                        CommandCase.CC1 => CommandCase.CC2,
                        CommandCase.CC2 => CommandCase.CC2,
                        CommandCase.CC3 => CommandCase.CC4,
                        CommandCase.CC4 => CommandCase.CC4,
                        CommandCase.CC2E => CommandCase.CC2,
                        CommandCase.CC3E => CommandCase.CC4E,
                        CommandCase.CC4E when Lc <= 255 => CommandCase.CC4,
                        CommandCase.CC4E when Lc > 255 => CommandCase.CC4E,
                        CommandCase.Unknown => CommandCase.CC2E,
                        _ => throw new NotSupportedException($"Command case {CommandCase} is not valid.")
                    };
                }
                CommandCase = newCommandCase;

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
                if (value > 0xFFFF)
                {
                    throw new Exception("Lc exceeds maximum size.");
                }

                _lc = value;
                HasLc = true;

                CommandCase newCommandCase;
                // Update command case
                if (value > 0xFF)
                {
                    newCommandCase = CommandCase switch
                    {
                        CommandCase.CC1 => CommandCase.CC3E,
                        CommandCase.CC2 => CommandCase.CC4E,
                        CommandCase.CC3 => CommandCase.CC3E,
                        CommandCase.CC4 => CommandCase.CC4E,
                        CommandCase.CC2E => CommandCase.CC4E,
                        CommandCase.CC3E => CommandCase.CC3E,
                        CommandCase.CC4E => CommandCase.CC4E,
                        CommandCase.Unknown => CommandCase.CC3E,
                        _ => throw new NotSupportedException($"Command case {CommandCase} is not valid.")
                    };
                }
                else
                {
                    newCommandCase = CommandCase switch
                    {
                        CommandCase.CC1 => CommandCase.CC3,
                        CommandCase.CC2 => CommandCase.CC4,
                        CommandCase.CC3 => CommandCase.CC3,
                        CommandCase.CC4 => CommandCase.CC4,
                        CommandCase.CC2E => CommandCase.CC4E,
                        CommandCase.CC3E => CommandCase.CC3,
                        CommandCase.CC4E when Le <= 255 => CommandCase.CC4,
                        CommandCase.CC4E when Le > 255 => CommandCase.CC4E,
                        CommandCase.Unknown => CommandCase.CC3,
                        _ => throw new NotSupportedException($"Command case {CommandCase} is not valid.")
                    };
                }
                CommandCase = newCommandCase;
            }
        }

        /// <summary>
        /// Accessor to the Ne value of the C-APDU.
        /// Valid values: 0, 1, 2, or 3
        /// </summary>
        public UInt32 LeFieldSize
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

                    // When command length Lc is present (and extended), expected response length is encoded as two bytes
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
        public UInt32 LcFieldSize
        {
            get
            {
                return _commandCase switch
                {
                    // Case 1 and 2 do not have command data
                    CommandCase.CC1 or CommandCase.CC2 or CommandCase.CC2E => 0,
                    // Case 3 and 4 are standard commands with short (1-byte) command data
                    CommandCase.CC3 or CommandCase.CC4 => 1,
                    // Extended-length commands with response lengths are encoded as three bytes (0x00, 0xXX, 0xXX)
                    CommandCase.CC3E or CommandCase.CC4E => 3,
                    // If the command case is unknown, the length is unknown.
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
        }

        /// <summary>
        /// Informs if Le has been parsed.
        /// Write access: <see cref="CommandCase"/> is updated.
        /// </summary>
        public bool HasLe
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
                            _commandCase = Le <= 255 ? CommandCase.CC2 : CommandCase.CC2E;
                            break;
                        case CommandCase.CC3:
                            _commandCase = Le <= 255 ? CommandCase.CC4 : CommandCase.CC4E;
                            break;
                        case CommandCase.CC3E:
                            _commandCase = CommandCase.CC4E;
                            break;
                    }
                }
                else
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC2 or CommandCase.CC2E:
                            _commandCase = CommandCase.CC1;
                            break;
                        case CommandCase.CC4:
                            _commandCase = CommandCase.CC3;
                            break;
                        case CommandCase.CC4E:
                            _commandCase = Lc <= 255 ? CommandCase.CC3 : CommandCase.CC3E;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Informs if Lc has been parsed.
        /// Write access: <see cref="CommandCase"/> is updated.
        /// </summary>
        public bool HasLc
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
                            _commandCase = Lc <= 255 ? CommandCase.CC3 : CommandCase.CC3E;
                            break;
                        case CommandCase.CC2:
                            _commandCase = Lc <= 255 ? CommandCase.CC4 : CommandCase.CC4E;
                            break;
                        case CommandCase.CC2E:
                            _commandCase = CommandCase.CC4E;
                            break;
                    }
                }
                else
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC3 or CommandCase.CC3E:
                            _commandCase = CommandCase.CC1;
                            break;
                        case CommandCase.CC4:
                            _commandCase = CommandCase.CC2;
                            break;
                        case CommandCase.CC4E:
                            _commandCase = Le <= 255 ? CommandCase.CC2 : CommandCase.CC2E;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns true when this Command APDU is an extended command APDU
        /// </summary>
        public bool IsExtended => _commandCase == CommandCase.CC2E || _commandCase == CommandCase.CC3E || _commandCase == CommandCase.CC4E;

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
                    length += Udc.Length + (int)LcFieldSize;
                }

                // If expected length is present, allocate space for it as well
                if (HasLe)
                {
                    length += (int)LeFieldSize;
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
                    {
                        Array.Reverse(lcBytes);
                    }
                    var lcOffset = lcBytes.Length - (int)LcFieldSize;
                    Array.Copy(lcBytes, lcOffset, data, offset, lcBytes.Length - lcOffset);
                    offset += (int)LcFieldSize;

                    Array.Copy(Udc, 0, data, offset, Udc.Length);
                    offset += Udc.Length;
                }

                // Handle Le
                if (HasLe)
                {
                    var leBytes = BitConverter.GetBytes(Le);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(leBytes);
                    }
                    var leOffset = leBytes.Length - (int)LeFieldSize;
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
            if (cAPDU.Length <= offset)
            {
                return this;
            }

            // Parse the length
            var lengthByte = cAPDU[offset++];
            if (cAPDU.Length == offset)
            {
                // Case 2, one byte length
                CommandCase = CommandCase.CC2;
                Le = lengthByte;

                return this;
            }

            if (lengthByte == 0 && cAPDU.Length == offset + 2)
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
            // Check if this is an extended APDU
            var isExtended = (null != reader.GetAttribute("extended"));

            Cla = reader.GetAttribute("cla").FromHexa()[0];
            Ins = reader.GetAttribute("ins").FromHexa()[0];
            P1 = reader.GetAttribute("p1").FromHexa()[0];
            P2 = reader.GetAttribute("p2").FromHexa()[0];
            if (reader.MoveToAttribute("le"))
            {
                // Convert Le from hexadecimal
                Le = Convert.ToUInt32(reader.ReadContentAsString(), 16);
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

            // Handle extended cases
            if (!isExtended)
            {
                return;
            }

            switch (_commandCase)
            {
                case CommandCase.CC2:
                    CommandCase = CommandCase.CC2E;
                    break;
                case CommandCase.CC3:
                    CommandCase = CommandCase.CC3E;
                    break;
                case CommandCase.CC4:
                    CommandCase = CommandCase.CC4E;
                    break;
                default:
                    break;
            }
        }

        /// <inheritdoc />
        public void WriteXml(XmlWriter writer)
        {
            // Extended length APDUs are tagged in XML
            if (IsExtended)
            {
                writer.WriteAttributeString("extended", "true");
            }

            writer.WriteAttributeString("cla", String.Format("{0:X2}", Cla));
            writer.WriteAttributeString("ins", String.Format("{0:X2}", Ins));
            writer.WriteAttributeString("p1", String.Format("{0:X2}", P1));
            writer.WriteAttributeString("p2", String.Format("{0:X2}", P2));

            if (HasLe)
            {
                var leFormatString = IsExtended ? "X4" : "X2";
                writer.WriteAttributeString("le", Le.ToString(leFormatString));
            }

            if (HasLc)
            {
                writer.WriteString(Udc.ToHexa());
            }
        }

        #endregion

        #region >> IEquatable<T> Members

        /// <inheritdoc />
        public bool Equals(CommandAPDU other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Compare the basic information
            if (_commandCase != other._commandCase || Cla != other.Cla || Ins != other.Ins || P1 != other.P1 || P2 != other.P2)
            {
                return false;
            }

            // Compare Lc and Le
            if (_hasLc != other._hasLc || _hasLe != other._hasLe || _lc != other._lc || _le != other._le)
            {
                return false;
            }

            // Compare Udc
            if (_udc == null)
            {
                return other._udc == null;
            }
            return _udc.SequenceEqual(other._udc);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)_commandCase;
                hashCode = (hashCode * 397) ^ _hasLc.GetHashCode();
                hashCode = (hashCode * 397) ^ _hasLe.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)_lc;
                hashCode = (hashCode * 397) ^ (int)_le;
                hashCode = (hashCode * 397) ^ (_udc != null ? _udc.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Cla.GetHashCode();
                hashCode = (hashCode * 397) ^ Ins.GetHashCode();
                hashCode = (hashCode * 397) ^ P1.GetHashCode();
                hashCode = (hashCode * 397) ^ P2.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}