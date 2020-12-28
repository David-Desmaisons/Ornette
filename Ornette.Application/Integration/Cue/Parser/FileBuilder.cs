using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class FileBuilder : ICueElementAggregator<CueTrack>
    {
        private readonly ICueElementAggregator<CueFile> _SheetBuilder;
        private readonly string _Name;
        private readonly string _Type;
        private readonly List<CueTrack> _Tracks = new List<CueTrack>();

        public FileBuilder(ICueElementAggregator<CueFile> sheetBuilder, string name, string type)
        {
            _Name = name;
            _Type = type;
            _SheetBuilder = sheetBuilder;
        }

        public ICueElementBuilder Parse(CueInstruction command)
        {
            switch (command.Command)
            {
                case CueCommand.Track:
                    return new TrackBuilder(this,  command.ConvertParameterToInt(0), command.Parameters[1]);

                case CueCommand.Index:
                    return this;

                default:
                    AddBuiltFile();
                    return _SheetBuilder.Parse(command);
            }
        }

        public CueSheet Build()
        {
            AddBuiltFile();
            return _SheetBuilder.Build();
        }

        private void AddBuiltFile()
        {
            var file = new CueFile(_Name, _Type, _Tracks);
            _SheetBuilder.AddChild(file);
        }

        public void AddChild(CueTrack track)
        {
            _Tracks.Add(track);
        }
    }
}
