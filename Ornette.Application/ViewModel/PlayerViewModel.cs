using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ornette.Application.ViewModel
{
    public class PlayerViewModel : Neutronium.BuildingBlocks.ViewModel
    {
        public ObservableCollection<string> Tracks { get; }

        public PlayerViewModel(IEnumerable<string> pathsToMusic)
        {
            Tracks = new ObservableCollection<string>(pathsToMusic);
        }
    }
}
