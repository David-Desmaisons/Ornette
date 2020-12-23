using System;
using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class TrackBuilder : ICueElementBuilder
    {
        private readonly FileBuilder _FileBuilder;
        private readonly int _Number;
        private readonly string _Type;
        private readonly IList<TimeSpan> _Index = new List<TimeSpan>();

        private string _Songwriter;
        private string _Performer;
        private string _Title;
        private TimeSpan? _PreGap;
        private TimeSpan? _PostGap;
        private string _Isrc;

        public TrackBuilder(FileBuilder fileBuilder, int number, string type)
        {
            _FileBuilder = fileBuilder;
            _Number = number;
            _Type = type;
        }

        public ICueElementBuilder Parse(string command, string[] parameters)
        {
            switch (command)
            {
                case CueCommand.Title:
                    _Title = parameters[0];
                    return this;

                case CueCommand.Performer:
                    _Performer = parameters[0];
                    return this;

                case CueCommand.SongWriter:
                    _Songwriter = parameters[0];
                    return this;

                case CueCommand.Index:
                    ParseIndex(_Index, parameters);
                    return this;

                case CueCommand.File:
                case CueCommand.Track:
                    var track = Build();
                    _FileBuilder.AddTrack(track);
                    return _FileBuilder.Parse(command, parameters);

                default:
                    return this;
            }
        }

        public ICueElementBuilder End()
        {
            AddBuildTrack();
            return _FileBuilder.End();
        }

        private void AddBuildTrack()
        {
            var track = Build();
            _FileBuilder.AddTrack(track);
        }

        private CueTrack Build()
        {
            throw new NotImplementedException();
        }

        private static void ParseIndex(IList<TimeSpan> indexes, string[] parameters)
        {

        }
    }
}
