using System;
using WSCT.Wrapper;

namespace WSCT.Core.Fluent.Helpers
{
    public class UnexpectedErrorCodeException : Exception
    {
        public ErrorCode ErrorCode { get; private set; }

        public UnexpectedErrorCodeException(ErrorCode errorCode)
            : base($"ErrorCode={errorCode}")
        {
            ErrorCode = errorCode;
        }
    }
}
