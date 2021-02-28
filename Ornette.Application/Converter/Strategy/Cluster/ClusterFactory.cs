using Ornette.Application.Infra;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using System.Collections.Generic;
using System.Linq;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public class ClusterFactory : IClusterFactory
    {
        private static readonly bool[] _IsLosslessValues = { true, false };

        public IEnumerable<MusicCluster> GetClustersFromFolderContext(FolderContext context)
        {
            return GetClusterBuildersFromFolderContext(context, null).Select(builder => builder.Build());
        }

        private static IEnumerable<ClusterBuilder> GetClusterBuildersFromFolderContext(FolderContext context,
            ClusterBuilder rootBuilder)
        {
            var hasLossless = context.Has(FileType.LosslessMusic);
            var hasLoosy = context.Has(FileType.LoosyMusic);

            if ((hasLossless || hasLoosy))
            {
                return _IsLosslessValues.SelectMany(isLossless => GetClusterBuildersFromFolderContext(context, isLossless));
            }

            if (rootBuilder == null)
            {
                return context.Children.Values
                    .SelectMany(childContext => GetClusterBuildersFromFolderContext(childContext, null))
                    .Select(builder => builder.Merge(context));
            }

            rootBuilder.Merge(context);
            return Enumerable.Empty<ClusterBuilder>();
        }

        private static IEnumerable<ClusterBuilder> GetClusterBuildersFromFolderContext(FolderContext context, bool isLossless)
        {
            var lookingFor = isLossless ? FileType.LosslessMusic : FileType.LoosyMusic;
            var except = isLossless ? FileType.LoosyMusic : FileType.LosslessMusic;

            if (!context.Has(lookingFor))
            {
                yield break;
            }

            var builder = new ClusterBuilder(context.Path, isLossless, context.Files.DuplicateExcept(except));
            foreach (var cluster in context.Children.Values.SelectMany(childContext => GetClusterBuildersFromFolderContext(childContext, builder)))
            {
                yield return cluster;
            }
            yield return builder;
        }
    }
}
