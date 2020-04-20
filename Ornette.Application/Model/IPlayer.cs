using System;
using System.Collections.ObjectModel;
using Ornette.Application.MusicPlayer;

namespace Ornette.Application.Model
{
    public interface IPlayer
    {
        IObservable<Track> CurrentTrack { get; }
        ObservableCollection<Track> Tracks { get; }
        IObservable<PlayEvent> Events { get; }

        double Volume { get; set; }

        void SetCurrentTrack(Track track);
        void SetPosition(TimeSpan? value);
        void Play();
        void Pause();
        void Stop();
    }
}
