namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for the different share modes of readers.
    /// </summary>
    public enum ShareMode
    {
        /// <summary>
        /// Exclusive use.
        /// </summary>
        Exclusive = 0x0001,

        /// <summary>
        /// Shared use.
        /// </summary>
        Shared = 0x0002,

        /// <summary>
        /// Direct use.
        /// </summary>
        Direct = 0x0004
    }
}