namespace Ornette.Application.Model.Descriptions
{
    public class AlbumDescription
    {
        public AlbumDescription(string name, string[] artists, string[] genres, uint? trackCount, uint? year, ImageDescription[] images)
        {
            Name = name;
            Artists = artists;
            Genres = genres;
            Year = year;
            Images = images;
            TrackCount = trackCount;
        }

        public string Name { get; }
        public uint? Year { get; }
        public uint? TrackCount { get; }
        public string[] Artists { get; }
        public string[] Genres { get; }
        public ImageDescription[] Images { get;  }
    }
}
