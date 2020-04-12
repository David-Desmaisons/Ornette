using Neutronium.BuildingBlocks.Application.ViewModels;

namespace Ornette.ViewModel.Common
{
    public class GlobalApplicationInformation
    {
        public static ApplicationInformation Information { get; } =
            new ApplicationInformation("Ornette", "David Desmaisons");
    }
}
