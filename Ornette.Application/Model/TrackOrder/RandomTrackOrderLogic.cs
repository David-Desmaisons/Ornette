using Ornette.Application.Infra;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ornette.Application.Model.TrackOrder
{
    internal class RandomTrackOrderLogic : ITrackOrderLogic
    {
        private readonly IRandomProvider _RandomProvider;
        private readonly ObservableCollection<Track> _Tracks;
        private readonly IList<Track> _PlayedTracks = new List<Track>();

        public RandomTrackOrderLogic(IRandomProvider randomProvider, ObservableCollection<Track> tracks)
        {
            _Tracks = tracks;
            _RandomProvider = randomProvider;
        }

        public Track GetFirst()
        {
            _PlayedTracks.Clear();
            return GetNext(_Tracks);
        }

        public void SetCurrentTrack(Track track)
        {
            _PlayedTracks.Clear();
            _PlayedTracks.Add(track);
        }

        public NextTrack GetNext(Track track, bool autoPlay)
        {
            if (_Tracks.Count == 0)
                return NextTrack.None;

            var tracks = _Tracks.Where(t => !_PlayedTracks.Contains(t)).ToList();
            var nextTrack = GetNext(tracks);
            if (nextTrack != null)
                return NextTrack.PlayTrack(nextTrack);

            _PlayedTracks.Clear();
            return autoPlay ? NextTrack.PlayTrack(GetFirst()) : NextTrack.None;
        }

        public Track GetBack(Track track)
        {
            var currentCount = _PlayedTracks.Count;
            if (currentCount == 0)
                return null;

            var index = currentCount - 1;
            var result = _PlayedTracks[index];
            _PlayedTracks.RemoveAt(index);
            return result;
        }

        private Track GetNext(IList<Track> tracks)
        {
            if (tracks.Count == 0)
                return null;
            
            var index = _RandomProvider.Next(tracks.Count);
            var result = tracks[index];
            _PlayedTracks.Add(result);
            return result;
        }
    }
}
