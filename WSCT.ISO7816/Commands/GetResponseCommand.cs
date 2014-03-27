namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// Wrapper for ISO/IEC 7816 GET RESPONSE C-APDU
    /// </summary>
    public class GetResponseCommand : CommandAPDU
    {
        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetResponseCommand()
        {
            Ins = 0xC0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="le"></param>
        public GetResponseCommand(uint le)
            : this()
        {
            P1 = 0x00;
            P2 = 0x00;
            Le = le;
        }

        #endregion
    }
}