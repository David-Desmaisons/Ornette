using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;

namespace Ornette.Application.Integration.Cue
{
    public class CueTrack
    {
        private readonly IDictionary<int,CueIndex> _Index;

        public CueTrack(int number, string type, string title, string performer, string songwriter, string isrc,
            IDictionary<int, CueIndex> index, CueIndex? preGap, CueIndex? postGap)
        {
            Title = title;
            Performer = performer;
            Songwriter = songwriter;
            Isrc = isrc;
            _Index = index;
            PreGap = preGap;
            PostGap = postGap;
            Number = number;
            Type = type;
        }

        public int Number { get; }
        public string Type { get; }
        public string Songwriter { get; }
        public string Performer { get; }
        public string Title { get; }
        public CueIndex? PreGap { get; }
        public CueIndex? PostGap { get; }
        public string Isrc { get; }

        public CueIndex? GetIndex(int index) => _Index.TryGetValue(index, out var cueIndex) ? cueIndex : default(CueIndex?);
        public IEnumerable<KeyValuePair<int, CueIndex>> Indexes => _Index.OrderBy(kvp => kvp.Key);

        public override string ToString() => $"{Number} {Type} - {Title}";
    }
}
