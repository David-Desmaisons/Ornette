using Neutronium.BuildingBlocks.Application.ViewModels;
using Ornette.ViewModel.Common;

namespace Ornette.ViewModel
{
    /// <summary>
    /// ViewModel for the "about" route
    /// </summary>
    public class AboutViewModel
    {
        public ApplicationInformation Information => GlobalApplicationInformation.Information;
    }
}
