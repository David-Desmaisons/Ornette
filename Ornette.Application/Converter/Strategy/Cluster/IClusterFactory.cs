using System.Collections.Generic;
using Ornette.Application.Io;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public interface IClusterFactory
    {
        IEnumerable<MusicCluster> GetClustersFromFolderContext(FolderContext context);
    }
}
