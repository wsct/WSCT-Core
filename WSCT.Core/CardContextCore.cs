using System;
using System.Runtime.InteropServices;
using WSCT.Wrapper;

namespace WSCT.Core
{
    /// <summary>
    /// Represents a basic object capable of managing smartcard resources.
    /// </summary>
    public class CardContextCore : ICardContext
    {
        #region >> Fields

        private IntPtr _context;

        private string[] _groups;

        private string[] _readers;

        #endregion

        #region >> Constructor

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CardContextCore()
        {
            _context = IntPtr.Zero;
        }

        #endregion

        #region >> ICardContext Members

        /// <inheritdoc />
        public IntPtr Context
        {
            get { return _context; }
        }

        /// <inheritdoc />
        public string[] Groups
        {
            get { return _groups; }
        }

        /// <inheritdoc />
        public int GroupsCount
        {
            get { return _groups.Length; }
        }

        /// <inheritdoc />
        public string[] Readers
        {
            get { return _readers; }
        }

        /// <inheritdoc />
        public int ReadersCount
        {
            get { return _readers.Length; }
        }

        /// <inheritdoc />
        public ErrorCode Cancel()
        {
            return Primitives.Api.SCardCancel(_context);
        }

        /// <inheritdoc />
        public virtual ErrorCode Establish()
        {
            return Primitives.Api.SCardEstablishContext((int)Scope.System, null, null, ref _context);
        }

        /// <inheritdoc />
        public ErrorCode GetStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            return Primitives.Api.SCardGetStatusChange(Context, timeout, readerStates, (uint)readerStates.Length);
        }

        /// <inheritdoc />
        public ErrorCode IsValid()
        {
            return Primitives.Api.SCardIsValidContext(_context);
        }

        /// <inheritdoc />
        public virtual ErrorCode ListReaders(string group)
        {
            var zStringPtr = IntPtr.Zero;
            var zStringSize = Primitives.Api.AutoAllocate;
            var ret = Primitives.Api.SCardListReaders(Context, group, ref zStringPtr, ref zStringSize);
            if (zStringPtr == IntPtr.Zero)
            {
                _readers = new string[0];
            }
            else
            {
                var readersStr = Marshal.PtrToStringAuto(zStringPtr, (int)zStringSize);
                _readers = readersStr.Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode ListReaderGroups()
        {
            var zStringPtr = IntPtr.Zero;
            var zStringSize = Primitives.Api.AutoAllocate;
            var ret = Primitives.Api.SCardListReaderGroups(Context, ref zStringPtr, ref zStringSize);
            if (zStringPtr == IntPtr.Zero)
            {
                _groups = new string[0];
            }
            else
            {
                var groupsStr = Marshal.PtrToStringAuto(zStringPtr, (int)zStringSize);
                _groups = groupsStr.Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode Release()
        {
            return Primitives.Api.SCardReleaseContext(_context);
        }

        #endregion
    }
}