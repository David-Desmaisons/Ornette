using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive;
using System.Reactive.Concurrency;
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
        private readonly Subject<NexTrack> _CurrentTrackSubject = new Subject<NexTrack>();
        private readonly Subject<PlayEvent> _EventsSubject = new Subject<PlayEvent>();
        private Track _CurrentTrack;

        public ObservableCollection<Track> Tracks { get; } = new ObservableCollection<Track>();
        public IObservable<Track> CurrentTrack { get; }
        public IObservable<PlayEvent> Events { get; }

        public double Volume
        {
            get => _MusicPlayer.Volume;
            set => _MusicPlayer.Volume = value;
        }

        public bool AutoReplay { get; set; } = false;

        public Player(IMusicPlayer musicPlayer)
        {
            _MusicPlayer = musicPlayer;

            var trackFlow = _CurrentTrackSubject.Distinct();
            CurrentTrack = trackFlow.Select(tr => tr.Track).ObserveOn(DispatcherScheduler.Current);
            Events = _EventsSubject.ObserveOn(DispatcherScheduler.Current);

            Tracks.ObserveCollectionChanges().Subscribe(OnNext);
            trackFlow.Subscribe(UpdatePlayer);
        }

        public void SetCurrentTrack(Track value)
        {
            ChangeTrack(value, true);
        }

        private void ChangeTrack(Track track, bool play)
        {
            var nextTrack = new NexTrack(track, play);
            _CurrentTrackSubject.OnNext(nextTrack);
        }

        public void SetPositionInSeconds(int value)
        {
            _TrackPlayer?.SetPositionInSeconds(value);
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

        private void UpdatePlayer(NexTrack value)
        {
            _Listener?.Dispose();

            _CurrentTrack = value.Track;
            _TrackPlayer = _MusicPlayer.CreateTrackPlayer(_CurrentTrack.Path);
            _Listener = _TrackPlayer.Subscribe(OnNext);
            if (value.Play)
            {
                Play();
                return;
            }
            Stop();
        }

        public void OnNext(EventPattern<NotifyCollectionChangedEventArgs> collectionChanged)
        {
            var @event = collectionChanged.EventArgs;
            if ((@event.Action != NotifyCollectionChangedAction.Add) || (Tracks.Count != @event.NewItems.Count))
            {
                return;
            }
            ChangeTrack(Tracks[0], false);
        }

        public void OnNext(PlayEvent value)
        {
            if (value.State == PlayState.Ended)
            {
                OnEnded();
                return;
            }
            _EventsSubject.OnNext(value);
        }

        private void OnEnded()
        {
            var next = GetNexTrack();
            _Listener?.Dispose();
            Stop();
            if (next == null)
                return;

            _CurrentTrackSubject.OnNext(next);
        }

        private NexTrack GetNexTrack()
        {
            var nextIndex = GetNextIndex();
            if (nextIndex == -1)
                return null;

            var play = (nextIndex > 0) || AutoReplay;
            return new NexTrack(Tracks[nextIndex], play);
        }

        private int GetNextIndex()
        {
            var currentCount = Tracks.Count;
            if (currentCount == 0)
                return -1;

            var nextIndex = Tracks.IndexOf(_CurrentTrack) + 1;
            return (nextIndex > currentCount - 1) ? 0 : nextIndex;
        }
    }
}
