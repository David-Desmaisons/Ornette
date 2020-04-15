using System;

namespace Ornette.Application.Player
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