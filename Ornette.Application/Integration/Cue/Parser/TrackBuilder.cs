using System;
using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class TrackBuilder : ICueElementBuilder
    {
        private readonly ICueElementAggregator<CueTrack> _FileBuilder;
        private readonly int _Number;
        private readonly string _Type;
        private readonly IList<TimeSpan> _Index = new List<TimeSpan>();

        private string _Songwriter;
        private string _Performer;
        private string _Title;
        private TimeSpan? _PreGap;
        private TimeSpan? _PostGap;
        private string _Isrc;

        public TrackBuilder(ICueElementAggregator<CueTrack> fileBuilder, int number, string type)
        {
            _FileBuilder = fileBuilder;
            _Number = number;
            _Type = type;
        }

        public ICueElementBuilder Parse(CueInstruction command)
        {
            switch (command.Command)
            {
                case CueCommand.Title:
                    _Title = command.Parameters[0];
                    return this;

                case CueCommand.Performer:
                    _Performer = command.Parameters[0];
                    return this;

                case CueCommand.SongWriter:
                    _Songwriter = command.Parameters[0];
                    return this;

                case CueCommand.Index:
                    ParseIndex(_Index, command.Parameters);
                    return this;

                case CueCommand.File:
                case CueCommand.Track:
                    AddBuiltTrack();
                    return _FileBuilder.Parse(command);

                default:
                    return this;
            }
        }

        public ICueElementBuilder End()
        {
            AddBuiltTrack();
            return _FileBuilder.End();
        }

        private void AddBuiltTrack()
        {
            var track = Build();
            _FileBuilder.AddChild(track);
        }

        private CueTrack Build()
        {
            return null;
        }

        private static void ParseIndex(IList<TimeSpan> indexes, string[] parameters)
        {

        }
    }
}
