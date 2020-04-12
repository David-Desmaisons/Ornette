using System.Windows;
using CommonServiceLocator;
using Neutronium.BuildingBlocks.Application.LifeCycleHook;
using Neutronium.BuildingBlocks.Application.Navigation;
using Neutronium.BuildingBlocks.Application.ViewModels;
using Neutronium.BuildingBlocks.Application.WindowServices;
using Neutronium.MVVMComponents;
using Neutronium.WPF.ViewModel;
using Ornette;
using Ornette.ViewModel.Common;

namespace Music.Converter
{
    /// <summary>
    /// Builder for application main viewModel.
    /// Responsible for setting-up routing and dependency injection
    /// </summary>
    public class ApplicationViewModelBuilder
    {
        /// <summary>
        /// Main application viewModel as built by the builder
        /// </summary>
        public ApplicationViewModel<ApplicationInformation> ApplicationViewModel { get; }

        private readonly LifeCycleEventsRegister _LifeCycleEventsRegister;

        /// <summary>
        /// Instantiate the builder and create the ApplicationViewModel.
        /// RegisterSingleton the application services to the dependency injection.
        /// </summary>
        /// <param name="wpfWindow">
        /// Application main window
        /// </param>
        public ApplicationViewModelBuilder(Window wpfWindow)
        {
            var window = new WindowViewModel(wpfWindow);
            var routeSolver = RoutingConfiguration.Register();
            var serviceLocatorBuilder = new DependencyInjectionConfiguration();
            var serviceLocatorLazy = serviceLocatorBuilder.GetServiceLocator();

            var navigation = new NavigationViewModel(serviceLocatorLazy, routeSolver);

            serviceLocatorBuilder.RegisterSingleton<IWindowViewModel>(window);
            serviceLocatorBuilder.RegisterSingleton<INavigator>(navigation);
            serviceLocatorBuilder.RegisterSingleton(navigation);

            ApplicationViewModel = new ApplicationViewModel<ApplicationInformation>(window, navigation, GlobalApplicationInformation.Information);
            serviceLocatorBuilder.RegisterSingleton<IMessageBox>(ApplicationViewModel);
            serviceLocatorBuilder.RegisterSingleton<INotificationSender>(ApplicationViewModel);

            var serviceLocator = serviceLocatorLazy.Value;
            _LifeCycleEventsRegister = RegisterLifeCycleEvents(serviceLocator);
        }

        private static LifeCycleEventsRegister RegisterLifeCycleEvents(IServiceLocator serviceLocator)
        {
            var register = serviceLocator.GetInstance<LifeCycleEventsRegister>();
            return register.Register();
        }
    }
}
