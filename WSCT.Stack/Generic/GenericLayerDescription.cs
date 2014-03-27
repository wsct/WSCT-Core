using System;
using System.Xml.Serialization;

namespace WSCT.Stack.Generic
{
    /// <summary>
    /// Generic layer description (by its name, assembly and class name) for dynamic load.
    /// </summary>
    public abstract class GenericLayerDescription
    {
        #region >> Fields

        private string _className;
        private string _dllName;
        private string _name;
        private string _pathToDll;

        #endregion

        #region >> Properties

        /// <summary>
        /// Name of the layer.
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return _name; }
            set { _name = (value == "" ? null : value); }
        }

        /// <summary>
        /// Name of the assembly (without path).
        /// </summary>
        [XmlElement("dll")]
        public string DllName
        {
            get { return _dllName; }
            set { _dllName = (value == "" ? null : value); }
        }

        /// <summary>
        /// Canonical name of the class.
        /// </summary>
        [XmlElement("className")]
        public string ClassName
        {
            get { return _className; }
            set { _className = (value ?? ""); }
        }

        /// <summary>
        /// Path to the assembly (directory only).
        /// </summary>
        [XmlElement("pathToDll")]
        public string PathToDll
        {
            get { return _pathToDll; }
            set { _pathToDll = (value ?? ""); }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Boolean IsValid
        {
            get { return (Name != null && _dllName != null); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        protected GenericLayerDescription()
        {
            _pathToDll = "";
        }

        #endregion
    }
}