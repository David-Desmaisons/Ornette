using System;
using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue
{
    public class CueSheet
    {
        public CueSheet(string performer, string title, string songwriter, List<CueFile> files, IDictionary<string, IList<string>> remarks)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Performer = performer ?? throw new ArgumentNullException(nameof(performer));
            Files = files ?? throw new ArgumentNullException(nameof(files));
            Songwriter = songwriter;
            Remarks = remarks;

            if (files.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(files), "Sheet must have at least one file");

        }

        public string Songwriter { get; }
        public string Performer { get; }
        public string Title { get; }
        public IDictionary<string, IList<string>> Remarks { get; }
        public List<CueFile> Files { get; }

        public override string ToString() => $"{Title} {Performer}";
    }
}
