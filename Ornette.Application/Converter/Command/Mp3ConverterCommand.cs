using Ornette.Application.Converter.Mp3;

namespace Ornette.Application.Converter.Command
{
    public abstract class Mp3ConverterCommand: ImportCommand
    {
        public abstract Mp3Encoding TargetEncoding { get; }
    }
}
