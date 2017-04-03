using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WSCT.Wrapper.PCSCLite64
{
    /// <summary>
    /// Wrapper of PC/SC (pcsclite) for 64 bits linux.
    /// </summary>
    public class Primitives : IPrimitives
    {
        #region >> Fields

        /// <summary>
        /// PC/SC resources management.
        /// </summary>
        internal static IntPtr ActiveContext;

        private UInt32 _defaultBufferSize = 1024;

        #endregion

        #region >> IPrimitives Members

        /// <inheritdoc />
        public uint AutoAllocate
        {
            get { return UInt32.MaxValue; }
        }

        public uint DefaultBufferSize
        {
            get { return _defaultBufferSize; }
            set { _defaultBufferSize = value; }
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
        public ErrorCode SCardConnect(IntPtr context, string readerName, ShareMode shareMode, Protocol preferedProtocol, ref IntPtr card, ref Protocol activeProtocol)
        {
            ErrorCode ret;

            unsafe
            {
                var uactiveProtocol = (ulong)activeProtocol;
                readerName += "\0";
                // PC/SC lite does not support unicode, only utf8
                // Ensure that encoding of readername if utf8
                var def = Encoding.Default.GetBytes(readerName);
                var utf8ReaderName = Encoding.Convert(Encoding.Default, Encoding.UTF8, def);
                fixed (byte* preaderName = utf8ReaderName)
                {
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
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardControl(IntPtr card, uint controlCode, byte[] sendBuffer, uint sendSize, ref byte[] recvBuffer, uint recvSize, ref uint returnedSize)
        {
            ulong ulreturnedSize = returnedSize;
            var ret = UnsafePrimitives.SCardControl(card, controlCode, sendBuffer, sendSize, ref recvBuffer, recvSize, ref ulreturnedSize);
            returnedSize = (uint)ulreturnedSize;

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardDisconnect(IntPtr card, Disposition disposition)
        {
            unsafe
            {
                return UnsafePrimitives.SCardDisconnect(
                    (void*)card,
                    (ulong)disposition
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
                {
                    ActiveContext = context;
                }
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardGetAttrib(IntPtr card, UInt32 attributeId, ref byte[] recvAttribute, ref UInt32 recvAttributeSize)
        {
            ErrorCode ret;

            ulong ulrecvAttributeSize = recvAttributeSize;

            unsafe
            {
                if (recvAttributeSize == AutoAllocate)
                {
                    fixed (byte* precvAttribute = recvAttribute)
                    {
                        ret = UnsafePrimitives.SCardGetAttrib(
                            (void*)card,
                            attributeId,
                            precvAttribute,
                            &ulrecvAttributeSize
                            );
                    }
                    if (ret == ErrorCode.Success)
                    {
                        recvAttribute = new byte[ulrecvAttributeSize];
                        fixed (byte* precvAttribute = recvAttribute)
                        {
                            ret = UnsafePrimitives.SCardGetAttrib(
                                (void*)card,
                                attributeId,
                                precvAttribute,
                                &ulrecvAttributeSize
                                );
                        }
                    }
                    else
                    {
                        recvAttributeSize = 0;
                        recvAttribute = new byte[0];
                    }
                }
                else
                {
                    fixed (byte* precvAttribute = recvAttribute)
                    {
                        ret = UnsafePrimitives.SCardGetAttrib(
                            (void*)card,
                            attributeId,
                            precvAttribute,
                            &ulrecvAttributeSize
                            );
                    }
                }
            }

            recvAttributeSize = (uint)ulrecvAttributeSize;

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

            ulong ulsize = size;

            unsafe
            {
                // PC/SC lite does not support unicode, only utf8
                // Ensure that encoding of groups if utf8
                var def = Encoding.Default.GetBytes(groups);
                var utf8Groups = Encoding.Convert(Encoding.Default, Encoding.UTF8, def);

                fixed (byte* pgroups = utf8Groups)
                {
                    if (size == AutoAllocate)
                    {
                        err = UnsafePrimitives.SCardListReaders(
                            (void*)context,
                            (char*)pgroups,
                            (char*)readers,
                            &ulsize
                            );
                        if (err == ErrorCode.Success)
                        {
                            var creaders = new char[ulsize];
                            fixed (char* pcreaders = creaders)
                            {
                                err = UnsafePrimitives.SCardListReaders(
                                    (void*)context,
                                    (char*)pgroups,
                                    pcreaders,
                                    &ulsize
                                    );
                                readers = (IntPtr)pcreaders;
                            }
                        }
                    }
                    else
                    {
                        err = UnsafePrimitives.SCardListReaders(
                            (void*)context,
                            (char*)pgroups,
                            (char*)readers,
                            &ulsize
                            );
                    }
                }
            }

            size = (uint)ulsize;

            return err;
        }

        /// <inheritdoc />
        public ErrorCode SCardListReaderGroups(IntPtr context, ref IntPtr groups, ref uint size)
        {
            ErrorCode err;

            ulong ulsize = size;

            unsafe
            {
                if (size == AutoAllocate)
                {
                    err = UnsafePrimitives.SCardListReaderGroups(
                        (void*)context,
                        (char*)groups,
                        &ulsize
                        );
                    if (err == ErrorCode.Success)
                    {
                        var cgroups = new char[ulsize];
                        fixed (char* pcgroups = cgroups)
                        {
                            err = UnsafePrimitives.SCardListReaderGroups(
                                (void*)context,
                                pcgroups,
                                &ulsize
                                );
                            groups = (IntPtr)pcgroups;
                        }
                    }
                }
                else
                {
                    err = UnsafePrimitives.SCardListReaderGroups(
                        (void*)context,
                        (char*)groups,
                        &ulsize
                        );
                }
            }

            size = (uint)ulsize;

            return err;
        }

        /// <inheritdoc />
        public ErrorCode SCardReconnect(IntPtr card, ShareMode shareMode, Protocol preferedProtocol, Disposition initialisation, ref Protocol activeProtocol)
        {
            ErrorCode err;

            unsafe
            {
                ulong protocol = (uint)activeProtocol;
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
                {
                    ActiveContext = IntPtr.Zero;
                }
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

            ulong ulreaderNameSize = readerNameSize;
            ulong ulatrSize = atrSize;

            unsafe
            {
                var ustatus = (ulong)status;
                var uprotocol = (ulong)protocol;
                fixed (uint* preaderNameSize = &readerNameSize)
                {
                    fixed (uint* patrSize = &atrSize)
                    {
                        if (readerNameSize != AutoAllocate && atrSize != AutoAllocate)
                        {
                            ret = UnsafePrimitives.SCardStatus(
                                (void*)card,
                                (char*)readerName,
                                &ulreaderNameSize,
                                &ustatus,
                                &uprotocol,
                                (byte*)atr,
                                &ulatrSize
                                );
                        }
                        else
                        {
                            //TODO
                            throw new NotImplementedException();
                        }
                        status = (State)ustatus;
                        protocol = (Protocol)uprotocol;
                        readerNameSize = *preaderNameSize;
                        atrSize = *patrSize;
                    }
                }
            }

            readerNameSize = (uint)ulreaderNameSize;
            atrSize = (uint)ulatrSize;

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardStatus(IntPtr card, ref string readerName, ref State state, ref Protocol protocol, ref byte[] atr)
        {
            var atrPtr = IntPtr.Zero;
            ulong zReaderNameSize = AutoAllocate;
            var zReaderNamePtr = new IntPtr();
            ulong atrSize = AutoAllocate;
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
            {
                readerName = "";
            }
            else
            {
                var readerStr = Marshal.PtrToStringAuto(zReaderNamePtr, (int)zReaderNameSize - 2);
                readerName = readerStr.Split('\0')[0];
            }
            if (atrPtr == IntPtr.Zero)
            {
                atr = new byte[0];
            }
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
            ErrorCode ret;

            unsafe
            {
                var ustatus = (ulong)status;
                var uprotocol = (ulong)protocol;
                ulong readerNameSize = 0;
                ulong atrSize = 0;
                ret = UnsafePrimitives.SCardStatus((void*)card, null, &readerNameSize, &ustatus, &uprotocol, null, &atrSize);
                status = (State)ustatus;
                protocol = (Protocol)uprotocol;
            }

            return ret;
        }

        /// <inheritdoc />
        public ErrorCode SCardTransmit(IntPtr card, ref AbstractIoRequest sendPci, byte[] sendBuffer, uint sendSize, ref AbstractIoRequest recvPci, ref byte[] recvBuffer, ref uint recvSize)
        {
            ErrorCode ret;

            ulong ulrecvSize = recvSize;

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
                    ulrecvSize = DefaultBufferSize;
                    recvBuffer = new byte[ulrecvSize];
                    fixed (byte* psendBuffer = sendBuffer)
                    {
                        fixed (byte* precvBuffer = recvBuffer)
                        {
                            ret = UnsafePrimitives.SCardTransmit(
                                (void*)card,
                                (void*)ptrsendPci,
                                psendBuffer,
                                sendSize,
                                (void*)ptrrecvPci,
                                precvBuffer,
                                &ulrecvSize
                                );
                        }
                    }
                    if (ret == ErrorCode.Success)
                    {
                        Array.Resize(ref recvBuffer, (int)ulrecvSize);
                    }
                }
                else
                {
                    //TODO Seems to be problems with pcsclite in this case...
                    fixed (byte* psendBuffer = sendBuffer)
                    {
                        fixed (byte* precvBuffer = recvBuffer)
                        {
                            ret = UnsafePrimitives.SCardTransmit(
                                (void*)card,
                                (void*)ptrsendPci,
                                psendBuffer,
                                sendSize,
                                (void*)ptrrecvPci,
                                precvBuffer,
                                &ulrecvSize
                                );
                        }
                    }
                }
            }

            Marshal.FreeHGlobal(ptrsendPci);
            Marshal.FreeHGlobal(ptrrecvPci);

            recvSize = (uint)ulrecvSize;

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