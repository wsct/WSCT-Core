
namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents a TLV object where the value is a string
    /// </summary>
    public class StringTLVObject : AbstractTLVObject
    {
        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StringTLVObject()
            : base()
        {
        }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            return tlv.value.toString(); ;
        }
    }
}
