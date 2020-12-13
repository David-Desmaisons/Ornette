using Ornette.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;
using Ornette.Application.Io.Extension;

namespace Ornette.Application.Io
{
    public class FolderContext
    {
        private readonly Dictionary<string, FolderContext> _Children;
        private readonly Dictionary<FileType, string[]> _Files;

        public FolderContext(string path, Dictionary<string, FolderContext> children, Dictionary<FileType, string[]> files)
        {
            _Children = children;
            _Files = files;
            Path = path;
        }

        public string Path { get; }
        public IReadOnlyDictionary<string, FolderContext> Children => _Children;
        public IEnumerable<string> Get(FileType fileType) => _Files.GetOrDefault(fileType) ?? Enumerable.Empty<string>();
        public IEnumerable<string> GetAll(FileType fileType) => Get(fileType).Concat(_Children.Values.SelectMany(v => v.Get(fileType)));
    }
}
