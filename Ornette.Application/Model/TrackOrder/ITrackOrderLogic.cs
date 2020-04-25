namespace Ornette.Application.Model.TrackOrder
{
    public interface ITrackOrderLogic
    {
        Track GetFirst();

        void SetCurrentTrack(Track track);

        NextTrack GetNext(Track track, bool autoPlay);

        Track GetBack(Track track);
    }
}
