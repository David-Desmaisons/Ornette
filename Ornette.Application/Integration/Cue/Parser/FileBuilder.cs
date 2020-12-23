using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class FileBuilder : ICueElementBuilder
    {
        private readonly SheetBuilder _SheetBuilder;
        private readonly string _Name;
        private readonly string _Type;
        private readonly List<CueTrack> _Tracks = new List<CueTrack>();

        public FileBuilder(SheetBuilder sheetBuilder, string name, string type)
        {
            _Name = name;
            _Type = type;
            _SheetBuilder = sheetBuilder;
        }

        public ICueElementBuilder Parse(string command, string[] parameters)
        {
            switch (command)
            {
                case CueCommand.Track:
                    return new TrackBuilder(this, int.Parse(parameters[0]), parameters[1]);

                default:
                    return _SheetBuilder.Parse(command, parameters);
            }
        }

        public ICueElementBuilder End()
        {
            return _SheetBuilder.End();
        }

        public void AddTrack(CueTrack track)
        {
            _Tracks.Add(track);
        }
    }
}
