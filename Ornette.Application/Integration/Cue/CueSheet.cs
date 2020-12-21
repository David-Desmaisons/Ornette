using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue
{
    public class CueSheet
    {
        public string Songwriter { get; set; }
        public string Performer { get; set; }
        public string Title { get; set; }
        public IDictionary<string, List<string>> Remarks { get; } = new Dictionary<string, List<string>>();
        public List<CueFile> Files = new List<CueFile>();
    }
}
