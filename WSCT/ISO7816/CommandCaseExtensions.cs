namespace WSCT.ISO7816
{
    public static class CommandCaseExtensions
    {
        /// <summary>
        /// Returns <c>true</c> when <paramref name="commandCase"/> is extended.
        /// </summary>
        /// <param name="commandCase"></param>
        /// <returns></returns>
        public static bool IsExtended(this CommandCase commandCase)
        {
            return commandCase is
                CommandCase.CC2E or
                CommandCase.CC3E or
                CommandCase.CC4E;
        }
    }
}
