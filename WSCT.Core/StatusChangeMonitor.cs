using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using WSCT.Wrapper;
using WSCT.Helpers;

namespace WSCT.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class StatusChangeMonitor
    {
        #region >> Delegates

        /// <summary>
        /// Delegate for event sent when a card is inserted a reader in the system
        /// </summary>
        /// <param name="readerState"></param>
        public delegate void onCardInsertionEventHandler(AbstractReaderState readerState);
        /// <summary>
        /// Delegate for event sent when a card is removed from a reader in the system
        /// </summary>
        /// <param name="readerState"></param>
        public delegate void onCardRemovalEventHandler(AbstractReaderState readerState);
        /// <summary>
        /// Delegate for event sent when a reader is inserted in the system
        /// </summary>
        /// <param name="insertedReaders"></param>
        public delegate void onReaderInsertionEventHandler(String[] insertedReaders);
        /// <summary>
        /// Delegate for event sent when a reader is removed from the system
        /// </summary>
        /// <param name="removedReaders"></param>
        public delegate void onReaderRemovalEventHandler(String[] removedReaders);

        #endregion

        #region >> Events

        /// <summary>
        /// 
        /// </summary>
        public onCardInsertionEventHandler onCardInsertionEvent;
        /// <summary>
        /// 
        /// </summary>
        public onCardRemovalEventHandler onCardRemovalEvent;

        #endregion

        #region >> Fields

        ICardContext _context;
        String[] _readerNames;
        AbstractReaderState[] _readerStates;
        Boolean _initDone;
        Thread _thread;
        Boolean _threadContinue;

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public ICardContext context
        {
            get { return _context; }
            set
            {
                _context = value;
                _readerNames = new String[_context.readersCount];
                _readerStates = new AbstractReaderState[_readerNames.Length];
                for (int i = 0; i < context.readersCount; i++)
                    _readerNames[i] = context.readers[i];
                _initDone = false;
            }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public StatusChangeMonitor() :
            this(null, new String[0])
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="readerNames"></param>
        public StatusChangeMonitor(ICardContext context, String[] readerNames)
        {
            _context = context;
            _readerNames = readerNames;
            _readerStates = new AbstractReaderState[_readerNames.Length];
            _thread = new Thread(this.waitForChanges);
            _initDone = false;
            _threadContinue = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public StatusChangeMonitor(ICardContext context) :
            this(context, context.readers)
        {
        }

        #endregion

        #region >> Members

        /// <summary>
        /// Start the monitoring thread
        /// </summary>
        public void start()
        {
            _threadContinue = true;
            _thread = new Thread(this.waitForChanges);
            _thread.Start();
        }

        /// <summary>
        /// Stop the monitoring thread
        /// </summary>
        public void stop()
        {
            if (_threadContinue)
            {
                _threadContinue = false;
                _thread.Abort();
                _thread.Join();
            }
        }

        /// <summary>
        /// Waits for the presence of a card in one of the monitored readers
        /// and returns the information about the change
        /// </summary>
        /// <param name="timeout">Maximum wait time (ms)</param>
        /// <returns>Informations about the change, or null if no card is present when the <paramref name="timeout"/>.</returns>
        public AbstractReaderState waitForCardPresence(uint timeout)
        {
            waitForChange(0);

            AbstractReaderState readerState = _readerStates.ToList<AbstractReaderState>().Find(
                delegate(AbstractReaderState rs) { return ((rs.eventState & EventState.SCARD_STATE_PRESENT) != 0); }
                );

            // Fire the insertion event, because waitForChange only fire the event on change detection
            if (readerState != null)
                if (onCardInsertionEvent != null) onCardInsertionEvent(readerState);

            if (readerState == null)
            {
                waitForChange(timeout);

                readerState = _readerStates.ToList<AbstractReaderState>().Find(
                    delegate(AbstractReaderState rs) { return ((rs.eventState & EventState.SCARD_STATE_PRESENT) != 0); }
                    );
            }

            return readerState;
        }

        /// <summary>
        /// Waits for a change of state of one of the monitored readers, and returns details.
        /// Events are fired when catched.
        /// </summary>
        /// <param name="timeout">Maximum wait time (ms)</param>
        /// <returns>Informations about the change, or null if no change occured until the <paramref name="timeout"/>.</returns>
        public void waitForChange(uint timeout)
        {
            if (!_initDone)
                _initDone = (updateInitialStates() == ErrorCode.SCARD_S_SUCCESS);

            foreach (AbstractReaderState readerState in _readerStates)
            {
                readerState.eventState &= ~EventState.SCARD_STATE_CHANGED;
                readerState.currentState = readerState.eventState;
            }

            ErrorCode result = _context.getStatusChange(timeout, _readerStates);

            switch (result)
            {
                case ErrorCode.SCARD_S_SUCCESS: // Change occured
                    fireChangeEvents();
                    break;
                case ErrorCode.SCARD_E_TIMEOUT: // Nothing changed
                    break;
                case ErrorCode.ERROR_INVALID_HANDLE: // Context has been lost
                case ErrorCode.SCARD_E_INVALID_PARAMETER:
                case ErrorCode.SCARD_E_CANCELLED:
                    _threadContinue = false;
                    break;
                default:
                    throw new Exception(String.Format("Error occured in getStatusChange() {0}", result));
            }
        }

        #endregion

        /// <summary>
        /// Waits for changes of state of any of the monitored readers. (for use by monitor thread only)
        /// Events are fired when catched.
        /// Loop until thread is stopped by using <see cref="stop"/> method
        /// </summary>
        void waitForChanges()
        {
            if (!_initDone)
                _initDone = (updateInitialStates() == ErrorCode.SCARD_S_SUCCESS);

            _threadContinue = true;

            do
            {
                waitForChange(250);
            }
            while (_threadContinue);
        }

        /// <summary>
        /// Initializes <see cref="_readerStates"/>
        /// </summary>
        /// <returns></returns>
        ErrorCode updateInitialStates()
        {
            for (int i = 0; i < _readerNames.Length; i++)
            {
                _readerStates[i] = Primitives.api.createReaderStateInstance(_readerNames[i], EventState.SCARD_STATE_UNAWARE, EventState.SCARD_STATE_UNAWARE);
            }

            ErrorCode result = _context.getStatusChange(0, _readerStates);

            switch (result)
            {
                case ErrorCode.SCARD_S_SUCCESS:
                case ErrorCode.SCARD_E_TIMEOUT:
                    break;
                default:
                    throw new Exception(String.Format("Error occured in getStatusChange() {0}", result));
            }

            return result;
        }

        /// <summary>
        /// Fire events for states that changed
        /// </summary>
        void fireChangeEvents()
        {
            foreach (AbstractReaderState readerState in _readerStates)
            {
                if ((readerState.eventState & EventState.SCARD_STATE_CHANGED) != 0)
                {
                    AbstractReaderState publishedReaderState = readerState;
                    if ((readerState.eventState & EventState.SCARD_STATE_PRESENT) != (readerState.currentState & EventState.SCARD_STATE_PRESENT))
                    {
                        if ((readerState.eventState & EventState.SCARD_STATE_PRESENT) != 0)
                        {
                            if (onCardInsertionEvent != null) onCardInsertionEvent(publishedReaderState);
                        }
                        else
                        {
                            if (onCardRemovalEvent != null) onCardRemovalEvent(publishedReaderState);
                        }
                    }
                }
            }
        }
    }
}
