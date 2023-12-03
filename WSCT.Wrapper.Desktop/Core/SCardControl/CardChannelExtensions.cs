using System;
using System.Linq;
using System.Reflection;
using WSCT.Core;
using WSCT.Core.Events;

namespace WSCT.Wrapper.Desktop.Core.SCardControl
{
    public static class CardChannelExtensions
    {
        /// <summary>
        /// Supports direct communication with the reader device.
        /// </summary>
        /// <param name="cardChannel">Current <see cref="ICardChannel"/> instance to send the command to.</param>
        /// <param name="controlCode">Vendor-defined control code.</param>
        /// <param name="command">Input data buffer.</param>
        /// <param name="response">Output data buffer (auto instantiated).</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        public static ErrorCode Control(this ICardChannel cardChannel, uint controlCode, byte[] command, out byte[] response)
        {
            var card = cardChannel.FindHandle();

            var recvSize = Primitives.Api.AutoAllocate;
            response = Array.Empty<byte>();
            uint returnedSize = default;

            var ret = Primitives.Api.SCardControl(
                card,
                controlCode,
                command,
                (uint)command.Length,
                ref response,
                recvSize,
                ref returnedSize
                );

            return ret;
        }

        /// <summary>
        /// "Observable" version of <see cref="Control(ICardChannel,uint,byte[],ref byte[])"/>
        /// </summary>
        /// <param name="cardChannel"></param>
        /// <param name="controlCode"></param>
        /// <param name="command"></param>
        /// <param name="response"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public static ErrorCode Control(this ICardChannel cardChannel, uint controlCode, byte[] command, out byte[] response, EventHandler<BeforeControlEventArgs> before, EventHandler<AfterControlEventArgs> after)
        {
            before.Invoke(cardChannel, new BeforeControlEventArgs { ControlCode = controlCode, Command = command });

            var ret = Control(cardChannel, controlCode, command, out response);

            after.Invoke(cardChannel, new AfterControlEventArgs { ControlCode = controlCode, Command = command, Response = response, ReturnValue = ret });

            return ret;
        }

        /// <summary>
        /// Explore the class hierarchy of the instance to get the most probable raw handle.<br/>
        /// WARNING: it may not give a valid result in secure/restricted execution environments (IIS for example).
        /// </summary>
        /// <remarks>
        /// This trick is needed because Control is not part of <see cref="ICardChannel"/> yet.
        /// </remarks>
        /// <param name="cardChannel"></param>
        /// <returns></returns>
        internal static IntPtr FindHandle(this ICardChannel cardChannel)
        {
            return FindCardHandleByFirstIntPtr(cardChannel);
        }

        private static readonly string[] FieldNames = { "_card", "card" };

        private static IntPtr FindCardHandleByFirstIntPtr(this ICardChannel cardChannel)
        {
            var currentCardChannel = cardChannel;
            while (true)
            {
                var fields = currentCardChannel.GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                // Search for an IntPtr candidate field
                var intPtrField = fields.FirstOrDefault(f => f.FieldType == typeof(IntPtr) && FieldNames.Any(n => n == f.Name));

                if (intPtrField != default)
                {
                    return (IntPtr)intPtrField.GetValue(currentCardChannel);
                }

                // Search for an ICardChannel candidate field
                var channelField = fields.FirstOrDefault(f => f.FieldType == typeof(ICardChannel));

                if (channelField == default)
                {
                    throw new Exception($"{nameof(FindHandle)}: No card handle was found.");
                }

                currentCardChannel = (ICardChannel)channelField.GetValue(currentCardChannel);
            }
        }
    }
}
