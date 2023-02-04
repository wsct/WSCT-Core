using System.Runtime.CompilerServices;
using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.Stack;
using WSCT.Wrapper;

namespace WSCT.ISO7816
{
    /// <summary>
    /// Layer allowing APDU adaptation for T=0 smartcards.
    /// </summary>
    public class T0FriendlyCardChannelLayer : ICardChannelLayer
    {
        private ICardChannelStack _stack;

        #region >> Constructors

        public T0FriendlyCardChannelLayer()
        {
        }

        #endregion

        #region >> ICardChannel

        /// <inheritdoc />
        public Protocol Protocol =>
            GetNextLayer().Protocol;

        /// <inheritdoc />
        public string ReaderName =>
            GetNextLayer().ReaderName;

        /// <inheritdoc />
        public void Attach(ICardContext context, string readerName) =>
            GetNextLayer().Attach(context, readerName);

        /// <inheritdoc />
        public ErrorCode Connect(ShareMode shareMode, Protocol preferredProtocol) =>
            GetNextLayer().Connect(shareMode, preferredProtocol);

        /// <inheritdoc />
        public ErrorCode Disconnect(Disposition disposition) =>
            GetNextLayer().Disconnect(disposition);

        /// <inheritdoc />
        public ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer) =>
            GetNextLayer().GetAttrib(attrib, ref buffer);

        /// <inheritdoc />
        public State GetStatus() =>
            GetNextLayer().GetStatus();

        /// <inheritdoc />
        public ErrorCode Reconnect(ShareMode shareMode, Protocol preferredProtocol, Disposition initialization) =>
            GetNextLayer().Reconnect(shareMode, preferredProtocol, initialization);

        /// <inheritdoc />
        public ErrorCode Transmit(ICardCommand command, ICardResponse response) =>
            Protocol switch
            {
                Protocol.T0 => GetNextLayer().T0Transmit(command as CommandAPDU, response as ResponseAPDU),
                _ => GetNextLayer().Transmit(command, response)
            };

        #endregion

        #region >> ICardChannelLayer

        /// <inheritdoc />
        public string LayerId => "T0 Adapter";

        /// <inheritdoc />
        public void SetStack(ICardChannelStack stack)
        {
            _stack = stack;
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ICardChannelLayer GetNextLayer() =>
            _stack.RequestLayer(this, SearchMode.Next);
    }
}
