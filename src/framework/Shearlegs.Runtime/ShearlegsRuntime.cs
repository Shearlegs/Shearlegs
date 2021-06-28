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
            serviceCollection.AddTransient<NuGetPackageManager>();

            serviceCollection.AddSingleton<IPluginManager, PluginManager>();
            serviceCollection.AddTransient<IPluginLoader, NuGetPluginLoader>();
        }
    }
}
