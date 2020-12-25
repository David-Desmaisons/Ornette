using FluentAssertions;
using Ornette.Application.Integration.Cue.Parser;
using Xunit;

namespace Ornette.Application.Tests.Integration.Cue.Parser
{
    public class CueInstructionTest
    {
        [Theory]
        [InlineData("REM GENRE Jazz", "REM")]
        [InlineData("REM COMMENT \"ExactAudioCopy v0.99pb4\"", "REM")]
        [InlineData("PERFORMER \"Grachan Moncur III\"", "PERFORMER")]
        [InlineData("    TRACK 01 AUDIO", "TRACK")]
        [InlineData("    TITLE \"Air Raid\"", "TITLE")]
        [InlineData("    INDEX 01 00:00:00", "INDEX")]
        public void FromLine_Parses_Command(string content, string expectedCommand)
        {
            var instruction = CueInstruction.FromLine(content);
            instruction.Command.Should().Be(expectedCommand);
        }

        [Theory]
        [InlineData("REM GENRE Jazz", new[] { "GENRE", "Jazz" })]
        [InlineData("REM COMMENT \"ExactAudioCopy v0.99pb4\"", new[] { "COMMENT", "ExactAudioCopy v0.99pb4" })]
        [InlineData("PERFORMER \"Grachan Moncur III\"", new[] {"Grachan Moncur III"})]
        [InlineData("    TRACK 01 AUDIO", new[] { "01", "AUDIO" })]
        [InlineData("    TITLE \"Air Raid\"", new[] { "Air Raid"})]
        [InlineData("    INDEX 01 00:00:00", new[] { "01", "00:00:00" })]
        public void FromLine_Parses_Parameters(string content, string[] expectedParameters)
        {
            var instruction = CueInstruction.FromLine(content);
            instruction.Parameters.Should().Equal(expectedParameters);
        }

        [Theory]
        [InlineData("REM")]
        [InlineData("bla")]
        [InlineData("TITLE \"a")]
        public void FromLine_Returns_Null_When_Content_Is_Invalid(string content)
        {
            var instruction = CueInstruction.FromLine(content);
            instruction.Should().BeNull();
        }
    }
}
