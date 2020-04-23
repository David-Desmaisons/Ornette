namespace Ornette.Application.Model
{
    internal class NextTrack
    {
        public Track Track { get; }
        public bool Play { get; }

        internal static NextTrack None { get; } = new NextTrack(null, false);

        internal NextTrack(Track track, bool play)
        {
            Track = track;
            Play = play;
        }
    }
}
