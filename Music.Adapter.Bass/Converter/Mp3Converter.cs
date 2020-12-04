using Ornette.Application.Converter;
using Ornette.Application.Converter.Command;
using System;
using System.Threading;

namespace Music.Adapter.Bass.Converter
{
    public class Mp3Converter : IMusicConverter<Mp3ConverterCommand>
    {
        public IObservable<ConvertedFile> Convert(Mp3ConverterCommand command, CancellationToken token = default, IProgress<IConvertUpdate> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}
