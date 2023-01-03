using WSCT.Wrapper;

namespace WSCT.Core.Fluent.Helpers
{
    public static class ErrorCodeExtensions
    {
        /// <summary>
        /// Throws an <see cref="UnexpectedErrorCodeException"> when not Success.
        /// </summary>
        public static ErrorCode ThrowIfNotSuccess(this ErrorCode errorCode)
        {
            if (errorCode != ErrorCode.Success)
            {
                throw new UnexpectedErrorCodeException(errorCode);
            }

            return errorCode;
        }
    }
}
