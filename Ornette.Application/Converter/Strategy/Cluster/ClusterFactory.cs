using System.Collections.Generic;
using System.Linq;
using Ornette.Application.Infra;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public class ClusterFactory: IClusterFactory
    {
        public IEnumerable<MusicCluster> GetClustersFromFolderContext(FolderContext context)
        {
            return GetClusterBuildersFromFolderContext(context, null).Select(builder => builder.Build());
        }

        private IEnumerable<ClusterBuilder> GetClusterBuildersFromFolderContext(FolderContext context, ClusterBuilder rootBuilder)
        {
            var hasLossless = context.Has(FileType.LosslessMusic);
            var hasLoosy = context.Has(FileType.LoosyMusic);

            if ((!hasLossless && !hasLoosy))
            {
                if (rootBuilder != null)
                {
                    rootBuilder.Merge(context);
                    yield break;
                }

                foreach (var cluster in context.Children.Values.SelectMany(childContext => GetClusterBuildersFromFolderContext(childContext, null)))
                {
                    yield return cluster;
                }
                yield break;
            }

            if (hasLoosy)
            {
                var builder = new ClusterBuilder(context.Path, false, context.Files.DuplicateExcept(FileType.LosslessMusic));
                foreach (var cluster in context.Children.Values.SelectMany(childContext => GetClusterBuildersFromFolderContext(childContext, builder)))
                {
                    yield return cluster;
                }
                yield return builder;
            }

            if (hasLossless)
            {
                var builder = new ClusterBuilder(context.Path, true, context.Files.DuplicateExcept(FileType.LoosyMusic));
                foreach (var cluster in context.Children.Values.SelectMany(childContext => GetClusterBuildersFromFolderContext(childContext, builder)))
                {
                    yield return cluster;
                }
                yield return builder;
            }
        }
    }
}
