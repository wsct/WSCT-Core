using System.Text;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents a TLV object where the value is BCD encoded.
    /// </summary>
    public class BcdTlvObject : AbstractTlvObject
    {
        #region >> Object

        /// <inheritdoc />
        public override string ToString()
        {
            var s = new StringBuilder();
            foreach (var b in Tlv.Value)
            {
                s.AppendFormat("{0}{1}", b/16, b%16);
            }
            return s.ToString();
        }

        #endregion
    }
}