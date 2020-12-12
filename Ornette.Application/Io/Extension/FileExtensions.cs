using System.Collections.Generic;
using System.IO;
using MoreCollection.Extensions;

namespace Ornette.Application.Io.Extension
{
    public static class FileExtensions
    {
        public static string Cue => ".cue";
        public static string Txt => ".txt";
        public static string Xml => ".xml";
        public static string Pdf => ".pdf";

        private static readonly IDictionary<string, FileType> _Dictionary = new Dictionary<string, FileType>();

        private static void Associate(string fileName, FileType fileType) => _Dictionary.Add(fileName, fileType);

        private static void Associate(string[] fileNames, FileType fileType) => fileNames.ForEach(music => Associate(music, fileType));

        static FileExtensions()
        {
            Associate(MusicExtensions.Lossless, FileType.LosslessMusic);
            Associate(MusicExtensions.Lossy, FileType.LoosyMusic);
            Associate(ImageExtensions.All, FileType.Image);
            Associate(ZippedExtensions.All, FileType.Zipped);
            Associate(Cue, FileType.Cue);
            Associate(Pdf, FileType.Pdf);
            Associate(Xml, FileType.Xml);
            Associate(Txt, FileType.Txt);
        }

        public static FileType GetFileTypeFromFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return GetFileTypeFromFileExtension(extension);
        }

        public static FileType GetFileTypeFromFileExtension(string extension)
        {
            return _Dictionary.GetOrDefault(extension.ToLower(), FileType.Other);
        }
    }
}
