using Ornette.Application.Model.Descriptions;

namespace Ornette.Application.Converter
{
    public class ConvertedFile
    {
        public ConvertedFile(string path, TrackDescription description, IConvertContext context)
        {
            Path = path;
            Context = context;
            Description = description;
        }

        public string Path { get; }
        public TrackDescription Description { get; }
        public IConvertContext Context { get; }
    }
}