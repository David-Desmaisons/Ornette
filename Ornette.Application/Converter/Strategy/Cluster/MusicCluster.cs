using System.Collections.Generic;
using Ornette.Application.Io.Extension;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public class MusicCluster
    {
        public IReadOnlyDictionary<FileType, string[]> Files { get; }

        public string MainFolder { get; }
        public bool IsLossless { get; }

        public MusicCluster(string mainFolder, bool isLossless, IReadOnlyDictionary<FileType, string[]> files)
        {
            MainFolder = mainFolder;
            IsLossless = isLossless;
            Files = files;
        }
    }
}
