using System;
using System.Collections.ObjectModel;
using Ornette.Application.MusicPlayer;

namespace Ornette.Application.Model
{
    public interface IPlayer
    {
        IObservable<Track> CurrentTrack { get; }
        IObservable<PlayEvent> Events { get; }

        bool AutoReplay { get; set; }
        bool RandomPlay { get; set; }
        double Volume { get; set; }
        ObservableCollection<Track> Tracks { get; }

        void SetCurrentTrack(Track track);
        void SetPositionInSeconds(int value);
        void Play();
        void Pause();
        void Stop();
    }
}
