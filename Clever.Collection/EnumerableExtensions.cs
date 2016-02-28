using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Collection
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> input, Action<T> action)
        {
            var enumerated = input.ToArray();
            foreach (var item in enumerated)
            {
                action(item);
            }
            return enumerated;
        }

        public static IEnumerable<TOut> ForEachAndSelect<T, TOut>(this IEnumerable<T> input, Func<T, TOut> action)
        {
            var enumerated = input.ToArray();
            var results = new List<TOut>();
            foreach (var item in enumerated)
            {
                results.Add(action(item));
            }
            return results.ToArray();
        }

        public static Dictionary<TKey, TValue> ForEach<TKey, TValue>(this IDictionary<TKey, TValue> input, Action<TKey, TValue> action)
        {
            var enumerated = input.ToArray();
            foreach (var item in enumerated)
            {
                action(item.Key, item.Value);
            }
            return enumerated.ToDictionary();
        }

        public static Dictionary<TKey, TValue> WhereKey<TKey, TValue>(this IDictionary<TKey, TValue> source, Predicate<TKey> filter)
        {
            return source.Where(x => filter(x.Key)).ToDictionary();
        }

        public static Dictionary<TKey, TValue> WhereValue<TKey, TValue>(this IDictionary<TKey, TValue> source, Predicate<TValue> filter)
        {
            return source.Where(x => filter(x.Value)).ToDictionary();
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> input)
        {
            return input.ToDictionary(x => x.Key, x => x.Value);
        }

        public static Dictionary<TKey, TAcc> GroupAndAggregate<T, TKey, TCur, TAcc>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TCur> valueSelector, TAcc initialValue, Func<TAcc, TCur, TAcc> combinator)
        {
            return 
                source.GroupBy(
                    keySelector, 
                    (key, itemGroup) => itemGroup.Aggregate(
                        new KeyValuePair<TKey, TAcc>(key, initialValue), 
                        (acc, cur) => new KeyValuePair<TKey, TAcc>(key, combinator(acc.Value, valueSelector(cur)))
                    )
                ).ToDictionary();
        }

        public static Dictionary<TKey, int> GroupAndSum<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, int> valueSelector, int initialValue = 0)
        {
            return source.GroupAndAggregate(keySelector, valueSelector, initialValue, (cur, acc) => cur + acc);
        }

        public static Dictionary<TKey, long> GroupAndSum<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, long> valueSelector, long initialValue = 0L)
        {
            return source.GroupAndAggregate(keySelector, valueSelector, initialValue, (cur, acc) => cur + acc);
        }

        public static Dictionary<TKey, double> GroupAndSum<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, double> valueSelector, double initialValue = 0.0)
        {
            return source.GroupAndAggregate(keySelector, valueSelector, initialValue, (cur, acc) => cur + acc);
        }

        public static Dictionary<TKey, decimal> GroupAndSum<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, decimal> valueSelector, decimal initialValue = 0m)
        {
            return source.GroupAndAggregate(keySelector, valueSelector, initialValue, (cur, acc) => cur + acc);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> array, T item)
        {
            return array.Concat(new[] { item });
        }
    }
}