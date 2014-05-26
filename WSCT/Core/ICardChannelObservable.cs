using WSCT.Core.APDU;
using WSCT.Wrapper;

namespace WSCT.Core
{

    #region >> Delegates

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.Connect"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="shareMode"><inheritdoc cref="ICardChannel.Connect"/>.</param>
    /// <param name="preferedProtocol"><inheritdoc cref="ICardChannel.Connect"/>.</param>
    public delegate void BeforeConnect(ICardChannel channel, ShareMode shareMode, Protocol preferedProtocol);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.Connect"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="shareMode"><inheritdoc cref="ICardChannel.Connect"/>.</param>
    /// <param name="preferedProtocol"><inheritdoc cref="ICardChannel.Connect"/>.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterConnect(ICardChannel channel, ShareMode shareMode, Protocol preferedProtocol, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.Disconnect"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="disposition"><inheritdoc cref="ICardChannel.Disconnect"/>.</param>
    public delegate void BeforeDisconnect(ICardChannel channel, Disposition disposition);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.Disconnect"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="disposition"><inheritdoc cref="ICardChannel.Disconnect"/>.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterDisconnect(ICardChannel channel, Disposition disposition, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.GetAttrib"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="attrib"><inheritdoc cref="ICardChannel.GetAttrib"/>.</param>
    /// <param name="buffer"><inheritdoc cref="ICardChannel.GetAttrib"/>.</param>
    public delegate void BeforeGetAttrib(ICardChannel channel, Attrib attrib, byte[] buffer);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.GetAttrib"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="attrib"><inheritdoc cref="ICardChannel.GetAttrib"/>.</param>
    /// <param name="buffer"><inheritdoc cref="ICardChannel.GetAttrib"/>.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterGetAttrib(ICardChannel channel, Attrib attrib, byte[] buffer, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.GetStatus"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    public delegate void BeforeGetStatus(ICardChannel channel);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.GetStatus"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="state">Return value of the caller.</param>
    public delegate void AfterGetStatus(ICardChannel channel, State state);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.Reconnect"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="shareMode"><inheritdoc cref="ICardChannel.GetAttrib"/>.</param>
    /// <param name="preferedProtocol"><inheritdoc cref="ICardChannel.Reconnect"/>.</param>
    /// <param name="initialization"><inheritdoc cref="ICardChannel.Reconnect"/>.</param>
    public delegate void BeforeReconnect(ICardChannel channel, ShareMode shareMode, Protocol preferedProtocol, Disposition initialization);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.Reconnect"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="shareMode"><inheritdoc cref="ICardChannel.GetAttrib"/>.</param>
    /// <param name="preferedProtocol"><inheritdoc cref="ICardChannel.Reconnect"/>.</param>
    /// <param name="initialization"><inheritdoc cref="ICardChannel.Reconnect"/>.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterReconnect(ICardChannel channel, ShareMode shareMode, Protocol preferedProtocol, Disposition initialization, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.Transmit"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="command"><inheritdoc cref="ICardChannel.Transmit"/>.</param>
    /// <param name="response"><inheritdoc cref="ICardChannel.Transmit"/>.</param>
    public delegate void BeforeTransmit(ICardChannel channel, ICardCommand command, ICardResponse response);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.Transmit"/>.
    /// </summary>
    /// <param name="channel">Caller instance.</param>
    /// <param name="command"><inheritdoc cref="ICardChannel.Transmit"/>.</param>
    /// <param name="response"><inheritdoc cref="ICardChannel.Transmit"/>.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterTransmit(ICardChannel channel, ICardCommand command, ICardResponse response, ErrorCode error);

    #endregion

    /// <summary>
    /// Interface to be implemented by all observable <see cref="ICardChannel"/> instance.
    /// </summary>
    public interface ICardChannelObservable : ICardChannel
    {
        #region >> Events

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.Connect"/>.
        /// </summary>
        event BeforeConnect BeforeConnectEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.Connect"/>.
        /// </summary>
        event AfterConnect AfterConnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.Disconnect"/>.
        /// </summary>
        event BeforeDisconnect BeforeDisconnectEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.Disconnect"/>.
        /// </summary>
        event AfterDisconnect AfterDisconnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.GetAttrib"/>.
        /// </summary>
        event BeforeGetAttrib BeforeGetAttribEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.GetAttrib"/>.
        /// </summary>
        event AfterGetAttrib AfterGetAttribEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.GetStatus"/>.
        /// </summary>
        event BeforeGetStatus BeforeGetStatusEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.GetStatus"/>.
        /// </summary>
        event AfterGetStatus AfterGetStatusEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.Reconnect"/>.
        /// </summary>
        event BeforeReconnect BeforeReconnectEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.Reconnect"/>.
        /// </summary>
        event AfterReconnect AfterReconnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.Transmit"/>.
        /// </summary>
        event BeforeTransmit BeforeTransmitEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.Transmit"/>.
        /// </summary>
        event AfterTransmit AfterTransmitEvent;

        #endregion
    }
}