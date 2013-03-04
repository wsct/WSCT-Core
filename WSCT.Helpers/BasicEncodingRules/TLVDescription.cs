using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WSCT.Helpers.BasicEncodingRules
{

    /// <summary>
    /// Describes the TLV representation of a given tag.
    /// </summary>
    /// <remarks><inheritdoc cref="TLVDictionary"/></remarks>
    public class TLVDescription
    {
        #region >> Encapsulated Classes

        /// <summary>
        /// Description of field value
        /// </summary>
        public class Value
        {
            /// <summary>
            /// Format of the field
            /// </summary>
            /// <value>Default is <c>"b"</c></value>
            [XmlAttribute("format")]
            public String format = "b";
            /// <summary>
            /// Minimum size of the field
            /// </summary>
            [XmlAttribute("minSize")]
            public Byte minSize = 0;
            /// <summary>
            /// Maximum size of the field
            /// </summary>
            [XmlAttribute("maxSize")]
            public Byte maxSize = Byte.MaxValue;
        }

        #endregion

        #region >> Fields

        String _hexaValue;
        String _name;
        String _longName;
        String _source;

        Value _value;

        String _dllName;
        String _className;
        String _pathToDll;

        #endregion

        #region >> Properties

        /// <summary>
        /// Accessor to tag field represented in hexadecimal as a <c>String</c>.
        /// </summary>
        [XmlAttribute("hexaValue")]
        public String hexaValue
        {
            get { return _hexaValue; }
            set { _hexaValue = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the (short) name of the tag.
        /// </summary>
        [XmlAttribute("name")]
        public String name
        {
            get { return _name; }
            set { _name = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the long name of the tag.
        /// </summary>
        [XmlAttribute("longName")]
        public String longName
        {
            get { return _longName; }
            set { _longName = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the source of the tag (ICC/Terminal/Issuer/...)
        /// </summary>
        [XmlAttribute("source")]
        public String source
        {
            get { return _source; }
            set { _source = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the format of the field
        /// </summary>
        [XmlElement("value")]
        public Value value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Accessor to the name of DLL file containing the <see cref="className"/>
        /// </summary>
        [XmlElement("dll")]
        public String dllName
        {
            get { return _dllName; }
            set { _dllName = (value == "" ? null : value); }
        }

        /// <summary>
        /// Accessor to the name of the class to be used
        /// </summary>
        [XmlElement("className")]
        public String className
        {
            get { return _className; }
            set { _className = (value == null ? "" : value); }
        }

        /// <summary>
        /// Accessor to the path to dll file on disk
        /// </summary>
        [XmlElement("pathToDll")]
        public String pathToDll
        {
            get { return _pathToDll; }
            set { _pathToDll = (value == null ? "" : value); }
        }

        /// <summary>
        /// Informs if descriptor is correctly defined
        /// </summary>
        [XmlIgnore]
        public Boolean isValid
        {
            get { return (_hexaValue != null && _className != null); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TLVDescription()
        {
            _pathToDll = "";
            _value = new Value();
        }

        #endregion
    }
}
