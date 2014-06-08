using System;
using WSCT.Core.APDU;
using WSCT.Core.Events;
using WSCT.Helpers.Events;
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
        protected ICardChannel channel;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="channel"><c>ICardChannel</c> instance to wrap.</param>
        public CardChannelObservable(ICardChannel channel)
        {
            this.channel = channel;
        }

        #endregion

        #region >> ICardChannel Membres

        /// <summary>
        /// Protocol T
        /// </summary>
        public Protocol Protocol
        {
            get { return channel.Protocol; }
        }

        /// <inheritdoc />
        public string ReaderName
        {
            get { return channel.ReaderName; }
        }

        /// <inheritdoc />
        public void Attach(ICardContext context, string readerName)
        {
            channel.Attach(context, readerName);
        }

        /// <inheritdoc />
        public ErrorCode Connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            BeforeConnectEvent.Raise(this, new BeforeConnectEventArgs { ShareMode = shareMode, PreferedProtocol = preferedProtocol });

            var ret = channel.Connect(shareMode, preferedProtocol);

            AfterConnectEvent.Raise(this, new AfterConnectEventArgs { ShareMode = shareMode, PreferedProtocol = preferedProtocol, ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Disconnect(Disposition disposition)
        {
            BeforeDisconnectEvent.Raise(this, new BeforeDisconnectEventArgs { Disposition = disposition });

            var ret = channel.Disconnect(disposition);

            AfterDisconnectEvent.Raise(this, new AfterDisconnectEventArgs { Disposition = disposition, ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer)
        {
            BeforeGetAttribEvent.Raise(this, new BeforeGetAttribEventArgs { Attrib = attrib, Buffer = buffer });

            var ret = channel.GetAttrib(attrib, ref buffer);

            AfterGetAttribEvent.Raise(this, new AfterGetAttribEventArgs { Attrib = attrib, Buffer = buffer, ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public State GetStatus()
        {
            BeforeGetStatusEvent.Raise(this, new BeforeGetStatusEventArgs());

            var state = channel.GetStatus();

            AfterGetStatusEvent.Raise(this, new AfterGetStatusEventArgs { State = state });

            return state;
        }

        /// <inheritdoc />
        public ErrorCode Reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            BeforeReconnectEvent.Raise(this, new BeforeReconnectEventArgs { ShareMode = shareMode, PreferedProtocol = preferedProtocol, Initialization = initialization });

            var ret = channel.Reconnect(shareMode, preferedProtocol, initialization);

            AfterReconnectEvent.Raise(this, new AfterReconnectEventArgs { ShareMode = shareMode, PreferedProtocol = preferedProtocol, Initialization = initialization, ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Transmit(ICardCommand command, ICardResponse response)
        {
            BeforeTransmitEvent.Raise(this, new BeforeTransmitEventArgs { Command = command, Response = response });

            var ret = channel.Transmit(command, response);

            AfterTransmitEvent.Raise(this, new AfterTransmitEventArgs { Command = command, Response = response, ReturnValue = ret });

            return ret;
        }

        #endregion

        #region >> ICardChannelObservable Membres

        /// <inheritdoc />
        public event EventHandler<BeforeConnectEventArgs> BeforeConnectEvent;

        /// <inheritdoc />
        public event EventHandler<AfterConnectEventArgs> AfterConnectEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeDisconnectEventArgs> BeforeDisconnectEvent;

        /// <inheritdoc />
        public event EventHandler<AfterDisconnectEventArgs> AfterDisconnectEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeGetAttribEventArgs> BeforeGetAttribEvent;

        /// <inheritdoc />
        public event EventHandler<AfterGetAttribEventArgs> AfterGetAttribEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeGetStatusEventArgs> BeforeGetStatusEvent;

        /// <inheritdoc />
        public event EventHandler<AfterGetStatusEventArgs> AfterGetStatusEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeReconnectEventArgs> BeforeReconnectEvent;

        /// <inheritdoc />
        public event EventHandler<AfterReconnectEventArgs> AfterReconnectEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeTransmitEventArgs> BeforeTransmitEvent;

        /// <inheritdoc />
        public event EventHandler<AfterTransmitEventArgs> AfterTransmitEvent;

        #endregion
    }
}