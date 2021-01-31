using FluentAssertions;
using Ornette.Application.Converter.Strategy.Cluster;
using Ornette.Application.Io;
using System.Collections.Generic;
using Ornette.Application.Tests.Converter.Strategy.Cluster.Helper;
using Xunit;

namespace Ornette.Application.Tests.Converter.Strategy.Cluster
{
    public class ClusterFactoryTest
    {
        private readonly ClusterFactory _ClusterFactory;


        public ClusterFactoryTest()
        {
            _ClusterFactory = new ClusterFactory();
        }

        [Theory]
        [ClassData(typeof(ClusterDataProvider))]
        public void GetClustersFromFolderContext_Creates_Cluster_From_FolderContext(FolderContext context,
            IEnumerable<MusicCluster> expectedClusters)
        {
            var result = _ClusterFactory.GetClustersFromFolderContext(context);

            result.Should().BeEquivalentTo(expectedClusters);
        }
    }
}
