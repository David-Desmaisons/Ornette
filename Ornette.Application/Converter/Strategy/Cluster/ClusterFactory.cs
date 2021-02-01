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
            return GetClustersFromFolderContext(context, null);
        }

        private IEnumerable<MusicCluster> GetClustersFromFolderContext(FolderContext context, ClusterBuilder clusterBuilder)
        {
            var hasLossless = context.Has(FileType.LosslessMusic);
            var hasLoosy = context.Has(FileType.LoosyMusic);

            if ((clusterBuilder != null) && (!hasLossless && !hasLoosy))
            {
                clusterBuilder.Merge(context);
                yield break;
            }

            if (hasLoosy)
            {
                var builder = new ClusterBuilder(context.Path, false, context.Files.DuplicateExcept(FileType.LosslessMusic));
                foreach (var cluster in context.Children.Values.SelectMany(childContext => GetClustersFromFolderContext(childContext, builder)))
                {
                    yield return cluster;
                }
                yield return builder.Build();
            }

            if (hasLossless)
            {
                var builder = new ClusterBuilder(context.Path, true, context.Files.DuplicateExcept(FileType.LoosyMusic));
                foreach (var cluster in context.Children.Values.SelectMany(childContext => GetClustersFromFolderContext(childContext, builder)))
                {
                    yield return cluster;
                }
                yield return builder.Build();
            }

            if ((!hasLossless && !hasLoosy))
            {
                foreach (var cluster in context.Children.Values.SelectMany(childContext => GetClustersFromFolderContext(childContext, null)))
                {
                    yield return cluster;
                }
            }
        }

        private IEnumerable<MusicCluster> PrivateGetClustersFromFolderContext(FolderContext context)
        {
            return Enumerable.Empty<MusicCluster>();
        }
    }
}
