using Ornette.Application.Converter.Command;
using Ornette.Application.Model.Descriptions;

namespace Ornette.Application.Converter
{
    public class ConvertedFile
    {
        public ConvertedFile(string path, TrackDescription description, ImportCommand context)
        {
            Path = path;
            Context = context;
            Description = description;
        }

        public string Path { get; }
        public TrackDescription Description { get; }
        public ImportCommand Context { get; }
    }
}