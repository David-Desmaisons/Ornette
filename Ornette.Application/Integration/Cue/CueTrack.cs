using System;
using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue
{
    public class CueTrack
    {
        public string Songwriter { get; set; }
        public string Performer { get; set; }
        public string Title { get; set; }
        public IList<TimeSpan> Index { get; } = new List<TimeSpan>();
        public TimeSpan? PreGap { get; set; }
        public TimeSpan? PostGap { get; set; }
        public string Isrc { get; set; }
    }
}
