using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData.Binding;
using Ornette.Application.MusicPlayer;

namespace Ornette.Application.Model
{
    public class Player : IPlayer
    {
        private readonly IMusicPlayer _MusicPlayer;
        private ITrackPlayer _TrackPlayer;
        private IDisposable _Listener;
        private readonly Subject<Track> _CurrentTrackSubject = new Subject<Track>();
        private Track _CurrentTrack;

        public ObservableCollection<Track> Tracks { get; } = new ObservableCollection<Track>();

        public IObservable<Track> CurrentTrack => _CurrentTrackSubject.Distinct();

        public double Volume
        {
            get => _MusicPlayer.Volume;
            set => _MusicPlayer.Volume = value;
        }

        public Player(IMusicPlayer musicPlayer)
        {
            _MusicPlayer = musicPlayer;

            Tracks.ObserveCollectionChanges().Subscribe(OnNext);
            CurrentTrack.Subscribe(UpdatePlayer);
        }

        public void SetCurrentTrack(Track value)
        {
            _CurrentTrackSubject.OnNext(value);
        }

        public void Play()
        {
            _TrackPlayer?.Play();
        }

        public void Pause()
        {
            _TrackPlayer?.Pause();
        }

        public void Stop()
        {
            _TrackPlayer?.Stop();
        }

        private void UpdatePlayer(Track value)
        {
            _Listener?.Dispose();

            _CurrentTrack = value;
            _TrackPlayer = _MusicPlayer.CreateTrackPlayer(_CurrentTrack.Path);
            _Listener = _TrackPlayer.Subscribe(OnNext);
        }

        public void OnNext(EventPattern<NotifyCollectionChangedEventArgs> collectionChanged)
        {
            var @event = collectionChanged.EventArgs;
            if ((@event.Action != NotifyCollectionChangedAction.Add) || (Tracks.Count != @event.NewItems.Count))
            {
                return;
            }
            LoadFirstTrack();
        }

        private void LoadFirstTrack()
        {
            if (Tracks.Count == 0)
                return;

            SetCurrentTrack(Tracks[0]);
        }

        public void OnNext(PlayEvent value)
        {
        }
    }
}
