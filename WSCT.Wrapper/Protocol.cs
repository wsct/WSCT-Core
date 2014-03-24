namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for transport protocols.
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// Unknown protocol.
        /// </summary>
        Unset = 0x0000,
        /// <summary>
        /// The ISO 7816/3 T=0 protocol is in use.
        /// </summary>
        T0 = 0x0001,
        /// <summary>
        /// The ISO 7816/3 T=1 protocol is in use.
        /// </summary>
        T1 = 0x0002,
        /// <summary>
        /// The Raw Transfer protocol is in use.
        /// </summary>
        Raw = 0x0004,
        /// <summary>
        /// 
        /// </summary>
        T15 = 0x0008,
        /// <summary>
        /// T=0 or T=1 protocol.
        /// </summary>
        Any = (T0 | T1)
    }
}
