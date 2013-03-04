using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Abstract IoRequest structure to be implemented by PC/SC wrapper s
    /// </summary>
    public abstract class AbstractIoRequest
    {
        /// <summary>Protocol (see <see cref="Protocol"/>)</summary>
        public abstract UInt32 protocol { get; set; }

        /// <summary>PCI length</summary>
        public abstract UInt32 pciLength { get; set; }
    }
}
