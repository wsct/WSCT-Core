namespace WSCT.ISO7816.Commands
{
    /// <summary>
    /// Wrapper for ISO/IEC 7816 APPEND RECORD C-APDU.
    /// </summary>
    public class AppendRecordCommand : CommandAPDU
    {
        #region >> Properties

        /// <summary>
        /// SFI (Short File Identifier).
        /// </summary>
        public byte Sfi
        {
            get { return (byte)(P2 >> 3); }
            set { P2 = (byte)(value << 3); }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AppendRecordCommand()
        {
            Ins = 0xE2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfi"></param>
        /// <param name="udc"></param>
        public AppendRecordCommand(byte sfi, byte[] udc)
            : this()
        {
            P1 = 0x00;
            Sfi = sfi;
            Udc = udc;
        }

        #endregion
    }
}