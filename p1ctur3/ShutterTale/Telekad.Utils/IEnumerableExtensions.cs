using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekad.Utils
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Finds the index in the collection where the predicate evaluates to true.
        /// 
        /// Returns -1 if no matching item found
        /// </summary>
        /// <typeparam name="TSource">Type of collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="predicate">Function to evaluate</param>
        /// <returns>Index where predicate is true, or -1 if not found.</returns>
        public static int FindIndex<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                var obj = enumerator.Current;
                if (predicate(obj))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Finds the index in the collection where the predicate evaluates to true.
        /// 
        /// Returns -1 if no matching item found
        /// </summary>
        /// <typeparam name="TSource">Type of collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="predicate">Function to evaluate</param>
        /// <returns>Index where predicate is true, or -1 if not found.</returns>
        public static int FindIndex<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                var obj = enumerator.Current;
                if (predicate(obj, index))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements. If source collection is null it returns default value
        /// </summary>
        /// <typeparam name="TSource">Type of collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <returns>the first element of a sequence, or a default value if the sequence contains no elements</returns>
        public static TSource SafeFirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                return default(TSource);
            return source.FirstOrDefault();
        }

        /// <summary>
        /// Returns the first element of a sequence. If source collection is null it throws null parameter exception
        /// </summary>
        /// <typeparam name="TSource">Type of collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <returns>the first element of a sequence</returns>
        public static TSource SafeFirst<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            return source.First();
        }

        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> predicate)
        {
            return first.SequenceEqual(second, new DelegateEqualityComparer<TSource>(predicate));
        }

    }
}
