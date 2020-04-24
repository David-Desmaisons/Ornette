namespace Ornette.Application.Model
{
    public class NextTrack
    {
        public Track Track { get; }
        public bool Play { get; }

        internal static NextTrack None { get; } = new NextTrack(null, false);

        internal NextTrack(Track track, bool play)
        {
            Track = track;
            Play = play;
        }

        internal static NextTrack PlayTrack(Track track) => new NextTrack(track, true);
    }
}
