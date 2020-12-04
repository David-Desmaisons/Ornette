using System;
using Xunit;
using Ornette.Application.Converter;
using Ornette.Application.Converter.Command;
using Ornette.Application.Converter.Mp3;
using Xunit.Abstractions;

namespace Ornette.Application.Tests.Converter
{
    public class ApiTests
    {
        private readonly ITestOutputHelper _TestOutputHelper;

        public ApiTests(ITestOutputHelper testOutputHelper)
        {
            _TestOutputHelper = testOutputHelper;
        }

        private const string _Directory = "";
        private const string _DirectoryTarget = "";

        [Fact]
        public void CheckApi()
        {
            var converter = default(IMusicConverter<Mp3ConverterCommand>);
            var command = new Mp3ConverterCommand(_Directory, _DirectoryTarget, Mp3Encoding.HighestStandard);
            converter.Convert(command).Subscribe(file => _TestOutputHelper.WriteLine(file.Path));
        }
    }
}
