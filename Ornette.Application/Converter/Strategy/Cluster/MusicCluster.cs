using System.Collections.Generic;
using MoreCollection.Extensions;
using Ornette.Application.Io.Extension;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public class MusicCluster
    {
        private readonly Dictionary<FileType, string[]> _Files;

        public string MainFolder { get; }
        public bool IsLossless { get; }

        public MusicCluster(string mainFolder, string[] cueSheetFiles, bool isLossless, string[] associatedFiles, Dictionary<FileType, string[]> files)
        {
            MainFolder = mainFolder;
            IsLossless = isLossless;
            _Files = files;
        }

        public string[] Get(FileType fileType) => _Files.GetOrDefault(fileType);
    }
}
