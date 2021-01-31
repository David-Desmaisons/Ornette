using FluentAssertions;
using Ornette.Application.Converter.Strategy.Cluster;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Ornette.Application.Tests.Converter.Strategy.Cluster
{
    public class ClusterFactoryTest
    {
        private readonly ClusterFactory _ClusterFactory;
        private const string RootPath = "C:\\root";
        private const string Scans = "scans";

        public ClusterFactoryTest()
        {
            _ClusterFactory = new ClusterFactory();
        }

        public static IEnumerable<object[]> GetTestData()
        {
            var emptyChildren = new Dictionary<string, FolderContext>();
            var noMusic = new FolderContext(RootPath, emptyChildren, new Dictionary<FileType, string[]>
                {
                    { FileType.Image, new []{"a.jpg", "b.gif"}},
                    { FileType.Cue, new []{"a.cue"}}
                }
            );
            yield return new object[]
            {
                noMusic,
                Enumerable.Empty<MusicCluster>()
            };

            var simpleLoosyContext = new FolderContext(RootPath, emptyChildren, new Dictionary<FileType, string[]>
                {
                    { FileType.LoosyMusic, new []{"a.mp3", "b.mp3"}}
                }
            );
            var simpleLoosyClusterCluster = new MusicCluster(RootPath, false, simpleLoosyContext.Files);
            yield return new object[]
            {
                simpleLoosyContext,
                new [] { simpleLoosyClusterCluster }
            };

            var simpleLosslessContext = new FolderContext(RootPath, emptyChildren, new Dictionary<FileType, string[]>
                {
                    { FileType.LosslessMusic, new []{"a.flac", "b.flac"}}
                }
            );
            var simpleLosslessCluster = new MusicCluster(RootPath, true, simpleLosslessContext.Files);
            yield return new object[]
            {
                simpleLosslessContext,
                new [] { simpleLosslessCluster }
            };

            var simpleMixedContext = new FolderContext(RootPath, emptyChildren, new Dictionary<FileType, string[]>
                {
                    { FileType.LosslessMusic, new []{"a.flac", "b.flac"}},
                    { FileType.LoosyMusic, new []{"a.mp3", "b.mp3"}}
                }
            );
            yield return new object[]
            {
                simpleMixedContext,
                new [] { simpleLoosyClusterCluster, simpleLosslessCluster }
            };

            var imageFolder = new FolderContext(Path.Combine(RootPath, Scans), emptyChildren, new Dictionary<FileType, string[]>
            {
                { FileType.Image, new []{"a.jpeg", "b.jpeg"}}
            });
            var contextWithImageFolder = new FolderContext(RootPath, new Dictionary<string, FolderContext>
                {
                    { Scans, imageFolder }
                }, 
                new Dictionary<FileType, string[]>
                {
                    { FileType.LosslessMusic, new []{"a.flac"}},
                    { FileType.Image, new []{"covert.gif"}},
                }
            );
            var groupedArtCluster = new MusicCluster(RootPath, true, new Dictionary<FileType, string[]>
            {
                { FileType.LosslessMusic, new []{"a.flac"}},
                { FileType.Image, new []{ "covert.gif", "a.jpeg", "b.jpeg"}}
            });

            yield return new object[]
            {
                contextWithImageFolder,
                new [] { groupedArtCluster }
            };
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void GetClustersFromFolderContext_Creates_Cluster_From_FolderContext(FolderContext context,
            IEnumerable<MusicCluster> expectedClusters)
        {
            var result = _ClusterFactory.GetClustersFromFolderContext(context);

            result.Should().BeEquivalentTo(expectedClusters);
        }
    }
}
