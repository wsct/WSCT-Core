using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.ISO7816.AnswerToReset
{
    /// <summary>
    /// ISO/IEC 7816 Interface byte of ATR
    /// </summary>
    public class InterfaceByte
    {
        #region >> Enumerations

        /// <summary>
        /// Identifiers of interface bytes
        /// </summary>
        public enum Id
        {
            /// <summary>
            /// Identifier for T0 byte
            /// </summary>
            T0,
            /// <summary>
            /// Identifier for TA1 byte
            /// </summary>
            TA1,
            /// <summary>
            /// Identifier for TB1 byte
            /// </summary>
            TB1,
            /// <summary>
            /// Identifier for TC1 byte
            /// </summary>
            TC1,
            /// <summary>
            /// Identifier for TD1 byte
            /// </summary>
            TD1,
            /// <summary>
            /// Identifier for TA2 byte
            /// </summary>
            TA2,
            /// <summary>
            /// Identifier for TB2 byte
            /// </summary>
            TB2,
            /// <summary>
            /// Identifier for TC2 byte
            /// </summary>
            TC2,
            /// <summary>
            /// Identifier for TD2 byte
            /// </summary>
            TD2,
            /// <summary>
            /// Identifier for TA3 byte
            /// </summary>
            TA3,
            /// <summary>
            /// Identifier for TB3 byte
            /// </summary>
            TB3,
            /// <summary>
            /// Identifier for TC3 byte
            /// </summary>
            TC3,
            /// <summary>
            /// Identifier for TD3 byte
            /// </summary>
            TD3,
            /// <summary>
            /// Identifier for TA4 byte
            /// </summary>
            TA4,
            /// <summary>
            /// Identifier for TB4 byte
            /// </summary>
            TB4,
            /// <summary>
            /// Identifier for TC4 byte
            /// </summary>
            TC4,
            /// <summary>
            /// Identifier for TD4 byte
            /// </summary>
            TD4,
            /// <summary>
            /// Identifier for TA5 byte
            /// </summary>
            TA5,
            /// <summary>
            /// Identifier for TB5 byte
            /// </summary>
            TB5,
            /// <summary>
            /// Identifier for TC5 byte
            /// </summary>
            TC5
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// Value of interface byte
        /// </summary>
        public Byte value
        { get; set; }

        /// <summary>
        /// Identifier of the byte
        /// </summary>
        public Id id
        { get; set; }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Accessor to a named interface byte
        /// </summary>
        /// <param name="id"></param>
        /// <param name="b"></param>
        public InterfaceByte(Id id, Byte b)
        {
            this.id = id;
            this.value = b;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return String.Format("{0}:{1:X2}", id, value);
        }
    }
}
