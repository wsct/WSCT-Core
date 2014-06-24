using System;

namespace WSCT.ISO7816.AnswerToReset
{
    /// <summary>
    /// ISO/IEC 7816 Interface byte of ATR
    /// </summary>
    public class InterfaceByte
    {
        #region >> Properties

        /// <summary>
        /// Value of interface byte.
        /// </summary>
        public byte Value { get; set; }

        /// <summary>
        /// Identifier of the byte.
        /// </summary>
        public InterfaceId Id { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="b"></param>
        public InterfaceByte(InterfaceId id, byte b)
        {
            Id = id;
            Value = b;
        }

        #endregion

        #region >> Object

        /// <inheritdoc />
        public override string ToString()
        {
            return String.Format("{0}:{1:X2}", Id, Value);
        }

        #endregion
    }
}