using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for state values
    /// </summary>
    public enum State : uint
    {
        /// <summary>Unknown state</summary>
        SCARD_UNKNOWN = 1,
        /// <summary>There is no card in the reader</summary>
        SCARD_ABSENT = 1,
        /// <summary>There is a card in the reader, but it has not been moved into position for use.</summary>
        SCARD_PRESENT = 2,
        /// <summary>There is a card in the reader in position for use. The card is not powered.</summary>
        SCARD_SWALLOWED = 3,
        /// <summary>Power is being provided to the card, but the reader driver is unaware of the mode of the card.</summary>
        SCARD_POWERED = 4,
        /// <summary>The card has been reset and is awaiting PTS negotiation.</summary>
        SCARD_NEGOTIABLE = 5,
        /// <summary>The card has been reset and specific communication protocols have been established.</summary>
        SCARD_SPECIFIC = 6
    }
}
