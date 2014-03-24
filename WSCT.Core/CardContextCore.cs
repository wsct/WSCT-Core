using System;
using System.Runtime.InteropServices;

using WSCT.Wrapper;

namespace WSCT.Core
{
    /// <summary>
    /// Represents a basic object capable of managing smartcard resources
    /// </summary>
    public class CardContextCore : ICardContext
    {
        #region >> Fields

        private IntPtr _context;

        private String[] _groups;

        private String[] _readers;

        #endregion

        #region >> Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardContextCore()
        {
            _context = IntPtr.Zero;
        }

        #endregion

        #region >> ICardContext Members

        /// <inheritdoc />
        public IntPtr context
        {
            get
            {
                return _context;
            }
        }

        /// <inheritdoc />
        public String[] groups
        {
            get
            {
                return _groups;
            }
        }

        /// <inheritdoc />
        public int groupsCount
        {
            get
            {
                return _groups.Length;
            }
        }

        /// <inheritdoc />
        public String[] readers
        {
            get
            {
                return _readers;
            }
        }

        /// <inheritdoc />
        public int readersCount
        {
            get
            {
                return _readers.Length;
            }
        }

        /// <inheritdoc />
        public ErrorCode cancel()
        {
            return Primitives.Api.SCardCancel(_context);
        }

        /// <inheritdoc />
        public virtual ErrorCode establish()
        {
            return Primitives.Api.SCardEstablishContext((int)Scope.System, null, null, ref _context);
        }

        /// <inheritdoc />
        public ErrorCode getStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            return Primitives.Api.SCardGetStatusChange(context, timeout, readerStates, (uint)readerStates.Length);
        }

        /// <inheritdoc />
        public ErrorCode isValid()
        {
            return Primitives.Api.SCardIsValidContext(_context);
        }

        /// <inheritdoc />
        public virtual ErrorCode listReaders(String group)
        {
            var zStringPtr = IntPtr.Zero;
            var zStringSize = Primitives.Api.AutoAllocate;
            var ret = Primitives.Api.SCardListReaders(context, group, ref zStringPtr, ref zStringSize);
            if (zStringPtr == IntPtr.Zero)
            {
                _readers = new String[0];
            }
            else
            {
                var readersStr = Marshal.PtrToStringAuto(zStringPtr, (int)zStringSize);
                _readers = readersStr.ToString().Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode listReaderGroups()
        {
            var zStringPtr = IntPtr.Zero;
            var zStringSize = Primitives.Api.AutoAllocate;
            var ret = Primitives.Api.SCardListReaderGroups(context, ref zStringPtr, ref zStringSize);
            if (zStringPtr == IntPtr.Zero)
            {
                _groups = new String[0];
            }
            else
            {
                var groupsStr = Marshal.PtrToStringAuto(zStringPtr, (int)zStringSize);
                _groups = groupsStr.ToString().Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode release()
        {
            return Primitives.Api.SCardReleaseContext(_context);
        }

        #endregion
    }
}