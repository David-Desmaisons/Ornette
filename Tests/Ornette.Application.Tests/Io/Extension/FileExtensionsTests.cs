using FluentAssertions;
using Ornette.Application.Io.Extension;
using Xunit;

namespace Ornette.Application.Tests.Io.Extension
{
    public class FileExtensionsTests
    {
        [Theory]
        [InlineData(".cue", FileType.Cue)]
        [InlineData(".mp3", FileType.LoosyMusic)]
        [InlineData(".aiff", FileType.LoosyMusic)]
        [InlineData(".ape", FileType.LosslessMusic)]
        [InlineData(".FLAC", FileType.LosslessMusic)]
        [InlineData(".zip", FileType.Zipped)]
        [InlineData(".rar", FileType.Zipped)]
        [InlineData(".txt", FileType.Txt)]
        [InlineData(".pdf", FileType.Pdf)]
        [InlineData(".xmL", FileType.Xml)]
        [InlineData(".exe", FileType.Other)]
        public void GetFileTypeFromFileExtension_Returns_Type_From_Extension(string extension, FileType expectedType)
        {
            var result = FileExtensions.GetFileTypeFromFileExtension(extension);

            result.Should().Be(expectedType);
        }

        [Theory]
        [InlineData("a-love-supreme.cue", FileType.Cue)]
        [InlineData("foxy-lady.mp3", FileType.LoosyMusic)]
        [InlineData("iWannaBeYourDog.aiff", FileType.LoosyMusic)]
        [InlineData("le-fleuve.ape", FileType.LosslessMusic)]
        [InlineData("Lonely Woman.FLAC", FileType.LosslessMusic)]
        [InlineData("WhenTheBluesWillLeave.zip", FileType.Zipped)]
        [InlineData("CatfishBlues.rar", FileType.Zipped)]
        [InlineData("1isTheLoneliestNumber.txt", FileType.Txt)]
        [InlineData("NoQuarter.pdf", FileType.Pdf)]
        [InlineData("WhenTheLeveeBreaks.xmL", FileType.Xml)]
        [InlineData("AlBlues.exe", FileType.Other)]
        public void GetFileTypeFromFileName_Returns_Type_From_Extension(string extension, FileType expectedType)
        {
            var result = FileExtensions.GetFileTypeFromFileName(extension);

            result.Should().Be(expectedType);
        }
    }
}
