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
                { FileType.Cue, new []{"a.cue"}}
            };
            _SimpleLoosyContext = new FolderContext(RootPath, _EmptyChildren, _LoosyMusicContent);
            _SimpleLosslessContext = new FolderContext(RootPath, _EmptyChildren, _LosslessMusicContent);
            var imageContext = new FolderContext(Path.Combine(RootPath, Scans), _EmptyChildren, _ImageContent);
            _ChildrenWithArt = new Dictionary<string, FolderContext>
            {
                { Scans, imageContext }
            };
            _SimpleLoosyCluster = new MusicCluster(RootPath, false, _LoosyMusicContent);
            _SimpleLosslessCluster = new MusicCluster(RootPath, true, _LosslessMusicContent);
        }

        private object[] GetEmptyContext()
        {
            var noMusic = new FolderContext(RootPath, _EmptyChildren, _CueContent.Merge(_ImageContent).Convert());
            return new object[]
            {
                noMusic,
                Enumerable.Empty<MusicCluster>()
            };
        }

        private object[] GetSimpleLoosyContext()
        {
            return new object[]
            {
                _SimpleLoosyContext,
                new [] { _SimpleLoosyCluster }
            };
        }

        private object[] GetSimpleLosslessContext()
        {
            return new object[]
            {
                _SimpleLosslessContext,
                new [] { _SimpleLosslessCluster }
            };
        }

        private object[] GetSimpleMixedContext()
        {
            var simpleMixedContext = new FolderContext(RootPath, _EmptyChildren, _LoosyMusicContent.Merge(_LosslessMusicContent).Convert());
            return new object[]
            {
                simpleMixedContext,
                new [] { _SimpleLoosyCluster, _SimpleLosslessCluster }
            };
        }

        private object[] GetWithImageContext()
        {
            var mixedContent = _LosslessMusicContent.Merge(_ImageSingleContent);
            var contextWithImageFolder = new FolderContext(RootPath,
                _ChildrenWithArt,
                mixedContent.Convert()
            );
            var groupedArtCluster = new MusicCluster(RootPath, true, mixedContent.Merge(_ImageContent).Convert());

            return new object[]
            {
                contextWithImageFolder,
                new [] { groupedArtCluster }
            };
        }

        private IEnumerable<object[]> GetData()
        {
            yield return GetEmptyContext();
            yield return GetSimpleLoosyContext();
            yield return GetSimpleLosslessContext();
            yield return GetSimpleMixedContext();
            yield return GetWithImageContext();
        }

        public IEnumerator<object[]> GetEnumerator() => GetData().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
