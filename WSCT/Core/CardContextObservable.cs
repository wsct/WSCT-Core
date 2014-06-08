using System;
using WSCT.Core.Events;
using WSCT.Helpers.Events;
using WSCT.Wrapper;

namespace WSCT.Core
{
    /// <summary>
    /// Allows an existing <see cref="ICardContext"/> instance to be observed by using delegates and wrapping it.
    /// </summary>
    public class CardContextObservable : ICardContextObservable
    {
        #region >> Fields

        /// <summary>
        /// Wrapped <see cref="ICardContext"/> instance.
        /// </summary>
        protected ICardContext context;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="context"><b>ICardContext</b> instance to wrap.</param>
        public CardContextObservable(ICardContext context)
        {
            this.context = context;
        }

        #endregion

        #region >> ICardContext Membres

        /// <inheritdoc />
        public IntPtr Context
        {
            get { return context.Context; }
        }

        /// <inheritdoc />
        public string[] Groups
        {
            get { return context.Groups; }
        }

        /// <inheritdoc />
        public int GroupsCount
        {
            get { return context.GroupsCount; }
        }

        /// <inheritdoc />
        public string[] Readers
        {
            get { return context.Readers; }
        }

        /// <inheritdoc />
        public int ReadersCount
        {
            get { return context.ReadersCount; }
        }

        /// <inheritdoc />
        public ErrorCode Cancel()
        {
            BeforeCancelEvent.Raise(this, new BeforeCancelEventArgs());

            var ret = context.Cancel();

            AfterCancelEvent.Raise(this, new AfterCancelEventArgs { ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Establish()
        {
            BeforeEstablishEvent.Raise(this, new BeforeEstablishEventArgs());

            var ret = context.Establish();

            AfterEstablishEvent.Raise(this, new AfterEstablishEventArgs { ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode GetStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            BeforeGetStatusChangeEvent.Raise(this, new BeforeGetStatusChangeEventArgs { TimeOut = timeout, ReaderStates = readerStates });

            var ret = context.GetStatusChange(timeout, readerStates);

            AfterGetStatusChangeEvent.Raise(this, new AfterGetStatusChangeEventArgs { TimeOut = timeout, ReaderStates = readerStates, ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode IsValid()
        {
            BeforeIsValidEvent.Raise(this, new BeforeIsValidEventArgs());

            var ret = context.IsValid();

            AfterIsValidEvent.Raise(this, new AfterIsValidEventArgs { ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode ListReaderGroups()
        {
            BeforeListReaderGroupsEvent.Raise(this, new BeforeListReaderGroupsEventArgs());

            var ret = context.ListReaderGroups();

            AfterListReaderGroupsEvent.Raise(this, new AfterListReaderGroupsEventArgs { ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode ListReaders(string group)
        {
            BeforeListReadersEvent.Raise(this, new BeforeListReadersEventArgs { Group = group });

            var ret = context.ListReaders(group);

            AfterListReadersEvent.Raise(this, new AfterListReadersEventArgs { Group = group, ReturnValue = ret });

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Release()
        {
            BeforeReleaseEvent.Raise(this, new BeforeReleaseEventArgs());

            var ret = context.Release();

            AfterReleaseEvent.Raise(this, new AfterReleaseEventArgs { ReturnValue = ret });

            return ret;
        }

        #endregion

        #region >> ICardContextObservable Membres

        /// <inheritdoc />
        public event EventHandler<BeforeCancelEventArgs> BeforeCancelEvent;

        /// <inheritdoc />
        public event EventHandler<AfterCancelEventArgs> AfterCancelEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeEstablishEventArgs> BeforeEstablishEvent;

        /// <inheritdoc />
        public event EventHandler<AfterEstablishEventArgs> AfterEstablishEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeGetStatusChangeEventArgs> BeforeGetStatusChangeEvent;

        /// <inheritdoc />
        public event EventHandler<AfterGetStatusChangeEventArgs> AfterGetStatusChangeEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeIsValidEventArgs> BeforeIsValidEvent;

        /// <inheritdoc />
        public event EventHandler<AfterIsValidEventArgs> AfterIsValidEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeListReaderGroupsEventArgs> BeforeListReaderGroupsEvent;

        /// <inheritdoc />
        public event EventHandler<AfterListReaderGroupsEventArgs> AfterListReaderGroupsEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeListReadersEventArgs> BeforeListReadersEvent;

        /// <inheritdoc />
        public event EventHandler<AfterListReadersEventArgs> AfterListReadersEvent;

        /// <inheritdoc />
        public event EventHandler<BeforeReleaseEventArgs> BeforeReleaseEvent;

        /// <inheritdoc />
        public event EventHandler<AfterReleaseEventArgs> AfterReleaseEvent;

        #endregion
    }
}