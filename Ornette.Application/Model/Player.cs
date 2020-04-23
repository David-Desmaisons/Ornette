﻿using Ornette.Application.MusicPlayer;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Ornette.Application.Model.TrackOrder;

namespace Ornette.Application.Model
{
    public class Player : IPlayer
    {
        private readonly IMusicPlayer _MusicPlayer;
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
                _TrackOrderLogic = TrackOrderLogic.GetLogic(value);
            }
        }

        public Player(IMusicPlayer musicPlayer)
        {
            _MusicPlayer = musicPlayer;
            _TrackOrderLogic = new LinearTrackOrderLogic { Tracks = Tracks };

            var trackFlow = _CurrentTrackSubject.Distinct();
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
            var next = _TrackOrderLogic.GetNextTrack(_CurrentTrack, AutoReplay);
            _CurrentTrackSubject.OnNext(next);
        }
    }
}
