using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ornette.Application.Io;
using Ornette.Application.Model;
using Ornette.Application.Model.Descriptions;

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

            var album = new AlbumDescriptionBuilder()
                .SetName(tag.Album)
                .SetArtists(tag.AlbumArtists)
                .SetGenres(tag.Genres)
                .SetTrackCount(tag.TrackCount)
                .SetYear(tag.Year);

            return new TrackDescriptionBuilder()
                .SetAlbum(album)
                .SetName(tag.Title)
                .SetTrackNumber(tag.Track)
                .SetDuration(tagFile.Properties.Duration)
                .Build();
        }
    }
}
