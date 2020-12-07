using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ornette.Application.Infra;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using Ornette.Application.Model;
using Ornette.Application.Model.Descriptions;
using TagLib;

namespace Ornette.IO
{
    public class IoReader : IIoReader
    {
        public IEnumerable<Track> GetDirectoryTracks(string path)
        {
            return GetByExtension(path, MusicExtensions.All).Select(GetTrack).Where(track => track != null);
        }

        public FolderContext GetFolderContext(string path)
        {
            var tracks = GetDirectoryTracks(path);
            var images = GetByExtension(path, ImageExtensions.All).ToArray();
            var cue = Directory.GetFiles(path, $"*{FileExtensions.Cue}");
            var children = new Dictionary<string, FolderContext>();
            foreach (var directory in Directory.GetDirectories(path))
            {
                children.Add(directory, GetFolderContext(directory));
            }
            return new FolderContext(path, children, tracks.ToArray(), images, cue); ;
        }

        private static IEnumerable<string> GetByExtension(string path, IEnumerable<string> extensions)
        {
            return extensions.SelectMany(ext => Directory.GetFiles(path, $"*{ext}"));
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
                .SetImages(tag.Pictures?.Select(GetImage));

            return new TrackDescriptionBuilder()
                .SetAlbum(albumBuilder)
                .SetName(tag.Title)
                .SetTrackNumber(tag.Track)
                .SetDuration(properties.Duration)
                .Build();
        }

        private static ImageDescription GetImage(IPicture picture)
        {
            var url = UrlHelper.BuildFromByteArray(picture.MimeType, picture.Data.Data);
            return new ImageDescription(url, picture.Filename, (ImageType) picture.Type);
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
