namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents a TLV object where the value is a string
    /// </summary>
    public class StringTlvObject : AbstractTlvObject
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new <see cref="StringTlvObject"/> instance.
        /// </summary>
        public StringTlvObject()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="StringTlvObject"/> instance.
        /// </summary>
        /// <param name="tag">Tag of the object.</param>
        /// <param name="text">Text value.</param>
        public StringTlvObject(uint tag, string text)
        {
            Tlv = new TlvData(tag, (uint)text.Length, text.FromString());
        }

        #endregion

        #region >> Object

        /// <inheritdoc/>
        public override string ToString()
        {
            return Text;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return Tlv.Value.ToAsciiString(); }
            set
            {
                Tlv.Value = value.FromString();
            }
        }
    }
}