using System;
using System.Collections.Generic;
using System.Linq;
using Ornette.Application.Model;

namespace Ornette.Application.Io
{
    public class FolderContext
    {
        private readonly Dictionary<string, FolderContext> _Children;

        public FolderContext(string path, Dictionary<string, FolderContext> children,Track[] tracks, string[] images, string[] cues)
        {
            Images = images;
            Cues = cues;
            _Children = children;
            Path = path;
            Tracks = tracks;
        }

        public string Path { get;  }
        public string[] Images { get; }
        public string[] Cues { get;  }
        public Track[] Tracks { get; }

        public IReadOnlyDictionary<string, FolderContext> Children => _Children;
        public IEnumerable<string> AllImages => Collect(ctx => ctx.Images);
        public IEnumerable<Track> AllTracks => Collect(ctx => ctx.Tracks);
        public IEnumerable<string> AllCues => Collect(ctx => ctx.Cues);

        private IEnumerable<T> Collect<T>(Func<FolderContext, IEnumerable<T>> extractor)
        {
            return extractor(this).Concat(_Children.Values.SelectMany(v => v.Collect<T>(extractor)));
        }
    }
} 
