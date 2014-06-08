using System;
using WSCT.Core.Events;

namespace WSCT.Core
{
    /// <summary>
    /// Interface to be implemented by all observable <see cref="ICardContext"/> instance.
    /// </summary>
    public interface ICardContextObservable : ICardContext
    {
        #region >> Events

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.Cancel"/>.
        /// </summary>
        event EventHandler<BeforeCancelEventArgs> BeforeCancelEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.Cancel"/>.
        /// </summary>
        event EventHandler<AfterCancelEventArgs> AfterCancelEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.Establish"/>.
        /// </summary>
        event EventHandler<BeforeEstablishEventArgs> BeforeEstablishEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.Establish"/>.
        /// </summary>
        event EventHandler<AfterEstablishEventArgs> AfterEstablishEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.GetStatusChange"/>.
        /// </summary>
        event EventHandler<BeforeGetStatusChangeEventArgs> BeforeGetStatusChangeEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.GetStatusChange"/>.
        /// </summary>
        event EventHandler<AfterGetStatusChangeEventArgs> AfterGetStatusChangeEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.IsValid"/>.
        /// </summary>
        event EventHandler<BeforeIsValidEventArgs> BeforeIsValidEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.IsValid"/>.
        /// </summary>
        event EventHandler<AfterIsValidEventArgs> AfterIsValidEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.ListReaderGroups"/>.
        /// </summary>
        event EventHandler<BeforeListReaderGroupsEventArgs> BeforeListReaderGroupsEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.ListReaderGroups"/>.
        /// </summary>
        event EventHandler<AfterListReaderGroupsEventArgs> AfterListReaderGroupsEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.ListReaders"/>.
        /// </summary>
        event EventHandler<BeforeListReadersEventArgs> BeforeListReadersEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.ListReaders"/>.
        /// </summary>
        event EventHandler<AfterListReadersEventArgs> AfterListReadersEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.Release"/>.
        /// </summary>
        event EventHandler<BeforeReleaseEventArgs> BeforeReleaseEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.Release"/>.
        /// </summary>
        event EventHandler<AfterReleaseEventArgs> AfterReleaseEvent;

        #endregion
    }
}