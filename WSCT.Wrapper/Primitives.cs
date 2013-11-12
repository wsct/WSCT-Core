using System;
using System.Collections.Generic;
using System.IO;
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
                // As MacOS often pretends to be Unix, this basic hack allows to detect MacOS from some of its specific directories.
                if (Directory.Exists("/Applications") & Directory.Exists("/System") & Directory.Exists("/Users") & Directory.Exists("/Volumes"))
                {
                    api = new MacOSX.Primitives();
                }
                else
                {
                    if (Environment.Is64BitProcess)
                        api = new PCSCLite64.Primitives();
                    else
                        api = new PCSCLite32.Primitives();
                }
            }
            else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                api = new MacOSX.Primitives();
            }
            else
            {
                api = new WinSCard.Primitives();
            }
        }

        #endregion
    }
}
