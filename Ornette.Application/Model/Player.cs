using Ornette.Application.Model.TrackOrder;
using Ornette.Application.MusicPlayer;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Ornette.Application.Model
{
    public class Player : IPlayer
    {
        private readonly IMusicPlayer _MusicPlayer;
        private readonly ITrackOrderLogicFactory _TrackOrderLogicFactory;
        private ITrackPlayer _TrackPlayer;
        private IDisposable _Listener;
        private readonly Subject<NextTrack> _CurrentTrackSubject = new Subject<NextTrack>();
        private readonly Subject<PlayEvent> _EventsSubject = new Subject<PlayEvent>();
        private Track _CurrentTrack;
        private ITrackOrderLogic _TrackOrderLogic;

        public ObservableCollection<Track> Tracks { get; } = new ObservableCollection<Track>();
        public IObservable<Track> CurrentTrack { get; }
        public IObservable<PlayEvent> Events { get; }

        public double Volume
        {
            get => _MusicPlayer.Volume;
            set => _MusicPlayer.Volume = value;
        }

        public bool AutoReplay { get; set; } = false;

        private bool _RandomPlay;
        public bool RandomPlay
        {
            get => _RandomPlay;
            set
            {
                _RandomPlay = value;
                _TrackOrderLogic = _TrackOrderLogicFactory.GetLogic(Tracks, value);
                if (_CurrentTrack != null)
                    _TrackOrderLogic.SetCurrentTrack(_CurrentTrack);
            }
        }

        public Player(IMusicPlayer musicPlayer, ITrackOrderLogicFactory trackOrderLogicFactory)
        {
            _MusicPlayer = musicPlayer;
            _TrackOrderLogicFactory = trackOrderLogicFactory;

            RandomPlay = false;
            var trackFlow = _CurrentTrackSubject.DistinctUntilChanged();
            CurrentTrack = trackFlow.Select(tr => tr.Track).ObserveOn(DispatcherScheduler.Current);
            Events = _EventsSubject.ObserveOn(DispatcherScheduler.Current);
            trackFlow.Subscribe(UpdatePlayer);
        }

        public void SetCurrentTrack(Track value)
        {
            _TrackOrderLogic.SetCurrentTrack(value);
            ChangeTrack(value, true);
        }

        private void ChangeTrack(Track track, bool play)
        {
            var nextTrack = new NextTrack(track, play);
            _CurrentTrackSubject.OnNext(nextTrack);
        }

        public void SetPositionInSeconds(int value)
        {
            _TrackPlayer?.SetPositionInSeconds(value);
        }

        public void Play()
        {
            if (_CurrentTrack == null)
            {
                ChangeTrack(_TrackOrderLogic.GetFirst(), true);
                return;
            }
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

        public void Next()
        {
            var next = _TrackOrderLogic.GetNext(_CurrentTrack, AutoReplay);
            _CurrentTrackSubject.OnNext(next);
        }

        public void Back()
        {
            var next = _TrackOrderLogic.GetBack(_CurrentTrack);
            _CurrentTrackSubject.OnNext(NextTrack.PlayTrack(next));
        }

        private void UpdatePlayer(NextTrack value)
        {
            Stop();
            _Listener?.Dispose();

            _CurrentTrack = value.Track;
            if (_CurrentTrack == null)
            {
                OnNext(PlayEvent.Ready);
                return;
            }

            _TrackPlayer = _MusicPlayer.CreateTrackPlayer(_CurrentTrack.Path);
            _Listener = _TrackPlayer.Subscribe(OnNext);
            if (value.Play)
                Play();
        }

        private void OnNext(PlayEvent value)
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
            var next = _TrackOrderLogic.GetNext(_CurrentTrack, AutoReplay);
            _CurrentTrackSubject.OnNext(next);
        }
    }
}
