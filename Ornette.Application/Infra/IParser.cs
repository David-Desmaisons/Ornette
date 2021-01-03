using System.Collections.Generic;

namespace Ornette.Application.Infra
{
    public interface IParser<T> where T : class
    {
        T Parse(IEnumerable<string> content);
    }
}
