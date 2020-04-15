namespace Ornette.Application.Model
{
    public class Track
    {
        public string Path { get; }

        public TrackMetaData MetaData { get; }

        public Track(string path, TrackMetaData metaData)
        {
            Path = path;
            MetaData = metaData;
        }
    }
}
