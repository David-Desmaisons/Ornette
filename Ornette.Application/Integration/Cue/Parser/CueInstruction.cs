using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class CueInstruction
    {
        private static readonly Regex _Reader = new Regex(@"^\s*(\w+)\s+(?:""(.*)""|([^""\s]\S*))(?:\s+(?:""(.*)""|([^""\s]\S*)))?$");

        private CueInstruction(string command, string[] parameters)
        {
            Command = command;
            Parameters = parameters;
        }

        public string Command { get; }
        public string[] Parameters { get; }

        public static CueInstruction FromLine(string content)
        {
            var match = _Reader.Match(content);
            if (!match.Success)
                return null;

            var groups = match.Groups;
            var parameters = groups.Cast<Group>().Skip(2)
                .Where(g => g.Success)
                .Select(g => g.Value)
                .ToArray();

            return new CueInstruction(groups[1].Value.ToUpper(), parameters);
        }

        public CueIndex ConvertParameterToCueIndex(int index)
        {
            var parts = Parameters[index].Split(':');

            if (parts.Length != 3)
                throw new ArgumentOutOfRangeException();

            var minutes = int.Parse(parts[0]);
            var seconds = int.Parse(parts[1]);
            var frames = int.Parse(parts[2]);
            return new CueIndex(minutes, seconds, frames);
        }

        public int ConvertParameterToInt(int index)
        {
            return int.Parse(Parameters[index]);
        }
    }
}
