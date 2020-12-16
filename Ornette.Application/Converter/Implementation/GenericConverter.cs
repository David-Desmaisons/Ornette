using Ornette.Application.Converter.Command;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Ornette.Application.Converter.Implementation
{
    public class GenericConverter : IMusicConverter<ConvertCommand>
    {
        private readonly Dictionary<Type, Converter> _Converters = new Dictionary<Type, Converter>();
        private delegate IObservable<ConvertedFile> Converter(ConvertCommand command, CancellationToken token, IProgress<IConvertUpdate> progress);

        public GenericConverter(IMusicConverter<Mp3TracksConverterCommand> tracksConverter, IMusicConverter<Mp3CueConverterCommand> cueConverter, IMusicConverter<Mp3FolderConverterCommand> folderConverter)
        {
            Register(tracksConverter);
            Register(cueConverter);
            Register(folderConverter);
        }

        private void Register<T>(IMusicConverter<T> converter) where T : ConvertCommand 
            => _Converters[typeof(T)] = (command, token, progress) => converter.Convert(command as T, token, progress);

        public IObservable<ConvertedFile> Convert(ConvertCommand command, CancellationToken token = default, IProgress<IConvertUpdate> progress = null)
        {
            _Converters.TryGetValue(command.GetType(), out var converter);
            return converter?.Invoke(command, token, progress);
        }
    }
}
