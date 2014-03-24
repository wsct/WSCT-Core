using System;
using WSCT.Wrapper;

namespace WSCT.Core
{
    #region >> Delegates

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.cancel"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    public delegate void beforeCancel(ICardContext context);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardContext.cancel"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterCancel(ICardContext context, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of an <see cref="ICardContext.establish"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    public delegate void beforeEstablish(ICardContext context);
    /// <summary>
    /// Delegate for event sent after execution of an <see cref="ICardContext.establish"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterEstablish(ICardContext context, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of an <see cref="ICardContext.getStatusChange"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="timeout"></param>
    /// <param name="readerStates"></param>
    public delegate void beforeGetStatusChange(ICardContext context, UInt32 timeout, AbstractReaderState[] readerStates);
    /// <summary>
    /// Delegate for event sent after execution of an <see cref="ICardContext.establish"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="timeout"></param>
    /// <param name="readerStates"></param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterGetStatusChange(ICardContext context, UInt32 timeout, AbstractReaderState[] readerStates, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.isValid"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    public delegate void beforeIsValid(ICardContext context);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardContext.isValid"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterIsValid(ICardContext context, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.listReaders"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="group"><inheritdoc cref="ICardContext.listReaders"/></param>
    public delegate void beforeListReaders(ICardContext context, String group);
    /// <summary >
    /// Delegate for event sent after execution of a <see cref="ICardContext.listReaders"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="group"><inheritdoc cref="ICardContext.listReaders"/></param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterListReaders(ICardContext context, String group, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.listReaderGroups"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    public delegate void beforeListReaderGroups(ICardContext context);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardContext.listReaderGroups"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterListReaderGroups(ICardContext context, ErrorCode error);

    /// <summary>
    /// Delegate for event sent before execution of a <see cref="ICardContext.release"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    public delegate void beforeRelease(ICardContext context);
    /// <summary>
    /// Delegate for event sent after execution of a <see cref="ICardContext.release"/>
    /// </summary>
    /// <param name="context">Caller instance</param>
    /// <param name="error">Return value of the caller</param>
    public delegate void afterRelease(ICardContext context, ErrorCode error);

    #endregion

    /// <summary>
    /// Interface to be implemented by all observable <see cref="ICardContext"/> instance
    /// </summary>
    public interface ICardContextObservable : ICardContext
    {
        #region >> Events

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.cancel"/>
        /// </summary>
        event beforeCancel beforeCancelEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.cancel"/>
        /// </summary>
        event afterCancel afterCancelEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.establish"/>
        /// </summary>
        event beforeEstablish beforeEstablishEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.establish"/>
        /// </summary>
        event afterEstablish afterEstablishEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.getStatusChange"/>
        /// </summary>
        event beforeGetStatusChange beforeGetStatusChangeEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.getStatusChange"/>
        /// </summary>
        event afterGetStatusChange afterGetStatusChangeEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.isValid"/>
        /// </summary>
        event beforeIsValid beforeIsValidEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.isValid"/>
        /// </summary>
        event afterIsValid afterIsValidEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.listReaderGroups"/>
        /// </summary>
        event beforeListReaderGroups beforeListReaderGroupsEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.listReaderGroups"/>
        /// </summary>
        event afterListReaderGroups afterListReaderGroupsEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.listReaders"/>
        /// </summary>
        event beforeListReaders beforeListReadersEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.listReaders"/>
        /// </summary>
        event afterListReaders afterListReadersEvent;

        /// <summary>
        /// Event sent before execution of a <see cref="ICardContext.release"/>
        /// </summary>
        event beforeRelease beforeReleaseEvent;
        /// <summary>
        /// Event sent after execution of a <see cref="ICardContext.release"/>
        /// </summary>
        event afterRelease afterReleaseEvent;

        #endregion
    }
}
