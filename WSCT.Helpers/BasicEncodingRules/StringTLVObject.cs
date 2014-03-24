
namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents a TLV object where the value is a string
    /// </summary>
    public class StringTLVObject : AbstractTLVObject
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new <see cref="StringTLVObject"/> instance.
        /// </summary>
        public StringTLVObject()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="StringTLVObject"/> instance.
        /// </summary>
        /// <param name="tag">Tag of the object.</param>
        /// <param name="text">Text value.</param>
        public StringTLVObject(uint tag, string text)
        {
            tlv = new TLVData(tag, (uint)text.Length, text.fromString());
        }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            return tlv.value.toString(); ;
        }
    }
}
