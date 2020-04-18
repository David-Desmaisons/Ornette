using Ornette.Application.Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Ornette.Application.Model
{
    public class Player: IPlayer
    {
        private readonly IMusicPlayer _MusicPlayer;
        private ITrackPlayer _TrackPlayer;
        private IDisposable _Listener;
        private readonly Track[] _Tracks;
        private readonly Subject<TrackMetaData> _CurrentTrackSubject = new Subject<TrackMetaData>();

        private TrackMetaData _CurrentTrack;

        public ObservableCollection<TrackMetaData> Tracks { get; }

        public IObservable<TrackMetaData> CurrentTrack => _CurrentTrackSubject.Distinct();

        public double Volume
        {
            get => _MusicPlayer.Volume;
            set => _MusicPlayer.Volume = value;
        }

        public Player(IMusicPlayer musicPlayer, IEnumerable<Track> tracks)
        {
            _MusicPlayer = musicPlayer;

            tracks = tracks ?? Enumerable.Empty<Track>();
            Tracks = new ObservableCollection<TrackMetaData>(tracks.Select(t => t.MetaData));
            _Tracks = tracks.ToArray();

            CurrentTrack.Subscribe(UpdatePlayer);

            SetCurrentTrack(_Tracks[0].MetaData);
        }

        public void SetCurrentTrack(TrackMetaData value)
        {
            _CurrentTrackSubject.OnNext(value);
            UpdatePlayer(value);
        }

        public void Play()
        {
            _TrackPlayer.Play();
        }

        public void Pause()
        {
            _TrackPlayer.Pause();
        }

        public void Stop()
        {
            _TrackPlayer.Stop();
        }

        private void UpdatePlayer(TrackMetaData value)
        {
            _Listener?.Dispose();

            var track = _Tracks.First(t => t.MetaData == value);
            _TrackPlayer = _MusicPlayer.CreateTrackPlayer(track.Path);
            _Listener = _TrackPlayer.Subscribe(OnNext);
        }

        public void OnNext(PlayEvent value)
        {
        }
    }
}
