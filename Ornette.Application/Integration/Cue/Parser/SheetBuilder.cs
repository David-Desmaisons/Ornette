using System.Collections.Generic;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class SheetBuilder : ICueElementAggregator<CueFile>
    {
        private readonly List<CueFile> _Files = new List<CueFile>();
        private string _Songwriter;
        private string _Performer;
        private string _Title;

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

                case CueCommand.File:
                    return new FileBuilder(this, command.Parameters[0], command.Parameters[1]);

                default:
                    return this;
            }
        }

        public ICueElementBuilder End()
        {
            return this;
        }

        public CueSheet Build()
        {
            return new CueSheet(_Performer, _Title, _Songwriter, _Files, new Dictionary<string, List<string>>());
        }

        public void AddChild(CueFile file)
        {
            _Files.Add(file);
        }
    }
}
