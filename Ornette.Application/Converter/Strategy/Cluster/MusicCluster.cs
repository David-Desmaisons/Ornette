using System.Collections.Generic;
using MoreCollection.Extensions;
using Ornette.Application.Io.Extension;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public class MusicCluster
    {
        public Dictionary<FileType, string[]> Files { get; }

        public string MainFolder { get; }
        public bool IsLossless { get; }

        public MusicCluster(string mainFolder, bool isLossless, Dictionary<FileType, string[]> files)
        {
            MainFolder = mainFolder;
            IsLossless = isLossless;
            Files = files;
        }

        public string[] Get(FileType fileType) => Files.GetOrDefault(fileType);
    }
}
