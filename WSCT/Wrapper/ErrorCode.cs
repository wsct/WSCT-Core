namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for errors that can be raised by WinSCard.
    /// </summary>
    public enum ErrorCode : uint
    {
        /// <summary>
        /// Success: No error was encountered.
        /// </summary>
        Success = 0x00000000,

        /// <summary>
        /// Fatal: An internal consistency check failed.
        /// </summary>
        InternalError = 0x80100001,

        /// <summary>
        /// Error: The action was cancelled by an SCardCancel request.
        /// </summary>
        Cancelled = 0x80100002,

        /// <summary>
        /// Error: The supplied handle was invalid.
        /// </summary>
        InvalidHandle = 0x80100003,

        /// <summary>
        /// Error: One or more of the supplied parameters could not be properly interpreted.
        /// </summary>
        InvalidParameter = 0x80100004,

        /// <summary>
        /// Error: Registry startup information is missing or invalid.
        /// </summary>
        InvalidTarget = 0x80100005,

        /// <summary>
        /// Error: Not enough memory available to complete this commandAPDU.
        /// </summary>
        NoMemory = 0x80100006,

        /// <summary>
        /// Fatal: An internal consistency timer has expired.
        /// </summary>
        WaitedTooLong = 0x80100007,

        /// <summary>
        /// Error: The data buffer to receive returned data is too small for the returned data.
        /// </summary>
        InsufficientBuffer = 0x80100008,

        /// <summary>
        /// Error: The specified reader name is not recognized.
        /// </summary>
        UnknownReader = 0x80100009,

        /// <summary>
        /// Error: The user-specified timeout value has expired.
        /// </summary>
        Timeout = 0x8010000A,

        /// <summary>
        /// Error: The smart card cannot be accessed because of other connections outstanding.
        /// </summary>
        SharingViolation = 0x8010000B,

        /// <summary>
        /// Error: The operation requires a Smart Card, but no Smart Card is currently in the device.
        /// </summary>
        NoSmartcard = 0x8010000C,

        /// <summary>
        /// Error: The specified smart card name is not recognized.
        /// </summary>
        UnknownCard = 0x8010000D,

        /// <summary>
        /// Error: The system could not dispose of the media in the requested manner.
        /// </summary>
        CantDispose = 0x8010000E,

        /// <summary>
        /// Error: The requested protocols are incompatible with the protocol currently in use with the smart card.
        /// </summary>
        ProtoMismatch = 0x8010000F,

        /// <summary>
        /// Error: The reader or smart card is not ready to accept commands.
        /// </summary>
        NotReady = 0x80100010,

        /// <summary>
        /// Error: One or more of the supplied parameters values could not be properly interpreted.
        /// </summary>
        InvalidValue = 0x80100011,

        /// <summary>
        /// Error: The action was cancelled by the system, presumably to log off or shut down.
        /// </summary>
        SystemCancelled = 0x80100012,

        /// <summary>
        /// Fatal: An internal communications error has been detected.
        /// </summary>
        CommError = 0x80100013,

        /// <summary>
        /// Fatal: An internal error has been detected, but the source is unknown.
        /// </summary>
        UnknownError = 0x80100014,

        /// <summary>
        /// Error: An ATR obtained from the registry is not a valid ATR String.
        /// </summary>
        InvalidAtr = 0x80100015,

        /// <summary>
        /// Error: An attempt was made to end a non-existent transaction.
        /// </summary>
        NotTransacted = 0x80100016,

        /// <summary>
        /// Error: The specified reader is not currently available for use.
        /// </summary>
        ReaderUnavailable = 0x80100017,

        /// <summary>
        /// P: The operation has been aborted to allow the server application to exit.
        /// </summary>
        Shutdown = 0x80100018,

        /// <summary>
        /// The PCI Receive buffer was too small.
        /// </summary>
        PciTooSmall = 0x80100019,

        /// <summary>
        /// The reader driver does not meet minimal requirements for support.
        /// </summary>
        ReaderUnsupported = 0x8010001A,

        /// <summary>
        /// The reader driver did not produce a unique reader name.
        /// </summary>
        DuplicateReader = 0x8010001B,

        /// <summary>
        /// The smart card does not meet minimal requirements for support.
        /// </summary>
        CardUnsupported = 0x8010001C,

        /// <summary>
        /// The Smart card resource manager is not running.
        /// </summary>
        NoService = 0x8010001D,

        /// <summary>
        /// The Smart card resource manager has shut down.
        /// </summary>
        ServiceStopped = 0x8010001E,

        /// <summary>
        /// An unexpected card error has occurred.
        /// </summary>
        Unexpected = 0x8010001F,

        /// <summary>
        /// No Primary Provider can be found for the smart card.
        /// </summary>
        IccInstallation = 0x80100020,

        /// <summary>
        /// The requested order of object creation is not supported.
        /// </summary>
        IccCreateorder = 0x80100021,

        /// <summary>
        /// This smart card does not support the requested feature.
        /// </summary>
        UnsupportedFeature = 0x80100022,

        /// <summary>
        /// The identified directory does not exist in the smart card.
        /// </summary>
        DirNotFound = 0x80100023,

        /// <summary>
        /// The identified file does not exist in the smart card.
        /// </summary>
        FileNotFound = 0x80100024,

        /// <summary>
        /// The supplied path does not represent a smart card directory.
        /// </summary>
        NoDir = 0x80100025,

        /// <summary>
        /// The supplied path does not represent a smart card file.
        /// </summary>
        NoFile = 0x80100026,

        /// <summary>
        /// Access is denied to this file.
        /// </summary>
        NoAccess = 0x80100027,

        /// <summary>
        /// The smartcard does not have enough memory to store the information.
        /// </summary>
        WriteTooMany = 0x80100028,

        /// <summary>
        /// There was an error trying to set the smart card file object pointer.
        /// </summary>
        BadSeek = 0x80100029,

        /// <summary>
        /// The supplied PIN is incorrect.
        /// </summary>
        InvalidChv = 0x8010002A,

        /// <summary>
        /// An unrecognized error code was returned from a layered component.
        /// </summary>
        UnknownResMng = 0x8010002B,

        /// <summary>
        /// The requested certificate does not exist.
        /// </summary>
        NoSuchCertificate = 0x8010002C,

        /// <summary>
        /// The requested certificate could not be obtained.
        /// </summary>
        CertificateUnavailable = 0x8010002D,

        /// <summary>
        /// Cannot find a smart card reader.
        /// </summary>
        NoReadersAvailable = 0x8010002E,

        /// <summary>
        /// A communications error with the smart card has been detected. Retry the operation.
        /// </summary>
        CommDataLost = 0x8010002F,

        /// <summary>
        /// The requested key container does not exist on the smart card.
        /// </summary>
        NoKeyContainer = 0x80100030,

        /// <summary>
        /// The Smart card resource manager is too busy to complete this operation.
        /// </summary>
        ServerTooBusy = 0x80100031,

        /// <summary>
        /// Warning: The reader cannot communicate with the smart card, due to ATR configuration conflicts.
        /// </summary>
        UnsupportedCard = 0x80100065,

        /// <summary>
        /// Warning: The smart card is not responding to a reset.
        /// </summary>
        UnresponsiveCard = 0x80100066,

        /// <summary>
        /// Warning: Power has been removed from the smart card, so that further communication is not possible.
        /// </summary>
        UnpoweredCard = 0x80100067,

        /// <summary>
        /// Warning: The smart card has been reset, so any shared state information is invalid.
        /// </summary>
        ResetCard = 0x80100068,

        /// <summary>
        /// Warning: The smart card has been removed, so that further communication is not possible.
        /// </summary>
        RemovedCard = 0x80100069,

        /// <summary>
        /// Warning: Access was denied because of a security violation.
        /// </summary>
        SecurityViolation = 0x8010006A,

        /// <summary>
        /// Warning: The card cannot be accessed because the wrong PIN was presented.
        /// </summary>
        WrongChv = 0x8010006B,

        /// <summary>
        /// Warning: The card cannot be accessed because the maximum number of PIN entry attempts has been reached.
        /// </summary>
        ChvBlocked = 0x8010006C,

        /// <summary>
        /// Warning: The end of the smart card file has been reached.
        /// </summary>
        Eof = 0x8010006D,

        /// <summary>
        /// Warning: The action was cancelled by the user.
        /// </summary>
        CancelledByUser = 0x8010006E,

        /// <summary>
        /// Warning: No PIN was presented to the smart card.
        /// </summary>
        CardNotAuthenticated = 0x8010006F,

        // Windows errors that can be raised

        /// <summary>
        /// The handle is invalid.
        /// </summary>
        ErrorInvalidHandle = 6,

        /// <summary>
        /// The parameter is incorrect.
        /// </summary>
        ErrorInvalidParameter = 87
    }
}