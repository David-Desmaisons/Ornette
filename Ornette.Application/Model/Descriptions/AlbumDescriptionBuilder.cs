using System.Collections.Generic;
using System.Linq;

namespace Ornette.Application.Model.Descriptions
{
    public class AlbumDescriptionBuilder
    {
        public AlbumDescription Build()
        {
            return new AlbumDescription(Name, Artists, Genres, TrackCount, Year, Images);
        }

        public string Name { get; private set; }
        public uint? Year { get; private set; }
        public uint? TrackCount { get; private set; }
        public string[] Artists { get; private set; }
        public string[] Genres { get; private set; }
        public ImageDescription[] Images { get; private set; }

        public AlbumDescriptionBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        public AlbumDescriptionBuilder SetYear(uint? year)
        {
            Year = year;
            return this;
        }

        public AlbumDescriptionBuilder SetTrackCount(uint? trackCount)
        {
            TrackCount = trackCount;
            return this;
        }

        public AlbumDescriptionBuilder SetArtists(string[] artists)
        {
            Artists = artists;
            return this;
        }

        public AlbumDescriptionBuilder SetGenres(string[] genres)
        {
            Genres = genres;
            return this;
        }

        public AlbumDescriptionBuilder SetImages(ImageDescription[] images)
        {
            Images = images;
            return this;
        }

        public AlbumDescriptionBuilder SetImages(IEnumerable<ImageDescription> images)
        {
            Images = images?.ToArray();
            return this;
        }
    }
}
