namespace Ornette.Application.Model
{
    public class TrackMetaData
    {
        public TrackMetaData(string name, int? trackNumber = null)
        {
            TrackNumber = trackNumber;
            Name = name;
        }

        public int? TrackNumber { get; }
        public string Name { get; }
    }
}
