using System;
using System.Collections.ObjectModel;

namespace Ornette.Application.Model
{
    public interface IPlayer
    {
        IObservable<Track> CurrentTrack { get; }
        ObservableCollection<Track> Tracks { get; }
        double Volume { get; set; }

        void SetCurrentTrack(Track track);
        void Play();
        void Pause();
        void Stop();
    }
}
