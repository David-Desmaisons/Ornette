using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;

namespace Ornette.Application.Infra
{
    public static class DictionaryExtension
    {
        public static Dictionary<TKey, TValue> DuplicateExcept<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> from, TKey removeKey)
        {
            var result = new Dictionary<TKey, TValue>();
            from.Where(kvp => !EqualityComparer<TKey>.Default.Equals(kvp.Key, removeKey))
                .ForEach(kvp => result[kvp.Key] = kvp.Value);
            return result;
        }

        public static Dictionary<TKey, List<TValue>> Convert<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue[]> from)
        {
            var result = new Dictionary<TKey, List<TValue>>();
            from?.ForEach(kvp => result[kvp.Key] = new List<TValue>(kvp.Value));
            return result;
        }

        public static Dictionary<TKey, TValue[]> Convert<TKey, TValue>(this Dictionary<TKey, List<TValue>> from)
        {
            var result = new Dictionary<TKey, TValue[]>();
            from?.ForEach(kvp => result[kvp.Key] = kvp.Value.ToArray());
            return result;
        }

        public static Dictionary<TKey, List<TValue>> Merge<TKey, TValue>(this Dictionary<TKey, TValue[]> from, IReadOnlyDictionary<TKey, TValue[]> with)
        {
            return from.Convert().Merge(with);
        }

        public static Dictionary<TKey, List<TValue>> Merge<TKey, TValue>(this Dictionary<TKey, List<TValue>> from, IReadOnlyDictionary<TKey, TValue[]> with)
        {
            with.ForEach(kvp => from.GetOrAddEntity(kvp.Key, _ => new List<TValue>()).AddRange(kvp.Value));
            return from;
        }
    }
}
