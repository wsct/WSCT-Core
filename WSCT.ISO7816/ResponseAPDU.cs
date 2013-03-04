using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.Helpers;

namespace WSCT.ISO7816
{

    /// <summary>
    /// Represents the normalized (ISO7816) R-APDU obtained after execution of a C-APDU by a smart card
    /// </summary>
    [XmlRoot("ResponseAPDU")]
    public class ResponseAPDU : ICardResponse, IXmlSerializable
    {
        #region >> Fields

        Byte[] _rAPDU;
        int _bufferSize;

        #endregion

        #region >> Properties

        /// <summary>
        /// UDR retrieved from R-APDU
        /// </summary>
        public Byte[] udr
        {
            get
            {
                if (_rAPDU != null && _rAPDU.Length >= 2)
                {
                    Byte[] ret = new Byte[_rAPDU.Length - 2];
                    Array.Copy(_rAPDU, ret, _rAPDU.Length - 2);
                    return ret;
                }
                else
                    return new Byte[0];
            }
            set
            {
                if (_rAPDU != null && _rAPDU.Length >= 2)
                {
                    Byte oldSW1 = sw1;
                    Byte oldSW2 = sw2;
                    _rAPDU = new Byte[value.Length + 2];
                    Array.Copy(value, _rAPDU, value.Length);
                    sw1 = oldSW1;
                    sw2 = oldSW2;
                }
                else
                {
                    _rAPDU = new Byte[value.Length + 2];
                    Array.Copy(value, _rAPDU, value.Length);
                }
            }
        }

        /// <summary>
        /// High byte of R-APDU State Word
        /// </summary>
        public Byte sw1
        {
            get
            {
                if (_rAPDU != null && _rAPDU.Length >= 2)
                    return _rAPDU[_rAPDU.Length - 2];
                else
                    return 0;
            }
            set { _rAPDU[_rAPDU.Length - 2] = value; }
        }

        /// <summary>
        /// Low byte of R-APDU State Word
        /// </summary>
        public Byte sw2
        {
            get
            {
                if (_rAPDU != null && _rAPDU.Length >= 2)
                    return _rAPDU[_rAPDU.Length - 1];
                else
                    return 0;
            }
            set { _rAPDU[_rAPDU.Length - 1] = value; }
        }

        /// <summary>
        /// R-APDU State Word
        /// </summary>
        public UInt16 statusWord
        {
            get { return (UInt16)((sw1 << 8) + sw2); }
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
        public ResponseAPDU(Byte[] udr, Byte sw1, Byte sw2)
            : this()
        {
            this.udr = new Byte[udr.Length + 2];
            Array.Copy(udr, this.udr, udr.Length);
            this.sw1 = sw1;
            this.sw2 = sw2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        /// <param name="size"></param>
        public ResponseAPDU(Byte[] rAPDU, UInt32 size)
            : this()
        {
            parse(rAPDU, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        public ResponseAPDU(Byte[] rAPDU)
            : this()
        {
            parse(rAPDU);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        public ResponseAPDU(String rAPDU)
            : this()
        {
            parse(rAPDU);
        }

        #endregion

        #region >> Object Membres

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            if (udr.Length > 0)
                return String.Format("{0} {1:X2}-{2:X2}", udr.toHexa(), sw1, sw2);
            else
                return String.Format("{0:X2}-{1:X2}", sw1, sw2);
        }

        #endregion

        #region >> ICardResponse Membres

        /// <summary>
        /// 
        /// </summary>
        public int bufferSize
        {
            get
            {
                return _bufferSize;
            }
            set
            {
                _bufferSize = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        /// <returns></returns>
        public ICardResponse parse(byte[] rAPDU)
        {
            parse(rAPDU, (UInt32)rAPDU.Length);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAPDU"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ICardResponse parse(byte[] rAPDU, UInt32 size)
        {
            size = (size < rAPDU.Length ? size : (UInt32)rAPDU.Length);
            this._rAPDU = new Byte[size];
            Array.Copy(rAPDU, this._rAPDU, size);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cAPDU"></param>
        /// <returns></returns>
        public ICardResponse parse(string cAPDU)
        {
            parse(cAPDU.fromHexa());
            return this;
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
            sw1 = reader.GetAttribute("sw1").fromHexa()[0];
            sw2 = reader.GetAttribute("sw2").fromHexa()[0];
            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                udr = reader.ReadString().fromHexa();
                reader.ReadEndElement();
            }
        }

        /// <inheritdoc />
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("sw1", String.Format("{0:X2}", sw1));
            writer.WriteAttributeString("sw2", String.Format("{0:X2}", sw2));
            writer.WriteString(udr.toHexa());
        }

        #endregion
    }
}