﻿using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue
{
    public class CueSheet
    {
        public CueSheet(string performer, string title, string songwriter, List<CueFile> files, IDictionary<string, List<string>> remarks)
        {
            Songwriter = songwriter;
            Files = files;
            Remarks = remarks;
            Title = title;
            Performer = performer;
        }

        public string Songwriter { get; }
        public string Performer { get; }
        public string Title { get; }
        public IDictionary<string, List<string>> Remarks { get; }
        public List<CueFile> Files { get; }
    }
}
