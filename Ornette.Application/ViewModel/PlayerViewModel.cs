using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ornette.Application.ViewModel
{
    public class PlayerViewModel : Neutronium.BuildingBlocks.ViewModel
    {
        private readonly IMusicPlayer _MusicPlayer;
        public ObservableCollection<string> Tracks { get; }

        public double Volume
        {
            get => _MusicPlayer.Volume;
            set => _MusicPlayer.Volume = value;
        }

        public PlayerViewModel(IMusicPlayer musicPlayer, IEnumerable<string> pathsToMusic)
        {
            _MusicPlayer = musicPlayer;
            Tracks = new ObservableCollection<string>(pathsToMusic);
        }
    }
}
