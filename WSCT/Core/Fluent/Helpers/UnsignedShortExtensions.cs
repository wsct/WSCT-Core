using System.Runtime.CompilerServices;

namespace WSCT.Core.Fluent.Helpers
{
    public static class UnsignedShortExtensions
    {
        /// <summary>
        /// Throw an <see cref="UnexpectedStatusWordException"> when the value is not 0x9000
        /// </summary>
        public static ushort ThrowIfSWNot9000(this ushort statusWord, [CallerMemberName] string callerName = default, [CallerFilePath] string callerPath = default, [CallerLineNumberAttribute] int callerLine = default)
        {
            if (statusWord != 0x9000)
            {
                throw new UnexpectedStatusWordException(statusWord, callerName, callerPath, callerLine);
            }

            return statusWord;
        }
    }
}
