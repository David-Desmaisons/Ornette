using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class TrackBuilder : ICueElementBuilder
    {
        private readonly ICueElementAggregator<CueTrack> _FileBuilder;
        private readonly int _Number;
        private readonly string _Type;
        private readonly IDictionary<int, CueIndex> _Index = new Dictionary<int, CueIndex>();

        private string _Songwriter;
        private string _Performer;
        private string _Title;
        private CueIndex? _PreGap;
        private CueIndex? _PostGap;
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
                    _Index.Add(command.ConvertParameterToInt(0), command.ConvertParameterToCueIndex(1));
                    return this;

                case CueCommand.PreGap:
                    _PreGap = command.ConvertParameterToCueIndex(0);
                    return this;

                case CueCommand.PostGap:
                    _PostGap = command.ConvertParameterToCueIndex(0);
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
            var track = new CueTrack(_Number, _Type, _Title, _Performer, _Songwriter, _Isrc, _Index, _PreGap, _PostGap);
            _FileBuilder.AddChild(track);
        }
    }
}
