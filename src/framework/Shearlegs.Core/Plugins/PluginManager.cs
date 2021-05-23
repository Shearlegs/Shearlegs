using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Attributes;
using Shearlegs.API.Plugins.Loaders;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.AssemblyLoading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins
{
    public class PluginManager : IPluginManager
    {
        private readonly IPluginLoader pluginLoader;

        public PluginManager(IPluginLoader pluginLoader)
        {
            this.pluginLoader = pluginLoader;
        }

        public async Task<IPluginResult> ExecutePluginAsync(byte[] pluginData, string parametersJson)
        {
            using AssemblyContext context = AssemblyContext.Create();
            using MemoryStream ms = new MemoryStream(pluginData);
            Assembly pluginAssembly = await pluginLoader.LoadPluginAsync(context, ms);

            IPlugin pluginInstance = ActivatePlugin(pluginAssembly, parametersJson);
            IPluginResult result = await pluginInstance.ExecuteAsync();

            return result;
        }

        private IPlugin ActivatePlugin(Assembly assembly, string parametersJson)
        {
            Type pluginType = assembly.GetTypes().FirstOrDefault(x => x.GetInterface(nameof(IPlugin)) != null);

            if (pluginType == null)
            {
                throw new NotImplementedException("The plugin assembly is missing IPlugin type");
            }

            IEnumerable<Type> services = assembly.GetTypes().Where(x => x.GetCustomAttribute<ServiceAttribute>() != null);
            Type parameters = assembly.GetTypes().FirstOrDefault(x => x.GetCustomAttribute<ParametersAttribute>() != null);

            // Add plugin as singleton service
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(pluginType);

            foreach (Type serviceType in services)
            {
                serviceCollection.Add(new ServiceDescriptor(serviceType, serviceType, serviceType.GetCustomAttribute<ServiceAttribute>().Lifetime));
            }

            if (parameters != null)
                serviceCollection.Add(new ServiceDescriptor(parameters, JsonConvert.DeserializeObject(parametersJson, parameters)));

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return (IPlugin)serviceProvider.GetRequiredService(pluginType);
        }
    }
}
