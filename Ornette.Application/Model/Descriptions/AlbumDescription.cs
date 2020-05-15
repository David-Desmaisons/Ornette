namespace Ornette.Application.Model.Descriptions
{
    public class AlbumDescription
    {
        public AlbumDescription(string name, ArtistDescription[] artists)
        {
            Name = name;
            Artists = artists;
        }

        public string Name { get; }
        public ArtistDescription[] Artists { get; }
    }
}
