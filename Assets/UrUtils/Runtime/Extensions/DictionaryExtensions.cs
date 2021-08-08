using System.Collections.Generic;
using System.Linq;

namespace UrUtils.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }
    }
}
