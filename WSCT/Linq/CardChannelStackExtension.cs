using System.Collections.Generic;
using System.Linq;
using WSCT.Stack;

namespace WSCT.Linq
{
    /// <summary>
    /// Extension methods for <see cref="ICardChannelLayer"/>.
    /// </summary>
    public static class CardChannelStackExtension
    {
        /// <summary>
        /// Encapsulates an <see cref="ICardChannelLayer"/> and add <see cref="ICardChannelLayerObservable"/> behaviour.
        /// </summary>
        /// <param name="stack">Card channel stack to encapsulate.</param>
        /// <returns></returns>
        public static ICardChannelStackObservable ToObservableStack(this ICardChannelStack stack)
        {
            var observable = stack as ICardChannelStackObservable;

            return observable ?? new CardChannelStackObservable(stack);
        }
    }
}
