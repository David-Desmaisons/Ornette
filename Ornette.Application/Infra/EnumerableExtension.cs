using System.Collections.Generic;

namespace Ornette.Application.Infra
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> from, T with)
        {
            foreach (var item in from)
            {
                yield return item;
            }
            yield return with;
        }
    }
}
