using System;
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
        private readonly Dictionary<string, FolderContext> _EmptyChildren = new Dictionary<string, FolderContext>();
        private readonly FolderContext _SimpleLoosyContext;
        private readonly FolderContext _SimpleLosslessContext;
        private readonly MusicCluster _SimpleLoosyCluster;
        private readonly MusicCluster _SimpleLosslessCluster;
        private readonly Dictionary<string, FolderContext> _ChildrenWithArt;
        private readonly Dictionary<FileType, string[]> _LoosyMusicContent;
        private readonly Dictionary<FileType, string[]> _LosslessMusicContent;
        private readonly Dictionary<FileType, string[]> _ImageContent;
        private readonly Dictionary<FileType, string[]> _ImageSingleContent;
        private readonly Dictionary<FileType, string[]> _CueContent;

        public ClusterDataProvider()
        {
            _LoosyMusicContent = new Dictionary<FileType, string[]>
            {
                {FileType.LoosyMusic, new[] {"a.mp3", "b.mp3"}}
            };
            _LosslessMusicContent = new Dictionary<FileType, string[]>
            {
                {FileType.LosslessMusic, new[] {"a.flac", "b.flac"}}
            };
            _ImageContent = new Dictionary<FileType, string[]>
            {
                {FileType.Image, new[] {"a.jpeg", "b.jpeg"}}
            };
            _ImageSingleContent = new Dictionary<FileType, string[]>
            {
                {FileType.Image, new[] {"c.gif"}}
            };
            _CueContent = new Dictionary<FileType, string[]>
            {
                {FileType.Cue, new[] {"a.cue"}}
            };
            _SimpleLoosyContext = new FolderContext(RootPath, _EmptyChildren, _LoosyMusicContent);
            _SimpleLosslessContext = new FolderContext(RootPath, _EmptyChildren, _LosslessMusicContent);
            var imageContext = new FolderContext(Path.Combine(RootPath, Scans), _EmptyChildren, _ImageContent);
            _ChildrenWithArt = new Dictionary<string, FolderContext>
            {
                {Scans, imageContext}
            };
            _SimpleLoosyCluster = new MusicCluster(RootPath, false, _LoosyMusicContent);
            _SimpleLosslessCluster = new MusicCluster(RootPath, true, _LosslessMusicContent);
        }

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

        private IEnumerable<TestData> GetData()
        {
            yield return GetEmptyContext();
            yield return GetSimpleLoosyContext();
            yield return GetSimpleLosslessContext();
            yield return GetSimpleMixedContext();
            yield return GetContextWithImage();
        }

        public IEnumerator<object[]> GetEnumerator() => 
            GetData().Select(testData => new object[] {testData.Input, testData.ExpectedResult})
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
