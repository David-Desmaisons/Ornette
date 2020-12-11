using Ornette.Application.Converter;
using Ornette.Application.Converter.Command;
using System;
using System.Threading;
using Music.Adapter.Bass.Core;

namespace Music.Adapter.Bass.Converter
{
    public class Mp3Converter : IMusicConverter<Mp3FolderConverterCommand>
    {
        private readonly ISessionManager _SessionManager;

        public Mp3Converter(ISessionManager sessionManager)
        {
            _SessionManager = sessionManager;
        }

        public IObservable<ConvertedFile> Convert(Mp3FolderConverterCommand command, CancellationToken token = default(CancellationToken), IProgress<IConvertUpdate> progress = null)
        {
            _SessionManager.Restart();
            var a = new Un4seen.Bass.Misc.EncoderMP3(1);
            var b = new Un4seen.Bass.Misc.EncoderLAME(1);


            throw new NotImplementedException();
        }
    }
}
