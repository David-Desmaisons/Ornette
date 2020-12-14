using System;
using Ornette.Application.Converter.Command;
using Ornette.Application.Io;

namespace Ornette.Application.Converter
{
    public interface IConvertStrategy
    {
        IObservable<Mp3ConverterCommand> GetMp3Commands(FolderContext context, Mp3FolderConverterCommand command, IProgress<IConvertUpdate> progress);
    }
}
