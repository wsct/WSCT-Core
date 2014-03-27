using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Interface to be implemented by all PC/SC API wrappers.
    /// </summary>
    public interface IPrimitives
    {
        /// <summary>
        /// Value used by PC/SC API to ask for auto allocation.
        /// </summary>
        UInt32 AutoAllocate { get; }

        /// <summary>
        /// Default maximum buffer size used to fetch PC/SC responses.
        /// </summary>
        UInt32 DefaultBufferSize { get; set; }

        #region >> PC/SC functions

        /// <summary>
        /// This function terminates all outstanding actions within a specific resource manager context.
        /// <para>The only requests that you can cancel are those that require waiting for external action by the smart card or user.
        /// Any such outstanding action requests will terminate with a status indication that the action was canceled.
        /// This is especially useful to force outstanding <see cref="SCardGetStatusChange"/> calls to terminate.</para>
        /// </summary>
        /// <param name="context">A handle that identifies the resource manager context.</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        ErrorCode SCardCancel(
            [In] IntPtr context
            );

        /// <summary>
        /// Establishes a connection (using a specific resource manager context) between the calling application and a smart card contained by a specific reader.
        /// If no card exists in the specified reader, an error is returned.
        /// </summary>
        /// <param name="context">A handle that identifies the resource manager context.</param>
        /// <param name="readerName">The name of the reader that contains the target card.</param>
        /// <param name="shareMode">A flag that indicates whether other applications may form connections to the card.</param>
        /// <param name="preferedProtocol">A bitmask of acceptable protocols for the connection. Possible values may be combined with the OR operation.</param>
        /// <param name="card">A handle that identifies the connection to the smart card in the designated reader.</param>
        /// <param name="activeProtocol">A flag that indicates the established active protocol.</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDCOMM</c>
        /// <code lang="none">
        /// RESPONSECODE Connect(
        ///		IN STR ReaderName // Friendly name for a Reader
        ///		IN DWORD Flags // Desired access mode information
        ///		IN DWORD PreferredProtocols // Card communications protocols that may be used
        ///		OUT DWORD ActiveProtocol // Protocol actually in use
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardConnect(
            [In] IntPtr context,
            [In] string readerName,
            [In] ShareMode shareMode,
            [In] Protocol preferedProtocol,
            [In, Out] ref IntPtr card,
            [In, Out] ref Protocol activeProtocol
            );

        /// <summary>
        /// Sends a commandAPDU directly to the IFD Handler to be processed by the reader.
        /// This is useful for creating client side reader drivers for functions like PIN pads, biometrics, or other extensions to the normal smart card reader that are not normally handled by PC/SC.
        /// </summary>
        /// <param name="card">Reference value returned from <see cref="SCardConnect">SCardConnect</see></param>
        /// <param name="controlCode">Specifies the control code for the operation. This value identifies the specific operation to be performed.</param>
        /// <param name="sendBuffer">Buffer that contains the data required to perform the operation. This parameter can be <c>null</c> if the <paramref name="controlCode"/> parameter specifies an operation that does not require input data.</param>
        /// <param name="sendSize">Integer that specifies the size, in bytes, of the buffer pointed to by <c>sendBuffer</c>.</param>
        /// <param name="recvBuffer">Buffer that receives the operation's output data. This parameter can be <c>null</c> if the <c>ControlCode</c> parameter specifies an operation that does not produce output data.</param>
        /// <param name="recvSize">Size, in bytes, of the buffer pointed to by <paramref name="recvBuffer"/>. (<c>SCARD_AUTOALLOCATE</c> not allowed)</param>
        /// <param name="returnedSize">Size, in bytes, of the data stored into the buffer pointed to by <c>recvBuffer</c>.</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDCOMM</c>
        /// <code lang="none">
        /// RESPONSECODE Control(
        ///		IN DWORD ControlCode // Vendor-defined control code
        ///		IN BYTE[] InBuffer // Input data buffer
        ///		IN OUT BYTE[] OutBuffer // Output data buffer
        ///		OUT DWORD OutBufferLength // Length of data in output data buffer
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardControl(
            [In] IntPtr card,
            [In] UInt32 controlCode,
            [In, Out] byte[] sendBuffer,
            [In] UInt32 sendSize,
            [In, Out] ref byte[] recvBuffer,
            [In] UInt32 recvSize,
            [In, Out] ref UInt32 returnedSize
            );

        /// <summary>
        /// Terminates a connection to the connection made through <see cref="SCardConnect"/>.
        /// </summary>
        /// <param name="card">Connection made from SCardConnect</param>
        /// <param name="disposition">Reader function to execute</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDCOMM</c>
        /// <code lang="none">
        /// RESPONSECODE Disconnect(
        ///		IN DWORD Disposition // Desired Card disposition action
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardDisconnect(
            [In] IntPtr card,
            [In] Disposition disposition
            );

        /// <summary>
        /// Establishes the resource manager context (the scope) within which database operations are performed.
        /// </summary>
        /// <param name="scope">Scope of the resource manager context.</param>
        /// <param name="notUsed1">Reserved for future use and must be <c>null</c>.</param>
        /// <param name="notUsed2">Reserved for future use and must be <c>null</c>.</param>
        /// <param name="context">Handle to the established resource manager context.
        /// This handle can now be supplied to other functions attempting to do work within this context.</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>RESOURCEMANAGER</c>
        /// <code lang="none">
        /// RESPONSECODE EstablishContext (
        ///		IN DWORD Scope, // A scope indicator (see below)
        ///		IN DWORD Reserved1, // Reserved for future use to allow privileged administrative programs to act on behalf of another user
        ///		IN DWORD Reserved2 // Reserved for future use to allow privileged administrative programs to act on behalf of another terminal
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardEstablishContext(
            [In] UInt32 scope,
            [In] Object notUsed1,
            [In] Object notUsed2,
            [In, Out] ref IntPtr context
            );

        /// <summary>
        /// Gets the current reader attributes for the given handle.
        /// It does not affect the state of the reader, driver, or card.
        /// </summary>
        /// <param name="card">Reference value returned from <see cref="SCardConnect"/>.</param>
        /// <param name="attributeId">Identifier for the attribute to get. Note that vendors may not support all attributes.</param>
        /// <param name="recvAttribute">R-APDU from the card. <paramref name="recvAttribute"/> should be <c>null</c> as the allocation will be automatic (otherwise initial instance will be lost).</param>
        /// <param name="recvAttributeSize">On input, can be <see cref="AutoAllocate"/>, or the size allocated in <paramref name="recvAttribute"/>. On output, receives the number of bytes in <paramref name="recvAttribute"/>.</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        ErrorCode SCardGetAttrib(
            [In] IntPtr card,
            [In] UInt32 attributeId,
            [In, Out] ref byte[] recvAttribute,
            [In, Out] ref UInt32 recvAttributeSize
            );

        /// <summary>
        /// The <c>SCardGetStatusChange</c> function blocks execution until the current availability of the cards in a specific set of readers changes.
        /// The caller supplies a list of readers to be monitored by an <c>SCARD_READERSTATE_x32OS</c> array and the maximum amount of time (in milliseconds) 
        /// that it is willing to wait for an action to occur on one of the listed readers.
        /// Note that <c>SCardGetStatusChange</c> uses the user-supplied value in the <c>currentState</c> members of the <c>rgReaderStates</c> <c>SCARD_READERSTATE_x32OS</c> array 
        /// as the definition of the current state of the readers.
        /// The function returns when there is a change in availability, having filled in the eventState members of rgReaderStates appropriately.
        /// </summary>
        /// <param name="context">Handle that identifies the resource manager context.</param>
        /// <param name="timeout">Maximum amount of time (in milliseconds) to wait for an action. A value of zero causes the function to return immediately. A value of INFINITE causes this function never to time out.</param>
        /// <param name="readerStates">Array of <c>SCARD_READERSTATE_x32OS</c> structures that specify the readers to watch, and receives the result.</param>
        /// <param name="readers">Number of elements in the <c>readerStates</c> array.</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDTRACK</c>
        /// <code lang="none">
        /// RESPONSECODE GetStatusChange(
        ///		IN OUT SCARD_READERSTATE_x32OS[] ReaderStates // Array of READERSTATE // structures for readers of interest
        ///		IN DWORD Timeout // Time-out value in milliseconds
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardGetStatusChange(
            [In] IntPtr context,
            [In] UInt32 timeout,
            [In, Out] AbstractReaderState[] readerStates,
            [In, Out] UInt32 readers
            );

        /// <summary>
        /// Determines whether a smart card context handle is valid.
        /// </summary>
        /// <param name="context">Handle that identifies the resource manager context.</param>
        /// <returns><see cref="ErrorCode.Success"/> if the context is valid.</returns>
        ErrorCode SCardIsValidContext(
            [In] IntPtr context
            );

        /// <summary>
        /// Returns a list of currently available readers on the system.
        /// If the application sends <paramref name="groups"/> and <paramref name="readers"/> as null then it will return the size of the buffer needed to allocate in <paramref name="size"/>.
        /// The reader names is a multi-string and separated by a nul character ('\0') and ended by a double nul character.
        /// </summary>
        /// <param name="context">Connection context to the PC/SC Resource Manager</param>
        /// <param name="groups">List of groups to list readers</param>
        /// <param name="readers">Multi-string buffer (NULL separated) containing list of readers</param>
        /// <param name="size">On input, can be <see cref="AutoAllocate"/> to autoallocate <paramref name="readers"/>, or the size allocated in <paramref name="readers"/>. On output, receives the number of bytes in <paramref name="groups"/> including NULL's</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>RESOURCEQUERY</c>
        /// <code lang="none">
        /// RESPONSECODE ListReaders(
        ///		IN STR[] Groups // Array of strings containing Group names of interest
        ///		OUT STR[] Readers // Array of strings containing Readers within the Groups
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardListReaders(
            [In] IntPtr context,
            [In] string groups,
            [In, Out] ref IntPtr readers,
            [In, Out] ref UInt32 size
            );

        /// <summary>
        /// Returns a list of currently available reader groups on the system.
        /// If the application sends <paramref name="groups"/> as null then it will return the size of the buffer needed to allocate in <paramref name="size"/>.
        /// The group names is a multi-string and separated by a nul character ('\0') and ended by a double nul character.
        /// </summary>
        /// <param name="context">Connection context to the PC/SC Resource Manager</param>
        /// <param name="groups">Multi-string buffer (NULL separated) containing list of groups of readers</param>
        /// <param name="size">On input, can be <see cref="AutoAllocate"/> to autoallocate <paramref name="groups"/>, or the size allocated in <paramref name="groups"/>. On output, receives the number of bytes in <paramref name="groups"/> including NULL's</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>RESOURCEQUERY</c>
        /// <code lang="none">
        /// RESPONSECODE ListReaderGroups(
        ///		OUT STR[] Groups // Array of strings containing the Group names
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardListReaderGroups(
            [In] IntPtr context,
            [In, Out] ref IntPtr groups,
            [In, Out] ref UInt32 size
            );

        /// <summary>
        /// Reestablishes a connection to a reader that was previously connected to using SCardConnect().
        /// In a multi application environment it is possible for an application to reset the card in shared mode.
        /// When this occurs any other application trying to access certain commands will be returned the value <see cref="ErrorCode.ResetCard"/>.
        /// When this occurs <c>SCardReconnect()</c> must be called in order to acknowledge that the card was reset and allow it to change it's state accordingly.
        /// </summary>
        /// <param name="card">Handle to a previous call to connect</param>
        /// <param name="shareMode">Mode of connection type: exclusive or shared</param>
        /// <param name="preferedProtocol">Desired protocol use</param>
        /// <param name="initialisation">Desired action taken on the card/reader</param>
        /// <param name="activeProtocol">Established protocol to this connection</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDCOMM</c>
        /// <code lang="none">
        /// RESPONSECODE Reconnect(
        ///		IN DWORD Flags // desired access mode
        ///		IN DWORD PreferredProtocols // card communications protocols which may be used
        ///		IN DWORD Initialization // Specify card initialization to be performed
        ///		OUT DWORD ActiveProtocol // protocol actually in use
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardReconnect(
            [In] IntPtr card,
            [In] ShareMode shareMode,
            [In] Protocol preferedProtocol,
            [In] Disposition initialisation,
            [In, Out] ref Protocol activeProtocol
            );

        /// <summary>
        /// destroys a communication context to the PC/SC Resource Manager.
        /// This must be the last function called in a PC/SC application.
        /// </summary>
        /// <param name="context">Reference value returned from <see cref="SCardEstablishContext"/></param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>RESOURCEMANAGER</c>
        /// <code lang="none">
        /// RESPONSECODE ReleaseContext()
        /// </code>
        /// </remarks>
        ErrorCode SCardReleaseContext(
            [In] IntPtr context
            );

        /// <summary>
        /// Sets the given reader attribute for the given handle.
        /// It does not affect the state of the reader, reader driver, or smart card.
        /// Not all attributes are supported by all readers (nor can they be set at all times) as many of the attributes are under direct control of the transport protocol.
        /// </summary>
        /// <param name="card">Reference value returned from <see cref="SCardConnect"/>.</param>
        /// <param name="attributeId">Specifies the identifier for the attribute to set.</param>
        /// <param name="newAttribute">Pointer to a buffer that supplies the attribute whose identifier is supplied in dwAttrId.</param>
        /// <param name="newAttributeSize">Count of bytes that represent the length of the attribute value in the pbAttr  buffer.</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        ErrorCode SCardSetAttrib(
            [In] IntPtr card,
            [In] UInt32 attributeId,
            [In] byte[] newAttribute,
            [In] UInt32 newAttributeSize
            );

        /// <summary>
        /// Provides the current status of a smart card in a reader.
        /// You can call it any time after a successful call to <see cref="SCardConnect"/> and before a successful call to <see cref="SCardDisconnect"/>.
        /// It does not affect the state of the reader or reader driver.
        /// <para>Use only WITH <paramref name="atrSize"/>=<see cref="AutoAllocate"/></para>
        /// </summary>
        /// <param name="card">Reference value returned from SCardConnect.</param>
        /// <param name="readerName">List of friendly names (multiple string) by which the currently connected reader is known.</param>
        /// <param name="readerNameSize">On input, supplies the length of the szReaderName buffer.</param>
        /// <param name="status">One of <see name="State">State</see> enumeration</param>
        /// <param name="protocol">Current protocol, if any. The returned value is meaningful only if the returned value of pdwState is SCARD_SPECIFICMODE.</param>
        /// <param name="atr">ATR originally returned by the card</param>
        /// <param name="atrSize">On input, MUST be <see cref="AutoAllocate"/>, or this method will fail. On output, receives the number of bytes in the ATR string (32 bytes maximum).</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDCOMM</c>
        /// <code lang="none">
        /// RESPONSECODE Status(
        ///		OUT STR[] Reader // Friendly name of the connected reader
        ///		OUT DWORD State // Current status the connection
        ///		OUT DWORD ActiveProtocol // protocol actually in use
        ///		OUT BYTE Atr[] // ATR data buffer
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardStatus(
            [In] IntPtr card,
            [In, Out] ref IntPtr readerName,
            [In, Out] ref UInt32 readerNameSize,
            [In, Out] ref State status,
            [In, Out] ref Protocol protocol,
            [In, Out] ref IntPtr atr,
            [In, Out] ref UInt32 atrSize
            );

        /// <summary>
        /// Provides the current status of a smart card in a reader.
        /// You can call it any time after a successful call to <see cref="SCardConnect"/> and before a successful call to <see cref="SCardDisconnect"/>.
        /// It does not affect the state of the reader or reader driver.
        /// </summary>
        /// <param name="card">Reference value returned from SCardConnect.</param>
        /// <param name="readerName">List of friendly names (multiple string) by which the currently connected reader is known.</param>
        /// <param name="state">One of <see name="State">State</see> enumeration</param>
        /// <param name="protocol">Current protocol, if any. The returned value is meaningful only if the returned value of pdwState is SCARD_SPECIFICMODE.</param>
        /// <param name="atr">ATR originally returned by the card</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDCOMM</c>
        /// <code lang="none">
        /// RESPONSECODE Status(
        ///		OUT STR[] Reader // Friendly name of the connected reader
        ///		OUT DWORD State // Current status the connection
        ///		OUT DWORD ActiveProtocol // protocol actually in use
        ///		OUT BYTE Atr[] // ATR data buffer
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardStatus(
            [In] IntPtr card,
            [In, Out] ref string readerName,
            [In, Out] ref State state,
            [In, Out] ref Protocol protocol,
            [In, Out] ref byte[] atr);

        /// <summary>
        /// Provides the current status of a smart card in a reader.
        /// You can call it any time after a successful call to <see cref="SCardConnect"/> and before a successful call to <see cref="SCardDisconnect"/>.
        /// It does not affect the state of the reader or reader driver.
        /// </summary>
        /// <param name="card">Reference value returned from SCardConnect.</param>
        /// <param name="status">One of <see name="State">State</see> enumeration</param>
        /// <param name="protocol">Current protocol, if any. The returned value is meaningful only if the returned value of pdwState is SCARD_SPECIFICMODE.</param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDCOMM</c>
        /// <code lang="none">
        /// RESPONSECODE Status(
        ///		OUT STR[] Reader // Friendly name of the connected reader
        ///		OUT DWORD State // Current status the connection
        ///		OUT DWORD ActiveProtocol // protocol actually in use
        ///		OUT BYTE Atr[] // ATR data buffer
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardStatus(
            [In] IntPtr card,
            [In, Out] ref State status,
            [In, Out] ref Protocol protocol
            );

        /// <summary>
        /// Sends a C-APDU to the smart card contained in the reader connected to by <see cref="SCardConnect"/>.
        /// The card responds and stores this R-APDU in <paramref name="recvBuffer"/> and it's length in <paramref name="recvSize"/>.
        /// <para>Use only WITH <paramref name="recvSize"/>=<see cref="AutoAllocate"/></para>
        /// <para><b>WARNING</b>: SCARD_AUTOALLOCATE is NOT supported on Windows XP and below (<c>winscard.dll</c> limitation)</para>
        /// </summary>
        /// <param name="card">Connection made from SCardConnect</param>
        /// <param name="sendPci">Structure of protocol information</param>
        /// <param name="sendBuffer">C-APDU to send to the card</param>
        /// <param name="sendSize">Length of the C-APDU</param>
        /// <param name="recvPci">Structure of protocol information</param>
        /// <param name="recvBuffer">R-APDU from the card. <paramref name="recvBuffer"/> should be <c>null</c> as the allocation will be automatic (otherwise initial instance will be lost).</param>
        /// <param name="recvSize">On input, <para>MUST be <see cref="AutoAllocate"/>, or this method will fail. On output, receives the number of bytes in <paramref name="recvBuffer"/>.</para></param>
        /// <returns><see cref="ErrorCode.Success"/> if succeeded.</returns>
        /// <remarks>
        /// PC/SC: class <c>SCARDCOMM</c>
        /// <code lang="none">
        /// RESPONSECODE Transmit(
        ///		IN SCARD_IO_HEADER SendPci // Send protocol structure
        ///		IN BYTE[] SendBuffer // Data buffer for send data
        ///		IN OUT SCARD_IO_HEADER RecvPci // Receive protocol structure
        ///		IN OUT BYTE[] RecvBuffer // Data buffer for receive data
        ///		OUT DWORD RecvLength // Length of received data
        /// )
        /// </code>
        /// </remarks>
        ErrorCode SCardTransmit(
            [In] IntPtr card,
            [In] ref AbstractIoRequest sendPci,
            [In] byte[] sendBuffer,
            [In] UInt32 sendSize,
            [In, Out] ref AbstractIoRequest recvPci,
            [In, Out] ref byte[] recvBuffer,
            [In, Out] ref UInt32 recvSize
            );

        /*        {
                    ErrorCode ret;

                    IntPtr ptrsendPci = Marshal.AllocHGlobal(Marshal.SizeOf(sendPci));
                    Marshal.StructureToPtr(sendPci, ptrsendPci, true);
                    IntPtr ptrrecvPci = Marshal.AllocHGlobal(Marshal.SizeOf(recvPci));
                    Marshal.StructureToPtr(recvPci, ptrrecvPci, true);

                    unsafe
                    {
                        if (recvSize == SCARD_AUTOALLOCATE)
                        {
                            // winscard.dll supports SCARD_AUTOALLOCATE only since Windows vista; winscard.dll is able to propose l recvSize to be used with all versions (XP+)
                            // pcsclite does not support recvBuffer = null, so no automatic discovery of recvSize
                            // For more portability: Wrapper don't use the native winscard.dll's SCARD_AUTOALLOCATE
                            recvSize = SafePrimitives.defaultBufferSize;
                            recvBuffer = new byte[recvSize];
                            fixed (byte* psendBuffer = sendBuffer)
                            fixed (uint* precvSize = &recvSize)
                            fixed (byte* precvBuffer = recvBuffer)
                            {
                                ret = UnsafePrimitives.SCardTransmit(
                                    (void*)card,
                                    (void*)ptrsendPci,
                                    psendBuffer,
                                    sendSize,
                                    (void*)ptrrecvPci,
                                    precvBuffer,
                                    precvSize
                                );
                            }
                            if (ret == ErrorCode.SCARD_S_SUCCESS)
                            {
                                Array.Resize<byte>(ref recvBuffer, (int)recvSize);
                            }
                        }
                        else
                        {
                            //TODO Seems to be problems with pcsclite in this case...
                            fixed (byte* psendBuffer = sendBuffer)
                            fixed (uint* precvSize = &recvSize)
                            fixed (byte* precvBuffer = recvBuffer)
                            {
                                ret = UnsafePrimitives.SCardTransmit(
                                    (void*)card,
                                    (void*)ptrsendPci,
                                    psendBuffer,
                                    sendSize,
                                    (void*)ptrrecvPci,
                                    precvBuffer,
                                    precvSize
                                );
                            }
                        }
                    }

                    Marshal.FreeHGlobal(ptrsendPci);
                    Marshal.FreeHGlobal(ptrrecvPci);

                    return ret;
                }*/

        #endregion

        /// <summary>
        /// Build an <see cref="AbstractReaderState"/> for the current OS.
        /// </summary>
        /// <param name="readerName">Name of the reader.</param>
        /// <param name="currentState">Current state.</param>
        /// <param name="eventState">Expected state.</param>
        /// <returns>The concrete <see cref="AbstractReaderState"/> object.</returns>
        AbstractReaderState CreateReaderStateInstance(
            string readerName,
            EventState currentState,
            EventState eventState
            );

        /// <summary>
        /// Builds an <see cref="AbstractIoRequest"/> for the current OS.
        /// </summary>
        /// <param name="protocol">T Protocol.</param>
        /// <returns>The concrete <see cref="AbstractIoRequest"/> object.</returns>
        AbstractIoRequest CreateIoRequestInstance(
            Protocol protocol
            );
    }
}