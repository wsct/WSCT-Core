namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// Wrapper for ISO/IEC 7816 GET DATA C-APDU
    /// </summary>
    public class GetDataCommand : CommandAPDU
    {
        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public uint Tag
        {
            get { return P1 * 0x100u + P2; }
            set
            {
                P1 = (byte)(value / 0x100);
                P2 = (byte)(value % 0x100);
            }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public GetDataCommand()
        {
            Ins = 0xCA;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="le"></param>
        public GetDataCommand(uint tag, uint le)
            : this()
        {
            Tag = tag;
            Le = le;
        }

        #endregion
    }
}