using Ornette.Application.Converter.Strategy.Cluster;
using Ornette.Application.Infra;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ornette.Application.Tests.Converter.Strategy.Cluster.Helper
{
    public class ClusterDataProvider : IEnumerable<object[]>
    {
        private const string RootPath = "C:\\root";
        private const string Scans = "scans";
        private readonly Dictionary<string, FolderContext> _EmptyChildren;
        private readonly FolderContext _SimpleLoosyContext;
        private readonly FolderContext _SimpleLosslessContext;
        private readonly MusicCluster _SimpleLoosyCluster;
        private readonly MusicCluster _SimpleLosslessCluster;
        private readonly Dictionary<string, FolderContext> _ChildrenWithArt;
        private readonly Dictionary<string, FolderContext> _ChildrenWithLoosy;
        private readonly Dictionary<FileType, string[]> _LoosyMusicContent;
        private readonly Dictionary<FileType, string[]> _LosslessMusicContent;
        private readonly Dictionary<FileType, string[]> _ImageContent;
        private readonly Dictionary<FileType, string[]> _ImageSingleContent;
        private readonly Dictionary<FileType, string[]> _CueContent;
        private readonly Dictionary<FileType, string[]> _EmptyContent;

        public ClusterDataProvider()
        {
            _EmptyContent = new Dictionary<FileType, string[]>();
            _LoosyMusicContent = CreateDictionary(FileType.LoosyMusic, "a.mp3", "b.mp3");
            _LosslessMusicContent = CreateDictionary(FileType.LosslessMusic, "a.flac", "b.flac");
            _ImageContent = CreateDictionary(FileType.Image, "a.jpeg", "b.jpeg");
            _ImageSingleContent = CreateDictionary(FileType.Image, "c.gif");
            _CueContent = CreateDictionary(FileType.Cue, "a.cue");

            _EmptyChildren = new Dictionary<string, FolderContext>();
            _SimpleLoosyContext = new FolderContext(RootPath, _EmptyChildren, _LoosyMusicContent);
            _SimpleLosslessContext = new FolderContext(RootPath, _EmptyChildren, _LosslessMusicContent);
            var imageContext = new FolderContext(Path.Combine(RootPath, Scans), _EmptyChildren, _ImageContent);
            _ChildrenWithArt = new Dictionary<string, FolderContext>
            {
                {Scans, imageContext}
            };
            _ChildrenWithLoosy = new Dictionary<string, FolderContext>
            {
                {"sub1", _SimpleLoosyContext}
            };

            _SimpleLoosyCluster = new MusicCluster(RootPath, false, _LoosyMusicContent);
            _SimpleLosslessCluster = new MusicCluster(RootPath, true, _LosslessMusicContent);
        }

        private static Dictionary<FileType, string[]> CreateDictionary(FileType fileType, params string[] values)
            => new Dictionary<FileType, string[]> { { fileType, values } };

        public struct TestData
        {
            public TestData(FolderContext input, params MusicCluster[] expectedResult)
            {
                Input = input;
                ExpectedResult = expectedResult;
            }

            public FolderContext Input { get; }
            public IEnumerable<MusicCluster> ExpectedResult { get; }
        }

        private TestData GetEmptyContext()
        {
            var noMusic = new FolderContext(RootPath, _EmptyChildren, _CueContent.Merge(_ImageContent).Convert());
            return new TestData(noMusic);
        }

        private TestData GetSimpleLoosyContext()
        {
            return new TestData(_SimpleLoosyContext, _SimpleLoosyCluster);
        }

        private TestData GetSimpleLosslessContext()
        {
            return new TestData(_SimpleLosslessContext, _SimpleLosslessCluster);
        }

        private TestData GetSimpleMixedContext()
        {
            var simpleMixedContext = new FolderContext(RootPath, _EmptyChildren,
                _LoosyMusicContent.Merge(_LosslessMusicContent).Convert());
            return new TestData(simpleMixedContext, _SimpleLoosyCluster, _SimpleLosslessCluster);
        }

        private TestData GetContextWithImage()
        {
            var mixedContent = _LosslessMusicContent.Merge(_ImageSingleContent);
            var contextWithImageFolder = new FolderContext(RootPath,
                _ChildrenWithArt,
                mixedContent.Convert()
            );
            var groupedArtCluster = new MusicCluster(RootPath, true, mixedContent.Merge(_ImageContent).Convert());

            return new TestData(contextWithImageFolder, groupedArtCluster);
        }

        private TestData GetNestedLoosyContext()
        {
            var rooContext = new FolderContext(RootPath,
                _ChildrenWithLoosy,
                _EmptyContent
            );

            return new TestData(rooContext, _SimpleLoosyCluster);
        }

        private TestData GetAdjacentMusicArtFolderContext()
        {
            var mixedChildren = _ChildrenWithLoosy.Merge(_ChildrenWithArt);
            var mixedContext = new FolderContext(RootPath,
                mixedChildren,
                _EmptyContent
            );
            var groupedArtCluster = new MusicCluster(RootPath, false, _LoosyMusicContent.Merge(_ImageContent).Convert());
            return new TestData(mixedContext, groupedArtCluster);
        }

        private TestData GetTopMusicArtFolderContext()
        {
            var rootWithArtAndChildrenWithLoosy = new FolderContext(RootPath,
                _ChildrenWithLoosy,
                _ImageContent
            );
            var groupedArtCluster = new MusicCluster(RootPath, false, _LoosyMusicContent.Merge(_ImageContent).Convert());
            return new TestData(rootWithArtAndChildrenWithLoosy, groupedArtCluster);
        }

        private IEnumerable<TestData> GetData()
        {
            yield return GetEmptyContext();
            yield return GetSimpleLoosyContext();
            yield return GetSimpleLosslessContext();
            yield return GetSimpleMixedContext();
            yield return GetContextWithImage();
            yield return GetNestedLoosyContext();
            yield return GetAdjacentMusicArtFolderContext();
            yield return GetTopMusicArtFolderContext();
        }

        public IEnumerator<object[]> GetEnumerator() =>
            GetData().Select(testData => new object[] { testData.Input, testData.ExpectedResult })
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
