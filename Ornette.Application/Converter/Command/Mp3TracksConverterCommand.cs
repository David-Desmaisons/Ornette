using Ornette.Application.Converter.Mp3;
using Ornette.Application.Model;

namespace Ornette.Application.Converter.Command
{
    public class Mp3TracksConverterCommand : ImportCommand
    {
        public Mp3TracksConverterCommand(Track[] source, string target, Mp3Encoding targetEncoding)
        {
            TargetEncoding = targetEncoding;
            Source = source;
            Target = target;
        }

        public Track[] Source { get; }
        public string Target { get; }
        public Mp3Encoding TargetEncoding { get; }
        public override SourceType SourceType => SourceType.Track;
    }
}
