using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreCollection.Extensions;
using Ornette.Application.Infra;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using Ornette.Application.Model;
using Ornette.Application.Model.Descriptions;
using TagLib;

namespace Ornette.IO.Implementation
{
    public class IoReader : IIoReader
    {
        public IEnumerable<Track> GetDirectoryTracks(string path)
        {
            return GetByExtension(path, MusicExtensions.All).Select(GetTrack).Where(track => track != null);
        }

        public FolderContext GetFolderContext(string path)
        {
            var children = new Dictionary<string, FolderContext>();
            Directory.GetDirectories(path)
                .ForEach(directory => children.Add(directory, GetFolderContext(directory)));

            var fileByType = new Dictionary<FileType, string[]>();
            Directory.GetFiles(path)
                    .GroupBy(FileExtensions.GetFileTypeFromFileName)
                    .ForEach(g => fileByType.Add(g.Key, g.ToArray()));
            return new FolderContext(path, children, fileByType);
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
            return new ImageDescription(url, picture.Filename, (ImageType)picture.Type);
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
