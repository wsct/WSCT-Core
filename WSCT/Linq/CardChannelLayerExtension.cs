using System.Collections.Generic;
using System.Linq;
using WSCT.Stack;

namespace WSCT.Linq
{
    /// <summary>
    /// Extension methods for <see cref="ICardChannelLayer"/>.
    /// </summary>
    public static class CardChannelLayerExtension
    {
        /// <summary>
        /// Encapsulates an <see cref="ICardChannelLayer"/> and adds <see cref="ICardChannelLayerObservable"/> behaviour.
        /// </summary>
        /// <param name="layer">Card channel layer to encapsulate.</param>
        /// <returns></returns>
        public static ICardChannelLayerObservable ToObservableLayer(this ICardChannelLayer layer)
        {
            var layerObservable = layer as ICardChannelLayerObservable;

            return layerObservable ?? new CardChannelLayerObservable(layer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        /// <returns></returns>
        public static IEnumerable<ICardChannelLayerObservable> ToObservableLayers(this IEnumerable<ICardChannelLayer> layers)
        {
            return layers.Select(layer => layer.ToObservableLayer());
        }
    }
}
