using System;
using Ornette.Application.Player;

namespace Music.Adapter.Bass.Player
{
    public class BrokenTrackPlayer : ITrackPlayer
    {
        public TimeSpan? Duration => null;
        public PlayState State => PlayState.Broken;

        public static ITrackPlayer Instance { get; } = new BrokenTrackPlayer();

        private BrokenTrackPlayer()
        {
        }

        public void Pause()
        {
        }

        public void Play()
        {
        }

        public void Stop()
        {
        }


        public void Dispose()
        {
        }

        public void SetPositionInSeconds(decimal value)
        {
        }

        private class NullDisposer : IDisposable
        {
            public void Dispose()
            {
            }
        }

        public IDisposable Subscribe(IObserver<PlayEvent> observer)
        {
            return new NullDisposer();
        }
    }
}
