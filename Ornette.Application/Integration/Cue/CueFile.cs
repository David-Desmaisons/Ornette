using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue
{
    public class CueFile
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IList<CueTrack> Tracks { get; } = new List<CueTrack>();
    }
}
