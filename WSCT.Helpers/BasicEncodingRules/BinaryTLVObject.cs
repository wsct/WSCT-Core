
namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents a TLV object where the value is binary data
    /// </summary>
    public class BinaryTLVObject : AbstractTLVObject
    {
        #region >> Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryTLVObject()
            : base()
        {
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return tlv.value.toHexa(); ;
        }
    }
}
