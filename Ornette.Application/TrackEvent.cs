using System;

namespace Ornette.Application
{
    public class TrackEvent
    {
        public TimeSpan? Position { get; }
        public PlayState State { get; }

        public TrackEvent(TimeSpan? position, PlayState state)
        {
            Position = position;
            State = state;
        }
    }
}