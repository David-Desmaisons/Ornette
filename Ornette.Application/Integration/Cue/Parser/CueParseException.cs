using System;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class CueParseException : Exception
    {
        public int Line { get; set; }

        public CueParseException(int line, string reason) : base(GetMessage(line, reason))
        {
            Line = line;
        }


        public CueParseException(int line, Exception innerException) :
            base(GetMessage(line, innerException.Message), innerException)
        {
            Line = line;
        }

        private static string GetMessage(int line, string reason)
            => $"Error parsing Cue data line: {line}, reason: {reason} ";
    }
}
