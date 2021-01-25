using System;
using System.Collections.Generic;
using System.Linq;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public class ClusterFactory: IClusterFactory
    {
        public IEnumerable<MusicCluster> GetClustersFromFolderContext(FolderContext context)
        {
            if (context.Has(FileType.LoosyMusic))
            {
                var files = context.Files;
                yield return new MusicCluster(context.Path, false, context.Files);
            }

            if (context.Has(FileType.LosslessMusic))
            {
                yield return new MusicCluster(context.Path, true, context.Files);
            }
        }

        private IEnumerable<MusicCluster> PrivateGetClustersFromFolderContext(FolderContext context)
        {
            return Enumerable.Empty<MusicCluster>();
        }
    }
}
