using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class SheetBuilder : ICueElementBuilder
    {
        private readonly List<CueFile> _Files = new List<CueFile>();
        private string _Songwriter;
        private string _Performer;
        private string _Title;

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

                case CueCommand.File:
                    return new FileBuilder(this, parameters[0], parameters[1]);

                default:
                    return this;
            }
        }

        public ICueElementBuilder End()
        {
            return this;
        }

        public void AddFile(CueFile file)
        {
            _Files.Add(file);
        }
    }
}
