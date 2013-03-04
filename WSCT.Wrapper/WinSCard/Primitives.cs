using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.WinSCard
{
    class Primitives : IPrimitives
    {
        #region >> Fields


        /// <summary>
        /// PC/SC resources management
        /// </summary>
        internal static IntPtr activeContext;

        UInt32 _defaultBufferSize = 1024;

        #endregion

        #region >> IPrimitives Members

        /// <inheritdoc />
        public uint SCARD_AUTOALLOCATE
        {
            get { return UInt32.MaxValue; }
        }

        public uint defaultBufferSize
        {
            get
            {
                return _defaultBufferSize;
            }
            set
            {
                _defaultBufferSize = value;
            }
        }

        /// <inheritdoc />
        public ErrorCode SCardCancel(IntPtr context)
        {
            unsafe
            {
                return UnsafePrimitives.SCardCancel((void*)context);
            }
        }

        /// <inheritdoc />
        public ErrorCode SCardConnect(IntPtr context, String readerName, ShareMode shareMode, Protocol preferedProtocol, ref IntPtr card, ref Protocol activeProtocol)
        {
            ErrorCode ret;
            unsafe
            {
                uint uactiveProtocol = (uint)activeProtocol;
                readerName += "\0";
                fixed (char* preaderName = readerName)
                fixed (void* pcard = &card)
                {
                    ret = UnsafePrimitives.SCardConnect(
                        (void*)context,
                        (char*)preaderName,
                        (uint)shareMode,
                        (uint)preferedProtocol,
                        (void**)pcard,
                        &uactiveProtocol
                    );
                    activeProtocol = (Protocol)uactiveProtocol;
                }
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardControl(IntPtr card, uint controlCode, byte[] sendBuffer, uint sendSize, ref byte[] recvBuffer, uint recvSize, ref uint returnedSize)
        {
            return UnsafePrimitives.SCardControl(card, controlCode, sendBuffer, sendSize, ref recvBuffer, recvSize, ref returnedSize);
        }

        /// <inheritdoc />
        public ErrorCode SCardDisconnect(IntPtr card, Disposition disposition)
        {
            unsafe
            {
                return UnsafePrimitives.SCardDisconnect(
                    (void*)card,
                    (uint)disposition
                );
            }
        }

        /// <inheritdoc />
        public ErrorCode SCardEstablishContext(uint scope, object notUsed1, object notUsed2, ref IntPtr context)
        {
            ErrorCode ret;

            unsafe
            {
                fixed (void* pcontext = &context)
                {
                    ret = UnsafePrimitives.SCardEstablishContext(
                        (uint)scope,
                        null,
                        null,
                        (void**)pcontext
                    );
                }
                if (ret == ErrorCode.SCARD_S_SUCCESS)
                    activeContext = context;
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardGetAttrib(IntPtr card, UInt32 attributeId, ref Byte[] recvAttribute, ref UInt32 recvAttributeSize)
        {
            ErrorCode ret;

            unsafe
            {
                if (recvAttributeSize == SCARD_AUTOALLOCATE)
                {
                    fixed (uint* precvAttributeSize = &recvAttributeSize)
                    {
                        fixed (byte* precvAttribute = recvAttribute)
                        {
                            ret = UnsafePrimitives.SCardGetAttrib(
                                (void*)card,
                                attributeId,
                                precvAttribute,
                                precvAttributeSize
                            );
                        }
                        if (ret == ErrorCode.SCARD_S_SUCCESS)
                        {
                            recvAttribute = new byte[*precvAttributeSize];
                            fixed (byte* precvAttribute = recvAttribute)
                            {
                                ret = UnsafePrimitives.SCardGetAttrib(
                                    (void*)card,
                                    attributeId,
                                    precvAttribute,
                                    precvAttributeSize
                                );
                            }
                        }
                        else
                        {
                            recvAttributeSize = 0;
                            recvAttribute = new Byte[0];
                        }
                    }
                }
                else
                {
                    fixed (uint* precvAttributeSize = &recvAttributeSize)
                    fixed (byte* precvAttribute = recvAttribute)
                    {
                        ret = UnsafePrimitives.SCardGetAttrib(
                            (void*)card,
                            attributeId,
                            precvAttribute,
                            precvAttributeSize
                        );

                    }
                }
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardGetStatusChange(IntPtr context, uint timeout, AbstractReaderState[] readerStates, uint readers)
        {
            ErrorCode ret;

            SCARD_READERSTATE[] scReaderStates = new SCARD_READERSTATE[readerStates.Length];
            for (int i = 0; i < readerStates.Length; i++)
            {
                scReaderStates[i] = ((ReaderState)readerStates[i]).scReaderState;
                scReaderStates[i].atr = null;
            }

            unsafe
            {
                ret = UnsafePrimitives.SCardGetStatusChange((void*)context, timeout, scReaderStates, (uint)scReaderStates.Length);
            }

            for (int i = 0; i < readerStates.Length; i++)
            {
                ((ReaderState)readerStates[i]).scReaderState = scReaderStates[i];
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardIsValidContext(IntPtr context)
        {
            unsafe
            {
                return UnsafePrimitives.SCardIsValidContext(
                    (void*)context
                );
            }
        }

        /// <inheritdoc />
        public ErrorCode SCardListReaders(IntPtr context, string groups, ref IntPtr readers, ref uint size)
        {
            ErrorCode err;

            unsafe
            {
                fixed (uint* psize = &size)
                fixed (char* pgroups = groups)
                {
                    if (size == SCARD_AUTOALLOCATE)
                    {
                        char* preaders;
                        err = UnsafePrimitives.SCardListReaders(
                            (void*)context,
                            (char*)pgroups,
                            (char*)&preaders,
                            psize
                        );
                        readers = UnsafePrimitives.charPointerToIntPtr(preaders, *psize);
                        UnsafePrimitives.SCardFreeMemory((void*)context, (void*)preaders);
                    }
                    else
                    {
                        err = UnsafePrimitives.SCardListReaders(
                            (void*)context,
                            (char*)pgroups,
                            (char*)readers,
                            psize
                        );
                    }
                    size = *psize;
                }
            }

            return err;
        }

        /// <inheritdoc />
        public ErrorCode SCardListReaderGroups(IntPtr context, ref IntPtr groups, ref uint size)
        {
            ErrorCode err;

            unsafe
            {
                fixed (uint* psize = &size)
                {
                    if (size == SCARD_AUTOALLOCATE)
                    {
                        char* pgroups;
                        err = UnsafePrimitives.SCardListReaderGroups(
                            (void*)context,
                            (char*)&pgroups,
                            psize
                        );
                        char[] cgroups = new char[*psize];
                        fixed (char* pcgroups = cgroups)
                        {
                            for (int i = 0; i < *psize; i++)
                                pcgroups[i] = pgroups[i];
                            UnsafePrimitives.SCardFreeMemory((void*)context, (void*)pgroups);
                            groups = (IntPtr)pcgroups;
                        }
                    }
                    else
                    {
                        err = UnsafePrimitives.SCardListReaderGroups(
                            (void*)context,
                            (char*)groups,
                            psize
                        );
                    }
                    size = *psize;
                }
            }

            return err;
        }

        /// <inheritdoc />
        public ErrorCode SCardReconnect(IntPtr card, ShareMode shareMode, Protocol preferedProtocol, Disposition initialisation, ref Protocol activeProtocol)
        {
            ErrorCode err;

            unsafe
            {
                uint protocol = (uint)activeProtocol;
                err = UnsafePrimitives.SCardReconnect(
                    (void*)card,
                    (uint)shareMode,
                    (uint)preferedProtocol,
                    (uint)initialisation,
                    (uint*)&protocol
                    );
                activeProtocol = (Protocol)protocol;
            }

            return err;
        }

        /// <inheritdoc />
        public ErrorCode SCardReleaseContext(IntPtr context)
        {
            ErrorCode ret;

            unsafe
            {
                ret = UnsafePrimitives.SCardReleaseContext(
                    (void*)context
                );
                if (ret == ErrorCode.SCARD_S_SUCCESS)
                    activeContext = IntPtr.Zero;
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardSetAttrib(IntPtr card, uint attributeId, byte[] newAttribute, uint newAttributeSize)
        {
            return UnsafePrimitives.SCardSetAttrib(card, attributeId, newAttribute, newAttributeSize);
        }

        /// <inheritdoc />
        public ErrorCode SCardStatus(IntPtr card, ref IntPtr readerName, ref uint readerNameSize, ref State status, ref Protocol protocol, ref IntPtr atr, ref uint atrSize)
        {
            ErrorCode ret = ErrorCode.SCARD_S_SUCCESS;

            unsafe
            {
                uint ustatus = (uint)status;
                uint uprotocol = (uint)protocol;
                fixed (uint* preaderNameSize = &readerNameSize)
                fixed (uint* patrSize = &atrSize)
                {
                    if (readerNameSize == SCARD_AUTOALLOCATE && atrSize == SCARD_AUTOALLOCATE)
                    {
                        char* preaderName;
                        byte* patr;
                        ret = UnsafePrimitives.SCardStatus(
                            (void*)card,
                            (char*)&preaderName,
                            (uint*)preaderNameSize,
                            (uint*)&ustatus,
                            (uint*)&uprotocol,
                            (byte*)&patr,
                            (uint*)patrSize
                        );
                        readerName = UnsafePrimitives.charPointerToIntPtr(preaderName, *preaderNameSize);
                        UnsafePrimitives.SCardFreeMemory((void*)activeContext, (void*)preaderName);
                        atr = UnsafePrimitives.bytePointerToIntPtr(patr, *patrSize);
                        UnsafePrimitives.SCardFreeMemory((void*)activeContext, (void*)patr);
                    }
                    else if (readerNameSize == SCARD_AUTOALLOCATE && atrSize != SCARD_AUTOALLOCATE)
                    {
                        char* preaderName;
                        ret = UnsafePrimitives.SCardStatus(
                            (void*)card,
                            (char*)&preaderName,
                            (uint*)preaderNameSize,
                            (uint*)&ustatus,
                            (uint*)&uprotocol,
                            (byte*)atr,
                            (uint*)patrSize
                        );
                        readerName = UnsafePrimitives.charPointerToIntPtr(preaderName, *preaderNameSize);
                        UnsafePrimitives.SCardFreeMemory((void*)activeContext, (void*)preaderName);
                    }
                    else if (readerNameSize != SCARD_AUTOALLOCATE && atrSize == SCARD_AUTOALLOCATE)
                    {
                        byte* patr;
                        ret = UnsafePrimitives.SCardStatus(
                            (void*)card,
                            (char*)readerName,
                            (uint*)preaderNameSize,
                            (uint*)&ustatus,
                            (uint*)&uprotocol,
                            (byte*)&patr,
                            (uint*)patrSize
                        );
                        atr = UnsafePrimitives.bytePointerToIntPtr(patr, *patrSize);
                        UnsafePrimitives.SCardFreeMemory((void*)activeContext, (void*)patr);
                    }
                    else
                    {
                        ret = UnsafePrimitives.SCardStatus(
                            (void*)card,
                            (char*)readerName,
                            (uint*)readerNameSize,
                            (uint*)&ustatus,
                            (uint*)&uprotocol,
                            (byte*)atr,
                            (uint*)atrSize
                        );
                    }
                    status = (State)ustatus;
                    protocol = (Protocol)uprotocol;
                    readerNameSize = *preaderNameSize;
                    atrSize = *patrSize;
                }
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardStatus(IntPtr card, ref string readerName, ref State state, ref Protocol protocol, ref byte[] atr)
        {
            IntPtr atrPtr = IntPtr.Zero;
            UInt32 zReaderNameSize = SCARD_AUTOALLOCATE;
            IntPtr zReaderNamePtr = new IntPtr();
            UInt32 atrSize = SCARD_AUTOALLOCATE;
            ErrorCode ret = UnsafePrimitives.SCardStatus(
                card,
                ref zReaderNamePtr,
                ref zReaderNameSize,
                ref state,
                ref protocol,
                ref atrPtr,
                ref atrSize
                );
            if (zReaderNamePtr == IntPtr.Zero)
                readerName = "";
            else
            {
                String readerStr = Marshal.PtrToStringAuto(zReaderNamePtr, (int)zReaderNameSize - 2);
                readerName = readerStr.ToString().Split(new char[] { '\0' })[0];
            }
            if (atrPtr == IntPtr.Zero)
                atr = new Byte[0];
            else
            {
                atr = new byte[atrSize];
                Marshal.Copy(atrPtr, atr, 0, (int)atrSize);
            }
            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardStatus(IntPtr card, ref State status, ref Protocol protocol)
        {
            ErrorCode ret = ErrorCode.SCARD_S_SUCCESS;

            unsafe
            {
                uint ustatus = (uint)status;
                uint uprotocol = (uint)protocol;
                char* readerName = null;
                uint readerNameSize = 0;
                byte* atr = null;
                uint atrSize = 0;
                ret = UnsafePrimitives.SCardStatus((void*)card, readerName, &readerNameSize, &ustatus, &uprotocol, atr, &atrSize);
                status = (State)ustatus;
                protocol = (Protocol)uprotocol;
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardTransmit(IntPtr card, ref AbstractIoRequest sendPci, byte[] sendBuffer, uint sendSize, ref AbstractIoRequest recvPci, ref byte[] recvBuffer, ref uint recvSize)
        {
            ErrorCode ret;

            IntPtr ptrsendPci = Marshal.AllocHGlobal(Marshal.SizeOf(((IoRequest)sendPci).scIoRequest));
            Marshal.StructureToPtr(((IoRequest)sendPci).scIoRequest, ptrsendPci, true);
            IntPtr ptrrecvPci = Marshal.AllocHGlobal(Marshal.SizeOf(((IoRequest)recvPci).scIoRequest));
            Marshal.StructureToPtr(((IoRequest)recvPci).scIoRequest, ptrrecvPci, true);

            unsafe
            {
                if (recvSize == SCARD_AUTOALLOCATE)
                {
                    // winscard.dll supports SCARD_AUTOALLOCATE only since Windows vista; winscard.dll is able to propose l recvSize to be used with all versions (XP+)
                    // pcsclite does not support recvBuffer = null, so no automatic discovery of recvSize
                    // For more portability: Wrapper don't use the native winscard.dll's SCARD_AUTOALLOCATE
                    recvSize = defaultBufferSize;
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
                        Array.Resize<Byte>(ref recvBuffer, (int)recvSize);
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
        }

        /// <inheritdoc />
        public AbstractReaderState createReaderStateInstance(string readerName, EventState currentState, EventState eventState)
        {
            return new ReaderState(readerName, currentState, eventState);
        }

        /// <inheritdoc />
        public AbstractIoRequest createIoRequestInstance(Protocol protocol)
        {
            return new IoRequest((uint)protocol);
        }

        #endregion
    }
}
