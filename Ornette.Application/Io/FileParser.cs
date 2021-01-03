using Ornette.Application.Infra;

namespace Ornette.Application.Io
{
    public class FileParser<T> : IFileParser<T> where T : class
    {
        private readonly IFileReader _Reader;
        private readonly IParser<T> _Parser;

        public FileParser(IFileReader reader, IParser<T> parser)
        {
            _Reader = reader;
            _Parser = parser;
        }

        public T Parse(string path)
        {
            var content = _Reader.ReadAllLines(path);
            return _Parser.Parse(content);
        }
    }
}
