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
        private readonly ISet<Track> _PlayedTracks = new HashSet<Track>();

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

        public NextTrack GetNextTrack(Track track, bool autoPlay)
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
