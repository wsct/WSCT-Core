using System;
using System.IO;

namespace WSCT.Wrapper.Desktop
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
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    if (Directory.Exists("/Applications") & Directory.Exists("/System") & Directory.Exists("/Users") & Directory.Exists("/Volumes"))
                    {
                        Api = new MacOSX.Primitives();
                    }
                    else
                    {
                        Api = Environment.Is64BitProcess ? (IPrimitives)new PCSCLite64.Primitives() : new PCSCLite32.Primitives();
                    }
                    break;
                case PlatformID.MacOSX:
                    Api = new MacOSX.Primitives();
                    break;
                default:
                    Api = new WinSCard.Primitives();
                    break;
            }
        }

        #endregion
    }
}