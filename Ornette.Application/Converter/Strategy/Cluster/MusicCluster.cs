using System.Collections.Generic;
using System.Linq;
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

        public override string ToString()
        {
            return $@"Folder: ""{MainFolder}"" Lossless: {IsLossless} {{{string.Join(", ", Files.Select(f => $"{f.Key} : [{string.Join(",", f.Value.Select(value => $@"""{value}"""))}]"))}}}";
        }
    }
}
 