using System;
using Ornette.Application.Converter.Command;
using Ornette.Application.Converter.Cue;
using Ornette.Application.Converter.Mp3;
using Ornette.Application.Converter.Strategy;
using Ornette.Application.Model;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;

namespace Ornette.Application.Converter.Implementation
{
    public class ConverterDispatcher : IConverterDispatcher
    {
        private readonly IMusicConverter<ConvertCommand> _Converter;

        private readonly string _Target;
        private readonly Mp3Encoding _Encoding;
        private readonly List<ConvertCommand> _Commands = new List<ConvertCommand>();

        public IObservable<ConvertedFile> Convert(CancellationToken token, IProgress<IConvertUpdate> progress)
            => Observable.ToObservable(_Commands).SelectMany(command => _Converter.Convert(command, token, progress));

        public ConverterDispatcher(IMusicConverter<ConvertCommand> converter, string target, Mp3Encoding encoding)
        {
            _Target = target;
            _Encoding = encoding;
            _Converter = converter;
        }

        void IConverterDispatcher.AddForCueConversion(Track source, CueData cue)
        {
            EnqueueCommand(new Mp3CueConverterCommand(source, cue, _Target, _Encoding));
        }

        void IConverterDispatcher.AddForTracksConversion(Track[] source)
        {
            EnqueueCommand(new Mp3TracksConverterCommand(source, _Target, _Encoding));
        }

        private void EnqueueCommand(ConvertCommand command) => _Commands.Add(command);

        void IConverterDispatcher.OnEnd()
        {
        }
    }
}
