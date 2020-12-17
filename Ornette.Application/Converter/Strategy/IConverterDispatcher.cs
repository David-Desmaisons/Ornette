using Ornette.Application.Converter.Cue;
using Ornette.Application.Model;

namespace Ornette.Application.Converter.Strategy
{
    public interface IConverterDispatcher
    {
        void AddForCueConversion(Track source, CueData cue);

        void AddForTracksConversion(Track[] source, string relativeDirectory=null);

        void OnEnd();
    }
}
