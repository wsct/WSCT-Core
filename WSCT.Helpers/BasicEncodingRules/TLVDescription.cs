using System;
using System.Xml.Serialization;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Describes the TLV representation of a given tag.
    /// </summary>
    /// <remarks><inheritdoc cref="TlvDictionary"/></remarks>
    public class TlvDescription
    {
        #region >> Encapsulated Classes

        /// <summary>
        /// Description of field value.
        /// </summary>
        public class ValueType
        {
            /// <summary>
            /// Format of the field.
            /// </summary>
            /// <value>Default is <c>"b"</c>.</value>
            [XmlAttribute("format")]
            public string Format = "b";

            /// <summary>
            /// Maximum size of the field.
            /// </summary>
            [XmlAttribute("maxSize")]
            public byte MaxSize = byte.MaxValue;

            /// <summary>
            /// Minimum size of the field.
            /// </summary>
            [XmlAttribute("minSize")]
            public byte MinSize = 0;
        }

        #endregion

        #region >> Fields

        private string _className;
        private string _dllName;
        private string _hexaValue;
        private string _longName;
        private string _name;
        private string _pathToDll;
        private string _source;

        #endregion

        #region >> Properties

        /// <summary>
        /// Accessor to tag field represented in hexadecimal as a <c>string</c>.
        /// </summary>
        [XmlAttribute("hexaValue")]
        public string HexaValue
        {
            get { return _hexaValue; }
            set { _hexaValue = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the (short) name of the tag.
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return _name; }
            set { _name = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the long name of the tag.
        /// </summary>
        [XmlAttribute("longName")]
        public string LongName
        {
            get { return _longName; }
            set { _longName = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the source of the tag (ICC/Terminal/Issuer/...)
        /// </summary>
        [XmlAttribute("source")]
        public string Source
        {
            get { return _source; }
            set { _source = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the format of the field
        /// </summary>
        [XmlElement("value")]
        public ValueType Value { get; set; }

        /// <summary>
        /// Accessor to the name of DLL file containing the <see cref="ClassName"/>
        /// </summary>
        [XmlElement("dll")]
        public string DllName
        {
            get { return _dllName; }
            set { _dllName = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the name of the class to be used
        /// </summary>
        [XmlElement("className")]
        public string ClassName
        {
            get { return _className; }
            set { _className = (value ?? String.Empty); }
        }

        /// <summary>
        /// Accessor to the path to dll file on disk.
        /// </summary>
        [XmlElement("pathToDll")]
        public string PathToDll
        {
            get { return _pathToDll; }
            set { _pathToDll = (value ?? String.Empty); }
        }

        /// <summary>
        /// Informs if descriptor is correctly defined
        /// </summary>
        [XmlIgnore]
        public Boolean IsValid
        {
            get { return (_hexaValue != null && _className != null); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TlvDescription()
        {
            _pathToDll = "";
            Value = new ValueType();
        }

        #endregion
    }
}