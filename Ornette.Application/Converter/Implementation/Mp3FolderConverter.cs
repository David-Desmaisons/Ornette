using System;
using System.Threading;
using Ornette.Application.Converter.Command;
using Ornette.Application.Io;

namespace Ornette.Application.Converter.Implementation
{
    public class Mp3FolderConverter: IMusicConverter<Mp3FolderConverterCommand>
    {
        private readonly IIoReader _IoReader;
        private readonly IMusicConverter<Mp3TracksConverterCommand> _TracksConverter;
        private readonly IMusicConverter<Mp3CueConverterCommand> _CueConverter;

        public Mp3FolderConverter(IIoReader ioReader, IMusicConverter<Mp3TracksConverterCommand> tracksConverter, IMusicConverter<Mp3CueConverterCommand> cueConverter)
        {
            _IoReader = ioReader;
            _TracksConverter = tracksConverter;
            _CueConverter = cueConverter;
        }

        public IObservable<ConvertedFile> Convert(Mp3FolderConverterCommand command, CancellationToken token = default(CancellationToken),
            IProgress<IConvertUpdate> progress = null)
        {
            var folderContext = _IoReader.GetFolderContext(command.Source);
            return null;
        }
    }
}
