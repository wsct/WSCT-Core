using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.Helpers;
using System.Xml;

namespace WSCT.ISO7816
{
    /// <summary>
    /// Represents the normalized (ISO7816) C-APDU to be sent to a smart card
    /// <para>C-APDU: <b>CLA INS P1 P2 (Lc UDC) (Le)</b></para>
    /// </summary>
    /// <remarks>
    /// Only short C-APDU (Le, Lc coded on 1 byte) are now supported
    /// </remarks>
    [XmlRoot("commandAPDU")]
    public class CommandAPDU : Core.APDU.ICardCommand, IXmlSerializable
    {
        #region >> Fields

        CommandCase _commandCase = CommandCase.CC1;

        Byte _cla = 0x00;
        Byte _ins = 0x00;
        Byte _p1 = 0x00;
        Byte _p2 = 0x00;
        UInt32 _le = 0x00;
        UInt32 _lc = 0x00;
        Byte[] _udc;

        Boolean _hasLe = false;
        Boolean _hasLc = false;

        #endregion

        #region >> Properties

        /// <summary>
        /// Accessor to the Command Case of the C-APDU
        /// Write access: <see cref="hasLe"/> and <see cref="hasLc"/> are updated
        /// </summary>
        public CommandCase commandCase
        {
            get { return _commandCase; }
            set
            {
                _commandCase = value;
                switch (value)
                {
                    case CommandCase.CC1: _hasLc = false; _hasLe = false; break;
                    case CommandCase.CC2: _hasLc = false; _hasLe = true; break;
                    case CommandCase.CC3: _hasLc = true; _hasLe = false; break;
                    case CommandCase.CC4: _hasLc = true; _hasLe = true; break;
                    default: break;
                }
            }
        }
        /// <summary>
        /// Accessor to the CLA byte of the C-APDU
        /// </summary>
        public Byte cla
        {
            get { return _cla; }
            set { _cla = value; }
        }
        /// <summary>
        /// Accessor to the INS byte of the C-APDU
        /// </summary>
        public Byte ins
        {
            get { return _ins; }
            set { _ins = value; }
        }
        /// <summary>
        /// Accessor to the P1 byte of the C-APDU
        /// </summary>
        public Byte p1
        {
            get { return _p1; }
            set { _p1 = value; }
        }
        /// <summary>
        /// Accessor to the P2 byte of the C-APDU
        /// </summary>
        public Byte p2
        {
            get { return _p2; }
            set { _p2 = value; }
        }
        /// <summary>
        /// Accessor to the Le value of the C-APDU
        /// </summary>
        public UInt32 le
        {
            get { return _le; }
            set { _le = value; hasLe = true; }
        }
        /// <summary>
        /// Accessor to the Lc value of the C-APDU
        /// </summary>
        public UInt32 lc
        {
            get { return _lc; }
            set { _lc = value; hasLc = true; }
        }
        /// <summary>
        /// Informs if Le has been parsed
        /// Write access: <see cref="commandCase"/> is updated
        /// </summary>
        public Boolean hasLe
        {
            get { return _hasLe; }
            set
            {
                _hasLe = value;
                if (value)
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC1: _commandCase = CommandCase.CC2; break;
                        case CommandCase.CC3: _commandCase = CommandCase.CC4; break;
                        default: break;
                    }
                }
                else
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC2: _commandCase = CommandCase.CC1; break;
                        case CommandCase.CC4: _commandCase = CommandCase.CC3; break;
                        default: break;
                    }
                }
            }
        }
        /// <summary>
        /// Informs if Lc has been parsed
        /// Write access: <see cref="commandCase"/> is updated
        /// </summary>
        public Boolean hasLc
        {
            get { return _hasLc; }
            set
            {
                _hasLc = value;
                if (value)
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC1: _commandCase = CommandCase.CC3; break;
                        case CommandCase.CC2: _commandCase = CommandCase.CC4; break;
                        default: break;
                    }
                }
                else
                {
                    switch (_commandCase)
                    {
                        case CommandCase.CC3: _commandCase = CommandCase.CC1; break;
                        case CommandCase.CC4: _commandCase = CommandCase.CC2; break;
                        default: break;
                    }
                }
            }
        }
        /// <summary>
        /// Accessor to the UDC of the C-APDU
        /// Write access: <see cref="lc"/> is updated
        /// </summary>
        public Byte[] udc
        {
            get { return (hasLc ? _udc : new Byte[0]); }
            set
            {
                _udc = value;
                lc = (uint)value.Length;
            }
        }

        /// <summary>
        /// Informs if the C-APDU is a Command Case 1
        /// </summary>
        public Boolean isCC1
        {
            get { return _commandCase == CommandCase.CC1; }
        }
        /// <summary>
        /// Informs if the C-APDU is a Command Case 2
        /// </summary>
        public Boolean isCC2
        {
            get { return _commandCase == CommandCase.CC2; }
        }
        /// <summary>
        /// Informs if the C-APDU is a Command Case 3
        /// </summary>
        public Boolean isCC3
        {
            get { return _commandCase == CommandCase.CC3; }
        }
        /// <summary>
        /// Informs if the C-APDU is a Command Case 4
        /// </summary>
        public Boolean isCC4
        {
            get { return _commandCase == CommandCase.CC4; }
        }

        /// <summary>
        /// Accessor to the entire C-APDU in Byte array format
        /// </summary>
        public byte[] binaryCommand
        {
            get
            {
                int length = 4 + (hasLc ? udc.Length + 1 : 0) + (hasLe ? 1 : 0);
                Byte[] data = new Byte[length];
                data[0] = cla;
                data[1] = ins;
                data[2] = p1;
                data[3] = p2;
                if (hasLc)
                {
                    data[4] = (Byte)lc;
                    Array.Copy(udc, 0, data, 5, udc.Length);
                    if (hasLe)
                        data[5 + udc.Length] = (Byte)le;
                }
                else
                {
                    if (hasLe)
                        data[4] = (Byte)le;
                }
                return data;
            }
        }

        /// <summary>
        /// Accessor to the entire C-APDU in String format (all bytes are represented in hexa)
        /// </summary>
        public string stringCommand
        {
            get { return binaryCommand.toHexa(); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CommandAPDU()
        {
        }
        /// <summary>
        /// Constructor for CC1 C-APDU
        /// </summary>
        /// <param name="cla">CLA byte of the C-APDU</param>
        /// <param name="ins">INS byte of the C-APDU</param>
        /// <param name="p1">P1 byte of the C-APDU</param>
        /// <param name="p2">P2 byte of the C-APDU</param>
        public CommandAPDU(Byte cla, Byte ins, Byte p1, Byte p2)
        {
            this.cla = cla;
            this.ins = ins;
            this.p1 = p1;
            this.p2 = p2;
        }
        /// <summary>
        /// Constructor for CC2 C-APDU
        /// </summary>
        /// <param name="cla">CLA byte of the C-APDU</param>
        /// <param name="ins">INS byte of the C-APDU</param>
        /// <param name="p1">P1 byte of the C-APDU</param>
        /// <param name="p2">P2 byte of the C-APDU</param>
        /// <param name="le">Le value of the C-APDU</param>
        public CommandAPDU(Byte cla, Byte ins, Byte p1, Byte p2, UInt32 le)
        {
            this.cla = cla;
            this.ins = ins;
            this.p1 = p1;
            this.p2 = p2;
            this.le = le;
        }
        /// <summary>
        /// Constructor for CC2 C-APDU
        /// </summary>
        /// <param name="cla">CLA byte of the C-APDU</param>
        /// <param name="ins">INS byte of the C-APDU</param>
        /// <param name="p1">P1 byte of the C-APDU</param>
        /// <param name="p2">P2 byte of the C-APDU</param>
        /// <param name="lc">Lc value of the C-APDU</param>
        /// <param name="udc">UDC of the C-APDU</param>
        public CommandAPDU(Byte cla, Byte ins, Byte p1, Byte p2, UInt32 lc, Byte[] udc)
        {
            this.cla = cla;
            this.ins = ins;
            this.p1 = p1;
            this.p2 = p2;
            this.udc = udc;
            this.lc = lc;
        }
        /// <summary>
        /// Constructor for CC4 C-APDU
        /// </summary>
        /// <param name="cla">CLA byte of the C-APDU</param>
        /// <param name="ins">INS byte of the C-APDU</param>
        /// <param name="p1">P1 byte of the C-APDU</param>
        /// <param name="p2">P2 byte of the C-APDU</param>
        /// <param name="lc">Lc value of the C-APDU</param>
        /// <param name="udc">UDC of the C-APDU</param>
        /// <param name="le">Le value of the C-APDU</param>
        public CommandAPDU(Byte cla, Byte ins, Byte p1, Byte p2, UInt32 lc, Byte[] udc, UInt32 le)
        {
            this.cla = cla;
            this.ins = ins;
            this.p1 = p1;
            this.p2 = p2;
            this.udc = udc;
            this.lc = lc;
            this.le = le;
        }
        /// <summary>
        /// Constructor for arbitrary C-APDU
        /// </summary>
        /// <param name="cAPDU">C-APDU to be assigned as a Byte array</param>
        public CommandAPDU(Byte[] cAPDU)
        {
            parse(cAPDU);
        }
        /// <summary>
        /// Constructor for arbitrary C-APDU
        /// </summary>
        /// <param name="cAPDU">C-APDU to be assigned as a String container (each byte is coded in hexa)</param>
        public CommandAPDU(String cAPDU)
        {
            parse(cAPDU);
        }

        #endregion

        #region >> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="udcPart"></param>
        public void appendUDC(Byte[] udcPart)
        {
            new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return stringCommand;
        }

        #endregion

        #region >> ICardCommand Membres

        /// <inheritdoc />
        public ICardCommand parse(byte[] cAPDU)
        {
            if (cAPDU.Length < 4)
                new Exception("cAPDU.Length<4");
            this.cla = cAPDU[0];
            this.ins = cAPDU[1];
            this.p1 = cAPDU[2];
            this.p2 = cAPDU[3];
            this.commandCase = CommandCase.CC1;

            UInt32 pos = 4;
            if (cAPDU.Length > pos)
            {
                UInt32 length = cAPDU[pos];
                pos += 1;
                if (cAPDU.Length >= pos + length)
                {
                    this.lc = length;
                    this.udc = new Byte[length];
                    Array.Copy(cAPDU, pos, this.udc, 0, length);
                    pos += length;
                    this.commandCase = CommandCase.CC3;
                    if (cAPDU.Length > pos)
                    {
                        this.le = cAPDU[pos];
                        this.commandCase = CommandCase.CC4;
                    }
                }
                else
                {
                    this.commandCase = CommandCase.CC2;
                    this.le = length;
                }
            }
            return this;
        }

        /// <inheritdoc />
        public ICardCommand parse(string cAPDU)
        {
            return parse(cAPDU.fromHexa());
        }

        #endregion

        #region >> IXmlSerializable Members

        /// <inheritdoc />
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <inheritdoc />
        public void ReadXml(XmlReader reader)
        {
            cla = reader.GetAttribute("cla").fromHexa()[0];
            ins = reader.GetAttribute("ins").fromHexa()[0];
            p1 = reader.GetAttribute("p1").fromHexa()[0];
            p2 = reader.GetAttribute("p2").fromHexa()[0];
            if (reader.MoveToAttribute("le"))
            {
                le = (uint)reader.ReadContentAsString().fromHexa()[0];
            }
            reader.MoveToElement();
            if (reader.IsEmptyElement)
            {   // <commandAPDU cla=... ins=... p1=... p2=... (le=...) />
                reader.ReadStartElement();
            }
            else
            {   // <commandAPDU cla=... ins=... p1=... p2=... (le=...) > udc </commandAPDU>
                reader.ReadStartElement();
                udc = reader.ReadString().fromHexa();
                reader.ReadEndElement();
            }
            reader.ReadStartElement();
        }

        /// <inheritdoc />
        public void WriteXml(XmlWriter writer)
        {   
            writer.WriteAttributeString("cla", String.Format("{0:X2}", cla));
            writer.WriteAttributeString("ins", String.Format("{0:X2}", ins));
            writer.WriteAttributeString("p1", String.Format("{0:X2}", p1));
            writer.WriteAttributeString("p2", String.Format("{0:X2}", p2));
            if (hasLc)
            {
                writer.WriteString(udc.toHexa());
            }
            if (hasLe)
            {
                writer.WriteAttributeString("le", String.Format("{0:X2}", le));
            }
        }

        #endregion
    }
}