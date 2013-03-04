using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.ISO7816.AnswerToReset
{
    /// <summary>
    /// ISO/IEC 7816 TS character
    /// </summary>
    public class TSCharacter
    {
        #region >> Properties

        /// <summary>
        /// Raw value
        /// </summary>
        public byte ts;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ts">Value of TS character</param>
        public TSCharacter(Byte ts)
        {
            this.ts = ts;
        }

        #endregion
    }
}
