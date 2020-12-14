using Ornette.Application.Converter.Cue;
using Ornette.Application.Converter.Mp3;
using Ornette.Application.Model;

namespace Ornette.Application.Converter.Command
{
    public class Mp3CueConverterCommand: Mp3ConverterCommand
    {
        public Mp3CueConverterCommand(Track[] source, CueData cue, string target, Mp3Encoding targetEncoding)
        {
            TargetEncoding = targetEncoding;
            Cue = cue;
            Source = source;
            Target = target;
        }

        public Track[] Source { get; }
        public CueData Cue { get; }
        public string Target { get; }
        public override Mp3Encoding TargetEncoding { get; }
        public override SourceType SourceType => SourceType.Track;
    }
}
