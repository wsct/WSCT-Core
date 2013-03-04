using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Wrapper;

namespace WSCT.Core.ConsoleTests
{

    /// <summary>
    /// Enumeration describing the level of the data to be logged
    /// </summary>
    public enum LogLevel : byte
    {
        /// <summary>
        /// 
        /// </summary>
        Info = 0,
        /// <summary>
        /// 
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 
        /// </summary>
        Error = 3
    }
}