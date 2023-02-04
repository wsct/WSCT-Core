using WSCT.Core;
using WSCT.ISO7816.Commands;
using WSCT.Wrapper;

namespace WSCT.ISO7816
{
    internal static class InternalAdapters
    {
        /// <summary>
        /// Adapts APDU to make it functionaly transmissible using T=0.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static ErrorCode T0Transmit(this ICardChannel cardChannel, CommandAPDU command, ResponseAPDU response)
        {
            if (command.HasLe is false || (command.HasLe && (command.HasLc is false)))
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

            if (response.Sw1 == 0x61)
            {
                var getResponseLe = initialLe == 0x00 ? response.Sw2 : initialLe;
                var getResponse = new GetResponseCommand(getResponseLe);

                return cardChannel.Transmit(getResponse, response);
            }

            if (response.Sw1 == 0x6C && initialLe == 0x00)
            {
                command.Le = response.Sw2;

                error = cardChannel.Transmit(command, response);

                command.Le = initialLe;

                return error;
            }

            return error;
        }
    }
}
