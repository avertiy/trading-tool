using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AVS.Trading.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Merge<TKey,TValue,TId>(this IDictionary<TKey,IList<TValue>> target, 
            IDictionary<TKey, IList<TValue>> source, 
            Func<TValue, TId> selector, Func<TValue, bool> predicate)
        {
            foreach (var kp in source)
            {
                if (!target.ContainsKey(kp.Key))
                {
                    target.Add(kp.Key, kp.Value);
                    continue;
                }
                target[kp.Key].Merge(kp.Value,selector, predicate);
            }
        }


        public static IDictionary<string, IList<TTo>> Cast<TFrom, TTo>(this IDictionary<string, IList<TFrom>> dict)
            where TFrom : TTo
        {
            var res = new Dictionary<string, IList<TTo>>();
            foreach (var kp in dict)
            {
                var list = new List<TTo>(kp.Value.Count);
                list.AddRange(kp.Value.Cast<TTo>());
                res[kp.Key] = list;
            }
            return res;
        }

        public static IDictionary<string, IList<TTo>> Cast<TFrom, TTo>(this Dictionary<string, IList<TFrom>> dict)
            where TFrom : TTo
        {
            var res = new Dictionary<string, IList<TTo>>();
            foreach (var kp in dict)
            {
                var list = new List<TTo>(kp.Value.Count);
                list.AddRange(kp.Value.Cast<TTo>());
                res[kp.Key] = list;
            }
            return res;
        }


        public static IDictionary<TKey, T2Value> 
            Convert<TKey,TValue, T2Value>(this IDictionary<TKey, TValue> dict, Func<TValue, T2Value> convert)
        {
            var res = new Dictionary<TKey, T2Value>();
            foreach (var kp in dict)
            {
                res[kp.Key] = convert(kp.Value);
            }
            return res;
        }
    }
}