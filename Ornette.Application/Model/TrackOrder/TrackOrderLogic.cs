using System.Collections.ObjectModel;
using Ornette.Application.Infra;

namespace Ornette.Application.Model.TrackOrder
{
    public class TrackOrderLogicFactory : ITrackOrderLogicFactory
    {
        private readonly IRandomProvider _RandomProvider;

        public TrackOrderLogicFactory(IRandomProvider randomProvider)
        {
            _RandomProvider = randomProvider;
        }

        public ITrackOrderLogic GetLogic(ObservableCollection<Track> tracks, bool random)
        {
            return random ?
                (ITrackOrderLogic)new RandomTrackOrderLogic(_RandomProvider, tracks) :
                new LinearTrackOrderLogic(tracks);
        }
    }
}
