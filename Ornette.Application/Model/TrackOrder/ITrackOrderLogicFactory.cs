using System.Collections.ObjectModel;

namespace Ornette.Application.Model.TrackOrder
{
    public interface ITrackOrderLogicFactory
    {
        ITrackOrderLogic GetLogic(ObservableCollection<Track> tracks, bool random);
    }
}
