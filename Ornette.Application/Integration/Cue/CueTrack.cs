using System;
using System.Collections.Generic;
using System.Linq;

namespace Ornette.Application.Integration.Cue
{
    public class CueTrack
    {
        private readonly IDictionary<int, CueIndex> _Index;

        public CueTrack(int number, string type, string title, string performer, string songwriter, string isrc,
            IDictionary<int, CueIndex> index, CueIndex? preGap, CueIndex? postGap)
        {
            Number = number;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Performer = performer ?? throw new ArgumentNullException(nameof(performer));
            _Index = index ?? throw new ArgumentNullException(nameof(index));
            Songwriter = songwriter;
            Isrc = isrc;
            PreGap = preGap;
            PostGap = postGap;

            if (!index.ContainsKey(1))
                throw new ArgumentOutOfRangeException(nameof(index), "Track must have an INDEX 01 value");
        }

        internal IEnumerable<string> Serialize()
        {
            yield return $"TRACK {Number:00} {Type}";
            yield return $@"  TITLE ""{Title}""";
            yield return $@"  PERFORMER ""{Performer}""";

            if (Isrc != null)
                yield return $@"  ISRC {Isrc}";

            if (Songwriter != null)
                yield return $@"  SONGWRITER ""{Songwriter}""";

            if (PreGap.HasValue)
                yield return $@"  PREGAP {PreGap}";

            if (PostGap.HasValue)
                yield return $@"  POSTGAP {PostGap}";

            foreach (var index in Indexes)
            {
                yield return $@"  INDEX {index.Key:00} {index.Value}";
            }
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
