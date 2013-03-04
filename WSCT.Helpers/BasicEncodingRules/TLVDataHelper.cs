using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Allows simple manipulation and conversion between <c>Byte[]</c>, <c>String</c> and <see cref="TLVData"/> types data.
    /// <para><c>ArrayOfBytes</c> also provides extension methods on <c>Byte[]</c> and <c>String</c> and <c>List&lt;TLVData&gt;</c></para>
    /// </summary>
    public static class TLVDataHelper
    {
        /// <summary>
        /// Converts a <c>Byte[]</c> into a TLVData by parsing the buffer.
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <returns>A new TLVData</returns>
        /// <remarks>
        /// It is equivalent to <c>new TLVData(buffer)</c>
        /// </remarks>
        /// <example>
        ///     <code>
        ///     Byte[] source = new Byte[] {0x88, 0x01, 0x0A}
        ///     TLVData tlv = source.toTLVData();
        ///     // Now tlv is T:88 L:01 V:0A
        ///     </code>
        /// </example>
        static public TLVData toTLVData(this Byte[] buffer)
        {
            return new TLVData(buffer);
        }

        /// <summary>
        /// Converts a <c>Byte[]</c> into a TLVData by parsing the buffer.
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <returns>A new TLVData</returns>
        /// <remarks>
        /// It is equivalent to <c>new TLVData(buffer)</c>
        /// </remarks>
        /// <example>
        ///     <code>
        ///     String source = "88 01 0A";
        ///     TLVData tlv = source.toTLVData();
        ///     // Now tlv is T:88 L:01 V:0A
        ///     </code>
        /// </example>
        static public TLVData toTLVData(this String buffer)
        {
            return new TLVData(buffer);
        }

        /// <summary>
        /// Converts a <c>List&lt;TLVData&gt;</c> into a <see cref="TLVData"/> by using the buffer as subfields.
        /// </summary>
        /// <param name="tlvList">Source data to convert</param>
        /// <param name="tag">Tag of the newly created <see cref="TLVData"/></param>
        /// <returns>A new TLVData</returns>
        /// <remarks>
        /// It is equivalent to <c>new TLVData(tag, buffer)</c>
        /// </remarks>
        /// <example>
        ///     <code>
        ///     TLVData tlv1 = "88 01 02".toTLVData();
        ///     TLVData tlv2 = "5F 2D 03 01 02 03".toTLVData();
        ///     List&gt;TLVData&lt; ltlv = new List&gt;TLVData&lt;();
        ///     ltlv.Add(tlv1);
        ///     ltlv.Add(tlv2);
        ///     ltlv = ltlv.toTLVData(0x20);
        ///     // Now ltlv = T:20 L:08  V:88 01 02 5F 2D 03 01 02 03
        ///     </code>
        /// </example>
        static public TLVData toTLVData(this List<TLVData> tlvList, UInt32 tag)
        {
            return new TLVData(tag, tlvList);
        }

        /// <summary>
        /// Converts a <c>List&lt;TLVData&gt;</c> into an XML representation <see cref="String"/>
        /// </summary>
        /// <param name="tlv">Source data to convert</param>
        /// <returns>A new String</returns>
        /// <remarks>
        /// <example>
        ///     <code>
        ///     TLVData tlv = "70 03 88 01 02".toTLVData();
        ///     String xmltlv = tlv.toXmlString();
        ///     // now xmltlv is 
        ///     // <![CDATA[
        ///     // <?xml version="1.0" encoding="utf-16"?>
        ///     // <tlvData tag="70" length="03" value="88 01 02">
        ///     //   <tlvData tag="88" length="01" value="02" />
        ///     // </tlvData>
        ///     // ]]>
        ///     </code>
        /// </example>
        /// </remarks>
        static public String toXmlString(this TLVData tlv)
        {
            XmlSerializer xs = new XmlSerializer(typeof(TLVData));
            System.IO.StringWriter sw = new System.IO.StringWriter();
            XmlSerializerNamespaces xsns = new XmlSerializerNamespaces();
            xsns.Add("", "");
            xs.Serialize(sw, tlv, xsns);
            return sw.ToString();
        }

        /// <summary>
        /// Export a TLV object to XML format using a dictionnary to resolve formating
        /// </summary>
        /// <param name="tlv">Source object</param>
        /// <param name="dictionary">TLV Dictionary to use to find the long name</param>
        /// <returns>The <c>String</c> representation</returns>
        static public String toXmlString(this TLVData tlv, TLVDictionary dictionary)
        {
            return tlv.toXmlNode(new XmlDocument(), dictionary).OuterXml;
        }

        /// <summary>
        /// Converts a TLVData into an XmlNode representation
        /// </summary>
        /// <param name="tlv">Source data to convert</param>
        /// <param name="xmlDoc">XML document used to create elements</param>
        /// <returns>A new XmlNode</returns>
        static public XmlNode toXmlNode(this TLVData tlv, System.Xml.XmlDocument xmlDoc)
        {
            xmlDoc.LoadXml(tlv.toXmlString());
            return xmlDoc["tlvData"];
        }

        /// <summary>
        /// Converts a TLVData into an XmlNode representation, enhanced with the long name of the field found in the <paramref name="dictionary"/>
        /// </summary>
        /// <param name="tlv">Source data to convert</param>
        /// <param name="xmlDoc">XML document used to create elements</param>
        /// <param name="dictionary">TLV Dictionary to use to find the long name</param>
        /// <returns>A new XmlNode</returns>
        static public XmlNode toXmlNode(this TLVData tlv, System.Xml.XmlDocument xmlDoc, TLVDictionary dictionary)
        {
            XmlNode xmlNode = tlv.toXmlNode(xmlDoc);

            insertDictionaryInformation(xmlNode, dictionary);
            insertDictionaryInformation(xmlNode.SelectNodes("tlvData"), dictionary);

            return xmlNode;
        }

        #region >> Private Methods

        static private void insertDictionaryInformation(XmlNode xmlNode, TLVDictionary dictionary)
        {
            TLVDescription description = dictionary.get(xmlNode.Attributes["tag"].Value);
            if (description != null)
            {
                XmlAttribute attribute = xmlNode.OwnerDocument.CreateAttribute("longName");
                attribute.Value = description.longName;
                xmlNode.Attributes.Append(attribute);
            }
        }

        static private void insertDictionaryInformation(XmlNodeList xmlNodeList, TLVDictionary dictionary)
        {
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                insertDictionaryInformation(xmlNode, dictionary);
                insertDictionaryInformation(xmlNode.SelectNodes("tlvData"), dictionary);
            }
        }

        #endregion
    }
}
