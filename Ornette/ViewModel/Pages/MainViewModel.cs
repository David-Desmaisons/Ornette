using MoreCollection.Extensions;
using Ornette.Application.Io;
using Ornette.Application.ViewModel;

namespace Ornette.UI.ViewModel.Pages
{
    /// <summary>
    /// ViewModel for the "main" route
    /// </summary>
    public class MainViewModel : Neutronium.BuildingBlocks.ViewModel
    {
        public PlayerViewModel Player { get; }
        private readonly IIoReader _Reader;

        public MainViewModel(PlayerViewModel player, IIoReader reader)
        {
            Player = player;
            _Reader = reader;

            Player.Tracks.AddRange(
                _Reader.GetDirectoryTracks("C:\\"));
        }
    }
}
