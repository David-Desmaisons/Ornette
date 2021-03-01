using MoreCollection.Extensions;
using Ornette.Application.Infra;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using System.Collections.Generic;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    internal class ClusterBuilder
    {
        public Dictionary<FileType, List<string>> Files { get; private set; }

        public string MainFolder { get; }
        public bool IsLossless { get; }

        public ClusterBuilder(string mainFolder, bool isLossless, Dictionary<FileType, string[]> files)
        {
            MainFolder = mainFolder;
            IsLossless = isLossless;
            Files = files.Convert();
        }

        public MusicCluster Build() => new MusicCluster(MainFolder, IsLossless, Files.Convert());

        internal ClusterBuilder Merge(FolderContext context)
        {
            Files = Files.Merge(context.Files);
            return this;
        }

        internal ClusterBuilder Merge(IReadOnlyDictionary<FileType, List<string>> context)
        {
            Files = Files.Merge(context);
            return this;
        }
    }
}
