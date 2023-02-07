using System;
using System.Collections.Generic;
using System.Linq;

namespace AVS.Trading.Core.Extensions
{
    public static class ListExtensions
    {
        public static void Merge<T, TKey>(this IList<T> target, IList<T> source, Func<T, TKey> selector, Func<T, bool> predicate)
        {
            var knownKeys = new HashSet<TKey>(target.Select(selector));

            foreach (var item in source)
            {
                if (predicate(item) && knownKeys.Add(selector(item)))
                    target.Add(item);
            }
        }

        public static void Merge<T, TKey>(this IList<T> target, IList<T> source, Func<T, TKey> selector)
        {
            var knownKeys = new HashSet<TKey>(target.Select(selector));

            foreach (var item in source)
            {
                if (knownKeys.Add(selector(item)))
                    target.Add(item);
            }
        }
    }
}