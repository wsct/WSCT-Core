using System;
using WSCT.Wrapper;

namespace WSCT.Core
{

    #region >> Delegates

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.Cancel"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    public delegate void BeforeCancel(ICardContext context);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardContext.Cancel"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterCancel(ICardContext context, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of an <see cref="ICardContext.Establish"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    public delegate void BeforeEstablish(ICardContext context);

    /// <summary>
    /// Delegate for event sent after execution of an <see cref="ICardContext.Establish"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterEstablish(ICardContext context, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of an <see cref="ICardContext.GetStatusChange"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    /// <param name="timeout"></param>
    /// <param name="readerStates"></param>
    public delegate void BeforeGetStatusChange(ICardContext context, UInt32 timeout, AbstractReaderState[] readerStates);

    /// <summary>
    /// Delegate for event sent after execution of an <see cref="ICardContext.Establish"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    /// <param name="timeout"></param>
    /// <param name="readerStates"></param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterGetStatusChange(ICardContext context, UInt32 timeout, AbstractReaderState[] readerStates, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.IsValid"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    public delegate void BeforeIsValid(ICardContext context);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardContext.IsValid"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterIsValid(ICardContext context, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.ListReaders"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    /// <param name="group"><inheritdoc cref="ICardContext.ListReaders"/>.</param>
    public delegate void BeforeListReaders(ICardContext context, string group);

    /// <summary >
    /// Delegate for event sent after execution of a <see cref="ICardContext.ListReaders"/>.
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="group"><inheritdoc cref="ICardContext.ListReaders"/>.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterListReaders(ICardContext context, string group, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.ListReaderGroups"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    public delegate void BeforeListReaderGroups(ICardContext context);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardContext.ListReaderGroups"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterListReaderGroups(ICardContext context, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.Release"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    public delegate void BeforeRelease(ICardContext context);

    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardContext.Release"/>.
    /// </summary>
    /// <param name="context">Caller instance.</param>
    /// <param name="error">Return value of the caller.</param>
    public delegate void AfterRelease(ICardContext context, ErrorCode error);

    #endregion

    /// <summary>
    /// Interface to be implemented by all observable <see cref="ICardContext"/> instance.
    /// </summary>
    public interface ICardContextObservable : ICardContext
    {
        #region >> Events

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.Cancel"/>.
        /// </summary>
        event BeforeCancel BeforeCancelEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.Cancel"/>.
        /// </summary>
        event AfterCancel AfterCancelEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.Establish"/>.
        /// </summary>
        event BeforeEstablish BeforeEstablishEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.Establish"/>.
        /// </summary>
        event AfterEstablish AfterEstablishEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.GetStatusChange"/>.
        /// </summary>
        event BeforeGetStatusChange BeforeGetStatusChangeEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.GetStatusChange"/>.
        /// </summary>
        event AfterGetStatusChange AfterGetStatusChangeEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.IsValid"/>.
        /// </summary>
        event BeforeIsValid BeforeIsValidEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.IsValid"/>.
        /// </summary>
        event AfterIsValid AfterIsValidEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.ListReaderGroups"/>.
        /// </summary>
        event BeforeListReaderGroups BeforeListReaderGroupsEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.ListReaderGroups"/>.
        /// </summary>
        event AfterListReaderGroups AfterListReaderGroupsEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.ListReaders"/>.
        /// </summary>
        event BeforeListReaders BeforeListReadersEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.ListReaders"/>.
        /// </summary>
        event AfterListReaders AfterListReadersEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.Release"/>.
        /// </summary>
        event BeforeRelease BeforeReleaseEvent;

        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.Release"/>.
        /// </summary>
        event AfterRelease AfterReleaseEvent;

        #endregion
    }
}