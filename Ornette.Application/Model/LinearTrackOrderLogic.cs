using System.Collections.ObjectModel;

namespace Ornette.Application.Model
{
    internal class LinearTrackOrderLogic : ITrackOrderLogic
    {
        private Track _CurrentTrack;
        public ObservableCollection<Track> Tracks { get; set; }

        public Track GetFirst()
        {
            return Tracks.Count > 0 ? Tracks[0] : null;
        }

        public void SetCurrentTrack(Track track)
        {
            _CurrentTrack = track;
        }

        public NextTrack GetNextTrack(Track track, bool autoPlay)
        {
            var nextIndex = GetNextIndex(track);
            if ((nextIndex == -1) || ((nextIndex == 0) && !autoPlay))
                return NextTrack.None;

            return new NextTrack(Tracks[nextIndex], true);
        }

        private int GetNextIndex(Track track)
        {
            var currentCount = Tracks.Count;
            if (currentCount == 0)
                return -1;

            var nextIndex = Tracks.IndexOf(track) + 1;
            return (nextIndex > currentCount - 1) ? 0 : nextIndex;
        }
    }
}
