using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using WSCT.Helpers;

namespace WSCT.ISO7816.StatusWord
{
    /// <summary>
    /// SW2 part
    /// </summary>
    [XmlRoot("sw1")]
    public class StatusWordHigh : IXmlSerializable
    {
        #region >> Fields

        List<StatusWordLow> _sw2List;
        Byte _sw1;

        #endregion

        #region >> Properties

        /// <summary>
        /// List of known SW2 values
        /// </summary>
        public List<StatusWordLow> sw2List
        {
            get { return _sw2List; }
            set { _sw2List = value; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public StatusWordHigh()
        {
            _sw2List = new List<StatusWordLow>();
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sw1"></param>
        /// <param name="sw2"></param>
        /// <returns></returns>
        public String getDescription(Byte sw1, Byte sw2)
        {
            String description = "";
            if (_sw1 == sw1)
            {
                foreach (StatusWordLow sw2Element in _sw2List)
                {
                    if (sw2Element.contains(sw2))
                        description = sw2Element.description;
                }
            }
            return description;
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
            _sw1 = reader.GetAttribute("value").fromHexa()[0];
            reader.ReadStartElement();
            XmlSerializer serializer = new XmlSerializer(typeof(StatusWordLow));
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        StatusWordLow sw2 = (StatusWordLow)serializer.Deserialize(reader);
                        _sw2List.Add(sw2);
                        break;
                    case XmlNodeType.Comment:
                        reader.Read();
                        break;
                }
            }
            reader.ReadEndElement();
        }

        /// <inheritdoc />
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("value", String.Format("{0:X2}", _sw1));
            XmlSerializer serializer = new XmlSerializer(typeof(StatusWordLow));
            foreach (StatusWordLow sw2 in _sw2List)
            {
                serializer.Serialize(writer, sw2);
            }
        }

        #endregion
    }
}
