using Neutronium.BuildingBlocks.Application.Navigation;
using Neutronium.Core.Navigation.Routing;

namespace Ornette
{
    /// <summary>
    /// Configure Routing
    /// </summary>
    public class RoutingConfiguration
    {
        public static IRouterSolver Register()
        {
            var router = new Router();
            BuildRoutes(router);
            return router;
        }

        /// <summary>
        /// Build the route. Add different logic here if needed.
        /// </summary>
        /// <param name="routeBuilder"></param>
        private static void BuildRoutes(IRouterBuilder routeBuilder)
        {
            var convention = routeBuilder.GetTemplateConvention("{vm}");
            typeof(RoutingConfiguration).GetTypesFromSameAssembly()
                .InNamespace("Ornette.ViewModel.Pages")
                .Register(convention);
        }
    }
}
