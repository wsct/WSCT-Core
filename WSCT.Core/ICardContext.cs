using System;

using WSCT.Wrapper;

namespace WSCT.Core
{

    /// <summary>
    /// Interface for card contexts, ie objects capable of managing the resources to access to readers.
    /// </summary>
    public interface ICardContext
    {
        #region >> Properties

        /// <summary>
        /// Accessor to the raw context.
        /// </summary>
        IntPtr context
        {
            get;
        }

        /// <summary>
        /// Accessor to the names of the groups of readers available (once discovered by <see cref="listReaderGroups()"/>).
        /// </summary>
        String[] groups { get; }

        /// <summary>
        /// Accessor to the number of groups of readers found.
        /// </summary>
        int groupsCount { get; }

        /// <summary>
        /// Accessor to the names of readers found in the group (once discovered by <see cref="listReaders"/>).
        /// </summary>
        String[] readers { get; }

        /// <summary>
        /// Accessor to the number of readers found.
        /// </summary>
        int readersCount { get; }

        #endregion

        #region >> Methods

        /// <summary>
        /// This function terminates all outstanding actions within a specific resource manager context.
        /// <seealso cref="IPrimitives.SCardCancel"/>
        /// </summary>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded</returns>
        ErrorCode cancel();

        /// <summary>
        /// Establishes the resource manager context.
        /// </summary>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded</returns>
        ErrorCode establish();

        /// <summary>
        /// The <see cref="getStatusChange"/> function blocks execution until the current availability of the cards in a specific set of readers changes.
        /// The caller supplies a list of readers to be monitored by a <see cref="AbstractReaderState"/> array and the maximum amount of time (in milliseconds)
        /// that it is willing to wait for an action to occur on one of the listed readers.
        /// Note that <see cref="getStatusChange"/> uses the user-supplied value in the currentState members of the <paramref name="readerStates"/> <see cref="AbstractReaderState"/> array 
        /// as the definition of the current state of the readers.
        /// The function returns when there is a change in availability, having filled in the <see cref="AbstractReaderState.EventState"/> members of <paramref name="readerStates"/> appropriately.
        /// </summary>
        /// <param name="timeout">The maximum amount of time, in milliseconds, to wait for an action. A value of zero causes the function to return immediately. A value of INFINITE causes this function never to time out.</param>
        /// <param name="readerStates">
        /// An array of SCARD_READERSTATE structures that specify the readers to watch, and that receives the result.
        /// To be notified of the arrival of a new smart card reader, set the szReader member of a SCARD_READERSTATE structure to "\\\\?PnP?\\Notification", and set all of the other members of that structure to zero.
        /// Important  Each member of each structure in this array must be initialized to zero and then set to specific values as necessary. If this is not done, the function will fail in situations that involve remote card readers.
        /// </param>
        /// <returns></returns>
        ErrorCode getStatusChange(UInt32 timeout, AbstractReaderState[] readerStates);

        /// <summary>
        /// Determines whether a smart card context handle is valid.
        /// </summary>
        /// <returns><see cref="ErrorCode.Success"/> if context is valid</returns>
        ErrorCode isValid();

        /// <summary>
        /// Provides the list of readers within a set of named reader groups.
        /// </summary>
        /// <param name="group">Name of the reader group</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded</returns>
        ErrorCode listReaders(String group);

        /// <summary>
        /// Provides the list of groups of readers available on the system.
        /// </summary>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded</returns>
        ErrorCode listReaderGroups();

        /// <summary>
        /// Closes the resource manager context.
        /// </summary>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded</returns>
        ErrorCode release();

        #endregion

    }
}