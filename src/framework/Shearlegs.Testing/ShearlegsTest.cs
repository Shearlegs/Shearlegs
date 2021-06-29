using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Runtime;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Shearlegs.Testing
{
    public class ShearlegsTest
    {
        /// <summary>
        /// Parameters helper methods
        /// </summary>
        public static ShearlegsTestParameters Parameters { get; } = new ShearlegsTestParameters();
        /// <summary>
        /// Results helper methods
        /// </summary>
        public static ShearlegsTestResults Results { get; } = new ShearlegsTestResults();

        private static IServiceProvider ServiceProvider { get; } = ShearlegsRuntime.BuildServiceProvider();

        /// <summary>
        /// Executes the plugin
        /// </summary>
        /// <typeparam name="T">Plugin type</typeparam>
        /// <typeparam name="R">Plugin result type</typeparam>
        /// <param name="parameters">Parameters object for plugin</param>
        /// <returns>Plugin result</returns>
        public static async Task<R> ExecutePluginAsync<T, R>(object parameters) where T : IPlugin where R : IPluginResult
        {
            IPluginManager pluginManager = ServiceProvider.GetRequiredService<IPluginManager>();
            Assembly pluginAssembly = typeof(T).Assembly;

            IPlugin pluginInstance = pluginManager.ActivatePlugin(pluginAssembly, JObject.FromObject(parameters).ToString());

            return (R)await pluginInstance.ExecuteAsync();
        }
    }
}
