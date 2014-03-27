using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WSCT.Core.APDU;
using WSCT.Helpers;

namespace WSCT.ISO7816
{
    /// <summary>
    /// Represents the normalized (ISO7816) R-APDU obtained after execution of a C-APDU by a smart card.
    /// </summary>
    [XmlRoot("ResponseAPDU")]
    public class ResponseAPDU : ICardResponse, IXmlSerializable
    {
        #region >> Fields

        private byte[] _rAPDU;

        #endregion

        #region >> Properties

        /// <summary>
        /// UDR retrieved from R-APDU.
        /// </summary>
        public byte[] Udr
        {
            get
            {
                if (_rAPDU != null && _rAPDU.Length >= 2)
                {
                    var ret = new byte[_rAPDU.Length - 2];
                    Array.Copy(_rAPDU, ret, _rAPDU.Length - 2);
                    return ret;
                }
                return new byte[0];
            }
            set
            {
                if (_rAPDU != null && _rAPDU.Length >= 2)
                {
                    var oldSw1 = Sw1;
                    var oldSw2 = Sw2;
                    _rAPDU = new byte[value.Length + 2];
                    Array.Copy(value, _rAPDU, value.Length);
                    Sw1 = oldSw1;
                    Sw2 = oldSw2;
                }
                else
                {
                    _rAPDU = new byte[value.Length + 2];
                    Array.Copy(value, _rAPDU, value.Length);
                }
            }
        }

        /// <summary>
        /// High byte of R-APDU State Word.
        /// </summary>
        public byte Sw1
        {
            get
            {
                if (_rAPDU != null && _rAPDU.Length >= 2)
                {
                    return _rAPDU[_rAPDU.Length - 2];
                }
                return 0;
            }
            set { _rAPDU[_rAPDU.Length - 2] = value; }
        }

        /// <summary>
        /// Low byte of R-APDU State Word.
        /// </summary>
        public byte Sw2
        {
            get
            {
                if (_rAPDU != null && _rAPDU.Length >= 2)
                {
                    return _rAPDU[_rAPDU.Length - 1];
                }
                return 0;
            }
            set { _rAPDU[_rAPDU.Length - 1] = value; }
        }

        /// <summary>
        /// R-APDU State Word.
        /// </summary>
        public UInt16 StatusWord
        {
            get { return (UInt16)((Sw1 << 8) + Sw2); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public ResponseAPDU()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="udr"></param>
        /// <param name="sw1"></param>
        /// <param name="sw2"></param>
        public ResponseAPDU(byte[] udr, byte sw1, byte sw2)
            : this()
        {
            Udr = new byte[udr.Length + 2];
            Array.Copy(udr, Udr, udr.Length);
            Sw1 = sw1;
            Sw2 = sw2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        /// <param name="size"></param>
        public ResponseAPDU(byte[] rAPDU, UInt32 size)
            : this()
        {
            Parse(rAPDU, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        public ResponseAPDU(byte[] rAPDU)
            : this()
        {
            Parse(rAPDU);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        public ResponseAPDU(string rAPDU)
            : this()
        {
            Parse(rAPDU);
        }

        #endregion

        #region >> Object Membres

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Udr.Length > 0)
            {
                return String.Format("{0} {1:X2}-{2:X2}", Udr.ToHexa(), Sw1, Sw2);
            }
            return String.Format("{0:X2}-{1:X2}", Sw1, Sw2);
        }

        #endregion

        #region >> ICardResponse Membres

        /// <summary>
        /// 
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        /// <returns></returns>
        public ICardResponse Parse(byte[] rAPDU)
        {
            Parse(rAPDU, (UInt32)rAPDU.Length);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ICardResponse Parse(byte[] rAPDU, UInt32 size)
        {
            size = (size < rAPDU.Length ? size : (UInt32)rAPDU.Length);
            _rAPDU = new byte[size];
            Array.Copy(rAPDU, _rAPDU, size);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cAPDU"></param>
        /// <returns></returns>
        public ICardResponse Parse(string cAPDU)
        {
            Parse(cAPDU.FromHexa());
            return this;
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
            Sw1 = reader.GetAttribute("sw1").FromHexa()[0];
            Sw2 = reader.GetAttribute("sw2").FromHexa()[0];
            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                Udr = reader.ReadString().FromHexa();
                reader.ReadEndElement();
            }
        }

        /// <inheritdoc />
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("sw1", String.Format("{0:X2}", Sw1));
            writer.WriteAttributeString("sw2", String.Format("{0:X2}", Sw2));
            writer.WriteString(Udr.ToHexa());
        }

        #endregion
    }
}