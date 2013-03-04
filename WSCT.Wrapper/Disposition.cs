using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for disposition modes when calling <b>connect</b> or <b>reconnect</b>
    /// </summary>
    public enum Disposition
    {
        /// <summary>Do not do anything special on reconnect.</summary>
        SCARD_LEAVE_CARD = 0,
        /// <summary>Reset the card.</summary>
        /// <remarks>Used to do a warm reset with SCardReconnect</remarks>
        SCARD_RESET_CARD = 1,
        /// <summary>Power down the card.</summary>
        /// <remarks>Used to do a cold reset with SCardReconnect</remarks>
        SCARD_UNPOWER_CARD = 2,
        /// <summary>Eject</summary>
        SCARD_EJECT_CARD = 3
    }
}
