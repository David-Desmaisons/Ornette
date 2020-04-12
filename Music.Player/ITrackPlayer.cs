using System;

namespace Music.Player
{
    public interface ITrackPlayer : IDisposable, IObservable<TrackEvent>
    {
        TimeSpan? Duration { get; }

        void Play();

        void Stop();

        void Pause();

        void SetPosition(TimeSpan value);
    }
}
