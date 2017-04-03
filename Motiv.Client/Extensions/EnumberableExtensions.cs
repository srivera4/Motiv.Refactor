using System;
using System.Collections.Generic;
using System.Linq;

namespace Motiv.Client.Extensions
{
    internal static class EnumberableExtensions
    {
        public static IOrderedEnumerable<TSource> OrderByWithDirection<TSource, TKey>
                                    (this IEnumerable<TSource> source,
                                     Func<TSource, TKey> keySelector,
                                     bool descending)
        {


            return descending ? source.OrderByDescending(keySelector)
                              : source.OrderBy(keySelector);
        }
    }
}
