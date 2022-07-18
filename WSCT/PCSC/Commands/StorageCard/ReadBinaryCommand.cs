using WSCT.ISO7816;

namespace WSCT.PCSC.Commands.StorageCard
{
    /// <summary>
    /// READ BINARY: read data from the card.
    /// FF B0 <address high> <address low> <length>
    /// </summary>
    /// <remarks>More information at http://pcscworkgroup.com/Download/Specifications/pcsc3_v2.01.09.pdf §3.2.2.1.8</remarks>
    public class ReadBinaryCommand : CommandAPDU
    {
        /// <summary>
        /// Creates a READ BINARY command from an address in range 0-255.
        /// </summary>
        public ReadBinaryCommand(byte address, int length) : this((ushort)address, length)
        {
        }

        /// <summary>
        /// Creates a READ BINARY command from an address in range 0-65535.
        /// </summary>
        public ReadBinaryCommand(ushort address, int length) : base(0xFF, 0xB0, 0x00, 0x00, (uint)length)
        {
            P1 = (byte)(address / 256);
            P2 = (byte)(address % 256);
        }
    }
}
