using System.Collections.Generic;
using System.Linq;
using Ornette.Application.Integration.Cue.Parser;

namespace Ornette.Application.Integration.Cue
{
    public class CueParser: ICueParser
    {
        public CueSheet Parse(IEnumerable<string> content)
        {
            return content.Select(CueInstruction.FromLine)
                .Aggregate<CueInstruction, ICueElementBuilder>(new SheetBuilder(), (builder, line) => builder.Parse(line))
                .Build();
        }
    }
}
