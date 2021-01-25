using Ornette.Application.Io.Extension;
using System.Collections.Generic;
using System.Linq;

namespace Ornette.Application.Io
{
    public class FolderContext
    {
        public IReadOnlyDictionary<FileType, string[]> Files { get; }
        public IReadOnlyDictionary<string, FolderContext> Children { get; }

        public FolderContext(string path, IReadOnlyDictionary<string, FolderContext> children, IReadOnlyDictionary<FileType, string[]> files)
        {
            Children = children;
            Files = files;
            Path = path;
        }

        public string Path { get; }

        public IEnumerable<FolderContext> AllContexts
        {
            get
            {
                yield return this;
                foreach (var context in Children.Values.SelectMany(child => child.AllContexts))
                {
                    yield return context;
                }
            }
        }

        public string[] Get(FileType fileType) => Files.TryGetValue(fileType, out var res) ? res : null;
        public bool Has(FileType fileType) => Files.ContainsKey(fileType);
        public IEnumerable<string> GetAll(FileType fileType) => Get(fileType).Concat(Children.Values.SelectMany(v => v.Get(fileType)));
    }
}
