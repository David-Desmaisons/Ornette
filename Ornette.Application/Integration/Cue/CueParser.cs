using System.Collections.Generic;
using MoreCollection.Extensions;

namespace Ornette.Application.Integration.Cue
{
    public class CueParser: ICueParser
    {
        public CueSheet Parse(IEnumerable<string> content)
        {
            var result = new CueSheet();
            content.ForEach(line => Parse(line, result));
            return result;
        }

        private static void Parse(string content, CueSheet sheet)
        {

        }
    }
}
