using System;

namespace Ornette.Application.MusicPlayer
{
    public class PlayEvent
    {
        public TimeSpan? Position { get; }
        public PlayState State { get; }

        public PlayEvent(TimeSpan? position, PlayState state)
        {
            Position = position;
            State = state;
        }
    }
}