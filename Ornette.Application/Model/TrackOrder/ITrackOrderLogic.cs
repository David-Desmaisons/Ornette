namespace Ornette.Application.Model.TrackOrder
{
    public interface ITrackOrderLogic
    {
        Track GetFirst();

        void SetCurrentTrack(Track track);

        NextTrack GetNextTrack(Track track, bool autoPlay);
    }
}
