using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Neutronium.MVVMComponents;
using Neutronium.MVVMComponents.Relay;
using Ornette.Application.Model;
using Ornette.Application.Player;

namespace Ornette.Application.ViewModel
{
    public class PlayerViewModel : Neutronium.BuildingBlocks.ViewModel
    {
        private readonly IMusicPlayer _MusicPlayer;
        private ITrackPlayer _TrackPlayer;
        private IDisposable _Listener;
        private readonly Track[] _Tracks;

        public ObservableCollection<TrackMetaData> Tracks { get; }

        private TrackMetaData _CurrentTrack;
        public TrackMetaData CurrentTrack
        {
            get => _CurrentTrack;
            private set
            {
                if (!Set(ref _CurrentTrack, value))
                    return;

                UpdatePlayer(value);
            }
        }

        public ICommandWithoutParameter Play { get; }
        public ICommandWithoutParameter Pause { get; }
        public ICommandWithoutParameter Stop { get; }

        public double Volume
        {
            get => _MusicPlayer.Volume;
            set => _MusicPlayer.Volume = value;
        }

        public PlayerViewModel(IMusicPlayer musicPlayer, IEnumerable<Track> tracks)
        {
            _MusicPlayer = musicPlayer;

            Play  = new RelayToogleCommand(DoPlay);
            Pause = new RelayToogleCommand(DoPause);
            Stop  = new RelayToogleCommand(DoStop);

            tracks = tracks ?? Enumerable.Empty<Track>();
            Tracks = new ObservableCollection<TrackMetaData>(tracks.Select(t => t.MetaData));
            _Tracks = tracks.ToArray();
            CurrentTrack = _Tracks[0].MetaData;
        }

        private void DoPlay()
        {
            _TrackPlayer.Play();
        }

        private void DoPause()
        {
            _TrackPlayer.Pause();
        }

        private void DoStop()
        {
            _TrackPlayer.Stop();
        }

        private void UpdatePlayer(TrackMetaData value)
        {
            _Listener?.Dispose();

            var track = _Tracks.First(t => t.MetaData == value);
            _TrackPlayer = _MusicPlayer.CreateTrackPlayer(track.Path);
            _Listener = _TrackPlayer.ObserveOn(SynchronizationContext.Current).Subscribe(OnNext);
        }

        public void OnNext(PlayEvent value)
        {
        }
    }
}
