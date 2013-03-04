using System;


namespace WSCT.Helpers.BasicEncodingRules
{
    /// <summary>
    /// Base abstract class of all TLV data interpretation
    /// It allows to bind a <see cref="TLVData"/> object with the associated <see cref="TLVDescription"/> object describing it.
    /// </summary>
    public class AbstractTLVObject : IFormattable
    {
        #region >> Fields

        private TLVData _tlv;
        private TLVDescription _tagDescription;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Défault constructor
        /// </summary>
        public AbstractTLVObject()
        {
            _tlv = null;
            _tagDescription = null;
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// <see cref="TLVDescription"/> used with this object
        /// </summary>
        public TLVDescription tlvDescription
        {
            get { return _tagDescription; }
            set { _tagDescription = value; }
        }

        /// <summary>
        /// <c>TLVData</c> associated with the object
        /// </summary>
        public TLVData tlv
        {
            get { return _tlv; }
            set { _tlv = value; }
        }

        #endregion

        /// <summary>
        /// Returns a String that represents the nested <see cref="TLVData"/> using the nested <see cref="TLVDescription"/>
        /// </summary>
        /// <returns>A String that represents the <see cref="TLVData"/> Object.</returns>
        public override string ToString()
        {
            if (_tagDescription.value.format == "b")
                return tlv.value.toHexa();
            if (_tagDescription.value.format == "ans")
                return tlv.value.toString();
            return base.ToString();
        }

        /// <inheritdoc cref="ToString()" />
        /// <inheritdoc select="param|returns" />
        public String ToString(String format, IFormatProvider formatProvider)
        {
            if (format != "" && format != null)
            {
                if (format == "N")
                    return tlvDescription.longName;
                if (format == "n")
                    return tlvDescription.name;
                if (format == "H")
                    return tlv.value.toHexa();
                if (format == "s")
                    return tlv.value.toString();
                if (format == "tlv")
                    return tlv.ToString();
                return null;
            }
            else
                return ToString();
        }

    }
}
