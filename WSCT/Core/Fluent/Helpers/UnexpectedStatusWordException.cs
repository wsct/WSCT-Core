using System;
using System.Runtime.CompilerServices;

namespace WSCT.Core.Fluent.Helpers
{
    public class UnexpectedStatusWordException : Exception
    {
        public ushort StatusWord { get; private set; }

        public UnexpectedStatusWordException(ushort sw, [CallerFilePathAttribute] string callerName = default, [CallerFilePath] string callerPath = default, [CallerLineNumberAttribute] int callerLine = default)
            : base($"SW={sw:X4} [in {callerPath}.{callerName}:{callerLine}]")
        {
            StatusWord = sw;
        }
    }
}
