using System;
using Xunit;
using Ornette.Application.Converter;
using Ornette.Application.Converter.Command;
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
            var builder = default(IMusicConverterBuilder);
            var command = new FolderImporterCommand(_Directory, _DirectoryTarget, Mp3Encoding.HighestStandard);
            var converter = builder.BuildFromDirectory(command);
            converter.Start().Subscribe(file => _TestOutputHelper.WriteLine(file.Path));
        }
    }
}
