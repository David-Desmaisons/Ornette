using Ornette.Application.Infra;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;

namespace Ornette.Application.Converter.Strategy.Cluster
{
    public class ClusterFactory : IClusterFactory
    {
        private static readonly bool[] _IsLosslessValues = { true, false };

        public IEnumerable<MusicCluster> GetClustersFromFolderContext(FolderContext context)
        {
            return GetClusterBuildersFromFolderContext(context, null).Build();
        }

        private class FolderIntrospection
        {
            public FolderIntrospection(IEnumerable<ClusterBuilder> builders, Dictionary<FileType, List<string>> context = null)
            {
                _Builders = builders;
                _Context = context;
            }

            private readonly IEnumerable<ClusterBuilder> _Builders;
            private readonly Dictionary<FileType, List<string>> _Context;

            public IEnumerable<MusicCluster> Build()
            {
                return _Builders.Select(builder => builder.Merge(_Context).Build());
            }

            public static FolderIntrospection Merge(IEnumerable<FolderIntrospection> childrenIntrospection, IReadOnlyDictionary<FileType, string[]> context = null)
            {
                var convertedContext = context?.Convert();
                childrenIntrospection.Where(introspection => !introspection._Builders.Any()).ForEach(
                    introspection => convertedContext.Merge(introspection._Context));
                return new FolderIntrospection(childrenIntrospection.SelectMany(introspection => introspection._Builders), convertedContext);
            }
        }

        private static FolderIntrospection GetClusterBuildersFromFolderContext(FolderContext context, ClusterBuilder rootBuilder)
        {
            var hasLossless = context.Has(FileType.LosslessMusic);
            var hasLoosy = context.Has(FileType.LoosyMusic);

            if ((hasLossless || hasLoosy))
            {
                var childrenIntrospection = _IsLosslessValues.SelectMany(isLossless => GetClusterBuildersFromFolderContext(context, isLossless));
                return FolderIntrospection.Merge(childrenIntrospection);
            }

            if (rootBuilder == null)
            {
                var childrenContext = context.Children.Values.Select(childContext => GetClusterBuildersFromFolderContext(childContext, null));
                return FolderIntrospection.Merge(childrenContext, context.Files);
            }

            rootBuilder.Merge(context);
            return new FolderIntrospection(Enumerable.Empty<ClusterBuilder>());
        }

        private static IEnumerable<FolderIntrospection> GetClusterBuildersFromFolderContext(FolderContext context, bool isLossless)
        {
            var lookingFor = isLossless ? FileType.LosslessMusic : FileType.LoosyMusic;
            var except = isLossless ? FileType.LoosyMusic : FileType.LosslessMusic;

            if (!context.Has(lookingFor))
            {
                return Enumerable.Empty<FolderIntrospection>();
            }

            var builder = new ClusterBuilder(context.Path, isLossless, context.Files.DuplicateExcept(except));
            var folderIntrospection = new FolderIntrospection(new[] {builder});
            return context.Children.Values
                .Select(childContext => GetClusterBuildersFromFolderContext(childContext, builder))
                .Concat(folderIntrospection);
        }
    }
}
