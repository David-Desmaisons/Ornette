using Ornette.Application.Model.Descriptions;

namespace Ornette.Application.Model
{
    public class Track
    {
        public string Path { get; }
        public TrackDescription MetaData { get; }

        public Track(string path, TrackDescription description)
        {
            Path = path;
            MetaData = description;
        }
    }
}
