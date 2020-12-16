using System.Collections.Generic;
using Ornette.Application.Converter.Command;
using Ornette.Application.Converter.Cue;
using Ornette.Application.Converter.Mp3;
using Ornette.Application.Converter.Strategy;
using Ornette.Application.Model;

namespace Ornette.Application.Converter.Implementation
{
    public class ConverterDispatcher : IConverterDispatcher
    {
        private readonly string _Target;
        private readonly Mp3Encoding _Encoding;
        private readonly List<Mp3ConverterCommand> _Commands = new List<Mp3ConverterCommand>();

        public IEnumerable<Mp3ConverterCommand> Commands => _Commands;

        public ConverterDispatcher(string target, Mp3Encoding encoding)
        {
            _Target = target;
            _Encoding = encoding;
        }

        void IConverterDispatcher.AddForCueConversion(Track source, CueData cue)
        {
            _Commands.Add(new Mp3CueConverterCommand(source, cue, _Target, _Encoding));
        }

        void IConverterDispatcher.AddForTracksConversion(Track[] source)
        {
            _Commands.Add(new Mp3TracksConverterCommand(source, _Target, _Encoding));
        }

        void IConverterDispatcher.OnEnd()
        {
        }
    }
}
