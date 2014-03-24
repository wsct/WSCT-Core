using System;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for event state values.
    /// </summary>
    [Flags]
    public enum EventState : uint
    {
        /// <summary>
        /// App wants status.
        /// </summary>
        StateUnaware = 0x00000000,
        /// <summary>
        /// Ignore this reader.
        /// </summary>
        StateIgnore = 0x00000001,
        /// <summary>
        /// State has changed.
        /// </summary>
        StateChanged = 0x00000002,
        /// <summary>
        /// Unknown state.
        /// </summary>
        StateUnknown = 0x00000004,
        /// <summary>
        /// Status unavailable.
        /// </summary>
        Unavailable = 0x00000008,
        /// <summary>
        /// Card removed.
        /// </summary>
        StateEmpty = 0x00000010,
        /// <summary>
        /// There is a card in the reader, but it has not been moved into position for use.
        /// </summary>
        StatePresent = 0x00000020,
        /// <summary>
        /// ATR matches card.
        /// </summary>
        StateAtrmatch = 0x00000040,
        /// <summary>
        /// Exclusive Mode.
        /// </summary>
        StateExclusive = 0x00000080,
        /// <summary>
        /// Shared Mode.
        /// </summary>
        StateInuse = 0x00000100,
        /// <summary>
        /// Unresponsive card.
        /// </summary>
        StateMute = 0x00000200,
        /// <summary>
        /// Unpowered card.
        /// </summary>
        StateUnpowered = 0x00000400
    }
}
