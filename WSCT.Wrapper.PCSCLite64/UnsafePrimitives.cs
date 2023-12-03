using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.PCSCLite64
{
    /// <summary>
    /// Unsafe api coming from winscard.dll library.
    /// </summary>
    internal class UnsafePrimitives
    {
        internal const string PcscLib = "libpcsclite.so.1";

        #region SCardCancel

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardCancel(
            [In] void* context
            );

        #endregion

        #region SCardConnect

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardConnect(
            [In] void* context,
            [In] char* readerName,
            [In] ulong shareMode,
            [In] ulong preferedProtocol,
            [In, Out] void** card,
            [In, Out] ulong* activeProtocol
            );

        #endregion

        #region SCardControl

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardControl(
            [In] void* card,
            [In] ulong controlCode,
            [In] byte* sendBuffer,
            [In] ulong sendSize,
            [In, Out]  byte* recvBuffer,
            [In] ulong recvSize,
            [In, Out] uint* returnedSize
            );

        #endregion

        #region SCardDisconnect

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardDisconnect(
            [In] void* card,
            [In] ulong disposition
            );

        #endregion

        #region SCardEstablishContext

        [DllImport(PcscLib)]
        public static extern unsafe ErrorCode SCardEstablishContext(
            [In] ulong scope,
            [In] void* notUsed1,
            [In] void* notUsed2,
            [In, Out] void** context
            );

        #endregion

        #region SCardFreeMemory

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardFreeMemory(
            void* context,
            void* resourceToFree
            );

        #endregion

        #region SCardGetAttrib

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardGetAttrib(
            [In] void* card,
            [In] ulong attributeId,
            [In, Out] byte* recvAttribute,
            [In, Out] ulong* recvAttributeSize
            );

        #endregion

        #region SCardGetStatusChange

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardGetStatusChange(
            void* hContext,
            ulong dwTimeout,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] ScardReaderState[] rgReaderStates,
            ulong cReaders
            );

        #endregion

        #region SCardIsValidContext

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardIsValidContext(
            [In] void* context
            );

        #endregion

        #region SCardListReaders

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardListReaders(
            [In] void* context,
            [In] char* groups,
            [In, Out] char* readers,
            [In, Out] ulong* size
            );

        #endregion

        #region SCardListReaderGroups

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardListReaderGroups(
            [In] void* context,
            [In, Out] char* groups,
            [In, Out] ulong* size
            );

        #endregion

        #region SCardReconnect

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardReconnect(
            [In] void* card,
            [In] ulong shareMode,
            [In] ulong preferedProtocol,
            [In] ulong initialisation,
            [In, Out] ulong* activeProtocol
            );

        #endregion

        #region SCardReleaseContext

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardReleaseContext(
            void* context
            );

        #endregion

        #region SCardSetAttrib

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern ErrorCode SCardSetAttrib(
            [In] IntPtr card,
            [In] UInt64 attributeId,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] newAttribute,
            [In] UInt64 newAttributeSize
            );

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardSetAttrib(
            [In] void* card,
            [In] ulong attributeId,
            [In] byte* newAttribute,
            [In] ulong newAttributeSize
            );

        #endregion

        #region SCardStatus

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern unsafe ErrorCode SCardStatus(
            [In] void* card,
            [In, Out] char* readerName,
            [In, Out] ulong* readerNameSize,
            [In, Out] ulong* status,
            [In, Out] ulong* protocol,
            [In, Out] byte* atr,
            [In, Out] ulong* atrSize
            );

        [DllImport(PcscLib, CharSet = CharSet.Auto)]
        public static extern ErrorCode SCardStatus(
            [In] IntPtr card,
            [In, Out] ref IntPtr readerName,
            [In, Out] ref UInt64 readerNameSize,
            [In, Out] ref State status,
            [In, Out] ref Protocol protocol,
            [In, Out] ref IntPtr atr,
            [In, Out] ref UInt64 atrSize
            );

        #endregion

        #region SCardTransmit

        [DllImport(PcscLib)]
        //		[DllImport("/home/sylvain/Documents/libtest.so.1")]
        public static extern unsafe ErrorCode SCardTransmit(
            [In] void* card,
            [In] /*SCARD_IO_REQUEST*/ void* sendPci,
            [In] byte* sendBuffer,
            [In] ulong sendSize,
            [In, Out] /*SCARD_IO_REQUEST*/ void* recvPci,
            [In, Out] byte* recvBuffer,
            [In, Out] ulong* recvSize
            );

        #endregion
    }
}