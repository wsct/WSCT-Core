using System;
using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.Wrapper;

namespace WSCT.ISO7816
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("This class is a relic of the past and should be safely removed. Use the Protocol property instead of ProtocolT.")]
    public class CardChannelIso7816 : ICardChannel
    {
        protected readonly ICardChannel _cardChannel;

        #region >> Construtors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="cardChannel"></param>
        public CardChannelIso7816(ICardChannel cardChannel)
            : this(cardChannel, ProtocolT.T0)
        {
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="cardChannel"></param>
        /// <param name="protocolT"></param>
        public CardChannelIso7816(ICardChannel cardChannel, ProtocolT protocolT)
        {
            this._cardChannel = cardChannel;
            ProtocolT = protocolT;
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public ProtocolT ProtocolT { get; set; }

        #endregion

        #region >> ICardChannel Membres

        /// <inheritdoc/>
        public Protocol Protocol
        {
            get { return _cardChannel.Protocol; }
        }

        /// <inheritdoc/>
        public string ReaderName
        {
            get { return _cardChannel.ReaderName; }
        }

        /// <inheritdoc/>
        public void Attach(ICardContext context, string readerName)
        {
            _cardChannel.Attach(context, readerName);
        }

        /// <inheritdoc/>
        public ErrorCode Connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            return _cardChannel.Connect(shareMode, preferedProtocol);
        }

        /// <inheritdoc/>
        public ErrorCode Disconnect(Disposition disposition)
        {
            return _cardChannel.Disconnect(disposition);
        }

        /// <inheritdoc/>
        public ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer)
        {
            return _cardChannel.GetAttrib(attrib, ref buffer);
        }

        /// <inheritdoc/>
        public State GetStatus()
        {
            return _cardChannel.GetStatus();
        }

        /// <inheritdoc/>
        public ErrorCode Reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            return _cardChannel.Reconnect(shareMode, preferedProtocol, initialization);
        }

        /// <inheritdoc/>
        public ErrorCode Transmit(ICardCommand command, ICardResponse response)
        {
            return _cardChannel.Transmit(command, response);
        }

        #endregion
    }
}