using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WSCT.ISO7816.StatusWord
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("StatusWordDictionary")]
    public class StatusWordDictionary
    {
        #region >> Fields

        private List<StatusWordHigh> _sw1List;

        #endregion

        #region >> Properties

        /// <summary>
        /// List of known SW1 values
        /// </summary>
        [XmlElement("sw1")]
        public List<StatusWordHigh> Sw1List
        {
            get { return _sw1List; }
            set { _sw1List = value; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public StatusWordDictionary()
        {
            _sw1List = new List<StatusWordHigh>();
        }

        #endregion

        #region >> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sw1"></param>
        /// <param name="sw2"></param>
        /// <returns></returns>
        public string GetDescription(byte sw1, byte sw2)
        {
            var description = "";
            foreach (var sw1Element in _sw1List)
            {
                description = sw1Element.GetDescription(sw1, sw2);
                if (description != "")
                {
                    break;
                }
            }
            return description;
        }

        #endregion
    }
}