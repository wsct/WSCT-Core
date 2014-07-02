namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents a TLV object where the value is binary data.
    /// </summary>
    public class BinaryTlvObject : AbstractTlvObject
    {
        #region >> Object

        /// <inheritdoc />
        public override string ToString()
        {
            return Tlv.Value.ToHexa();
        }

        #endregion
    }
}