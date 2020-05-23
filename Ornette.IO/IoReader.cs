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
            return allFiles.Select(ToTrack).Where(track => track != null);
        }

        public Track ToTrack(string filePath)
        {
            var description = ToTrackDescription(filePath);
            return new Track(filePath, description);
        }

        private static TrackDescription ToTrackDescription(string filePath)
        {
            var tagFile = TagLib.File.Create(filePath);
            var tag = tagFile.Tag;
            var album = new AlbumDescription(tag.Album, tag.AlbumArtists, tag.Genres, tag.TrackCount, tag.Year);
            return new TrackDescription(tag.Title, album, tag.Track, tagFile.Properties.Duration);
        }
    }
}
