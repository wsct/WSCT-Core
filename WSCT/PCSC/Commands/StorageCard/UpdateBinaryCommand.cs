using WSCT.ISO7816;

namespace WSCT.PCSC.Commands.StorageCard
{
    /// <summary>
    /// UPDATE BINARY: write data to the card.
    /// FF D6 <address high> <address low> <data.Length> <data>
    /// </summary>
    /// <remarks>More information at http://pcscworkgroup.com/Download/Specifications/pcsc3_v2.01.09.pdf §3.2.2.1.9</remarks>
    public class UpdateBinaryCommand : CommandAPDU
    {
        /// <summary>
        /// Creates an UPDATE BINARY command from an address in range 0-255.
        /// </summary>
        public UpdateBinaryCommand(byte address, byte[] data) : base(0xFF, 0xD6, 0x00, 0x00)
        {
            P2 = address;
            Udc = data;
        }

        /// <summary>
        /// Creates an UPDATE BINARY command from an address in range 0-65535.
        /// </summary>
        public UpdateBinaryCommand(ushort address, byte[] data) : base(0xFF, 0xD6, 0x00, 0x00)
        {
            P1 = (byte)(address / 256);
            P2 = (byte)(address % 256);
            Udc = data;
        }
    }
}
