using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using WSCT.Helpers;
using WSCT.Helpers.Reflection;
using WSCT.Helpers.BasicEncodingRules;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Manages a list of known TLV data representations.
    /// <para>List can be programmatically defined of read from an XML file (see <c>Dictionary.EMVTag.xsd</c> for XML format)</para>
    /// </summary>
    /// <remarks>
    /// <see cref="AbstractTLVObject" /> is the base abstract class for all TLV data representations.
    /// An instance of <see cref="TLVDescription" /> explains how to create a TLV data representation for a given tag.
    /// <see cref="TLVDictionary" /> allows to manage sets of <see cref="TLVDescription" />.
    /// </remarks>
    [XmlRoot("TlvDictionary")]
    public class TLVDictionary
    {
        #region >> Fields

        Dictionary<String, TLVDescription> _descByHexa;

        #endregion

        #region >> Properties

        Dictionary<String, TLVDescription> descByHexa
        {
            get
            {
                if (_descByHexa == null)
                {
                    _descByHexa = new Dictionary<string, TLVDescription>();
                    foreach (TLVDescription tlvd in tlvDescriptionList)
                        _descByHexa.Add(tlvd.hexaValue, tlvd);
                }
                return _descByHexa;
            }
            set { _descByHexa = value; }
        }

        /// <summary>
        /// List of <see cref="TLVDescription"/> objects known by the dictionary
        /// </summary>
        [XmlElement("tlvDesc")]
        public List<TLVDescription> tlvDescriptionList
        { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TLVDictionary()
        {
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Adds a description to the known TLV representation
        /// </summary>
        /// <param name="tlvDesc">TLV description for the tlvDesc to add</param>
        public void add(TLVDescription tlvDesc)
        {
            _descByHexa.Add(tlvDesc.hexaValue, tlvDesc);
        }

        /// <summary>
        /// Creates a new instance of the enhanced TLV object of <paramref name="tlv"/> object
        /// </summary>
        /// <param name="tlv">Reference <c>TLVData</c></param>
        /// <returns>A newly instance of the description class for the tlv</returns>
        public AbstractTLVObject createInstance(TLVData tlv)
        {
            AbstractTLVObject tag;
            TLVDescription desc;
            if (tlv.tag < 0x100)
                desc = get(String.Format("{0:X2}", tlv.tag));
            else
                desc = get(String.Format("{0:X4}", tlv.tag));
            if (desc == null)
            {
                tag = null;
            }
            else
            {
                tag = createInstance(desc);
                tag.tlv = tlv;
                tag.tlvDescription = desc;
            }
            return tag;
        }

        /// <summary>
        /// Creates a new instance of the TLV description described by <paramref name="tlvDesc"/>
        /// </summary>
        /// <param name="tlvDesc">Description of the tag</param>
        /// <returns>A new instance of the TLV object</returns>
        public static AbstractTLVObject createInstance(TLVDescription tlvDesc)
        {
            String dll = "";
            if (tlvDesc.dllName != null)
                dll = (tlvDesc.pathToDll == null ? "" : tlvDesc.pathToDll) + tlvDesc.dllName;
            return AssemblyLoader.createInstance<AbstractTLVObject>(dll, tlvDesc.className);
        }

        /// <summary>
        /// Creates a new instance of the TLV object having tag <paramref name="tagName"/>
        /// </summary>
        /// <param name="tagName">Hexa string representation of the tag value</param>
        /// <returns>A new instance of the TLV object</returns>
        public AbstractTLVObject createInstance(String tagName)
        {
            return TLVDictionary.createInstance(get(tagName));
        }

        /// <summary>
        /// Get the <see cref="TLVDescription"/> instance which name is <paramref name="tagHexaValue"/>
        /// </summary>
        /// <param name="tagHexaValue">Hexa string value of the tag</param>
        /// <returns>The TagDescription instance or null if not find</returns>
        public TLVDescription get(String tagHexaValue)
        {
            TLVDescription tagFound;
            descByHexa.TryGetValue(tagHexaValue, out tagFound);
            return tagFound;
        }

        #endregion

        #region >> IEnumerable Membres

        /// <summary>
        /// Enumerator of the registered <see cref="TLVDescription"/>s
        /// </summary>
        /// <returns>The enumerator</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            foreach (TLVDescription tag in _descByHexa.Values)
                yield return tag;
        }

        #endregion
    }
}
