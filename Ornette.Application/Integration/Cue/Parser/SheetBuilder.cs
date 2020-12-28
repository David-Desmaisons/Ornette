using System.Collections.Generic;
using MoreCollection.Extensions;

namespace Ornette.Application.Integration.Cue.Parser
{
    public class SheetBuilder : ICueElementAggregator<CueFile>
    {
        private readonly List<CueFile> _Files = new List<CueFile>();
        private readonly Dictionary<string, IList<string>> _Remarks = new Dictionary<string, IList<string>>();
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

                case CueCommand.Remark:
                    var list = _Remarks.GetOrAdd(command.Parameters[0], _ => new List<string>()).Item;
                    list.Add(command.Parameters[1]);
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

        public CueSheet Build()
        {
            return new CueSheet(_Performer, _Title, _Songwriter, _Files, _Remarks);
        }

        public void AddChild(CueFile file)
        {
            _Files.Add(file);
        }
    }
}
