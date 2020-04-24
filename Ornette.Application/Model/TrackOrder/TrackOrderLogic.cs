using System.Collections.ObjectModel;

namespace Ornette.Application.Model.TrackOrder
{
    public class TrackOrderLogicFactory : ITrackOrderLogicFactory
    {
        public ITrackOrderLogic GetLogic(ObservableCollection<Track> tracks, bool random)
        {
            return new LinearTrackOrderLogic(tracks);
        }
    }
}
