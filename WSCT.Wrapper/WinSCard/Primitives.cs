using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.WinSCard
{
    /// <summary>
    /// Wrapper of PC/SC (native winscard.dll) for windows.
    /// </summary>
    class Primitives : IPrimitives
    {
        #region >> Fields


        /// <summary>
        /// PC/SC resources management.
        /// </summary>
        internal static IntPtr ActiveContext;

        UInt32 _defaultBufferSize = 1024;

        #endregion

        #region >> IPrimitives Members

        /// <inheritdoc />
        public uint AutoAllocate
        {
            get { return UInt32.MaxValue; }
        }

        public uint DefaultBufferSize
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
                var uactiveProtocol = (uint)activeProtocol;
                readerName += "\0";
                fixed (char* preaderName = readerName)
                fixed (void* pcard = &card)
                {
                    ret = UnsafePrimitives.SCardConnect(
                        (void*)context,
                        preaderName,
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
                        scope,
                        null,
                        null,
                        (void**)pcontext
                    );
                }
                if (ret == ErrorCode.Success)
                    ActiveContext = context;
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardGetAttrib(IntPtr card, UInt32 attributeId, ref Byte[] recvAttribute, ref UInt32 recvAttributeSize)
        {
            ErrorCode ret;

            unsafe
            {
                if (recvAttributeSize == AutoAllocate)
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
                        if (ret == ErrorCode.Success)
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

            var scReaderStates = new ScardReaderState[readerStates.Length];
            for (var i = 0; i < readerStates.Length; i++)
            {
                scReaderStates[i] = ((ReaderState)readerStates[i]).ScReaderState;
                scReaderStates[i].atr = null;
            }

            unsafe
            {
                ret = UnsafePrimitives.SCardGetStatusChange((void*)context, timeout, scReaderStates, (uint)scReaderStates.Length);
            }

            for (var i = 0; i < readerStates.Length; i++)
            {
                ((ReaderState)readerStates[i]).ScReaderState = scReaderStates[i];
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
                    if (size == AutoAllocate)
                    {
                        char* preaders;
                        err = UnsafePrimitives.SCardListReaders(
                            (void*)context,
                            pgroups,
                            (char*)&preaders,
                            psize
                        );
                        if (err == ErrorCode.Success)
                        {
                            readers = UnsafePrimitives.CharPointerToIntPtr(preaders, *psize);
                            UnsafePrimitives.SCardFreeMemory((void*)context, preaders);
                        }
                    }
                    else
                    {
                        err = UnsafePrimitives.SCardListReaders(
                            (void*)context,
                            pgroups,
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
                    if (size == AutoAllocate)
                    {
                        char* pgroups;
                        err = UnsafePrimitives.SCardListReaderGroups(
                            (void*)context,
                            (char*)&pgroups,
                            psize
                        );
                        var cgroups = new char[*psize];
                        fixed (char* pcgroups = cgroups)
                        {
                            for (var i = 0; i < *psize; i++)
                                pcgroups[i] = pgroups[i];
                            UnsafePrimitives.SCardFreeMemory((void*)context, pgroups);
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
                var protocol = (uint)activeProtocol;
                err = UnsafePrimitives.SCardReconnect(
                    (void*)card,
                    (uint)shareMode,
                    (uint)preferedProtocol,
                    (uint)initialisation,
                    &protocol
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
                if (ret == ErrorCode.Success)
                    ActiveContext = IntPtr.Zero;
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
            ErrorCode ret;

            unsafe
            {
                var ustatus = (uint)status;
                var uprotocol = (uint)protocol;
                fixed (uint* preaderNameSize = &readerNameSize)
                fixed (uint* patrSize = &atrSize)
                {
                    if (readerNameSize == AutoAllocate && atrSize == AutoAllocate)
                    {
                        char* preaderName;
                        byte* patr;
                        ret = UnsafePrimitives.SCardStatus(
                            (void*)card,
                            (char*)&preaderName,
                            preaderNameSize,
                            &ustatus,
                            &uprotocol,
                            (byte*)&patr,
                            patrSize
                        );
                        if (ret == ErrorCode.Success)
                        {
                            readerName = UnsafePrimitives.CharPointerToIntPtr(preaderName, *preaderNameSize);
                            UnsafePrimitives.SCardFreeMemory((void*)ActiveContext, preaderName);
                            atr = UnsafePrimitives.BytePointerToIntPtr(patr, *patrSize);
                            UnsafePrimitives.SCardFreeMemory((void*)ActiveContext, patr);
                        }
                    }
                    else if (readerNameSize == AutoAllocate && atrSize != AutoAllocate)
                    {
                        char* preaderName;
                        ret = UnsafePrimitives.SCardStatus(
                            (void*)card,
                            (char*)&preaderName,
                            preaderNameSize,
                            &ustatus,
                            &uprotocol,
                            (byte*)atr,
                            patrSize
                        );
                        if (ret == ErrorCode.Success)
                        {
                            readerName = UnsafePrimitives.CharPointerToIntPtr(preaderName, *preaderNameSize);
                            UnsafePrimitives.SCardFreeMemory((void*)ActiveContext, preaderName);
                        }
                    }
                    else if (readerNameSize != AutoAllocate && atrSize == AutoAllocate)
                    {
                        byte* patr;
                        ret = UnsafePrimitives.SCardStatus(
                            (void*)card,
                            (char*)readerName,
                            preaderNameSize,
                            &ustatus,
                            &uprotocol,
                            (byte*)&patr,
                            patrSize
                        );
                        atr = UnsafePrimitives.BytePointerToIntPtr(patr, *patrSize);
                        UnsafePrimitives.SCardFreeMemory((void*)ActiveContext, patr);
                    }
                    else
                    {
                        ret = UnsafePrimitives.SCardStatus(
                            (void*)card,
                            (char*)readerName,
                            (uint*)readerNameSize,
                            &ustatus,
                            &uprotocol,
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
            var atrPtr = IntPtr.Zero;
            var zReaderNameSize = AutoAllocate;
            var zReaderNamePtr = new IntPtr();
            var atrSize = AutoAllocate;
            var ret = UnsafePrimitives.SCardStatus(
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
                var readerStr = Marshal.PtrToStringAuto(zReaderNamePtr, (int)zReaderNameSize - 2);
                readerName = readerStr.Split(new[] { '\0' })[0];
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
            var ret = ErrorCode.Success;

            unsafe
            {
                var ustatus = (uint)status;
                var uprotocol = (uint)protocol;
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

            var ptrsendPci = Marshal.AllocHGlobal(Marshal.SizeOf(((IoRequest)sendPci).ScIoRequest));
            Marshal.StructureToPtr(((IoRequest)sendPci).ScIoRequest, ptrsendPci, true);
            var ptrrecvPci = Marshal.AllocHGlobal(Marshal.SizeOf(((IoRequest)recvPci).ScIoRequest));
            Marshal.StructureToPtr(((IoRequest)recvPci).ScIoRequest, ptrrecvPci, true);

            unsafe
            {
                if (recvSize == AutoAllocate)
                {
                    // winscard.dll supports SCARD_AUTOALLOCATE only since Windows vista; winscard.dll is able to propose l recvSize to be used with all versions (XP+)
                    // pcsclite does not support recvBuffer = null, so no automatic discovery of recvSize
                    // For more portability: Wrapper don't use the native winscard.dll's SCARD_AUTOALLOCATE
                    recvSize = DefaultBufferSize;
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
                    if (ret == ErrorCode.Success)
                    {
                        Array.Resize(ref recvBuffer, (int)recvSize);
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
        public AbstractReaderState CreateReaderStateInstance(string readerName, EventState currentState, EventState eventState)
        {
            return new ReaderState(readerName, currentState, eventState);
        }

        /// <inheritdoc />
        public AbstractIoRequest CreateIoRequestInstance(Protocol protocol)
        {
            return new IoRequest((uint)protocol);
        }

        #endregion
    }
}
