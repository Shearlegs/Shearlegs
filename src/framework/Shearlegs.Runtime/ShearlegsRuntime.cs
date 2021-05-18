using Microsoft.Extensions.DependencyInjection;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Loaders;
using Shearlegs.Core.Plugins;
using Shearlegs.NuGet;
using System;

namespace Shearlegs.Runtime
{
    public class ShearlegsRuntime
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<NuGetPackageManager>();
            serviceCollection.AddSingleton<IPluginManager, PluginManager>();
            serviceCollection.AddTransient<IPluginLoader, NuGetPluginLoader>();
        }

        public static IServiceProvider BuildServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            RegisterServices(services);
            return services.BuildServiceProvider();
        }
    }
}
