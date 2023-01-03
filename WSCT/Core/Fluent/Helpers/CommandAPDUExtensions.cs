using WSCT.ISO7816;

namespace WSCT.Core.Fluent.Helpers
{
    public static class CommandAPDUExtensions
    {
        /// <summary>
        /// Transmit the <paramref name="command"/> and return the CRP wrapping it and the response.
        /// </summary>
        public static CommandResponsePair Transmit(this CommandAPDU command, ICardChannel channel)
        {
            var crp = new CommandResponsePair(command);

            crp.Transmit(channel);

            return crp;
        }
    }
}
