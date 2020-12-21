using Ornette.Application.Converter.Command;
using Ornette.Application.Converter.Strategy;
using Ornette.Application.Io;
using System;
using System.Reactive.Linq;
using System.Threading;
using Ornette.Application.Message;

namespace Ornette.Application.Converter.Implementation
{
    public class Mp3FolderConverter : IMusicConverter<Mp3FolderConverterCommand>
    {
        private readonly IIoReader _IoReader;
        private readonly IConvertFolderStrategy _ConvertStrategy;
        private readonly IMusicConverter<ConvertCommand> _Converter;

        public Mp3FolderConverter(IIoReader ioReader, IConvertFolderStrategy convertStrategy, IMusicConverter<ConvertCommand> converter)
        {
            _IoReader = ioReader;
            _Converter = converter;
            _ConvertStrategy = convertStrategy;
        }

        public IObservable<ConvertedFile> Convert(Mp3FolderConverterCommand command, CancellationToken token = default(CancellationToken),
            IProgress<IConvertUpdate> progress = null)
        {
            var folderContext = _IoReader.GetFolderContext(command.Source);
            var converterDispatcher = new ConverterDispatcher(_Converter, command.Target, command.TargetEncoding);

            var strategyProgress = new Progress<Feedback>(f => progress?.Report(new ConvertUpdate(f,command)));
            _ConvertStrategy.IntrospectFolder(folderContext, converterDispatcher, strategyProgress, token);
            return converterDispatcher
                        .Convert(token, progress)
                        .Select(convertedFile => new ConvertedFile(convertedFile.Path, convertedFile.Description, command));
        }
    }
}
