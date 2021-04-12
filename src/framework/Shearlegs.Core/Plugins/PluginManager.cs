using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Attributes;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.AssemblyLoading;
using Shearlegs.Core.NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins
{
    public class PluginManager : IPluginManager
    {
        private readonly NugetPackageManager nugetPackageManager;

        public PluginManager(NugetPackageManager nugetPackageManager)
        {
            this.nugetPackageManager = nugetPackageManager;
        }

        public async Task<IPluginResult> ExecutePluginAsync(byte[] pluginData, string parametersJson)
        {
            using AssemblyContext context = AssemblyContext.Create();
            Assembly pluginAssembly = await nugetPackageManager.LoadNugetPackageAsync(context, pluginData);

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

            foreach (Type service in services)
            {
                serviceCollection.Add(new ServiceDescriptor(service.GetType(), service.GetType(),
                    service.GetCustomAttribute<ServiceAttribute>().Lifetime));
            }

            if (parameters != null)
                serviceCollection.Add(new ServiceDescriptor(parameters, JsonConvert.DeserializeObject(parametersJson, parameters)));

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return (IPlugin)serviceProvider.GetRequiredService(pluginType);
        }
    }
}
