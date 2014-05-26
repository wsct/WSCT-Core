using WSCT.Core.APDU;
using WSCT.Wrapper;

namespace WSCT.Core
{
    /// <summary>
    /// Allows an existing <see cref="ICardChannel"/> instance to be observed by using delegates and wrapping it.
    /// </summary>
    public class CardChannelObservable : ICardChannelObservable
    {
        #region >> Fields

        /// <summary>
        /// Wrapped <see cref="ICardChannel"/> instance.
        /// </summary>
        protected ICardChannel _cardChannel;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="channel"><c>ICardChannel</c> instance to wrap.</param>
        public CardChannelObservable(ICardChannel channel)
        {
            _cardChannel = channel;
        }

        #endregion

        #region >> ICardChannel Membres

        /// <summary>
        /// Protocol T
        /// </summary>
        public Protocol Protocol
        {
            get { return _cardChannel.Protocol; }
        }

        /// <inheritdoc />
        public string ReaderName
        {
            get { return _cardChannel.ReaderName; }
        }

        /// <inheritdoc />
        public void Attach(ICardContext context, string readerName)
        {
            _cardChannel.Attach(context, readerName);
        }

        /// <inheritdoc />
        public ErrorCode Connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            if (BeforeConnectEvent != null)
            {
                BeforeConnectEvent(this, shareMode, preferedProtocol);
            }

            var ret = _cardChannel.Connect(shareMode, preferedProtocol);

            if (AfterConnectEvent != null)
            {
                AfterConnectEvent(this, shareMode, preferedProtocol, ret);
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Disconnect(Disposition disposition)
        {
            if (BeforeDisconnectEvent != null)
            {
                BeforeDisconnectEvent(this, disposition);
            }

            var ret = _cardChannel.Disconnect(disposition);

            if (AfterDisconnectEvent != null)
            {
                AfterDisconnectEvent(this, disposition, ret);
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer)
        {
            if (BeforeGetAttribEvent != null)
            {
                BeforeGetAttribEvent(this, attrib, buffer);
            }

            var ret = _cardChannel.GetAttrib(attrib, ref buffer);

            if (AfterGetAttribEvent != null)
            {
                AfterGetAttribEvent(this, attrib, buffer, ret);
            }

            return ret;
        }

        /// <inheritdoc />
        public State GetStatus()
        {
            if (BeforeGetStatusEvent != null)
            {
                BeforeGetStatusEvent(this);
            }

            var ret = _cardChannel.GetStatus();

            if (AfterGetStatusEvent != null)
            {
                AfterGetStatusEvent(this, ret);
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            if (BeforeReconnectEvent != null)
            {
                BeforeReconnectEvent(this, shareMode, preferedProtocol, initialization);
            }

            var ret = _cardChannel.Reconnect(shareMode, preferedProtocol, initialization);

            if (AfterReconnectEvent != null)
            {
                AfterReconnectEvent(this, shareMode, preferedProtocol, initialization, ret);
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Transmit(ICardCommand command, ICardResponse response)
        {
            if (BeforeTransmitEvent != null)
            {
                BeforeTransmitEvent(this, command, response);
            }

            var ret = _cardChannel.Transmit(command, response);

            if (AfterTransmitEvent != null)
            {
                AfterTransmitEvent(this, command, response, ret);
            }

            return ret;
        }

        #endregion

        #region >> ICardChannelObservable Membres

        /// <inheritdoc />
        public event BeforeConnect BeforeConnectEvent;

        /// <inheritdoc />
        public event AfterConnect AfterConnectEvent;

        /// <inheritdoc />
        public event BeforeDisconnect BeforeDisconnectEvent;

        /// <inheritdoc />
        public event AfterDisconnect AfterDisconnectEvent;

        /// <inheritdoc />
        public event BeforeGetAttrib BeforeGetAttribEvent;

        /// <inheritdoc />
        public event AfterGetAttrib AfterGetAttribEvent;

        /// <inheritdoc />
        public event BeforeGetStatus BeforeGetStatusEvent;

        /// <inheritdoc />
        public event AfterGetStatus AfterGetStatusEvent;

        /// <inheritdoc />
        public event BeforeReconnect BeforeReconnectEvent;

        /// <inheritdoc />
        public event AfterReconnect AfterReconnectEvent;

        /// <inheritdoc />
        public event BeforeTransmit BeforeTransmitEvent;

        /// <inheritdoc />
        public event AfterTransmit AfterTransmitEvent;

        #endregion
    }
}