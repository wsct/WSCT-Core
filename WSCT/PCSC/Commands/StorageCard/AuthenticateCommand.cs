using WSCT.ISO7816;

namespace WSCT.PCSC.Commands.StorageCard
{
    /// <summary>
    /// GENERAL AUTHENTICATE: Perform the authentication between IFD and card.
    /// FF 86 00 00 05 01 00 <blockAddress> <keyType> <keyNumber>
    /// </summary>
    /// <remarks>More information at http://pcscworkgroup.com/Download/Specifications/pcsc3_v2.01.09.pdf §3.2.2.1.6</remarks>
    public class AuthenticateCommand : CommandAPDU
    {
        #region >> Enumerations

        /// <summary>
        /// Key types usable for authentication.
        /// </summary>
        public enum KeyType : byte
        {
            KeyA = 0x60,
            KeyB = 0x61
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Creates an AUTHENTICATE command from an address in range 0-255.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="keyType"></param>
        /// <param name="keyNumber"></param>
        public AuthenticateCommand(byte address, KeyType keyType, byte keyNumber) : this((ushort)address, keyType, keyNumber)
        {
        }

        /// <summary>
        /// Creates an AUTHENTICATE command from an address in range 0-65535.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="keyType"></param>
        /// <param name="keyNumber"></param>
        public AuthenticateCommand(ushort address, KeyType keyType, byte keyNumber) : base("FF 86 00 00")
        {
            Udc = new byte[] {
                0x01,
                (byte)(address / 256),
                (byte)(address % 256),
                (byte)keyType,
                keyNumber,
            };
        }

        #endregion
    }
}
