using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ornette.Application.Io;
using Ornette.Application.Model;
using Ornette.Application.Model.Descriptions;
using TagLib;

namespace Ornette.IO
{
    public class IoReader : IIoReader
    {
        public IEnumerable<Track> GetDirectoryTracks(string path)
        {
            var allFiles = FileExtension.AllExtensions.SelectMany(ext => Directory.GetFiles(path, $"*{ext}"));
            return allFiles.Select(GetTrack).Where(track => track != null);
        }

        public Track GetTrack(string filePath)
        {
            var description = ToTrackDescription(filePath);
            return new Track(filePath, description);
        }

        private static TrackDescription ToTrackDescription(string filePath)
        {
            var tagFile = TagLib.File.Create(filePath);
            var tag = tagFile.Tag;
            var properties = tagFile.Properties;

            var albumBuilder = new AlbumDescriptionBuilder()
                .SetName(tag.Album)
                .SetArtists(GetArtists(tag))
                .SetGenres(tag.Genres)
                .SetTrackCount(tag.TrackCount)
                .SetYear(tag.Year)
                .SetImages(tag.Pictures?.Select(p => new ImageDescription(p.Filename, p.Filename, (ImageType)p.Type)));

            return new TrackDescriptionBuilder()
                .SetAlbum(albumBuilder)
                .SetName(tag.Title)
                .SetTrackNumber(tag.Track)
                .SetDuration(properties.Duration)
                .Build();
        }

        private static string[] GetArtists(Tag tag)
        {
            if (tag.AlbumArtists?.Length > 0)
                return tag.AlbumArtists;

            var artist = tag.FirstAlbumArtist ?? tag.FirstPerformer;
            return new[] { artist };
        }
    }
}
