using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Runtime;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Shearlegs.Test
{
    public class ShearlegsTest
    {
        public static ShearlegsTestHelper Helper { get; } = new ShearlegsTestHelper();
        public static ShearlegsTestParameters Parameters { get; } = new ShearlegsTestParameters();
        public static ShearlegsTestResults Results { get; } = new ShearlegsTestResults();

        public static IServiceProvider ServiceProvider { get; } = BuildServiceProvider();

        private static IServiceProvider BuildServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            ShearlegsRuntime.RegisterServices(services);
            return services.BuildServiceProvider();
        }

        public static async Task<R> ExecutePluginAsync<T, R>(object parameters) where T : IPlugin where R : IPluginResult
        {
            IPluginManager pluginManager = ServiceProvider.GetRequiredService<IPluginManager>();
            Assembly pluginAssembly = typeof(T).Assembly;

            IPlugin pluginInstance = pluginManager.ActivatePlugin(pluginAssembly, JObject.FromObject(parameters).ToString());

            return (R)await pluginInstance.ExecuteAsync();
        }
    }
}
