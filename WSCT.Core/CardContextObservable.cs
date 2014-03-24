using System;
using WSCT.Wrapper;

namespace WSCT.Core
{
    /// <summary>
    /// Allows an existing <see cref="ICardContext"/> instance to be observed by using delegates and wrapping it
    /// </summary>
    public class CardContextObservable : ICardContext, ICardContextObservable
    {
        #region >> Fields

        /// <summary>
        /// Wrapped <see cref="ICardContext"/> instance.
        /// </summary>
        protected ICardContext _cardContext;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"><b>ICardContext</b> instance to wrap</param>
        public CardContextObservable(ICardContext context)
        {
            _cardContext = context;
        }

        #endregion

        #region >> ICardContext Membres

        /// <inheritdoc />
        public IntPtr context
        {
            get { return _cardContext.context; }
        }

        /// <inheritdoc />
        public string[] groups
        {
            get { return _cardContext.groups; }
        }

        /// <inheritdoc />
        public int groupsCount
        {
            get { return _cardContext.groupsCount; }
        }

        /// <inheritdoc />
        public string[] readers
        {
            get { return _cardContext.readers; }
        }

        /// <inheritdoc />
        public int readersCount
        {
            get { return _cardContext.readersCount; }
        }

        /// <inheritdoc />
        public ErrorCode cancel()
        {
            if (beforeCancelEvent != null) beforeCancelEvent(this);
            var ret = _cardContext.cancel();
            if (afterCancelEvent != null) afterCancelEvent(this, ret);
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode establish()
        {
            if (beforeEstablishEvent != null) beforeEstablishEvent(this);
            var ret = _cardContext.establish();
            if (afterEstablishEvent != null) afterEstablishEvent(this, ret);
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode getStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            if (beforeGetStatusChangeEvent != null) beforeGetStatusChangeEvent(this, timeout, readerStates);
            var ret = _cardContext.getStatusChange(timeout, readerStates);
            if (afterGetStatusChangeEvent != null) afterGetStatusChangeEvent(this, timeout, readerStates, ret);
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode isValid()
        {
            if (beforeIsValidEvent != null) beforeIsValidEvent(this);
            var ret = _cardContext.isValid();
            if (afterIsValidEvent != null) afterIsValidEvent(this, ret);
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode listReaderGroups()
        {
            if (beforeListReaderGroupsEvent != null) beforeListReaderGroupsEvent(this);
            var ret = _cardContext.listReaderGroups();
            if (afterListReaderGroupsEvent != null) afterListReaderGroupsEvent(this, ret);
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode listReaders(String group)
        {
            if (beforeListReadersEvent != null) beforeListReadersEvent(this, group);
            var ret = _cardContext.listReaders(group);
            if (afterListReadersEvent != null) afterListReadersEvent(this, group, ret);
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode release()
        {
            if (beforeReleaseEvent != null) beforeReleaseEvent(this);
            var ret = _cardContext.release();
            if (afterReleaseEvent != null) afterReleaseEvent(this, ret);
            return ret;
        }

        #endregion

        #region >> ICardContextObservable Membres

        /// <inheritdoc />
        public event beforeCancel beforeCancelEvent;
        /// <inheritdoc />
        public event afterCancel afterCancelEvent;

        /// <inheritdoc />
        public event beforeEstablish beforeEstablishEvent;
        /// <inheritdoc />
        public event afterEstablish afterEstablishEvent;

        /// <inheritdoc />
        public event beforeGetStatusChange beforeGetStatusChangeEvent;
        /// <inheritdoc />
        public event afterGetStatusChange afterGetStatusChangeEvent;

        /// <inheritdoc />
        public event beforeIsValid beforeIsValidEvent;
        /// <inheritdoc />
        public event afterIsValid afterIsValidEvent;

        /// <inheritdoc />
        public event beforeListReaderGroups beforeListReaderGroupsEvent;
        /// <inheritdoc />
        public event afterListReaderGroups afterListReaderGroupsEvent;

        /// <inheritdoc />
        public event beforeListReaders beforeListReadersEvent;
        /// <inheritdoc />
        public event afterListReaders afterListReadersEvent;

        /// <inheritdoc />
        public event beforeRelease beforeReleaseEvent;
        /// <inheritdoc />
        public event afterRelease afterReleaseEvent;

        #endregion
    }
}
