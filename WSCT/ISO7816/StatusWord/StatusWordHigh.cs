using System;
using System.Collections.Generic;
using System.Linq;
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
        #region >> Properties

        /// <summary>
        /// List of known SW2 values.
        /// </summary>
        public List<StatusWordLow> Sw2List { get; set; }

        public byte Sw1 { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public StatusWordHigh()
        {
            Sw2List = new List<StatusWordLow>();
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Retrieves the description for <paramref name="sw2"/>.
        /// </summary>
        /// <param name="sw2"></param>
        /// <returns></returns>
        public string GetDescription(byte sw2)
        {
            var sw2Description = Sw2List.LastOrDefault(sw2Element => sw2Element.Contains(sw2));

            return sw2Description == null ? String.Empty : sw2Description.Description;
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
            Sw1 = reader.GetAttribute("value").FromHexa()[0];
            reader.ReadStartElement();
            var serializer = new XmlSerializer(typeof(StatusWordLow));
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        var sw2 = (StatusWordLow)serializer.Deserialize(reader);
                        Sw2List.Add(sw2);
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
            writer.WriteAttributeString("value", String.Format("{0:X2}", Sw1));
            var serializer = new XmlSerializer(typeof(StatusWordLow));
            foreach (var sw2 in Sw2List)
            {
                serializer.Serialize(writer, sw2);
            }
        }

        #endregion
    }
}