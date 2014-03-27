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
                        _hasLc = false;
                        _hasLe = true;
                        break;
                    case CommandCase.CC3:
                        _hasLc = true;
                        _hasLe = false;
                        break;
                    case CommandCase.CC4:
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
        /// Informs if the C-APDU is a Command Case 3.
        /// </summary>
        public Boolean IsCc3
        {
            get { return _commandCase == CommandCase.CC3; }
        }

        /// <summary>
        /// Informs if the C-APDU is a Command Case 4.
        /// </summary>
        public Boolean IsCc4
        {
            get { return _commandCase == CommandCase.CC4; }
        }

        /// <summary>
        /// Accessor to the entire C-APDU in byte array format.
        /// </summary>
        public byte[] BinaryCommand
        {
            get
            {
                var length = 4 + (HasLc ? Udc.Length + 1 : 0) + (HasLe ? 1 : 0);
                var data = new byte[length];
                data[0] = Cla;
                data[1] = Ins;
                data[2] = P1;
                data[3] = P2;
                if (HasLc)
                {
                    data[4] = (byte)Lc;
                    Array.Copy(Udc, 0, data, 5, Udc.Length);
                    if (HasLe)
                    {
                        data[5 + Udc.Length] = (byte)Le;
                    }
                }
                else
                {
                    if (HasLe)
                    {
                        data[4] = (byte)Le;
                    }
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

        #region >> ICardCommand Membres

        /// <inheritdoc />
        public ICardCommand Parse(byte[] cAPDU)
        {
            if (cAPDU.Length < 4)
            {
                throw new Exception("cApdu.Length<4");
            }
            Cla = cAPDU[0];
            Ins = cAPDU[1];
            P1 = cAPDU[2];
            P2 = cAPDU[3];
            CommandCase = CommandCase.CC1;

            UInt32 pos = 4;
            if (cAPDU.Length > pos)
            {
                UInt32 length = cAPDU[pos];
                pos += 1;
                if (cAPDU.Length >= pos + length)
                {
                    Lc = length;
                    Udc = new byte[length];
                    Array.Copy(cAPDU, pos, Udc, 0, length);
                    pos += length;
                    CommandCase = CommandCase.CC3;
                    if (cAPDU.Length > pos)
                    {
                        Le = cAPDU[pos];
                        CommandCase = CommandCase.CC4;
                    }
                }
                else
                {
                    CommandCase = CommandCase.CC2;
                    Le = length;
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
                Udc = reader.ReadString().FromHexa();
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