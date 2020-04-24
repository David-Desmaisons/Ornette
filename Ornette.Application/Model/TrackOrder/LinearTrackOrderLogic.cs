using System.Collections.ObjectModel;

namespace Ornette.Application.Model.TrackOrder
{
    internal class LinearTrackOrderLogic : ITrackOrderLogic
    {
        private readonly ObservableCollection<Track> _Tracks;

        public LinearTrackOrderLogic(ObservableCollection<Track> tracks)
        {
            _Tracks = tracks;
        }

        public Track GetFirst()
        {
            return _Tracks.Count > 0 ? _Tracks[0] : null;
        }

        public void SetCurrentTrack(Track track)
        {
        }

        public NextTrack GetNextTrack(Track track, bool autoPlay)
        {
            var nextIndex = GetNextIndex(track);
            if ((nextIndex == -1) || ((nextIndex == 0) && !autoPlay))
                return NextTrack.None;

            return new NextTrack(_Tracks[nextIndex], true);
        }

        private int GetNextIndex(Track track)
        {
            var currentCount = _Tracks.Count;
            if (currentCount == 0)
                return -1;

            var nextIndex = _Tracks.IndexOf(track) + 1;
            return (nextIndex > currentCount - 1) ? 0 : nextIndex;
        }
    }
}
