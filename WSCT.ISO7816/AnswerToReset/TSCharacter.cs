namespace WSCT.ISO7816.AnswerToReset
{
    /// <summary>
    /// ISO/IEC 7816-3 TS character.
    /// </summary>
    public class TsCharacter
    {
        #region >> Properties

        /// <summary>
        /// Raw value.
        /// </summary>
        public byte Ts { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ts">Value of TS character</param>
        public TsCharacter(byte ts)
        {
            Ts = ts;
        }

        #endregion
    }
}