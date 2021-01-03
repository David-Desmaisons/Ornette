using System;
using System.Collections.Generic;
using System.Linq;
using Ornette.Application.Infra;
using Ornette.Application.Integration.Cue.Parser;

namespace Ornette.Application.Integration.Cue
{
    public class CueParser: IParser<CueSheet>
    {
        public CueSheet Parse(IEnumerable<string> content)
        {
            return content.Select(LineContext.Create)
                .Aggregate<LineContext, ICueElementBuilder>(new SheetBuilder(), ParseLine)
                .Build();
        }

        private static ICueElementBuilder ParseLine(ICueElementBuilder builder, LineContext context)
        {
            try
            {
                return builder.Parse(context.Instruction);
            }
            catch(Exception exception)
            {
                throw new CueParseException(context.LineNumber, exception);
            }
        }

        private struct LineContext
        {
            private LineContext(int lineNumber, CueInstruction instruction)
            {
                LineNumber = lineNumber;
                Instruction = instruction;
            }

            public static LineContext Create(string content, int lineNumber) => new LineContext(lineNumber, CueInstruction.FromLine(content));

            public int LineNumber { get; }
            public CueInstruction Instruction { get; }
        }
    }
}
