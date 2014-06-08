using WSCT.Core;

namespace WSCT.Linq
{
    /// <summary>
    /// Extension methods for <see cref="ICardContext"/>.
    /// </summary>
    public static class CardContextExtension
    {
        /// <summary>
        /// Encapsulates an <see cref="ICardContext"/> and add <see cref="ICardContextObservable"/> behaviour.
        /// </summary>
        /// <param name="context">Card context to encapsulate.</param>
        /// <returns></returns>
        public static ICardContextObservable ToObservable(this ICardContext context)
        {
            var observable = context as ICardContextObservable;

            return observable ?? new CardContextObservable(context);
        }
    }
}
