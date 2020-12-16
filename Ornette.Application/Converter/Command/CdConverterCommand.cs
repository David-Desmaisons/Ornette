using Ornette.Application.Converter.Mp3;

namespace Ornette.Application.Converter.Command
{
    public class CdConverterCommand : ConvertCommand
    {
        public CdConverterCommand(string target, Mp3Encoding targetEncoding)
        {
            TargetEncoding = targetEncoding;
            Target = target;
        }

        public string Target { get; }
        public Mp3Encoding TargetEncoding { get; }
        public override SourceType SourceType => SourceType.CD;
    }
}
