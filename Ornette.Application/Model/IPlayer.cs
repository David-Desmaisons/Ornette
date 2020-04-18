using System;
using System.Collections.ObjectModel;

namespace Ornette.Application.Model
{
    public interface IPlayer
    {
        IObservable<TrackMetaData> CurrentTrack { get; }
        ObservableCollection<TrackMetaData> Tracks { get; }
        double Volume { get; set; }

        void SetCurrentTrack(TrackMetaData track);
        void Play();
        void Pause();
        void Stop();
    }
}
