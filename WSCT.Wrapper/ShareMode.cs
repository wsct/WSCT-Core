using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for the different share modes of readers
    /// </summary>
    public enum ShareMode
    {
        /// <summary></summary>
        SCARD_SHARE_EXCLUSIVE = 0x0001,
        /// <summary></summary>
        SCARD_SHARE_SHARED = 0x0002,
        /// <summary></summary>
        SCARD_SHARE_DIRECT = 0x0004
    }
}
