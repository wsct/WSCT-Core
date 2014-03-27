using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Allows simple manipulation and conversion between <c>byte[]</c>, <c>String</c> and <see cref="TlvData"/> types data.
    /// <para><c>ArrayOfBytes</c> also provides extension methods on <c>byte[]</c> and <c>String</c> and <c>List&lt;TLVData&gt;</c></para>
    /// </summary>
    public static class TlvDataHelper
    {
        /// <summary>
        /// Converts a <c>byte[]</c> into a TLVData by parsing the buffer.
        /// </summary>
        /// <param name="buffer">Source data to convert</param>
        /// <returns>A new TLVData</returns>
        /// <remarks>
        /// It is equivalent to <c>new TLVData(buffer)</c>
        /// </remarks>
        /// <example>
        ///     <code>
        ///     byte[] source = new byte[] {0x88, 0x01, 0x0A}
        ///     TLVData tlv = source.toTLVData();
        ///     // Now tlv is T:88 L:01 V:0A
        ///     </code>
        /// </example>
        public static TlvData ToTlvData(this byte[] buffer)
        {
            return new TlvData(buffer);
        }

        /// <summary>
        /// Converts a <c>byte[]</c> into a TLVData by parsing the buffer.
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
        public static TlvData ToTlvData(this String buffer)
        {
            return new TlvData(buffer);
        }

        /// <summary>
        /// Converts a <c>List&lt;TLVData&gt;</c> into a <see cref="TlvData"/> by using the buffer as subfields.
        /// </summary>
        /// <param name="tlvList">Source data to convert</param>
        /// <param name="tag">Tag of the newly created <see cref="TlvData"/></param>
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
        public static TlvData ToTlvData(this List<TlvData> tlvList, UInt32 tag)
        {
            return new TlvData(tag, tlvList);
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
        public static String ToXmlString(this TlvData tlv)
        {
            var xs = new XmlSerializer(typeof(TlvData));
            var sw = new StringWriter();
            var xsns = new XmlSerializerNamespaces();
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
        public static String ToXmlString(this TlvData tlv, TlvDictionary dictionary)
        {
            return tlv.ToXmlNode(new XmlDocument(), dictionary).OuterXml;
        }

        /// <summary>
        /// Converts a TLVData into an XmlNode representation
        /// </summary>
        /// <param name="tlv">Source data to convert</param>
        /// <param name="xmlDoc">XML document used to create elements</param>
        /// <returns>A new XmlNode</returns>
        public static XmlNode ToXmlNode(this TlvData tlv, XmlDocument xmlDoc)
        {
            xmlDoc.LoadXml(tlv.ToXmlString());
            return xmlDoc["tlvData"];
        }

        /// <summary>
        /// Converts a TLVData into an XmlNode representation, enhanced with the long name of the field found in the <paramref name="dictionary"/>
        /// </summary>
        /// <param name="tlv">Source data to convert</param>
        /// <param name="xmlDoc">XML document used to create elements</param>
        /// <param name="dictionary">TLV Dictionary to use to find the long name</param>
        /// <returns>A new XmlNode</returns>
        public static XmlNode ToXmlNode(this TlvData tlv, XmlDocument xmlDoc, TlvDictionary dictionary)
        {
            var xmlNode = tlv.ToXmlNode(xmlDoc);

            InsertDictionaryInformation(xmlNode, dictionary);
            InsertDictionaryInformation(xmlNode.SelectNodes("tlvData"), dictionary);

            return xmlNode;
        }

        #region >> Private Methods

        private static void InsertDictionaryInformation(XmlNode xmlNode, TlvDictionary dictionary)
        {
            var description = dictionary.Get(xmlNode.Attributes["tag"].Value);
            if (description != null)
            {
                var attribute = xmlNode.OwnerDocument.CreateAttribute("longName");
                attribute.Value = description.LongName;
                xmlNode.Attributes.Append(attribute);
            }
        }

        private static void InsertDictionaryInformation(XmlNodeList xmlNodeList, TlvDictionary dictionary)
        {
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                InsertDictionaryInformation(xmlNode, dictionary);
                InsertDictionaryInformation(xmlNode.SelectNodes("tlvData"), dictionary);
            }
        }

        #endregion
    }
}