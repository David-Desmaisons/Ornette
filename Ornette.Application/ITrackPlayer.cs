using System;

namespace Ornette.Application
{
    public interface ITrackPlayer : IDisposable, IObservable<TrackEvent>
    {
        TimeSpan? Duration { get; }

        PlayState State { get;  }

        void Play();

        void Stop();

        void Pause();

        void SetPosition(TimeSpan value);
    }
}
