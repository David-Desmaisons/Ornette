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
    }
}
