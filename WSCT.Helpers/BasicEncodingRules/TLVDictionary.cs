using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using WSCT.Helpers.Reflection;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Manages a list of known TLV data representations.
    /// <para>List can be programmatically defined of read from an XML file (see <c>Dictionary.EMVTag.xsd</c> for XML format).</para>
    /// </summary>
    /// <remarks>
    /// <see cref="AbstractTlvObject" /> is the base abstract class for all TLV data representations.
    /// An instance of <see cref="TlvDescription" /> explains how to create a TLV data representation for a given tag.
    /// <see cref="TlvDictionary" /> allows to manage sets of <see cref="TlvDescription" />.
    /// </remarks>
    [XmlRoot("TlvDictionary")]
    public class TlvDictionary
    {
        #region >> Fields

        private Dictionary<string, TlvDescription> _descByHexa;

        #endregion

        #region >> Properties

        private Dictionary<string, TlvDescription> DescByHexa
        {
            get
            {
                if (_descByHexa == null)
                {
                    _descByHexa = new Dictionary<string, TlvDescription>();
                    foreach (var tlvd in TlvDescriptionList)
                    {
                        _descByHexa.Add(tlvd.HexaValue, tlvd);
                    }
                }
                return _descByHexa;
            }
        }

        /// <summary>
        /// List of <see cref="TlvDescription"/> objects known by the dictionary
        /// </summary>
        [XmlElement("tlvDesc")]
        public List<TlvDescription> TlvDescriptionList { get; set; }

        #endregion

        #region >> Methods

        /// <summary>
        /// Adds a description to the known TLV representation
        /// </summary>
        /// <param name="tlvDesc">TLV description for the tlvDesc to add</param>
        public void Add(TlvDescription tlvDesc)
        {
            _descByHexa.Add(tlvDesc.HexaValue, tlvDesc);
        }

        /// <summary>
        /// Creates a new instance of the enhanced TLV object of <paramref name="tlv"/> object
        /// </summary>
        /// <param name="tlv">Reference <c>TLVData</c></param>
        /// <returns>A newly instance of the description class for the tlv</returns>
        public AbstractTlvObject CreateInstance(TlvData tlv)
        {
            AbstractTlvObject tag;
            TlvDescription desc;
            if (tlv.Tag < 0x100)
            {
                desc = Get(String.Format("{0:X2}", tlv.Tag));
            }
            else
            {
                desc = Get(String.Format("{0:X4}", tlv.Tag));
            }
            if (desc == null)
            {
                tag = null;
            }
            else
            {
                tag = CreateInstance(desc);
                tag.Tlv = tlv;
                tag.TlvDescription = desc;
            }
            return tag;
        }

        /// <summary>
        /// Creates a new instance of the TLV description described by <paramref name="tlvDesc"/>
        /// </summary>
        /// <param name="tlvDesc">Description of the tag</param>
        /// <returns>A new instance of the TLV object</returns>
        public static AbstractTlvObject CreateInstance(TlvDescription tlvDesc)
        {
            var dll = "";
            if (tlvDesc.DllName != null)
            {
                dll = (tlvDesc.PathToDll ?? String.Empty) + tlvDesc.DllName;
            }
            return AssemblyLoader.CreateInstance<AbstractTlvObject>(dll, tlvDesc.ClassName);
        }

        /// <summary>
        /// Creates a new instance of the TLV object having tag <paramref name="tagName"/>
        /// </summary>
        /// <param name="tagName">Hexa string representation of the tag value</param>
        /// <returns>A new instance of the TLV object</returns>
        public AbstractTlvObject CreateInstance(string tagName)
        {
            return CreateInstance(Get(tagName));
        }

        /// <summary>
        /// Get the <see cref="TlvDescription"/> instance which name is <paramref name="tagHexaValue"/>
        /// </summary>
        /// <param name="tagHexaValue">Hexa string value of the tag</param>
        /// <returns>The TagDescription instance or null if not find</returns>
        public TlvDescription Get(string tagHexaValue)
        {
            TlvDescription tagFound;
            DescByHexa.TryGetValue(tagHexaValue, out tagFound);
            return tagFound;
        }

        #endregion

        #region >> IEnumerable Membres

        /// <summary>
        /// Enumerator of the registered <see cref="TlvDescription"/>s
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator GetEnumerator()
        {
            return _descByHexa.Values.GetEnumerator();
        }

        #endregion
    }
}