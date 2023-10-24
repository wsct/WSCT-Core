using System;
using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.Wrapper;

namespace WSCT.ISO7816
{
    /// <summary>
    /// Channel allowing APDU adaptation for T=0 smartcards.
    /// </summary>
    public class T0FriendlyCardChannel : ICardChannel
    {
        private readonly ICardChannel _cardChannel;

        #region >> Constructors

        /// <summary>
        /// Initialize a new instance.
        /// </summary>
        /// <param name="cardChannel"></param>
        public T0FriendlyCardChannel(ICardChannel cardChannel)
        {
            _cardChannel = cardChannel ?? throw new ArgumentNullException(nameof(cardChannel));
        }

        #endregion

        #region >> ICardChannel

        /// <inheritdoc />
        public Protocol Protocol =>
            _cardChannel.Protocol == Protocol.T0 ? Protocol.T1 : _cardChannel.Protocol;

        /// <inheritdoc />
        public string ReaderName => _cardChannel.ReaderName;

        /// <inheritdoc />
        public void Attach(ICardContext context, string readerName) =>
            _cardChannel.Attach(context, readerName);

        /// <inheritdoc />
        public ErrorCode Connect(ShareMode shareMode, Protocol preferedProtocol) =>
            _cardChannel.Connect(shareMode, preferedProtocol);

        /// <inheritdoc />
        public ErrorCode Disconnect(Disposition disposition) =>
            _cardChannel.Disconnect(disposition);

        /// <inheritdoc />
        public ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer) =>
            _cardChannel.GetAttrib(attrib, ref buffer);

        /// <inheritdoc />
        public State GetStatus() =>
            _cardChannel.GetStatus();

        /// <inheritdoc />
        public ErrorCode Reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization) =>
            _cardChannel.Reconnect(shareMode, preferedProtocol, initialization);

        /// <inheritdoc />
        public ErrorCode Transmit(ICardCommand command, ICardResponse response)
        {
            return _cardChannel.Protocol switch
            {
                Protocol.T0 => _cardChannel.T0Transmit(command as CommandAPDU, response as ResponseAPDU),
                _ => _cardChannel.Transmit(command, response)
            };
        }

        #endregion
    }
}
