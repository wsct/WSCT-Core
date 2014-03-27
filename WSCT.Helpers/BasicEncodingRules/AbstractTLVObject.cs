using System;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Base abstract class of all TLV data interpretation.
    /// It allows to bind a <see cref="TlvData"/> object with the associated <see cref="BasicEncodingRules.TlvDescription"/> object describing it.
    /// </summary>
    public abstract class AbstractTlvObject : IFormattable
    {
        #region >> Fields

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        protected AbstractTlvObject()
        {
            Tlv = null;
            TlvDescription = null;
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// <see cref="BasicEncodingRules.TlvDescription"/> used with this object.
        /// </summary>
        public TlvDescription TlvDescription { get; set; }

        /// <summary>
        /// <c>TLVData</c> associated with the object.
        /// </summary>
        public TlvData Tlv { get; set; }

        #endregion

        #region >> Object

        /// <inheritdoc cref="ToString()" />
        /// <inheritdoc select="param|returns" />
        public String ToString(String format, IFormatProvider formatProvider)
        {
            if (!string.IsNullOrEmpty(format))
            {
                if (format == "N")
                {
                    return TlvDescription.LongName;
                }
                if (format == "n")
                {
                    return TlvDescription.Name;
                }
                if (format == "H")
                {
                    return Tlv.Value.ToHexa();
                }
                if (format == "s")
                {
                    return Tlv.Value.ToAsciiString();
                }
                if (format == "tlv")
                {
                    return Tlv.ToString();
                }
                return string.Empty;
            }

            return ToString();
        }

        /// <summary>
        /// Returns a String that represents the nested <see cref="TlvData"/> using the nested <see cref="BasicEncodingRules.TlvDescription"/>.
        /// </summary>
        /// <returns>A String that represents the <see cref="TlvData"/> Object.</returns>
        public override string ToString()
        {
            if (TlvDescription.Value.Format == "b")
            {
                return Tlv.Value.ToHexa();
            }
            if (TlvDescription.Value.Format == "ans")
            {
                return Tlv.Value.ToAsciiString();
            }
            return base.ToString();
        }

        #endregion
    }
}