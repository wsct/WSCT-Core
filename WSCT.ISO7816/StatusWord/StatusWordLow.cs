using System;
using System.Xml;
using System.Xml.Serialization;

using WSCT.Helpers;

namespace WSCT.ISO7816.StatusWord
{
    /// <summary>
    /// SW1 range and associated description
    /// </summary>
    [XmlRoot("sw2")]
    public class StatusWordLow : IXmlSerializable
    {
        #region >> Properties

        /// <summary>
        /// SW2 minimum value for the description
        /// </summary>
        public Byte from
        { get; set; }

        /// <summary>
        /// SW2 maximum value for the description
        /// </summary>
        public Byte to
        { get; set; }

        /// <summary>
        /// Associated description when <see cref="from"/> &amp;&lt; SW2 &amp;&lt; <see cref="to"/>
        /// </summary>
        public String description
        { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public StatusWordLow()
        {
            from = to = 0;
            description = "";
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sw2"></param>
        /// <returns></returns>
        public bool contains(Byte sw2)
        {
            return ((from <= sw2) && (sw2 <= to));
        }

        #endregion

        #region >> IXmlSerializable Members

        /// <inheritdoc />
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <inheritdoc />
        public void ReadXml(System.Xml.XmlReader reader)
        {   // <sw2 (from=... to=...) (value=...)> ... </sw2>
            if (reader.MoveToAttribute("from"))
            {
                from = reader.ReadContentAsString().fromHexa()[0];
            }
            if (reader.MoveToAttribute("to"))
            {
                to = reader.ReadContentAsString().fromHexa()[0];
            }
            if (reader.MoveToAttribute("value"))
            {
                from = to = reader.ReadContentAsString().fromHexa()[0];
            }
            reader.ReadStartElement();
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Text:
                        description = reader.ReadString();
                        break;
                    case XmlNodeType.Comment:
                        reader.Read();
                        break;
                }
            }
            reader.ReadEndElement();
        }

        /// <inheritdoc />
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            if (from == to)
            {
                writer.WriteAttributeString("value", String.Format("{0:X2}", from));
            }
            else
            {
                writer.WriteAttributeString("from", String.Format("{0:X2}", from));
                writer.WriteAttributeString("to", String.Format("{0:X2}", to));
            }
            writer.WriteString(description);
        }

        #endregion
    }
}
