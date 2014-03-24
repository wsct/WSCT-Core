namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for disposition modes when calling <b>Connect</b> or <b>Reconnect</b>.
    /// </summary>
    public enum Disposition
    {
        /// <summary>
        /// Do not do anything special on reconnect.
        /// </summary>
        LeaveCard = 0,
        /// <summary>
        /// Reset the card.
        /// </summary>
        /// <remarks>
        /// Used to do a warm reset with SCardReconnect.
        /// </remarks>
        ResetCard = 1,
        /// <summary>
        /// Power down the card.
        /// </summary>
        /// <remarks>
        /// Used to do a cold reset with SCardReconnect.
        /// </remarks>
        UnpowerCard = 2,
        /// <summary>
        /// Eject.
        /// </summary>
        EjectCard = 3
    }
}
