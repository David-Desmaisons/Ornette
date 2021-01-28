using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;

namespace Ornette.Application.Infra
{
    public static class DictionaryExtension
    {
        public static IReadOnlyDictionary<TKey, TValue> DuplicateExcept<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> from, TKey removeKey)
        {
            var result = new Dictionary<TKey, TValue>();
            from.Where(kvp => !EqualityComparer<TKey>.Default.Equals(kvp.Key, removeKey))
                .ForEach(kvp => result[kvp.Key] = kvp.Value);
            return result;
        }
    }
}
