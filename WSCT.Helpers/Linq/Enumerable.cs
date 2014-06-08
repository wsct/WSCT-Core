using System;
using System.Collections.Generic;

namespace WSCT.Helpers.Linq
{
    public static class Enumerable
    {
        /// <summary>
        /// Executes an action on each element of a sequence..
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void DoForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var element in source)
            {
                action(element);
            }
        }

        /// <summary>
        /// Returns the element of a sequence following the first element that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="IEnumerable&lt;TSource&gt;" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// The element in the sequence following the first element that passes the test in the specified predicate function.
        /// </returns>
        public static TSource Following<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext() && !predicate(enumerator.Current))
                {
                }

                if (enumerator.MoveNext())
                {
                    return enumerator.Current;
                }

                throw new InvalidOperationException("No element found satisfying predicate");
            }
        }

        /// <summary>
        /// Executes an action on each element of a sequence and returns the sequence.
        /// Execution is delayed until the enumeration occurs.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var element in source)
            {
                action(element);
                yield return element;
            }
        }

        /// <summary>
        /// Returns the element of a sequence preceding the first element that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="IEnumerable&lt;TSource&gt;" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// The element in the sequence preceding the first element that passes the test in the specified predicate function.
        /// </returns>
        public static TSource Preceding<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            using (var enumerator = source.GetEnumerator())
            {
                var found = false;
                var first = true;
                var element = default(TSource);

                while (enumerator.MoveNext() && !(found = predicate(enumerator.Current)))
                {
                    element = enumerator.Current;
                    first = false;
                }

                if (found && !first)
                {
                    return element;
                }

                throw new InvalidOperationException("No element found satisfying predicate");
            }

            // return source.TakeWhile(s=>!predicate(s)).Skip(1).LastOrDefault();
        }
    }
}