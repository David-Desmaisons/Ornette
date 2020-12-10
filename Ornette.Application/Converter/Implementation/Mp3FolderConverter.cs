using System;
using System.Threading;
using Ornette.Application.Converter.Command;

namespace Ornette.Application.Converter.Implementation
{
    public class Mp3FolderConverter: IMusicConverter<Mp3FolderConverterCommand>
    {
        public IObservable<ConvertedFile> Convert(Mp3FolderConverterCommand command, CancellationToken token = default(CancellationToken),
            IProgress<IConvertUpdate> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}
