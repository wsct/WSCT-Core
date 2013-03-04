using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Accessor to the concrete PC/SC API wrapper.
    /// It also automatically selects the wrapper corresponding to the Operating System in use.
    /// </summary>
    public class Primitives
    {
        #region >> Properties

        /// <summary>
        /// accessor to concrete API for current Operating System
        /// </summary>
        public static IPrimitives api;

        #endregion

        #region >> Static Constructor

        static Primitives()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                if (Environment.Is64BitProcess)
                    api = new PCSCLite64.Primitives();
                else
                    api = new PCSCLite32.Primitives();
            }
            else
            {
                api = new WinSCard.Primitives();
            }
        }

        #endregion
    }
}
