using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for errors that can be raised by WinSCard
    /// </summary>
    public enum ErrorCode : uint
    {
        /// <summary>No error was encountered.</summary>
        SCARD_S_SUCCESS = 0x00000000,
        /// <summary>An internal consistency check failed.</summary>
        SCARD_F_INTERNAL_ERROR = 0x80100001,
        /// <summary>The action was cancelled by an SCardCancel request.</summary>
        SCARD_E_CANCELLED = 0x80100002,
        /// <summary>The supplied handle was invalid.</summary>
        SCARD_E_INVALID_HANDLE = 0x80100003,
        /// <summary>One or more of the supplied parameters could not be properly interpreted.</summary>
        SCARD_E_INVALID_PARAMETER = 0x80100004,
        /// <summary>Registry startup information is missing or invalid.</summary>
        SCARD_E_INVALID_TARGET = 0x80100005,
        /// <summary>Not enough memory available to complete this commandAPDU.</summary>
        SCARD_E_NO_MEMORY = 0x80100006,
        /// <summary>An internal consistency timer has expired.</summary>
        SCARD_F_WAITED_TOO_LONG = 0x80100007,
        /// <summary>The data buffer to receive returned data is too small for the returned data.</summary>
        SCARD_E_INSUFFICIENT_BUFFER = 0x80100008,
        /// <summary>The specified reader name is not recognized.</summary>
        SCARD_E_UNKNOWN_READER = 0x80100009,
        /// <summary>The user-specified timeout value has expired.</summary>
        SCARD_E_TIMEOUT = 0x8010000A,
        /// <summary>The smart card cannot be accessed because of other connections outstanding.</summary>
        SCARD_E_SHARING_VIOLATION = 0x8010000B,
        /// <summary>The operation requires a Smart Card, but no Smart Card is currently in the device.</summary>
        SCARD_E_NO_SMARTCARD = 0x8010000C,
        /// <summary>The specified smart card name is not recognized.</summary>
        SCARD_E_UNKNOWN_CARD = 0x8010000D,
        /// <summary>The system could not dispose of the media in the requested manner.</summary>
        SCARD_E_CANT_DISPOSE = 0x8010000E,
        /// <summary>The requested protocols are incompatible with the protocol currently in use with the smart card.</summary>
        SCARD_E_PROTO_MISMATCH = 0x8010000F,
        /// <summary>The reader or smart card is not ready to accept commands.</summary>
        SCARD_E_NOT_READY = 0x80100010,
        /// <summary>One or more of the supplied parameters values could not be properly interpreted.</summary>
        SCARD_E_INVALID_VALUE = 0x80100011,
        /// <summary>The action was cancelled by the system, presumably to log off or shut down.</summary>
        SCARD_E_SYSTEM_CANCELLED = 0x80100012,
        /// <summary>An internal communications error has been detected.</summary>
        SCARD_F_COMM_ERROR = 0x80100013,
        /// <summary>An internal error has been detected, but the source is unknown.</summary>
        SCARD_F_UNKNOWN_ERROR = 0x80100014,
        /// <summary>An ATR obtained from the registry is not a valid ATR string.</summary>
        SCARD_E_INVALID_ATR = 0x80100015,
        /// <summary>An attempt was made to end a non-existent transaction.</summary>
        SCARD_E_NOT_TRANSACTED = 0x80100016,
        /// <summary>The specified reader is not currently available for use.</summary>
        SCARD_E_READER_UNAVAILABLE = 0x80100017,
        /// <summary>The operation has been aborted to allow the server application to exit.</summary>
        SCARD_P_SHUTDOWN = 0x80100018,
        /// <summary>The PCI Receive buffer was too small.</summary>
        SCARD_E_PCI_TOO_SMALL = 0x80100019,
        /// <summary>The reader driver does not meet minimal requirements for support.</summary>
        SCARD_E_READER_UNSUPPORTED = 0x8010001A,
        /// <summary>The reader driver did not produce a unique reader name.</summary>
        SCARD_E_DUPLICATE_READER = 0x8010001B,
        /// <summary>The smart card does not meet minimal requirements for support.</summary>
        SCARD_E_CARD_UNSUPPORTED = 0x8010001C,
        /// <summary>The Smart card resource manager is not running.</summary>
        SCARD_E_NO_SERVICE = 0x8010001D,
        /// <summary>The Smart card resource manager has shut down.</summary>
        SCARD_E_SERVICE_STOPPED = 0x8010001E,
        /// <summary>An unexpected card error has occurred.</summary>
        SCARD_E_UNEXPECTED = 0x8010001F,
        /// <summary>No Primary Provider can be found for the smart card.</summary>
        SCARD_E_ICC_INSTALLATION = 0x80100020,
        /// <summary>The requested order of object creation is not supported.</summary>
        SCARD_E_ICC_CREATEORDER = 0x80100021,
        /// <summary> This smart card does not support the requested feature.</summary>
        SCARD_E_UNSUPPORTED_FEATURE = 0x80100022,
        /// <summary>The identified directory does not exist in the smart card.</summary>
        SCARD_E_DIR_NOT_FOUND = 0x80100023,
        /// <summary>The identified file does not exist in the smart card.</summary>
        SCARD_E_FILE_NOT_FOUND = 0x80100024,
        /// <summary>The supplied path does not represent a smart card directory.</summary>
        SCARD_E_NO_DIR = 0x80100025,
        /// <summary>The supplied path does not represent a smart card file.</summary>
        SCARD_E_NO_FILE = 0x80100026,
        /// <summary>Access is denied to this file.</summary>
        SCARD_E_NO_ACCESS = 0x80100027,
        /// <summary>The smartcard does not have enough memory to store the information.</summary>
        SCARD_E_WRITE_TOO_MANY = 0x80100028,
        /// <summary>There was an error trying to set the smart card file object pointer.</summary>
        SCARD_E_BAD_SEEK = 0x80100029,
        /// <summary>The supplied PIN is incorrect.</summary>
        SCARD_E_INVALID_CHV = 0x8010002A,
        /// <summary>An unrecognized error code was returned from a layered component.</summary>
        SCARD_E_UNKNOWN_RES_MNG = 0x8010002B,
        /// <summary>The requested certificate does not exist.</summary>
        SCARD_E_NO_SUCH_CERTIFICATE = 0x8010002C,
        /// <summary>The requested certificate could not be obtained.</summary>
        SCARD_E_CERTIFICATE_UNAVAILABLE = 0x8010002D,
        /// <summary>Cannot find a smart card reader.</summary>
        SCARD_E_NO_READERS_AVAILABLE = 0x8010002E,
        /// <summary>A communications error with the smart card has been detected.  Retry the operation.</summary>
        SCARD_E_COMM_DATA_LOST = 0x8010002F,
        /// <summary>The requested key container does not exist on the smart card.</summary>
        SCARD_E_NO_KEY_CONTAINER = 0x80100030,
        /// <summary>The Smart card resource manager is too busy to complete this operation.</summary>
        SCARD_E_SERVER_TOO_BUSY = 0x80100031,
        /// <summary>The reader cannot communicate with the smart card, due to ATR configuration conflicts.</summary>
        SCARD_W_UNSUPPORTED_CARD = 0x80100065,
        /// <summary>The smart card is not responding to a reset.</summary>
        SCARD_W_UNRESPONSIVE_CARD = 0x80100066,
        /// <summary>Power has been removed from the smart card, so that further communication is not possible.</summary>
        SCARD_W_UNPOWERED_CARD = 0x80100067,
        /// <summary>The smart card has been reset, so any shared state information is invalid.</summary>
        SCARD_W_RESET_CARD = 0x80100068,
        /// <summary>The smart card has been removed, so that further communication is not possible.</summary>
        SCARD_W_REMOVED_CARD = 0x80100069,
        /// <summary>Access was denied because of a security violation.</summary>
        SCARD_W_SECURITY_VIOLATION = 0x8010006A,
        /// <summary>The card cannot be accessed because the wrong PIN was presented.</summary>
        SCARD_W_WRONG_CHV = 0x8010006B,
        /// <summary>The card cannot be accessed because the maximum number of PIN entry attempts has been reached.</summary>
        SCARD_W_CHV_BLOCKED = 0x8010006C,
        /// <summary>The end of the smart card file has been reached.</summary>
        SCARD_W_EOF = 0x8010006D,
        /// <summary>The action was cancelled by the user.</summary>
        SCARD_W_CANCELLED_BY_USER = 0x8010006E,
        /// <summary>No PIN was presented to the smart card.</summary>
        SCARD_W_CARD_NOT_AUTHENTICATED = 0x8010006F,

        // Windows errors that can be raised

        /// <summary>The handle is invalid.</summary>
        ERROR_INVALID_HANDLE = 6,
        /// <summary>The parameter is incorrect.</summary>
        ERROR_INVALID_PARAMETER = 87
    }
}
