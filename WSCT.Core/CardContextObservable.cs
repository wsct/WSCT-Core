using System;
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
        protected ICardContext _cardContext;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="context"><b>ICardContext</b> instance to wrap.</param>
        public CardContextObservable(ICardContext context)
        {
            _cardContext = context;
        }

        #endregion

        #region >> ICardContext Membres

        /// <inheritdoc />
        public IntPtr Context
        {
            get { return _cardContext.Context; }
        }

        /// <inheritdoc />
        public string[] Groups
        {
            get { return _cardContext.Groups; }
        }

        /// <inheritdoc />
        public int GroupsCount
        {
            get { return _cardContext.GroupsCount; }
        }

        /// <inheritdoc />
        public string[] Readers
        {
            get { return _cardContext.Readers; }
        }

        /// <inheritdoc />
        public int ReadersCount
        {
            get { return _cardContext.ReadersCount; }
        }

        /// <inheritdoc />
        public ErrorCode Cancel()
        {
            if (BeforeCancelEvent != null)
            {
                BeforeCancelEvent(this);
            }
            var ret = _cardContext.Cancel();
            if (AfterCancelEvent != null)
            {
                AfterCancelEvent(this, ret);
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Establish()
        {
            if (BeforeEstablishEvent != null)
            {
                BeforeEstablishEvent(this);
            }
            var ret = _cardContext.Establish();
            if (AfterEstablishEvent != null)
            {
                AfterEstablishEvent(this, ret);
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode GetStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            if (BeforeGetStatusChangeEvent != null)
            {
                BeforeGetStatusChangeEvent(this, timeout, readerStates);
            }
            var ret = _cardContext.GetStatusChange(timeout, readerStates);
            if (AfterGetStatusChangeEvent != null)
            {
                AfterGetStatusChangeEvent(this, timeout, readerStates, ret);
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode IsValid()
        {
            if (BeforeIsValidEvent != null)
            {
                BeforeIsValidEvent(this);
            }
            var ret = _cardContext.IsValid();
            if (AfterIsValidEvent != null)
            {
                AfterIsValidEvent(this, ret);
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode ListReaderGroups()
        {
            if (BeforeListReaderGroupsEvent != null)
            {
                BeforeListReaderGroupsEvent(this);
            }
            var ret = _cardContext.ListReaderGroups();
            if (AfterListReaderGroupsEvent != null)
            {
                AfterListReaderGroupsEvent(this, ret);
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode ListReaders(string group)
        {
            if (BeforeListReadersEvent != null)
            {
                BeforeListReadersEvent(this, group);
            }
            var ret = _cardContext.ListReaders(group);
            if (AfterListReadersEvent != null)
            {
                AfterListReadersEvent(this, group, ret);
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode Release()
        {
            if (BeforeReleaseEvent != null)
            {
                BeforeReleaseEvent(this);
            }
            var ret = _cardContext.Release();
            if (AfterReleaseEvent != null)
            {
                AfterReleaseEvent(this, ret);
            }
            return ret;
        }

        #endregion

        #region >> ICardContextObservable Membres

        /// <inheritdoc />
        public event BeforeCancel BeforeCancelEvent;

        /// <inheritdoc />
        public event AfterCancel AfterCancelEvent;

        /// <inheritdoc />
        public event BeforeEstablish BeforeEstablishEvent;

        /// <inheritdoc />
        public event AfterEstablish AfterEstablishEvent;

        /// <inheritdoc />
        public event BeforeGetStatusChange BeforeGetStatusChangeEvent;

        /// <inheritdoc />
        public event AfterGetStatusChange AfterGetStatusChangeEvent;

        /// <inheritdoc />
        public event BeforeIsValid BeforeIsValidEvent;

        /// <inheritdoc />
        public event AfterIsValid AfterIsValidEvent;

        /// <inheritdoc />
        public event BeforeListReaderGroups BeforeListReaderGroupsEvent;

        /// <inheritdoc />
        public event AfterListReaderGroups AfterListReaderGroupsEvent;

        /// <inheritdoc />
        public event BeforeListReaders BeforeListReadersEvent;

        /// <inheritdoc />
        public event AfterListReaders AfterListReadersEvent;

        /// <inheritdoc />
        public event BeforeRelease BeforeReleaseEvent;

        /// <inheritdoc />
        public event AfterRelease AfterReleaseEvent;

        #endregion
    }
}