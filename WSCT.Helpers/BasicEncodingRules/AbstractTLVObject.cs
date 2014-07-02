using System;

namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Base abstract class of all TLV data interpretation.
    /// It allows to bind a <see cref="TlvData"/> object with the associated <see cref="BasicEncodingRules.TlvDescription"/> object describing it.
    /// </summary>
    public abstract class AbstractTlvObject : IFormattable
    {
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
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
            {
                return ToString();
            }

            switch (format)
            {
                case "N":
                    return TlvDescription.LongName;
                case "n":
                    return TlvDescription.Name;
                case "H":
                    return Tlv.Value.ToHexa();
                case "s":
                    return Tlv.Value.ToAsciiString();
                case "tlv":
                    return Tlv.ToString();
                default:
                    return ToString();
            }
        }

        /// <summary>
        /// Returns a string that represents the nested <see cref="TlvData"/> using the nested <see cref="BasicEncodingRules.TlvDescription"/>.
        /// </summary>
        /// <returns>A string that represents the <see cref="TlvData"/> Object.</returns>
        public override string ToString()
        {
            switch (TlvDescription.Value.Format)
            {
                case "b":
                    return Tlv.Value.ToHexa();
                case "ans":
                    return Tlv.Value.ToAsciiString();
                default:
                    return base.ToString();
            }
        }

        #endregion
    }
}