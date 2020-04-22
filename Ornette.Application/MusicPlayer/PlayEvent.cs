namespace Ornette.Application.MusicPlayer
{
    public class PlayEvent
    {
        public int? PositionInSeconds { get; }
        public PlayState State { get; }

        public PlayEvent(int? positionInSeconds, PlayState state)
        {
            PositionInSeconds = positionInSeconds;
            State = state;
        }
    }
}