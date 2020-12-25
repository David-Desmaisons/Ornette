using System.Collections.Generic;
using System.Linq;
using Ornette.Application.Integration.Cue.Parser;

namespace Ornette.Application.Integration.Cue
{
    public class CueParser: ICueParser
    {
        public CueSheet Parse(IEnumerable<string> content)
        {
            var result = new SheetBuilder();
            content.Select(CueInstruction.FromLine)
                .Aggregate<CueInstruction, ICueElementBuilder>(result, (builder, line) => builder.Parse(line))
                .End();
            return result.Build();
        }
    }
}
