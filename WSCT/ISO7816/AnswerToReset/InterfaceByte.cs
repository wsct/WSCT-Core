using System;

namespace WSCT.ISO7816.AnswerToReset
{
    /// <summary>
    /// ISO/IEC 7816 Interface byte of ATR
    /// </summary>
    public class InterfaceByte
    {
        #region >> Enumerations

        /// <summary>
        /// Identifiers of interface bytes.
        /// </summary>
        public enum IdType
        {
            /// <summary>
            /// Identifier for T0 byte.
            /// </summary>
            T0,

            /// <summary>
            /// Identifier for TA1 byte.
            /// </summary>
            Ta1,

            /// <summary>
            /// Identifier for TB1 byte.
            /// </summary>
            Tb1,

            /// <summary>
            /// Identifier for Tc1 byte.
            /// </summary>
            Tc1,

            /// <summary>
            /// Identifier for TD1 byte.
            /// </summary>
            Td1,

            /// <summary>
            /// Identifier for TA2 byte.
            /// </summary>
            Ta2,

            /// <summary>
            /// Identifier for TB2 byte.
            /// </summary>
            Tb2,

            /// <summary>
            /// Identifier for TC2 byte.
            /// </summary>
            Tc2,

            /// <summary>
            /// Identifier for TD2 byte.
            /// </summary>
            Td2,

            /// <summary>
            /// Identifier for TA3 byte.
            /// </summary>
            Ta3,

            /// <summary>
            /// Identifier for TB3 byte.
            /// </summary>
            Tb3,

            /// <summary>
            /// Identifier for TC3 byte.
            /// </summary>
            Tc3,

            /// <summary>
            /// Identifier for TD3 byte.
            /// </summary>
            Td3,

            /// <summary>
            /// Identifier for TA4 byte.
            /// </summary>
            Ta4,

            /// <summary>
            /// Identifier for TB4 byte.
            /// </summary>
            Tb4,

            /// <summary>
            /// Identifier for TC4 byte.
            /// </summary>
            Tc4,

            /// <summary>
            /// Identifier for TD4 byte.
            /// </summary>
            Td4,

            /// <summary>
            /// Identifier for TA5 byte.
            /// </summary>
            Ta5,

            /// <summary>
            /// Identifier for TB5 byte.
            /// </summary>
            Tb5,

            /// <summary>
            /// Identifier for TC5 byte.
            /// </summary>
            Tc5
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// Value of interface byte.
        /// </summary>
        public byte Value { get; set; }

        /// <summary>
        /// Identifier of the byte.
        /// </summary>
        public IdType Id { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="b"></param>
        public InterfaceByte(IdType id, byte b)
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