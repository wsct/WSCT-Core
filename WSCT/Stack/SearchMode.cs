namespace WSCT.Stack
{
    /// <summary>
    /// Enumeration defining different modes of going through the stack.
    /// </summary>
    public enum SearchMode : byte
    {
        /// <summary>
        /// Seek next layer in the stack.
        /// </summary>
        Next,

        /// <summary>
        /// Seek previous layer in the stack.
        /// </summary>
        Previous,

        /// <summary>
        /// Seek top (first) layer in the stack.
        /// </summary>
        Top,

        /// <summary>
        /// Seek bottom (last) layer in the stack.
        /// </summary>
        Bottom
    }
}