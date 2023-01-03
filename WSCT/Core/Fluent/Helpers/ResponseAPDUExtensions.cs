using WSCT.ISO7816;

namespace WSCT.Core.Fluent.Helpers
{
    public static class ResponseAPDUExtensions
    {
        /// <summary>
        /// Throw an <see cref="UnexpectedStatusWordException"> when last SW is not 0x9000
        /// </summary>
        public static ResponseAPDU ThrowIfSWNot9000(this ResponseAPDU response)
        {
            if (response.StatusWord != 0x9000)
            {
                throw new UnexpectedStatusWordException(response.StatusWord);
            }

            return response;
        }
    }
}
