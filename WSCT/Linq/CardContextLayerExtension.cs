using System.Collections.Generic;
using System.Linq;
using WSCT.Stack;

namespace WSCT.Linq
{
    /// <summary>
    /// Extension methods for <see cref="ICardContextLayer"/>.
    /// </summary>
    public static class CardContextLayerExtension
    {
        /// <summary>
        /// Encapsulates an <see cref="ICardContextLayer"/> and add <see cref="ICardContextLayerObservable"/> behaviour.
        /// </summary>
        /// <param name="layer">Card context to encapsulate.</param>
        /// <returns></returns>
        public static ICardContextLayerObservable ToObservableLayer(this ICardContextLayer layer)
        {
            var layerObservable = layer as ICardContextLayerObservable;

            return layerObservable ?? new CardContextLayerObservable(layer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        /// <returns></returns>
        public static IEnumerable<ICardContextLayerObservable> ToObservableLayers(this IEnumerable<ICardContextLayer> layers)
        {
            foreach (var layer in layers)
            {
                yield return layer.ToObservableLayer();
            }
        }
    }
}
