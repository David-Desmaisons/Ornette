using Ornette.Application.Integration.Cue;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Ornette.Application.Tests.Integration.Cue
{
    public class CueParserTest
    {
        private readonly CueParser _CueParser;
        private readonly List<string[]> _CueContents;

        public CueParserTest()
        {
            _CueParser = new CueParser();
            _CueContents = new[] { "Andrew Hill - Andrew!!!", "Evolution", "Tim Berne - Fractured Fairy Tales" }
                .Select(name => File.ReadAllLines($".\\Integration\\Cue\\Resources\\{name}.cue")).ToList();
        }

        [Fact]
        public void Parse_parses_FileName()
        {

        }
    }
}
