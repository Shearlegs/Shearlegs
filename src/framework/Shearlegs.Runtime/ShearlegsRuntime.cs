using Microsoft.Extensions.DependencyInjection;
using Shearlegs.API.Constants;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Loaders;
using Shearlegs.Core.Plugins;
using Shearlegs.NuGet;
using System;
using System.IO;

namespace Shearlegs.Runtime
{
    public class ShearlegsRuntime
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<NuGetPackageManager>();

            serviceCollection.AddSingleton<IPluginManager, PluginManager>();
            serviceCollection.AddTransient<IPluginLoader, NuGetPluginLoader>();

            Directory.CreateDirectory(DirectoryConstants.CacheDirectory);
            Directory.CreateDirectory(DirectoryConstants.NugetPackagesDirectory);
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
