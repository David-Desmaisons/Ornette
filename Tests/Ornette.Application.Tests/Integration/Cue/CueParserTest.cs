using Ornette.Application.Integration.Cue;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
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
            _CueContents = new[] { "Andrew Hill - Andrew!!!", "Tim Berne - Fractured Fairy Tales", "Evolution" }
                .Select(name => File.ReadAllLines($".\\Integration\\Cue\\Resources\\{name}.cue")).ToList();
        }

        [Theory]
        [InlineData(0, "Andrew!!!")]
        [InlineData(1, "Fractured Fairy Tales")]
        public void Parse_Parses_FileName(int index, string expectedTitle)
        {
            var res = _CueParser.Parse(_CueContents[index]);
            res.Title.Should().Be(expectedTitle);
        }

        [Theory]
        [InlineData(0, "Andrew Hill")]
        [InlineData(1, "Tim Berne")]
        public void Parse_Parses_Performer(int index, string expectedPerformer)
        {
            var res = _CueParser.Parse(_CueContents[index]);
            res.Performer.Should().Be(expectedPerformer);
        }
    }
}
