using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<StatusWordHigh> sw1List;

        #endregion

        #region >> Properties

        /// <summary>
        /// List of known SW1 values
        /// </summary>
        [XmlElement("sw1")]
        public List<StatusWordHigh> Sw1List
        {
            get { return sw1List; }
            set { sw1List = value; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public StatusWordDictionary()
        {
            sw1List = new List<StatusWordHigh>();
        }

        #endregion

        #region >> Members

        /// <summary>
        /// Retrieves the description for status word <paramref name="sw1"/>-<paramref name="sw2"/>.
        /// </summary>
        /// <param name="sw1"></param>
        /// <param name="sw2"></param>
        /// <returns></returns>
        public string GetDescription(byte sw1, byte sw2)
        {
            var sw1Description = sw1List.FirstOrDefault(d => d.Sw1 == sw1);

            return sw1Description == null ? String.Empty : sw1Description.GetDescription(sw2);
        }

        #endregion
    }
}