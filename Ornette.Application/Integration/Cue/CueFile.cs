using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue
{
    public class CueFile
    {
        public CueFile(string name, string type, IList<CueTrack> tracks)
        {
            Name = name;
            Type = type;
            Tracks = tracks;
        }

        public string Name { get; }
        public string Type { get;  }
        public IList<CueTrack> Tracks { get; }

        public override string ToString() => $"{Name} {Type}";
    }
}
