namespace Ornette.Application.Player
{
    public class PlayEvent
    {
        public decimal? PositionInSeconds { get; }
        public PlayState State { get; }

        public static PlayEvent Ready { get; } = new PlayEvent(null, PlayState.Ready);

        public PlayEvent(decimal? positionInSeconds, PlayState state)
        {
            PositionInSeconds = positionInSeconds;
            State = state;
        }
    }
}