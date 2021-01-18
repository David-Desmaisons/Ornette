using System;
using System.Collections.Generic;
using Ornette.Application.Io;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public class ClusterFactory: IClusterFactory
    {
        public IEnumerable<MusicCluster> GetClustersFromFolderContext(FolderContext context)
        {
            throw new NotImplementedException();
        }
    }
}
