using System;
using FluentAssertions;
using Ornette.Application.Integration.Cue;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ornette.Application.Integration.Cue.Parser;
using Xunit;

namespace Ornette.Application.Tests.Integration.Cue
{
    public class CueParserTest
    {
        private readonly CueParser _CueParser;
        private readonly List<string[]> _CueContents;
        private readonly List<string[]> _InvalidCueContents;


        public CueParserTest()
        {
            _CueParser = new CueParser();
            _CueContents = new[] { "Andrew Hill - Andrew!!!", "Tim Berne - Fractured Fairy Tales", "The Specials" }
                .Select(ToFileContent).ToList();

            _InvalidCueContents = new[] { "Evolution" }.Select(ToFileContent).ToList();
        }

        private static string[] ToFileContent(string fileName) => File.ReadAllLines($".\\Integration\\Cue\\Resources\\{fileName}.cue");

        [Theory]
        [InlineData(0, "Andrew!!!")]
        [InlineData(1, "Fractured Fairy Tales")]
        public void Parse_Parses_Title(int index, string expectedTitle)
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

        [Fact]
        public void Parse_Parses_Remarks()
        {
            var res = _CueParser.Parse(_CueContents[0]);
            res.Remarks.Should().BeEquivalentTo(new Dictionary<string, IList<string>>()
            {
                {"GENRE", new []{"Jazz"} },
                {"DISCID", new []{ "730C0608" } },
                {"COMMENT", new []{"ExactAudioCopy v0.99pb4" }},
            });
        }

        [Theory]
        [InlineData(0, new[] { "Andrew Hill - Andrew!!!.wav" })]
        [InlineData(1, new[] { "Tim Berne - Fractured Fairy Tales.flac" })]
        [InlineData(2, new[] { "The Specials - Singles - 01 - Gangsters.wav", "The Specials - Singles - 02 - Rudi, A Message To You.wav" })]
        public void Parse_Parses_File_Name(int index, string[] expectedFilesName)
        {
            var res = _CueParser.Parse(_CueContents[index]);
            res.Files.Select(f => f.Name).Should().BeEquivalentTo(expectedFilesName);
        }

        [Theory]
        [InlineData(0, "WAVE")]
        [InlineData(1, "WAVE")]
        public void Parse_Parses_File_Type(int index, string expectedFileType)
        {
            var res = _CueParser.Parse(_CueContents[index]);
            res.Files.Select(f => f.Type).Should().BeEquivalentTo(new string[] { expectedFileType });
        }

        [Theory]
        [InlineData(0, 8)]
        [InlineData(1, 6)]
        public void Parse_Parses_File_Tracks_Name(int index, int expectedTrackNumber)
        {
            var res = _CueParser.Parse(_CueContents[index]);
            var file = res.Files[0];
            file.Tracks.Select(f => f.Number).Should().BeEquivalentTo(Enumerable.Range(1, expectedTrackNumber));
        }

        [Theory]
        [InlineData(0, 8)]
        [InlineData(1, 6)]
        public void Parse_Parses_File_Tracks_Type(int index, int expectedTrackNumber)
        {
            var res = _CueParser.Parse(_CueContents[index]);
            var file = res.Files[0];
            file.Tracks.Select(f => f.Type).Should().BeEquivalentTo(Enumerable.Repeat("AUDIO", expectedTrackNumber));
        }

        [Theory]
        [InlineData(0, 8, "Andrew Hill")]
        [InlineData(1, 6, "Tim Berne")]
        public void Parse_Parses_File_Tracks_Performer(int index, int expectedTrackNumber, string expectedPerformer)
        {
            var res = _CueParser.Parse(_CueContents[index]);
            var file = res.Files[0];
            file.Tracks.Select(f => f.Performer).Should().BeEquivalentTo(Enumerable.Repeat(expectedPerformer, expectedTrackNumber));
        }

        [Theory]
        [InlineData(0, new[] { "The Griots", "Black Monday", "Duplicity", "Le Serpent Qui Danse", "No Doubt", "Symmetry", "The Griots (alternate take)", "Symmetry (alternate take)" })]
        public void Parse_Parses_File_Tracks_Title(int index, IList<string> expectedNames)
        {
            var res = _CueParser.Parse(_CueContents[index]);
            var file = res.Files[0];
            file.Tracks.Select(f => f.Title).Should().BeEquivalentTo(expectedNames);
        }

        [Fact]
        public void Parse_Parses_File_Tracks_Index_0()
        {
            var expectedCueIndexes = new[]
            {
                default(CueIndex?),
                new CueIndex(6, 2, 55),
                new CueIndex(14, 59, 0),
                new CueIndex(21, 9, 60),
                new CueIndex(28, 5, 61),
                new CueIndex(32, 29, 22),
                new CueIndex(39, 37, 59),
                new CueIndex(44, 44, 64)
            };
            var res = _CueParser.Parse(_CueContents[0]);
            var file = res.Files[0];
            file.Tracks.Select(f => f.GetIndex(0)).Should().BeEquivalentTo(expectedCueIndexes);
        }

        [Fact]
        public void Parse_Parses_File_Tracks_Index_1()
        {
            var expectedCueIndexes = new[]
            {
                new CueIndex(0, 0, 0),
                new CueIndex(6, 4, 73),
                new CueIndex(15, 0, 53),
                new CueIndex(21, 12, 39),
                new CueIndex(28, 8, 37),
                new CueIndex(32, 31, 44),
                new CueIndex(39, 40, 37),
                new CueIndex(44, 46, 55)
            };
            var res = _CueParser.Parse(_CueContents[0]);
            var file = res.Files[0];
            file.Tracks.Select(f => f.GetIndex(1)).Should().BeEquivalentTo(expectedCueIndexes);
        }

        [Fact]
        public void Parse_Parses_File_Tracks_Index_2()
        {
            var expectedCueIndexes = new[]
            {
                default(CueIndex?),
                default(CueIndex?),
                default(CueIndex?),
                new CueIndex(34,22,34),
                default(CueIndex?),
                default(CueIndex?),
            };
            var res = _CueParser.Parse(_CueContents[1]);
            var file = res.Files[0];
            file.Tracks.Select(f => f.GetIndex(2)).Should().BeEquivalentTo(expectedCueIndexes);
        }

        [Fact]
        public void Parse_Parses_File_Tracks_Pregap()
        {
            var expectedCueIndexes = new[]
            {
                new CueIndex(0,0,28),
            };
            var res = _CueParser.Parse(_CueContents[2]);
            var file = res.Files[1];
            file.Tracks.Select(f => f.PreGap).Should().BeEquivalentTo(expectedCueIndexes);
        }

        [Fact]
        public void Parse_Not_Supported_Format_Throws()
        {
            Action parse = () => _CueParser.Parse(_InvalidCueContents[0]);
            parse.Should().Throw<CueParseException>()
                .Where(exception => exception.Line == 15)
                .WithInnerException<ArgumentOutOfRangeException>();

        }
    }
}
