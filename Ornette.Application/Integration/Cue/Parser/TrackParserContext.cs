using System;
using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class TrackParserContext : IParserContext
    {
        private readonly CueTrack _Track;
        private readonly SheetParserContext _SheetParserContext;
        public TrackParserContext(SheetParserContext sheetParserContext)
        {
            _SheetParserContext = sheetParserContext;
            _Track = new CueTrack();
        }

        public IParserContext Parse(string command, string[] parameters)
        {
            switch (command)
            {
                case "TITLE":
                    _Track.Title = string.Join(" ", parameters);
                    return this;

                case "PERFORMER":
                    _Track.Performer = string.Join(" ", parameters);
                    return this;

                case "INDEX":
                    ParseIndex(_Track.Index, parameters);
                    return this;

                default:
                    return this;
            }
        }

        private static void ParseIndex(IList<TimeSpan> indexes, string[] parameters)
        {

        }
    }
}
