using Ornette.Application.Converter.Mp3;

namespace Ornette.Application.Converter.Command
{
    public class Mp3FolderConverterCommand: ImportCommand
    {
        public Mp3FolderConverterCommand(string source, string target, Mp3Encoding targetEncoding)
        {
            TargetEncoding = targetEncoding;
            Target = target;
            Source = source;
        }

        public string Source { get; }
        public string Target { get; }
        public Mp3Encoding TargetEncoding { get; }
        public override SourceType SourceType => SourceType.Directory;
    }
}
