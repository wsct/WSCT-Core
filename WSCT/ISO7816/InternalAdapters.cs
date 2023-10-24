using WSCT.Core;
using WSCT.ISO7816.Commands;
using WSCT.Wrapper;

namespace WSCT.ISO7816
{
    internal static class InternalAdapters
    {
        /// <summary>
        /// Adapts APDU to make it technically transmissible using T=0.<br/>
        /// </summary>
        /// <remarks>
        ///   Status words <c>61xx</c> and <c>6Cxx</c> are handled for the following C-APDU:
        ///   <list type="bullet">
        ///     <item><term><c>CLA INS P1 P2 00</c></term><description></description></item>
        ///     <item><term><c>CLA INS P1 P2 Lc UDC Le</c></term><description></description></item>
        ///   </list>
        /// </remarks>
        /// <param name="cardChannel"></param>
        /// <param name="command"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static ErrorCode T0Transmit(this ICardChannel cardChannel, CommandAPDU command, ResponseAPDU response)
        {
            if (command.HasLe is false || (command.HasLc is false && command.Le != 0x00))
            {
                return cardChannel.Transmit(command, response);
            }

            var initialLe = command.Le;
            command.HasLe = false;

            var error = cardChannel.Transmit(command, response);

            if (error != ErrorCode.Success)
            {
                return error;
            }

            command.Le = initialLe;

            switch (response.Sw1)
            {
                case 0x61:
                {
                    var getResponseLe = initialLe == 0x00 ? response.Sw2 : initialLe;
                    var getResponse = new GetResponseCommand(getResponseLe);

                    return cardChannel.Transmit(getResponse, response);
                }
                case 0x6C:
                    command.Le = initialLe == 0x00 ? response.Sw2 : initialLe;

                    error = cardChannel.Transmit(command, response);

                    command.Le = initialLe;

                    return error;
                default:
                    return error;
            }
        }
    }
}
