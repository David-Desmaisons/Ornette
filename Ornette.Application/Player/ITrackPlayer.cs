using System;

namespace Ornette.Application.Player
{
    public interface ITrackPlayer : IDisposable, IObservable<PlayEvent>
    {
        TimeSpan? Duration { get; }

        PlayState State { get;  }

        void Play();

        void Stop();

        void Pause();

        void SetPosition(TimeSpan value);
    }
}
