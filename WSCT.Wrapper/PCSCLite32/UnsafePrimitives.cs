using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.PCSCLite32
{
    /// <summary>
    /// Unsafe api coming from winscard.dll library
    /// </summary>
    class UnsafePrimitives 
    {
        internal const string PCSC_LIB = "libpcsclite.so.1";

        #region SCardCancel

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardCancel(
            [In] void* context
            );

        #endregion

        #region SCardConnect

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardConnect(
            [In] void* context,
            [In] char* readerName,
            [In] uint shareMode,
            [In] uint preferedProtocol,
            [In, Out] void** card,
            [In, Out] uint* activeProtocol
            );

        #endregion

        #region SCardControl

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern ErrorCode SCardControl(
            [In] IntPtr card,
            [In] UInt32 controlCode,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            [In] Byte[] sendBuffer,
            [In] UInt32 sendSize,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            [In, Out] ref Byte[] recvBuffer,
            [In] UInt32 recvSize,
            [In, Out] ref UInt32 returnedSize
            );

        #endregion

        #region SCardDisconnect

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardDisconnect(
            [In] void* card,
            [In] uint disposition
            );

        #endregion

        #region SCardEstablishContext

        [DllImport(PCSC_LIB)]
        public static extern unsafe ErrorCode SCardEstablishContext(
            [In] uint scope,
            [In] void* notUsed1,
            [In] void* notUsed2,
            [In, Out] void** context
            );

        #endregion

        #region SCardFreeMemory

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardFreeMemory(
            void* context,
            void* resourceToFree
            );

        #endregion

        #region SCardGetAttrib

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardGetAttrib(
            [In] void* card,
            [In] uint attributeId,
            [In, Out] byte* recvAttribute,
            [In, Out] uint* recvAttributeSize
            );

        #endregion

        #region SCardGetStatusChange

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardGetStatusChange(
            void* hContext,
            uint dwTimeout,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            SCARD_READERSTATE[] rgReaderStates,
            uint cReaders
            );

        #endregion

        #region SCardIsValidContext

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardIsValidContext(
            [In] void* context
            );

        #endregion

        #region SCardListReaders

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardListReaders(
            [In] void* context,
            [In] char* groups,
            [In, Out] char* readers,
            [In, Out] uint* size
            );

        #endregion

        #region SCardListReaderGroups

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardListReaderGroups(
            [In] void* context,
            [In, Out] char* groups,
            [In, Out] uint* size
            );

        #endregion

        #region SCardReconnect

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardReconnect(
            [In] void* card,
            [In] uint shareMode,
            [In] uint preferedProtocol,
            [In] uint initialisation,
            [In, Out] uint* activeProtocol
            );

        #endregion

        #region SCardReleaseContext

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardReleaseContext(
            void* context
            );

        #endregion

        #region SCardSetAttrib

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern ErrorCode SCardSetAttrib(
            [In] IntPtr card,
            [In] UInt32 attributeId,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            [In] Byte[] newAttribute,
            [In] UInt32 newAttributeSize
            );

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardSetAttrib(
            [In] void* card,
            [In] uint attributeId,
            [In] byte* newAttribute,
            [In] uint newAttributeSize
            );

        #endregion

        #region SCardStatus

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardStatus(
            [In] void* card,
            [In, Out] char* readerName,
            [In, Out] uint* readerNameSize,
            [In, Out] uint* status,
            [In, Out] uint* protocol,
            [In, Out] byte* atr,
            [In, Out] uint* atrSize
            );

        [DllImport(PCSC_LIB, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardStatus(
            [In] IntPtr card,
            [In, Out] ref IntPtr readerName,
            [In, Out] ref UInt32 readerNameSize,
            [In, Out] ref State status,
            [In, Out] ref Protocol protocol,
            [In, Out] ref IntPtr atr,
            [In, Out] ref UInt32 atrSize
            );

        #endregion

        #region SCardTransmit

        [DllImport(PCSC_LIB)]
        public static extern unsafe ErrorCode SCardTransmit(
            [In] void* card,
            [In] /*SCARD_IO_REQUEST*/ void* sendPci,
            [In] byte* sendBuffer,
            [In] uint sendSize,
            [In, Out] /*SCARD_IO_REQUEST*/ void* recvPci,
            [In, Out] byte* recvBuffer,
            [In, Out] uint* recvSize
            );

        #endregion
    }
}