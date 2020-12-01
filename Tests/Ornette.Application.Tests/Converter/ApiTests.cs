using Xunit;
using Ornette.Application.Converter;

namespace Ornette.Application.Tests.Converter
{
    public class ApiTests
    {
        private const string _Directory = "";

        [Fact]
        public void CheckApi()
        {
            var builder = default(IMusicConverterBuilder);
            var converter = builder.BuildFromDirectory(_Directory, new TargetDestination());
            converter.Start();
        }
    }
}
