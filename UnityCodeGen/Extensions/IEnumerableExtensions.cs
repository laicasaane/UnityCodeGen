using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityCodeGen.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TOut> Map<TIn, TOut>(this IEnumerable<TIn> collection, Func<TIn, TOut> mapper)
        {
            return collection.Select(mapper);
        }

        public static TIn Reduce<TIn>(this IEnumerable<TIn> collection, Func<TIn, TIn, TIn> reducer)
        {
            if (collection.Count() == 0)
                return default;

            return collection.Aggregate(reducer);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> lambda)
        {
            foreach (var item in collection)
            {
                lambda(item);
            }
        }
    }
}