using System;
using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue
{
    public class CueFile
    {
        public CueFile(string name, string type, IList<CueTrack> tracks)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Tracks = tracks ?? throw new ArgumentNullException(nameof(tracks));

            if (tracks.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(tracks), "File must have at least one track");
        }

        public string Name { get; }
        public string Type { get; }
        public IList<CueTrack> Tracks { get; }

        public override string ToString() => $"{Name} {Type}";
    }
}
