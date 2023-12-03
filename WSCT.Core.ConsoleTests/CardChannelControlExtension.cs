using System;
using System.Reflection;
using WSCT.Wrapper;
using WSCT.Wrapper.Desktop;

namespace WSCT.Core.ConsoleTests
{
    public static class CardChannelControlExtension
    {
        /// <summary>
        /// </summary>
        public static ErrorCode Control(this ICardChannel cardChannel, uint controlCode, byte[] command, ref byte[] response)
        {
            // windows: (0x31 << 16 | (code) << 2)
            // linux: #define SCARD_CTL_CODE(code) (0x42000000 + (code))
            var _card = cardChannel.FindCardHandle();

            var recvSize = Primitives.Api.AutoAllocate;
            response = new byte[1024];
            uint returnedSize = 0;

            var ret = Primitives.Api.SCardControl(
                _card,
                controlCode,
                command,
                (UInt32)command.Length,
                ref response,
                recvSize,
                ref returnedSize
                );
            return ret;
        }

        private static IntPtr FindCardHandle(this ICardChannel cardChannel)
        {
            // Search for ICardChannel => follow until none
            var fields = cardChannel.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldValue = fields[0].GetValue(cardChannel);

            // then search for IntPtr
            var fields2 = fieldValue.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldValue2 = fields2[0].GetValue(fieldValue);

            // or search in depth for IntPtr named "_channel"?

            return (IntPtr)fieldValue2;
        }
    }
}
