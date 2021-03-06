﻿using System;
using CommonServiceLocator;
using Music.Adapter.Bass;
using Neutronium.BuildingBlocks.Application.LifeCycleHook;
using Neutronium.BuildingBlocks.ApplicationTools;
using Neutronium.BuildingBlocks.Wpf.Application;
using Neutronium.Core.WebBrowserEngine.Window;
using Neutronium.WPF.Internal;
using Ninject;
using Ornette.Application.Infra;
using Ornette.Application.Io;
using Ornette.Application.Model;
using Ornette.Application.Model.TrackOrder;
using Ornette.Application.MusicPlayer;
using Ornette.Application.ViewModel;
using Ornette.IO;
using Ornette.UI.ViewModel.Pages;

namespace Ornette.UI
{
    /// <summary>
    /// RegisterSingleton dependency injection for the application
    /// </summary>
    public class DependencyInjectionConfiguration : IDependencyInjectionConfiguration
    {
        private readonly StandardKernel _Kernel;
        private readonly Lazy<IServiceLocator> _ServiceLocator;

        public DependencyInjectionConfiguration()
        {
            var kernel = new StandardKernel(new NinjectSettings { UseReflectionBasedInjection = true });
            RegisterDependency(kernel);
            _Kernel = kernel;
            _ServiceLocator = new Lazy<IServiceLocator>(() => new NinjectServiceLocator(_Kernel));
        }

        /// <summary>
        /// Returns an instance of the serviceLocator
        /// </summary>
        /// <returns></returns>
        public Lazy<IServiceLocator> GetServiceLocator() => _ServiceLocator;

        /// <summary>
        /// RegisterSingleton an interface implementation as a singleton
        /// </summary>
        /// <typeparam name="T">Interface to register</typeparam>
        /// <param name="implementation">Singleton to register</param>
        public void RegisterSingleton<T>(T implementation) => _Kernel.Bind<T>().ToConstant(implementation);

        /// <summary>
        /// RegisterSingleton application injection dependency.
        /// Should be used to add more binding between class to interface.
        /// </summary>
        /// <param name="kernel">
        /// application Ninject kernel
        /// </param>
        private static void RegisterDependency(IKernel kernel)
        {
            var window = System.Windows.Application.Current.MainWindow;
            var application = new WpfApplication(window);
            kernel.Bind<IApplication>().ToConstant(application);
            kernel.Bind<IDispatcher>().ToConstant(new WPFUIDispatcher(window.Dispatcher));
            kernel.Bind<IApplicationLifeCycle>().To<ApplicationLifeCycle>();
            kernel.Bind<MainViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<PlayerViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<IPlayer>().To<Player>().InSingletonScope();
            kernel.Bind<IIoReader>().To<IoReader>().InSingletonScope();
            kernel.Bind<ITrackOrderLogicFactory>().To<TrackOrderLogicFactory>().InSingletonScope();
            kernel.Bind<IRandomProvider>().To<RandomProvider>();
            kernel.Bind<IMusicPlayer>().ToMethod(_ => BassMusicPlayer.Init(BassConfiguration.Email, BassConfiguration.Password)).InSingletonScope();
        }
    }
}
