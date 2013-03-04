using System;
using System.Collections.Generic;
using System.Text;
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
            return Primitives.api.SCardCancel(_context);
        }

        /// <inheritdoc />
        public virtual ErrorCode establish()
        {
            return Primitives.api.SCardEstablishContext((int)Scope.SCARD_SCOPE_SYSTEM, null, null, ref _context);
        }

        /// <inheritdoc />
        public ErrorCode getStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            return Primitives.api.SCardGetStatusChange(context, timeout, readerStates, (uint)readerStates.Length);
        }

        /// <inheritdoc />
        public ErrorCode isValid()
        {
            return Primitives.api.SCardIsValidContext(_context);
        }

        /// <inheritdoc />
        public virtual ErrorCode listReaders(String group)
        {
            IntPtr zStringPtr = IntPtr.Zero;
            UInt32 zStringSize = Primitives.api.SCARD_AUTOALLOCATE;
            ErrorCode ret = Primitives.api.SCardListReaders(context, group, ref zStringPtr, ref zStringSize);
            if (zStringPtr == IntPtr.Zero)
            {
                _readers = new String[0];
            }
            else
            {
                String readersStr = Marshal.PtrToStringAuto(zStringPtr, (int)zStringSize);
                _readers = readersStr.ToString().Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode listReaderGroups()
        {
            IntPtr zStringPtr = IntPtr.Zero;
            UInt32 zStringSize = Primitives.api.SCARD_AUTOALLOCATE;
            ErrorCode ret = Primitives.api.SCardListReaderGroups(context, ref zStringPtr, ref zStringSize);
            if (zStringPtr == IntPtr.Zero)
            {
                _groups = new String[0];
            }
            else
            {
                String groupsStr = Marshal.PtrToStringAuto(zStringPtr, (int)zStringSize);
                _groups = groupsStr.ToString().Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode release()
        {
            return Primitives.api.SCardReleaseContext(_context);
        }

        #endregion
    }
}