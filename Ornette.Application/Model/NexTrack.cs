namespace Ornette.Application.Model
{
    internal class NexTrack
    {
        public Track Track { get; }
        public bool Play { get; }

        internal NexTrack(Track track, bool play)
        {
            Track = track;
            Play = play;
        }
    }
}
