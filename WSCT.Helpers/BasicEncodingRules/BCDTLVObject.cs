using System;
using System.Text;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Represents a TLV object where the value is BCD encoded
    /// </summary>
    public class BCDTLVObject : AbstractTLVObject
    {
        #region >> Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public BCDTLVObject()
            : base()
        {
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (Byte b in tlv.value)
                s.AppendFormat("{0}{1}", b / 16, b % 16);
            return s.ToString();
        }
    }
}
