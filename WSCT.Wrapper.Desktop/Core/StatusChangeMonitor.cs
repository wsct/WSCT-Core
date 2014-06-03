using System;
using System.Linq;
using System.Threading;
using WSCT.Core;

namespace WSCT.Wrapper.Desktop.Core
{
    /// <summary>
    /// Object monitoring status change (see <see cref="ICardContext.GetStatusChange"/>).
    /// </summary>
    public class StatusChangeMonitor
    {
        #region >> Delegates

        /// <summary>
        /// Delegate for event sent when a card is inserted a reader in the system.
        /// </summary>
        /// <param name="readerState"></param>
        public delegate void OnCardInsertionEventHandler(AbstractReaderState readerState);

        /// <summary>
        /// Delegate for event sent when a card is removed from a reader in the system.
        /// </summary>
        /// <param name="readerState"></param>
        public delegate void OnCardRemovalEventHandler(AbstractReaderState readerState);

        /// <summary>
        /// Delegate for event sent when a reader is inserted in the system.
        /// </summary>
        /// <param name="insertedReaders"></param>
        public delegate void OnReaderInsertionEventHandler(string[] insertedReaders);

        /// <summary>
        /// Delegate for event sent when a reader is removed from the system.
        /// </summary>
        /// <param name="removedReaders"></param>
        public delegate void OnReaderRemovalEventHandler(string[] removedReaders);

        #endregion

        #region >> Events

        /// <summary>
        /// 
        /// </summary>
        public OnCardInsertionEventHandler OnCardInsertionEvent;

        /// <summary>
        /// 
        /// </summary>
        public OnCardRemovalEventHandler OnCardRemovalEvent;

        #endregion

        #region >> Fields

        private ICardContext _context;
        private Boolean _initDone;
        private string[] _readerNames;
        private AbstractReaderState[] _readerStates;
        private Thread _thread;
        private Boolean _threadContinue;

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public ICardContext Context
        {
            get { return _context; }
            set
            {
                _context = value;
                _readerNames = new string[_context.ReadersCount];
                _readerStates = new AbstractReaderState[_readerNames.Length];
                for (var i = 0; i < Context.ReadersCount; i++)
                {
                    _readerNames[i] = Context.Readers[i];
                }
                _initDone = false;
            }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public StatusChangeMonitor() :
            this(null, new string[0])
        {
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="readerNames"></param>
        public StatusChangeMonitor(ICardContext context, string[] readerNames)
        {
            _context = context;
            _readerNames = readerNames;
            _readerStates = new AbstractReaderState[_readerNames.Length];
            _thread = new Thread(WaitForChanges);
            _initDone = false;
            _threadContinue = false;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="context"></param>
        public StatusChangeMonitor(ICardContext context) :
            this(context, context.Readers)
        {
        }

        #endregion

        #region >> Members

        /// <summary>
        /// Start the monitoring thread.
        /// </summary>
        public void Start()
        {
            _threadContinue = true;
            _thread = new Thread(WaitForChanges);
            _thread.Start();
        }

        /// <summary>
        /// Stop the monitoring thread.
        /// </summary>
        public void Stop()
        {
            if (_threadContinue)
            {
                _threadContinue = false;
                _thread.Abort();
                _thread.Join();
            }
        }

        /// <summary>
        /// Waits for the presence of a card in one of the monitored readers.
        /// and returns the information about the change.
        /// </summary>
        /// <param name="timeout">Maximum wait time (ms).</param>
        /// <returns>Informations about the change, or null if no card is present when the <paramref name="timeout"/>.</returns>
        public AbstractReaderState WaitForCardPresence(uint timeout)
        {
            WaitForChange(0);

            var readerState = _readerStates.ToList().Find(
                rs => ((rs.EventState & EventState.StatePresent) != 0)
                );

            // Fire the insertion event, because waitForChange only fire the event on change detection
            if (readerState != null)
            {
                if (OnCardInsertionEvent != null)
                {
                    OnCardInsertionEvent(readerState);
                }
            }

            if (readerState == null)
            {
                WaitForChange(timeout);

                readerState = _readerStates.ToList().Find(
                    rs => ((rs.EventState & EventState.StatePresent) != 0)
                    );
            }

            return readerState;
        }

        /// <summary>
        /// Waits for a change of state of one of the monitored readers, and returns details.
        /// Events are fired when catched.
        /// </summary>
        /// <param name="timeout">Maximum wait time (ms).</param>
        /// <returns>Informations about the change, or null if no change occured until the <paramref name="timeout"/>.</returns>
        public void WaitForChange(uint timeout)
        {
            if (!_initDone)
            {
                _initDone = (UpdateInitialStates() == ErrorCode.Success);
            }

            foreach (var readerState in _readerStates)
            {
                readerState.EventState &= ~EventState.StateChanged;
                readerState.CurrentState = readerState.EventState;
            }

            var result = _context.GetStatusChange(timeout, _readerStates);

            switch (result)
            {
                case ErrorCode.Success: // Change occured
                    FireChangeEvents();
                    break;
                case ErrorCode.Timeout: // Nothing changed
                    break;
                case ErrorCode.ErrorInvalidHandle: // Context has been lost
                case ErrorCode.InvalidParameter:
                case ErrorCode.Cancelled:
                    _threadContinue = false;
                    break;
                default:
                    throw new Exception(String.Format("Error occured in getStatusChange() {0}", result));
            }
        }

        #endregion

        /// <summary>
        /// Waits for changes of state of any of the monitored readers. (for use by monitor thread only).
        /// Events are fired when catched.
        /// Loop until thread is stopped by using <see cref="Stop"/> method.
        /// </summary>
        private void WaitForChanges()
        {
            if (!_initDone)
            {
                _initDone = (UpdateInitialStates() == ErrorCode.Success);
            }

            _threadContinue = true;

            do
            {
                WaitForChange(250);
            } while (_threadContinue);
        }

        /// <summary>
        /// Initializes internal reader state.
        /// </summary>
        /// <returns></returns>
        private ErrorCode UpdateInitialStates()
        {
            for (var i = 0; i < _readerNames.Length; i++)
            {
                _readerStates[i] = Primitives.Api.CreateReaderStateInstance(_readerNames[i], EventState.StateUnaware, EventState.StateUnaware);
            }

            var result = _context.GetStatusChange(0, _readerStates);

            switch (result)
            {
                case ErrorCode.Success:
                case ErrorCode.Timeout:
                    break;
                default:
                    throw new Exception(String.Format("Error occured in getStatusChange() {0}", result));
            }

            return result;
        }

        /// <summary>
        /// Fire events for states that changed.
        /// </summary>
        private void FireChangeEvents()
        {
            foreach (var readerState in _readerStates)
            {
                if ((readerState.EventState & EventState.StateChanged) != 0)
                {
                    var publishedReaderState = readerState;
                    if ((readerState.EventState & EventState.StatePresent) != (readerState.CurrentState & EventState.StatePresent))
                    {
                        if ((readerState.EventState & EventState.StatePresent) != 0)
                        {
                            if (OnCardInsertionEvent != null)
                            {
                                OnCardInsertionEvent(publishedReaderState);
                            }
                        }
                        else
                        {
                            if (OnCardRemovalEvent != null)
                            {
                                OnCardRemovalEvent(publishedReaderState);
                            }
                        }
                    }
                }
            }
        }
    }
}