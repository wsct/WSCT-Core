using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents data formatted in BER TLV format.
    /// </summary>
    [XmlRoot("tlvData")]
    public class TlvData : IFormattable, IXmlSerializable
    {
        #region >> Fields

        private UInt32 _length;

        private List<TlvData> _subFields;
        private UInt32 _tag;
        private byte[] _value;

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
        /// tlv.value=new byte[1]{0x0A};
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>    
        /// </example>
        public TlvData()
        {
            _subFields = new List<TlvData>();
            _tag = 0;
            _length = 0;
        }

        /// <summary>
        /// Parses a string of hexa numbers as TLV data
        /// </summary>
        /// <param name="data">string in hexa to be parsed</param>
        /// <example>
        /// <code>
        /// TLVData tlvData = new TLVData("88 01 0A");
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>    
        /// </example>
        public TlvData(string data)
            : this()
        {
            Parse(data);
        }

        /// <summary>
        /// Parses an array of Bytes as TLV data
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <example>
        /// <code>
        /// TLVData tlvData = new TLVData(new byte[] {0x88, 0x01, 0x0A});
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>    
        /// </example>
        public TlvData(byte[] data)
            : this()
        {
            Parse(data);
        }

        /// <summary>
        /// Parses an array of Bytes as TLV data
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <param name="offset">Offset to begin parsing</param>
        /// <example>
        /// <code>
        /// TLVData tlvData = new TLVData(new byte[] {0x87, 0x01, 0x00, 0x88, 0x01, 0x0A},3);
        /// Console.WriteLine(String.Format("{0}"));
        /// </code>
        /// Output: <c>88 01 0A</c>
        /// </example>
        public TlvData(byte[] data, uint offset)
            : this()
        {
            Parse(data, offset);
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
        public TlvData(UInt32 tag, UInt32 length, byte[] value)
            : this()
        {
            _tag = tag;
            _length = length;
            _value = value;
        }

        /// <summary>
        /// Define the new instance with its T and a list of encapsulated <see cref="TlvData"/> objects.
        /// </summary>
        /// <param name="tag">Tag part</param>
        /// <param name="tlvList">List of encapsulated <see cref="TlvData"/> objects</param>
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
        public TlvData(UInt32 tag, List<TlvData> tlvList)
            : this()
        {
            _tag = tag;
            SubFields = tlvList;
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// Tag part of TLV
        /// </summary>
        [XmlIgnore]
        public UInt32 Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// Length part of TLV
        /// </summary>
        [XmlIgnore]
        public UInt32 Length
        {
            get
            {
                if (_length == 0)
                {
                    _length = (uint)Value.Length;
                }
                return _length;
            }
            set { _length = value; }
        }

        /// <summary>
        /// Value part of TLV
        /// </summary>
        [XmlIgnore]
        public byte[] Value
        {
            get
            {
                if (_value == null)
                {
                    if (IsConstructed() && (_subFields != null))
                    {
                        _value = new byte[0];
                        foreach (var tlv in _subFields)
                        {
                            var tlvBytes = tlv.ToByteArray();
                            var oldLength = _value.Length;
                            Array.Resize(ref _value, oldLength + tlvBytes.Length);
                            Array.Copy(tlvBytes, 0, _value, oldLength, tlvBytes.Length);
                        }
                    }
                    else
                    {
                        _value = new byte[_length];
                    }
                }
                return _value;
            }
            set
            {
                Length = (uint)value.Length;
                ParseV(value, 0);
            }
        }

        /// <summary>
        /// When constructed, list of all encapsulated <c>TLVData</c> objects
        /// </summary>
        public List<TlvData> SubFields
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
        public UInt32 LengthOfT
        {
            get
            {
                if (Tag <= 0xFF)
                {
                    return 1;
                }
                if (Tag <= 0xFFFF)
                {
                    return 2;
                }
                if (Tag <= 0xFFFFFF)
                {
                    return 3;
                }
                return 4;
            }
        }

        /// <summary>
        /// Accessor to the length in bytes of L field
        /// </summary>
        protected UInt32 LengthOfL
        {
            get
            {
                if (Length <= 0xFF)
                {
                    return 1;
                }
                if (Length <= 0xFFFF)
                {
                    return 2;
                }
                if (Length <= 0xFFFFFF)
                {
                    return 3;
                }
                return 4;
            }
        }

        /// <summary>
        /// Accessor to the length in bytes of V field
        /// </summary>
        protected UInt32 LengthOfV
        {
            get { return (UInt32)Value.Length; }
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Returns a byte array containing all bytes from tag to value
        /// </summary>
        /// <returns>byte[] representation of the object</returns>
        [Obsolete("Use TLVData.toByteArray() instead of this old method")]
        public byte[] BuildByteArray()
        {
            return String.Format("{0:T}{0:L}{0:Vh}", this).FromHexa();
        }

        /// <summary>
        /// Informs if a TLVData having tag <c>tagSearched</c> exists in subfields
        /// Search is recursive in subfields
        /// </summary>
        /// <param name="tagSearched">True to search recursively in subfields, or false</param>
        /// <returns>True if the tag is found</returns>
        public Boolean HasTag(UInt32 tagSearched)
        {
            return HasTag(tagSearched, true);
        }

        /// <summary>
        /// Informs if a TLVData having tag <c>tagSearched</c> exists in subfields
        /// Search can be recursive in subfields
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <param name="recursive">True to search recursively in subfields, or false</param>
        /// <returns>True if the tag is found</returns>
        public Boolean HasTag(UInt32 tagSearched, Boolean recursive)
        {
            if (Tag == tagSearched)
            {
                return true;
            }

            return _subFields.Any(subField => (subField.Tag == tagSearched) || (recursive && subField.HasTag(tagSearched, true)));
        }

        /// <summary>
        /// Obtains and returns first TLVData found having tag <c>tagSearched</c>
        /// Search is recursive in subfields
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <returns>TLVData object having tag <c>tagSearched</c></returns>
        public TlvData GetTag(UInt32 tagSearched)
        {
            return GetTag(tagSearched, true);
        }

        /// <summary>
        /// Obtains and returns first TLVData found having tag <c>tagSearched</c>
        /// Search can be recursive
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <param name="recursive">True to search recursively in subfields, or false</param>
        /// <returns>TLVData object having tag <c>tagSearched</c></returns>
        public TlvData GetTag(UInt32 tagSearched, Boolean recursive)
        {
            if (Tag == tagSearched)
            {
                return this;
            }
            TlvData found = null;
            foreach (var subField in _subFields)
            {
                if ((subField.Tag == tagSearched))
                {
                    found = subField;
                    break;
                }
                if (recursive)
                {
                    var subFound = subField.GetTag(tagSearched, true);
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
        public IEnumerable<TlvData> GetTags(UInt32 tagSearched)
        {
            return GetTags(tagSearched, true);
        }

        /// <summary>
        /// Obtains and returns all TLVData found having tag <c>tagSearched</c>
        /// Search can be recursive
        /// </summary>
        /// <param name="tagSearched">Number of the tag to search</param>
        /// <param name="recursive">True to search recursively in subfields, or false</param>
        /// <returns>List of TLVData objects having tag <c>tagSearched</c></returns>
        public IEnumerable<TlvData> GetTags(UInt32 tagSearched, Boolean recursive)
        {
            if (Tag == tagSearched)
            {
                yield return this;
            }

            foreach (var subField in _subFields)
            {
                if (recursive)
                {
                    foreach (var subFound in subField.GetTags(tagSearched, true))
                    {
                        yield return subFound;
                    }
                }
            }
        }

        /// <summary>
        /// Obtains and returns all TLVData found in subfields
        /// Search is recursive
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable GetTags()
        {
            yield return this;

            foreach (var subField in _subFields)
            {
                foreach (TlvData subFound in subField.GetTags())
                {
                    yield return subFound;
                }
            }
        }

        /// <summary>
        /// Informs if the current tag is constructed (ie value is TLV formatted)
        /// </summary>
        /// <returns><c>True</c> if tag is constructed</returns>
        public Boolean IsConstructed()
        {
            var mostSignificantByte = _tag;
            while ((mostSignificantByte >> 8) != 0)
            {
                mostSignificantByte >>= 8;
            }
            return ((mostSignificantByte & 0x20) == 0x20);
        }

        /// <summary>
        /// Parses a string of hexa numbers as TLV data
        /// </summary>
        /// <param name="data">string in hexa to be parsed</param>
        /// <returns>Number of bytes consumed</returns>
        public uint Parse(string data)
        {
            return Parse(data.FromHexa());
        }

        /// <summary>
        /// Parses an array of Bytes as TLV data
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <returns>Number of bytes consumed</returns>
        public uint Parse(byte[] data)
        {
            return Parse(data, 0);
        }

        /// <summary>
        /// Parses an array of Bytes as TLV data
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <param name="offset">Offset to begin parsing</param>
        /// <returns>New value of the offset (<paramref name="offset"/>+number of bytes consumed)</returns>
        public uint Parse(byte[] data, uint offset)
        {
            offset = ParseT(data, offset);
            offset = ParseL(data, offset);
            offset = ParseV(data, offset);
            return offset;
        }

        /// <summary>
        /// Parses the L (length) part of a TLV data from an array of bytes
        /// </summary>
        /// <param name="data">Array of Bytes to be parsed</param>
        /// <param name="offset">Offset to begin parsing</param>
        /// <returns>New value of the offset (<paramref name="offset"/>+number of bytes consumed)</returns>
        public uint ParseL(byte[] data, uint offset)
        {
            if (data[offset] >= 0x80)
            {
                var size = data[offset] - (uint)0x80;
                offset++;
                _length = 0;
                for (var i = 0; i < size; i++)
                {
                    _length = _length*0x100 + data[offset + i];
                }
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
        public uint ParseT(byte[] data, uint offset)
        {
            if ((data[offset] & 0x1F) == 0x1F)
            {
                _tag = data[offset];
                offset++;
                _tag = 0x100*_tag + data[offset];
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
        public uint ParseV(byte[] data, uint offset)
        {
            _value = new byte[_length];
            Array.Copy(data, offset, _value, 0, _length);
            offset += _length;

            if (IsConstructed())
            {
                uint offsetValue = 0;
                while (offsetValue < _length)
                {
                    var subData = new TlvData();
                    offsetValue = subData.Parse(_value, offsetValue);
                    _subFields.Add(subData);
                }
            }
            return offset;
        }

        /// <summary>
        /// Returns a byte array containing all bytes from tag to value
        /// </summary>
        /// <returns>byte[] representation of the object</returns>
        public byte[] ToByteArray()
        {
            var lenT = (int)LengthOfT;
            var lenL = (int)LengthOfL;
            var lenV = (int)LengthOfV;

            var byteArray = new byte[lenT + lenL + lenV];

            Array.Copy(Tag.ToByteArray(lenT), byteArray, lenT);
            Array.Copy(Length.ToByteArray(lenL), 0, byteArray, lenT, lenL);
            Array.Copy(Value, 0, byteArray, lenT + lenL, lenV);

            return byteArray;
        }

        #endregion

        #region >> IFormattable

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
            switch (format)
            {
                case "T":
                    var tagFormatter = "{0:X" + 2*LengthOfT + "}";
                    return String.Format(tagFormatter, _tag);
                case "L":
                    var lengthFormatter = "{0:X" + 2*LengthOfL + "}";
                    return String.Format(lengthFormatter, _length);
                case "V":
                    if (IsConstructed())
                    {
                        return _subFields.Aggregate(String.Empty, (current, subField) => current + String.Format("( {0} )", subField));
                    }
                    return _value.ToHexa();
                case "Vh":
                    return Value.ToHexa();
                default:
                    return String.Format("T:{0:T} L:{0:L} V:{0:V}", this);
            }
        }

        #endregion

        #region >> IXmlSerializable

        /// <inheritdoc />
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <inheritdoc />
        public void ReadXml(XmlReader reader)
        {
            ParseT(reader.GetAttribute("tag").FromHexa(), 0);

            reader.MoveToElement();
            if (reader.IsEmptyElement)
            {
                // <tlvData tag=... (length=...) value=... />
                var valueAttribute = reader.GetAttribute("value").FromHexa();
                if (reader.MoveToAttribute("length"))
                {
                    ParseL(reader.ReadContentAsString().FromHexa(), 0);
                }
                else
                {
                    Length = (uint)valueAttribute.Length;
                }
                ParseV(valueAttribute, 0);
                reader.ReadStartElement();
            }
            else
            {
                // <tlvData tag=... > ... </tlvData>
                reader.ReadStartElement();
                _value = null;
                _length = 0;
                _subFields = new List<TlvData>();
                var serializer = new XmlSerializer(typeof(TlvData));
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            var tlv = (TlvData)serializer.Deserialize(reader);
                            SubFields.Add(tlv);
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
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("tag", String.Format("{0:T}", this));
            if (IsConstructed())
            {
                var serializer = new XmlSerializer(typeof(TlvData));
                foreach (var tlv in SubFields)
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