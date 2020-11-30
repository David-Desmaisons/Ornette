namespace Ornette.Application.Converter
{
    public class ConvertedFile
    {
        public ConvertedFile(string path, IConvertContext context)
        {
            Path = path;
            Context = context;
        }

        public string Path { get; }
        public IConvertContext Context { get; }
    }
}