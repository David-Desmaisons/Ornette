using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue
{
    public interface ICueParser
    {
        CueSheet Parse(IEnumerable<string> content);
    }
}
