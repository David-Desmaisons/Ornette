using Neutronium.BuildingBlocks.Application.ViewModels;
using Ornette.Application.ViewModel;
using Ornette.ViewModel.Common;

namespace Ornette.ViewModel.Pages
{
    /// <summary>
    /// ViewModel for the "about" route
    /// </summary>
    public class AboutViewModel
    {
        public PlayerViewModel Player { get; }

        public ApplicationInformation Information => GlobalApplicationInformation.Information;

        public AboutViewModel(PlayerViewModel player)
        {
            Player = player;
        }
    }
}
