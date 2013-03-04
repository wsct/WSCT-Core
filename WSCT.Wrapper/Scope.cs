using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for scope
    /// </summary>
    public enum Scope : uint
    {
        /// <summary></summary>
        SCARD_SCOPE_USER = 0x0000,
        /// <summary></summary>
        SCARD_SCOPE_TERMINAL = 0x0001,
        /// <summary></summary>
        SCARD_SCOPE_SYSTEM = 0x0002
    }
}
