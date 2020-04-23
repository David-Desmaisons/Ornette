namespace Ornette.Application.MusicPlayer
{
    public class PlayEvent
    {
        public int? PositionInSeconds { get; }
        public PlayState State { get; }

        public static PlayEvent Ready { get; } = new PlayEvent(null, PlayState.Ready);

        public PlayEvent(int? positionInSeconds, PlayState state)
        {
            PositionInSeconds = positionInSeconds;
            State = state;
        }
    }
}