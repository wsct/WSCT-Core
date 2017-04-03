using WSCT.Stack;

namespace WSCT.Linq
{
    /// <summary>
    /// Extension methods for <see cref="ICardContextLayer"/>.
    /// </summary>
    public static class CardContextStackExtension
    {
        /// <summary>
        /// Encapsulates an <see cref="ICardContextLayer"/> and add <see cref="ICardContextLayerObservable"/> behaviour.
        /// </summary>
        /// <param name="stack">Card context stack to encapsulate.</param>
        /// <returns></returns>
        public static ICardContextStackObservable ToObservableStack(this ICardContextStack stack)
        {
            var observable = stack as ICardContextStackObservable;

            return observable ?? new CardContextStackObservable(stack);
        }
    }
}
