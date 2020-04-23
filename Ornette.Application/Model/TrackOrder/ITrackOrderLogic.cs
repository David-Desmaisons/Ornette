using System.Collections.ObjectModel;

namespace Ornette.Application.Model.TrackOrder
{
    internal interface ITrackOrderLogic
    {
        ObservableCollection<Track> Tracks { get; set; }

        Track GetFirst();

        void SetCurrentTrack(Track track);

        NextTrack GetNextTrack(Track track, bool autoPlay);
    }
}
