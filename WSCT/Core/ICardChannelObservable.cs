using System;
using WSCT.Core.Events;

namespace WSCT.Core
{
    /// <summary>
    /// Interface to be implemented by all observable <see cref="ICardChannel"/> instance.
    /// </summary>
    public interface ICardChannelObservable : ICardChannel
    {
        #region >> Events

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.Connect"/>.
        /// </summary>
        event EventHandler<BeforeConnectEventArgs> BeforeConnectEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.Connect"/>.
        /// </summary>
        event EventHandler<AfterConnectEventArgs> AfterConnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.Disconnect"/>.
        /// </summary>
        event EventHandler<BeforeDisconnectEventArgs> BeforeDisconnectEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.Disconnect"/>.
        /// </summary>
        event EventHandler<AfterDisconnectEventArgs> AfterDisconnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.GetAttrib"/>.
        /// </summary>
        event EventHandler<BeforeGetAttribEventArgs> BeforeGetAttribEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.GetAttrib"/>.
        /// </summary>
        event EventHandler<AfterGetAttribEventArgs> AfterGetAttribEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.GetStatus"/>.
        /// </summary>
        event EventHandler<BeforeGetStatusEventArgs> BeforeGetStatusEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.GetStatus"/>.
        /// </summary>
        event EventHandler<AfterGetStatusEventArgs> AfterGetStatusEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.Reconnect"/>.
        /// </summary>
        event EventHandler<BeforeReconnectEventArgs> BeforeReconnectEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.Reconnect"/>.
        /// </summary>
        event EventHandler<AfterReconnectEventArgs> AfterReconnectEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardChannel.Transmit"/>.
        /// </summary>
        event EventHandler<BeforeTransmitEventArgs> BeforeTransmitEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardChannel.Transmit"/>.
        /// </summary>
        event EventHandler<AfterTransmitEventArgs> AfterTransmitEvent;

        #endregion
    }
}