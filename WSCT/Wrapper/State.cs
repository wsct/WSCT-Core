namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for state values.
    /// </summary>
    public enum State : uint
    {
        /// <summary>
        /// Unknown state.
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// There is no card in the reader.
        /// </summary>
        Absent = 1,

        /// <summary>
        /// There is a card in the reader, but it has not been moved into position for use.
        /// </summary>
        Present = 2,

        /// <summary>
        /// There is a card in the reader in position for use. The card is not powered.
        /// </summary>
        Swallowed = 3,

        /// <summary>
        /// Power is being provided to the card, but the reader driver is unaware of the mode of the card.
        /// </summary>
        Powered = 4,

        /// <summary>
        /// The card has been reset and is awaiting PTS negotiation.
        /// </summary>
        Negotiable = 5,

        /// <summary>
        /// The card has been reset and specific communication protocols have been established.
        /// </summary>
        Specific = 6
    }
}