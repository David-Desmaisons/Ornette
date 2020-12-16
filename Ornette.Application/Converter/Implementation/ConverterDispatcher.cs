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
        private readonly IMusicConverter<Mp3TracksConverterCommand> _TracksConverter;
        private readonly IMusicConverter<Mp3CueConverterCommand> _CueConverter;
        private readonly string _Target;
        private readonly Mp3Encoding _Encoding;
        private readonly List<Converter> _Converters = new List<Converter>();

        private delegate IObservable<ConvertedFile> Converter(CancellationToken token, IProgress<IConvertUpdate> progress);


        public IObservable<ConvertedFile> Convert(CancellationToken token, IProgress<IConvertUpdate> progress)
            => Observable.ToObservable(_Converters).SelectMany(converter => converter(token, progress));

        public ConverterDispatcher(IMusicConverter<Mp3TracksConverterCommand> tracksConverter, IMusicConverter<Mp3CueConverterCommand> cueConverter, string target, Mp3Encoding encoding)
        {
            _Target = target;
            _Encoding = encoding;
            _TracksConverter = tracksConverter;
            _CueConverter = cueConverter;
        }

        void IConverterDispatcher.AddForCueConversion(Track source, CueData cue)
        {
            var command = new Mp3CueConverterCommand(source, cue, _Target, _Encoding);
            IObservable<ConvertedFile> Convertor(CancellationToken token, IProgress<IConvertUpdate> progress) => _CueConverter.Convert(command, token, progress);
            _Converters.Add(Convertor);
        }

        void IConverterDispatcher.AddForTracksConversion(Track[] source)
        {
            var command = new Mp3TracksConverterCommand(source, _Target, _Encoding);
            IObservable<ConvertedFile> Convertor(CancellationToken token, IProgress<IConvertUpdate> progress) => _TracksConverter.Convert(command, token, progress);
            _Converters.Add(Convertor);
        }

        void IConverterDispatcher.OnEnd()
        {
        }
    }
}
