using System;
using CommonServiceLocator;

namespace Ornette
{
    /// <summary>
    /// Dependency Injection used during bootstrap to encapsulate IOC implementation
    /// </summary>
    public interface IDependencyInjectionConfiguration
    {
        /// <summary>
        /// Returns an instance of the serviceLocator
        /// </summary>
        /// <returns></returns>
        Lazy<IServiceLocator> GetServiceLocator();

        /// <summary>
        /// RegisterSingleton an interface implementation as a singleton
        /// </summary>
        /// <typeparam name="T">Interface to register</typeparam>
        /// <param name="implementation">Singleton to register</param>
        void RegisterSingleton<T>(T implementation);
    }
}