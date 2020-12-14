using System;
using System.Reactive.Linq;
using System.Threading;
using Ornette.Application.Converter.Command;
using Ornette.Application.Io;

namespace Ornette.Application.Converter.Implementation
{
    public class Mp3FolderConverter: IMusicConverter<Mp3FolderConverterCommand>
    {
        private readonly IIoReader _IoReader;
        private readonly IConvertStrategy _ConvertStrategy;
        private readonly IMusicConverter<Mp3TracksConverterCommand> _TracksConverter;
        private readonly IMusicConverter<Mp3CueConverterCommand> _CueConverter;

        public Mp3FolderConverter(IIoReader ioReader, IConvertStrategy convertStrategy, IMusicConverter<Mp3TracksConverterCommand> tracksConverter, IMusicConverter<Mp3CueConverterCommand> cueConverter)
        {
            _IoReader = ioReader;
            _TracksConverter = tracksConverter;
            _CueConverter = cueConverter;
            _ConvertStrategy = convertStrategy;
        }

        public IObservable<ConvertedFile> Convert(Mp3FolderConverterCommand command, CancellationToken token = default(CancellationToken),
            IProgress<IConvertUpdate> progress = null)
        {
            var folderContext = _IoReader.GetFolderContext(command.Source);
            return _ConvertStrategy
                .GetMp3Commands(folderContext, command, progress)
                .SelectMany(subCommand => FromSubCommand(subCommand, token, progress));
        }

        private IObservable<ConvertedFile> FromSubCommand(Mp3ConverterCommand subCommand, CancellationToken token, IProgress<IConvertUpdate> progress)
        {
            switch (subCommand)
            {
                case Mp3CueConverterCommand cueConverterCommand:
                    return _CueConverter.Convert(cueConverterCommand, token, progress);

                case Mp3TracksConverterCommand mp3TracksConverterCommand:
                    return _TracksConverter.Convert(mp3TracksConverterCommand, token, progress);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
