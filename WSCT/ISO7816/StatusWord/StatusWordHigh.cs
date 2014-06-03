using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
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

        private byte _sw1;
        private List<StatusWordLow> _sw2List;

        #endregion

        #region >> Properties

        /// <summary>
        /// List of known SW2 values
        /// </summary>
        public List<StatusWordLow> Sw2List
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
        public string GetDescription(byte sw1, byte sw2)
        {
            var description = "";
            if (_sw1 == sw1)
            {
                foreach (var sw2Element in _sw2List)
                {
                    if (sw2Element.Contains(sw2))
                    {
                        description = sw2Element.Description;
                    }
                }
            }
            return description;
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
            _sw1 = reader.GetAttribute("value").FromHexa()[0];
            reader.ReadStartElement();
            var serializer = new XmlSerializer(typeof(StatusWordLow));
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        var sw2 = (StatusWordLow)serializer.Deserialize(reader);
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
            var serializer = new XmlSerializer(typeof(StatusWordLow));
            foreach (var sw2 in _sw2List)
            {
                serializer.Serialize(writer, sw2);
            }
        }

        #endregion
    }
}