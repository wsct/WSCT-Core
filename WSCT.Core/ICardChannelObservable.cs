using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Wrapper;
using WSCT.Core.APDU;

namespace WSCT.Core
{

    #region >> Delegates

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.connect"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="shareMode"><inheritdoc cref="ICardChannel.connect"/></param>
    /// <param name="preferedProtocol"><inheritdoc cref="ICardChannel.connect"/></param>
    public delegate void beforeConnect(ICardChannel channel, ShareMode shareMode, Protocol preferedProtocol);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.connect"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="shareMode"><inheritdoc cref="ICardChannel.connect"/></param>
    /// <param name="preferedProtocol"><inheritdoc cref="ICardChannel.connect"/></param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterConnect(ICardChannel channel, ShareMode shareMode, Protocol preferedProtocol, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.disconnect"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="disposition"><inheritdoc cref="ICardChannel.disconnect"/></param>
    public delegate void beforeDisconnect(ICardChannel channel, Disposition disposition);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.disconnect"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="disposition"><inheritdoc cref="ICardChannel.disconnect"/></param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterDisconnect(ICardChannel channel, Disposition disposition, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.getAttrib"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="attrib"><inheritdoc cref="ICardChannel.getAttrib"/></param>
    /// <param name="buffer"><inheritdoc cref="ICardChannel.getAttrib"/></param>
    public delegate void beforeGetAttrib(ICardChannel channel, Attrib attrib, Byte[] buffer);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.getAttrib"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="attrib"><inheritdoc cref="ICardChannel.getAttrib"/></param>
    /// <param name="buffer"><inheritdoc cref="ICardChannel.getAttrib"/></param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterGetAttrib(ICardChannel channel, Attrib attrib, Byte[] buffer, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.getStatus"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    public delegate void beforeGetStatus(ICardChannel channel);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.getStatus"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="state">Return value of the caller</param>
    public delegate void afterGetStatus(ICardChannel channel, State state);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.reconnect"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="shareMode"><inheritdoc cref="ICardChannel.getAttrib"/></param>
    /// <param name="preferedProtocol"><inheritdoc cref="ICardChannel.reconnect"/></param>
    /// <param name="initialization"><inheritdoc cref="ICardChannel.reconnect"/></param>
    public delegate void beforeReconnect(ICardChannel channel, ShareMode shareMode, Protocol preferedProtocol, Disposition initialization);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.reconnect"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="shareMode"><inheritdoc cref="ICardChannel.getAttrib"/></param>
    /// <param name="preferedProtocol"><inheritdoc cref="ICardChannel.reconnect"/></param>
    /// <param name="initialization"><inheritdoc cref="ICardChannel.reconnect"/></param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterReconnect(ICardChannel channel, ShareMode shareMode, Protocol preferedProtocol, Disposition initialization, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardChannel.transmit"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="command"><inheritdoc cref="ICardChannel.transmit"/></param>
    /// <param name="response"><inheritdoc cref="ICardChannel.transmit"/></param>
    public delegate void beforeTransmit(ICardChannel channel, ICardCommand command, ICardResponse response);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardChannel.transmit"/>
    /// </summary>
    /// <param name="channel">Caller instance</param>
    /// <param name="command"><inheritdoc cref="ICardChannel.transmit"/></param>
    /// <param name="response"><inheritdoc cref="ICardChannel.transmit"/></param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterTransmit(ICardChannel channel, ICardCommand command, ICardResponse response, ErrorCode error);

    #endregion

    /// <summary>
    /// Interface to be implemented by all observable <see cref="ICardChannel"/> instance
    /// </summary>
    public interface ICardChannelObservable : ICardChannel
    {
        #region >> Events

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.connect"/>
        /// </summary>
        event beforeConnect beforeConnectEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.connect"/>
        /// </summary>
        event afterConnect afterConnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.disconnect"/>
        /// </summary>
        event beforeDisconnect beforeDisconnectEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.disconnect"/>
        /// </summary>
        event afterDisconnect afterDisconnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.getAttrib"/>
        /// </summary>
        event beforeGetAttrib beforeGetAttribEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.getAttrib"/>
        /// </summary>
        event afterGetAttrib afterGetAttribEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.getStatus"/>
        /// </summary>
        event beforeGetStatus beforeGetStatusEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.getStatus"/>
        /// </summary>
        event afterGetStatus afterGetStatusEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.reconnect"/>
        /// </summary>
        event beforeReconnect beforeReconnectEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.reconnect"/>
        /// </summary>
        event afterReconnect afterReconnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.transmit(ICardCommand, ICardResponse)"/>
        /// </summary>
        event beforeTransmit beforeTransmitEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.transmit(ICardCommand, ICardResponse)"/>
        /// </summary>
        event afterTransmit afterTransmitEvent;

        #endregion
    }
}
