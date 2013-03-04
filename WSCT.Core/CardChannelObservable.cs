using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Wrapper;
using WSCT.Core.APDU;

namespace WSCT.Core
{
    /// <summary>
    /// Allows an existing <see cref="ICardChannel"/> instance to be observed by using delegates and wrapping it
    /// </summary>
    public class CardChannelObservable : ICardChannel, ICardChannelObservable
    {
        #region >> Fields

        /// <summary>
        /// Wrapped <see cref="ICardChannel"/> instance.
        /// </summary>
        protected ICardChannel _cardChannel;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel"><c>ICardChannel</c> instance to wrap</param>
        public CardChannelObservable(ICardChannel channel)
        {
            _cardChannel = channel;
        }

        #endregion

        #region >> ICardChannel Membres

        /// <summary>
        /// Protocol T
        /// </summary>
        public Protocol protocol
        {
            get { return _cardChannel.protocol; }
        }

        /// <inheritdoc />
        public String readerName
        {
            get { return _cardChannel.readerName; }
        }

        /// <inheritdoc />
        public void attach(ICardContext context, string readerName)
        {
            _cardChannel.attach(context, readerName);
        }

        /// <inheritdoc />
        public ErrorCode connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            if (beforeConnectEvent != null) beforeConnectEvent(this, shareMode, preferedProtocol);

            ErrorCode ret = _cardChannel.connect(shareMode, preferedProtocol);

            if (afterConnectEvent != null) afterConnectEvent(this, shareMode, preferedProtocol, ret);

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode disconnect(Disposition disposition)
        {
            if (beforeDisconnectEvent != null) beforeDisconnectEvent(this, disposition);

            ErrorCode ret = _cardChannel.disconnect(disposition);

            if (afterDisconnectEvent != null) afterDisconnectEvent(this, disposition, ret);

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode getAttrib(Attrib attrib, ref Byte[] buffer)
        {
            if (beforeGetAttribEvent != null) beforeGetAttribEvent(this, attrib, buffer);

            ErrorCode ret = _cardChannel.getAttrib(attrib, ref buffer);

            if (afterGetAttribEvent != null) afterGetAttribEvent(this, attrib, buffer, ret);

            return ret;
        }

        /// <inheritdoc />
        public State getStatus()
        {
            if (beforeGetStatusEvent != null) beforeGetStatusEvent(this);

            State ret = _cardChannel.getStatus();

            if (afterGetStatusEvent != null) afterGetStatusEvent(this, ret);

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            if (beforeReconnectEvent != null) beforeReconnectEvent(this, shareMode, preferedProtocol, initialization);

            ErrorCode ret = _cardChannel.reconnect(shareMode, preferedProtocol, initialization);

            if (afterReconnectEvent != null) afterReconnectEvent(this, shareMode, preferedProtocol, initialization, ret);

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode transmit(APDU.ICardCommand command, APDU.ICardResponse response)
        {
            if (beforeTransmitEvent != null) beforeTransmitEvent(this, command, response);

            ErrorCode ret = _cardChannel.transmit(command, response);

            if (afterTransmitEvent != null) afterTransmitEvent(this, command, response, ret);

            return ret;
        }

        #endregion

        #region >> ICardChannelObservable Membres

        /// <inheritdoc />
        public event beforeConnect beforeConnectEvent;
        /// <inheritdoc />
        public event afterConnect afterConnectEvent;

        /// <inheritdoc />
        public event beforeDisconnect beforeDisconnectEvent;
        /// <inheritdoc />
        public event afterDisconnect afterDisconnectEvent;

        /// <inheritdoc />
        public event beforeGetAttrib beforeGetAttribEvent;
        /// <inheritdoc />
        public event afterGetAttrib afterGetAttribEvent;

        /// <inheritdoc />
        public event beforeGetStatus beforeGetStatusEvent;
        /// <inheritdoc />
        public event afterGetStatus afterGetStatusEvent;

        /// <inheritdoc />
        public event beforeReconnect beforeReconnectEvent;
        /// <inheritdoc />
        public event afterReconnect afterReconnectEvent;

        /// <inheritdoc />
        public event beforeTransmit beforeTransmitEvent;
        /// <inheritdoc />
        public event afterTransmit afterTransmitEvent;

        #endregion
    }
}
