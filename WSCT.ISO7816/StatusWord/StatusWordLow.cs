using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WSCT.Helpers;

namespace WSCT.ISO7816.StatusWord
{
    /// <summary>
    /// SW1 range and associated description.
    /// </summary>
    [XmlRoot("sw2")]
    public class StatusWordLow : IXmlSerializable
    {
        #region >> Properties

        /// <summary>
        /// SW2 minimum value for the description.
        /// </summary>
        public byte From { get; set; }

        /// <summary>
        /// SW2 maximum value for the description.
        /// </summary>
        public byte To { get; set; }

        /// <summary>
        /// Associated description when <see cref="From"/> &amp;&lt; SW2 &amp;&lt; <see cref="To"/>.
        /// </summary>
        public String Description { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public StatusWordLow()
        {
            From = To = 0;
            Description = "";
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sw2"></param>
        /// <returns></returns>
        public bool Contains(byte sw2)
        {
            return ((From <= sw2) && (sw2 <= To));
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
            // <sw2 (from=... to=...) (value=...)> ... </sw2>
            if (reader.MoveToAttribute("from"))
            {
                From = reader.ReadContentAsString().FromHexa()[0];
            }
            if (reader.MoveToAttribute("to"))
            {
                To = reader.ReadContentAsString().FromHexa()[0];
            }
            if (reader.MoveToAttribute("value"))
            {
                From = To = reader.ReadContentAsString().FromHexa()[0];
            }
            reader.ReadStartElement();
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Text:
                        Description = reader.ReadString();
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
            if (From == To)
            {
                writer.WriteAttributeString("value", String.Format("{0:X2}", From));
            }
            else
            {
                writer.WriteAttributeString("from", String.Format("{0:X2}", From));
                writer.WriteAttributeString("to", String.Format("{0:X2}", To));
            }
            writer.WriteString(Description);
        }

        #endregion
    }
}