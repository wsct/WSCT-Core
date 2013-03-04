using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

using System.Xml.Serialization;
using System.Xml;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents data formatted in BER TLV format.
    /// </summary>
    [XmlRoot("tlvData")]
    public class TLVData : IFormattable, IXmlSerializable
    {
        #region >> Fields

        UInt32 _tag;
        UInt32 _length;
        Byte[] _value;

        List<TLVData> _subFields;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <example>
        /// <code>
        /// TLVData tlvData = new TLVData();
        /// tlvData.tag=0x88;
        /// tlv.length=0x01;
        /// tlv.value=new Byte[1]{0x0A};
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>    
        /// </example>
        public TLVData()
        {
            _subFields = new List<TLVData>();
            _tag = 0;
            _length = 0;
        }

        /// <summary>
        /// Parses a string of hexa numbers as TLV data
        /// </summary>
        /// <param name="data">String in hexa to be parsed</param>
        /// <example>
        /// <code>
        /// TLVData tlvData = new TLVData("88 01 0A");
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>    
        /// </example>
        public TLVData(String data)
            : this()
        {
            parse(data);
        }

        /// <summary>
        /// Parses an array of Bytes as TLV data
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <example>
        /// <code>
        /// TLVData tlvData = new TLVData(new Byte[] {0x88, 0x01, 0x0A});
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>    
        /// </example>
        public TLVData(Byte[] data)
            : this()
        {
            parse(data);
        }

        /// <summary>
        /// Parses an array of Bytes as TLV data
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <param name="offset">Offset to begin parsing</param>
        /// <example>
        /// <code>
        /// TLVData tlvData = new TLVData(new Byte[] {0x87, 0x01, 0x00, 0x88, 0x01, 0x0A},3);
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>
        /// </example>
        public TLVData(Byte[] data, uint offset)
            : this()
        {
            parse(data, offset);
        }

        /// <summary>
        /// Define the new instance with its T,L,V values.
        /// </summary>
        /// <param name="tag">Tag part</param>
        /// <param name="length">Length part</param>
        /// <param name="value">Value part</param>
        /// <example>
        /// <code>
        /// TLVData tlvData = new TLVData(0x88, 0x01, 0x0A);
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>
        /// </example>
        public TLVData(UInt32 tag, UInt32 length, Byte[] value)
            : this()
        {
            _tag = tag;
            _length = length;
            _value = value;
        }

        /// <summary>
        /// Define the new instance with its T and a list of encapsulated <see cref="TLVData"/> objects.
        /// </summary>
        /// <param name="tag">Tag part</param>
        /// <param name="tlvList">List of encapsulated <see cref="TLVData"/> objects</param>
        /// <example>
        /// <code>
        /// List&gt;TLVData&lt; tlvList = new List&gt;TLVData&lt;();
        /// tlvList.Add(new TLVData(0x88, 0x01, 0x0A));
        /// tlvList.Add(new TLVData(0x5F2D, 0x02, 0x01, 0x02));
        /// TLVData tlvData = new TLVData(0x77, tlvList);
        /// Console.WriteLine(String.Format("{0}"), tlvList);
        /// </code>
        /// Output: <c>77 08 88 01 0A 5F 2D 02 01 02</c>
        /// </example>
        public TLVData(UInt32 tag, List<TLVData> tlvList)
            : this()
        {
            _tag = tag;
            subFields = tlvList;
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// Tag part of TLV
        /// </summary>
        [XmlIgnore]
        public UInt32 tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// Length part of TLV
        /// </summary>
        [XmlIgnore]
        public UInt32 length
        {
            get
            {
                if (_length == 0)
                    _length = (uint)value.Length;
                return _length;
            }
            set { _length = value; }
        }

        /// <summary>
        /// Value part of TLV
        /// </summary>
        [XmlIgnore]
        public Byte[] value
        {
            get
            {
                if (_value == null)
                {
                    if (isConstructed() && (_subFields != null))
                    {
                        _value = new Byte[0];
                        foreach (TLVData tlv in _subFields)
                        {
                            Byte[] tlvBytes = tlv.toByteArray();
                            int oldLength = _value.Length;
                            Array.Resize<Byte>(ref _value, oldLength + tlvBytes.Length);
                            Array.Copy(tlvBytes, 0, _value, oldLength, tlvBytes.Length);
                        }
                    }
                    else
                    {
                        _value = new Byte[_length];
                    }
                }
                return _value;
            }
            set
            {
                length = (uint)value.Length;
                parseV(value, 0);
            }
        }

        /// <summary>
        /// When constructed, list of all encapsulated <c>TLVData</c> objects
        /// </summary>
        public List<TLVData> subFields
        {
            get { return _subFields; }
            set
            {
                _subFields = value;
                // Force _value to null and _length to 0 to force next "value" and "length" properties call to be computed again
                _value = null;
                _length = 0;
            }
        }

        /// <summary>
        /// Accessor to the length in bytes of T field
        /// </summary>
        public UInt32 lengthOfT
        {
            get
            {
                if (tag <= 0xFF)
                    return 1;
                else if (tag <= 0xFFFF)
                    return 2;
                else if (tag <= 0xFFFFFF)
                    return 3;
                return 4;
            }
        }

        /// <summary>
        /// Accessor to the length in bytes of L field
        /// </summary>
        protected UInt32 lengthOfL
        {
            get
            {
                if (length <= 0xFF)
                    return 1;
                else if (length <= 0xFFFF)
                    return 2;
                else if (length <= 0xFFFFFF)
                    return 3;
                return 4;
            }
        }

        /// <summary>
        /// Accessor to the length in bytes of V field
        /// </summary>
        protected UInt32 lengthOfV
        {
            get { return (UInt32)value.Length; }
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Returns a byte array containing all bytes from tag to value
        /// </summary>
        /// <returns>Byte[] representation of the object</returns>
        [Obsolete("Use TLVData.toByteArray() instead of this old method")]
        public Byte[] buildByteArray()
        {
            return String.Format("{0:T}{0:L}{0:Vh}", this).fromHexa();
        }

        /// <summary>
        /// Informs if a TLVData having tag <c>tagSearched</c> exists in subfields
        /// Search is recursive in subfields
        /// </summary>
        /// <param name="tagSearched">True to search recursively in subfields, or false</param>
        /// <returns>True if the tag is found</returns>
        public Boolean hasTag(UInt32 tagSearched)
        {
            return hasTag(tagSearched, true);
        }

        /// <summary>
        /// Informs if a TLVData having tag <c>tagSearched</c> exists in subfields
        /// Search can be recursive in subfields
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <param name="recursive">True to search recursively in subfields, or false</param>
        /// <returns>True if the tag is found</returns>
        public Boolean hasTag(UInt32 tagSearched, Boolean recursive)
        {
            if (tag == tagSearched)
                return true;
            Boolean found = false;
            foreach (TLVData subField in _subFields)
            {
                if ((subField.tag == tagSearched) || (recursive && subField.hasTag(tagSearched, recursive)))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        /// <summary>
        /// Obtains and returns first TLVData found having tag <c>tagSearched</c>
        /// Search is recursive in subfields
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <returns>TLVData object having tag <c>tagSearched</c></returns>
        public TLVData getTag(UInt32 tagSearched)
        {
            return getTag(tagSearched, true);
        }

        /// <summary>
        /// Obtains and returns first TLVData found having tag <c>tagSearched</c>
        /// Search can be recursive
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <param name="recursive">True to search recursively in subfields, or false</param>
        /// <returns>TLVData object having tag <c>tagSearched</c></returns>
        public TLVData getTag(UInt32 tagSearched, Boolean recursive)
        {
            if (tag == tagSearched)
                return this;
            TLVData found = null;
            foreach (TLVData subField in _subFields)
            {
                if ((subField.tag == tagSearched))
                {
                    found = subField;
                    break;
                }
                else if (recursive)
                {
                    TLVData subFound = subField.getTag(tagSearched, recursive);
                    if (subFound != null)
                    {
                        found = subFound;
                        break;
                    }
                }
            }
            return found;
        }

        /// <summary>
        /// Obtains and returns all TLVData found having tag <c>tagSearched</c>
        /// Search is recursive in subfields
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <returns>List of TLVData objects having tag <c>tagSearched</c></returns>
        public System.Collections.IEnumerable getTags(UInt32 tagSearched)
        {
            foreach (TLVData tlv in getTags(tagSearched, true))
                yield return tlv;
        }

        /// <summary>
        /// Obtains and returns all TLVData found having tag <c>tagSearched</c>
        /// Search can be recursive
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <param name="recursive">True to search recursively in subfields, or false</param>
        /// <returns>List of TLVData objects having tag <c>tagSearched</c></returns>
        public System.Collections.IEnumerable getTags(UInt32 tagSearched, Boolean recursive)
        {
            if (tag == tagSearched)
                yield return this;
            foreach (TLVData subField in _subFields)
            {
                if (recursive)
                {
                    foreach (TLVData subFound in subField.getTags(tagSearched, recursive))
                        yield return subFound;
                }
            }
        }

        /// <summary>
        /// Obtains and returns all TLVData found in subfields
        /// Search is recursive
        /// </summary>
        /// <returns>IEnumerable</returns>
        public System.Collections.IEnumerable getTags()
        {
            yield return this;
            foreach (TLVData subField in _subFields)
            {
                foreach (TLVData subFound in subField.getTags())
                {
                    yield return subFound;
                }
            }
        }

        /// <summary>
        /// Informs if the current tag is constructed (ie value is TLV formatted)
        /// </summary>
        /// <returns><c>True</c> if tag is constructed</returns>
        public Boolean isConstructed()
        {
            UInt32 mostSignificantByte = _tag;
            while ((mostSignificantByte >> 8) != 0)
                mostSignificantByte >>= 8;
            return ((mostSignificantByte & 0x20) == 0x20);
        }

        /// <summary>
        /// Parses a string of hexa numbers as TLV data
        /// </summary>
        /// <param name="data">String in hexa to be parsed</param>
        /// <returns>Number of bytes consumed</returns>
        public uint parse(String data)
        {
            return parse(data.fromHexa());
        }

        /// <summary>
        /// Parses an array of Bytes as TLV data
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <returns>Number of bytes consumed</returns>
        public uint parse(Byte[] data)
        {
            return parse(data, 0);
        }

        /// <summary>
        /// Parses an array of Bytes as TLV data
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <param name="offset">Offset to begin parsing</param>
        /// <returns>New value of the offset (<paramref name="offset"/>+number of bytes consumed)</returns>
        public uint parse(Byte[] data, uint offset)
        {
            offset = parseT(data, offset);
            offset = parseL(data, offset);
            offset = parseV(data, offset);
            return offset;
        }

        /// <summary>
        /// Parses the L (length) part of a TLV data from an array of bytes
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <param name="offset">Offset to begin parsing</param>
        /// <returns>New value of the offset (<paramref name="offset"/>+number of bytes consumed)</returns>
        public uint parseL(Byte[] data, uint offset)
        {
            if (data[offset] >= 0x80)
            {
                uint size = data[offset] - (uint)0x80;
                offset++;
                _length = 0;
                for (int i = 0; i < size; i++)
                    _length = _length * 0x100 + data[offset + i];
                offset += size;
            }
            else
            {
                _length = data[offset];
                offset++;
            }
            return offset;
        }

        /// <summary>
        /// Parses the T (tag) part of a TLV data from an array of bytes
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <param name="offset">Offset to begin parsing</param>
        /// <returns>New value of the offset (<paramref name="offset"/>+number of bytes consumed)</returns>
        public uint parseT(Byte[] data, uint offset)
        {
            if ((data[offset] & 0x1F) == 0x1F)
            {
                _tag = data[offset];
                offset++;
                _tag = 0x100 * _tag + data[offset];
                offset++;
            }
            else
            {
                _tag = data[offset];
                offset++;
            }
            return offset;
        }

        /// <summary>
        /// Parses the V (value) part of a TLV data from an array of bytes.
        /// </summary>
        /// <remarks>
        /// Note: length of the <c>TLVData</c> instance must be defined before calling this method.
        /// </remarks>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <param name="offset">Offset to begin parsing</param>
        /// <returns>New value of the offset (<paramref name="offset"/>+number of bytes consumed)</returns>
        public uint parseV(Byte[] data, uint offset)
        {
            _value = new Byte[_length];
            Array.Copy(data, offset, _value, 0, _length);
            offset += _length;

            if (isConstructed())
            {
                uint offsetValue = 0;
                TLVData subData;
                while (offsetValue < _length)
                {
                    subData = new TLVData();
                    offsetValue = subData.parse(_value, offsetValue);
                    _subFields.Add(subData);
                }
            }
            return offset;
        }

        /// <summary>
        /// Returns a byte array containing all bytes from tag to value
        /// </summary>
        /// <returns>Byte[] representation of the object</returns>
        public Byte[] toByteArray()
        {
            int lenT = (int)lengthOfT;
            int lenL = (int)lengthOfL;
            int lenV = (int)lengthOfV;

            Byte[] byteArray = new Byte[lenT + lenL + lenV];

            Array.Copy(tag.toByteArray(lenT), byteArray, (int)lenT);
            Array.Copy(length.toByteArray(lenL), 0, byteArray, (int)lenT, lenL);
            Array.Copy(value, 0, byteArray, lenT + lenL, lenV);

            return byteArray;
        }

        #endregion

        #region >> IFormattable Membres

        /// <inheritdoc />
        /// <example>
        /// <code>TLVData = new TLVData("6F 1A 84 0E 31 50 41 59 2E 53 59 53 2E 44 44 46 30 31 A5 08 88 01 02 5F 2D 02 66 72");</code>
        /// <para>Default format:
        /// <code>String.Format("{0}", tlv);</code>
        /// output: <c>T:6F L:1A V:( T:84 L:0E V:31 50 41 59 2E 53 59 53 2E 44 44 46 30 31 )( T:A5 L:08 V:( T:88 L:01 V:02 )( T:5F2D L:02 V:66 72 ) )</c>
        /// </para>
        /// <para>Format "T": tag only
        /// <code>String.Format("{0:T}", tlv);</code>
        /// output: <c>6F</c>
        /// </para>
        /// <para>Format "L": length only
        /// <code>String.Format("{0:L}", tlv);</code>
        /// output: <c>1A</c>
        /// </para>
        /// <para>Format "V": value only; value is interpreted as TLV if its a complex tag
        /// <code>String.Format("{0:V}", tlv);</code>
        /// output: <c>( T:84 L:0E V:31 50 41 59 2E 53 59 53 2E 44 44 46 30 31 )( T:A5 L:08 V:( T:88 L:01 V:02 )( T:5F2D L:02 V:66 72 ) )</c>
        /// </para>
        /// <para>Format "Vh": raw value, no interpretation
        /// <code>String.Format("{0:Vh}", tlv);</code>
        /// output: <c>84 0E 31 50 41 59 2E 53 59 53 2E 44 44 46 30 31 A5 08 88 01 02 5F 2D 02 66 72</c>
        /// </para>
        /// </example>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            String s = "";
            switch (format)
            {
                case "T":
                    String tagFormatter = "{0:X" + 2 * lengthOfT + "}";
                    s = String.Format(tagFormatter, _tag);
                    break;
                case "L":
                    String lengthFormatter = "{0:X" + 2 * lengthOfL + "}";
                    s = String.Format(lengthFormatter, _length);
                    break;
                case "V":
                    if (isConstructed())
                    {
                        foreach (TLVData subField in _subFields)
                            s += String.Format("( {0} )", subField);
                    }
                    else
                    {
                        s = _value.toHexa();
                    }
                    break;
                case "Vh":
                    s = value.toHexa();
                    break;
                default:
                    s = String.Format("T:{0:T} L:{0:L} V:{0:V}", this);
                    break;
            }
            return s;
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
        {
            parseT(reader.GetAttribute("tag").fromHexa(), 0);

            reader.MoveToElement();
            if (reader.IsEmptyElement)
            { // <tlvData tag=... (length=...) value=... />
                Byte[] valueAttribute = reader.GetAttribute("value").fromHexa();
                if (reader.MoveToAttribute("length"))
                {
                    parseL(reader.ReadContentAsString().fromHexa(), 0);
                }
                else
                {
                    length = (uint)valueAttribute.Length;
                }
                parseV(valueAttribute, 0);
                reader.ReadStartElement();
            }
            else
            { // <tlvData tag=... > ... </tlvData>
                reader.ReadStartElement();
                _value = null;
                _length = 0;
                _subFields = new List<TLVData>();
                XmlSerializer serializer = new XmlSerializer(typeof(TLVData));
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            TLVData tlv = (TLVData)serializer.Deserialize(reader);
                            subFields.Add(tlv);
                            break;
                        case XmlNodeType.Comment:
                            reader.Read();
                            break;
                    }
                }
                reader.ReadEndElement();
            }
        }

        /// <inheritdoc />
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("tag", String.Format("{0:T}", this));
            if (isConstructed())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TLVData));
                foreach (TLVData tlv in subFields)
                {
                    serializer.Serialize(writer, tlv);
                }
            }
            else
            {
                writer.WriteAttributeString("length", String.Format("{0:L}", this));
                writer.WriteAttributeString("value", String.Format("{0:Vh}", this));
            }
        }

        #endregion
    }
}
