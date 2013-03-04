using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for event state values
    /// </summary>
	public enum EventState : uint
    {
        /// <summary>App wants status</summary>
        SCARD_STATE_UNAWARE = 0x00000000,
        /// <summary>Ignore this reader</summary>
        SCARD_STATE_IGNORE = 0x00000001,
        /// <summary>State has changed</summary>
        SCARD_STATE_CHANGED = 0x00000002,
        /// <summary>Unknown state</summary>
        SCARD_STATE_UNKNOWN = 0x00000004,
        /// <summary>Status unavailable</summary>
        SCARD_STATE_UNAVAILABLE = 0x00000008,
        /// <summary>Card removed</summary>
        SCARD_STATE_EMPTY = 0x00000010,
        /// <summary>There is a card in the reader, but it has not been moved into position for use.</summary>
        SCARD_STATE_PRESENT = 0x00000020,
        /// <summary>ATR matches card</summary>
        SCARD_STATE_ATRMATCH = 0x00000040,
        /// <summary>Exclusive Mode</summary>
        SCARD_STATE_EXCLUSIVE = 0x00000080,
        /// <summary>Shared Mode</summary>
        SCARD_STATE_INUSE = 0x00000100,
        /// <summary>Unresponsive card</summary>
        SCARD_STATE_MUTE = 0x00000200,
        /// <summary>Unpowered card</summary>
        SCARD_STATE_UNPOWERED = 0x00000400
    }
}
