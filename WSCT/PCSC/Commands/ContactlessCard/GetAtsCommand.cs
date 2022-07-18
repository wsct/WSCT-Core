using WSCT.ISO7816;

namespace WSCT.PCSC.Commands.ContactlessCard
{
    /// <summary>
    /// GET DATA command to retrieve the card ATS.<br/>
    /// FF CA 01 00 00
    /// </summary>
    /// <remarks>More information at http://pcscworkgroup.com/Download/Specifications/pcsc3_v2.01.09.pdf §3.2.2.1.3</remarks>
    public class GetAtsCommand : CommandAPDU
    {
        public GetAtsCommand() : base(0xFF, 0xCA, 0x01, 0x00, 0x00)
        {
        }
    }
}
