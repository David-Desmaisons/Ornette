﻿using Ornette.Application.Converter.Command;
using Ornette.Application.Converter.Strategy;
using Ornette.Application.Io;
using System;
using System.Threading;

namespace Ornette.Application.Converter.Implementation
{
    public class Mp3FolderConverter : IMusicConverter<Mp3FolderConverterCommand>
    {
        private readonly IIoReader _IoReader;
        private readonly IConvertFolderStrategy _ConvertStrategy;
        private readonly IMusicConverter<Mp3TracksConverterCommand> _TracksConverter;
        private readonly IMusicConverter<Mp3CueConverterCommand> _CueConverter;

        public Mp3FolderConverter(IIoReader ioReader, IConvertFolderStrategy convertStrategy, IMusicConverter<Mp3TracksConverterCommand> tracksConverter, IMusicConverter<Mp3CueConverterCommand> cueConverter)
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
            var converterDispatcher = new ConverterDispatcher(_TracksConverter, _CueConverter, command.Target, command.TargetEncoding);
            _ConvertStrategy.IntrospectFolder(folderContext, converterDispatcher, progress, token);
            return converterDispatcher.Convert(token, progress);
        }
    }
}
