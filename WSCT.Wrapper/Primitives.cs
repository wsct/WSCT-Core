using System;
using System.IO;

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
        /// accessor to concrete API for current Operating System.
        /// </summary>
        public static IPrimitives Api;

        #endregion

        #region >> Static Constructor

        static Primitives()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                // As MacOS often pretends to be Unix, this basic hack allows to detect MacOS from some of its specific directories.
                if (Directory.Exists("/Applications") & Directory.Exists("/System") & Directory.Exists("/Users") & Directory.Exists("/Volumes"))
                {
                    Api = new MacOSX.Primitives();
                }
                else
                {
                    if (Environment.Is64BitProcess)
                        Api = new PCSCLite64.Primitives();
                    else
                        Api = new PCSCLite32.Primitives();
                }
            }
            else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                Api = new MacOSX.Primitives();
            }
            else
            {
                Api = new WinSCard.Primitives();
            }
        }

        #endregion
    }
}
