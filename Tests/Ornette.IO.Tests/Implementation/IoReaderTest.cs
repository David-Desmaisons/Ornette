using Xunit;

namespace Ornette.IO.Implementation.Tests
{
    public class IoReaderTest
    {
        private readonly IoReader _IoReader;

        public IoReaderTest()
        {
            _IoReader = new IoReader();
        }

        [Fact]
        public void GetFolderContext_Returns_Correct_Info()
        {
            var result = _IoReader.GetFolderContext("");
        }
    }
}
