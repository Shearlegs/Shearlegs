using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shearlegs.API.Exceptions;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Attributes;
using Shearlegs.API.Plugins.Content;
using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Loaders;
using Shearlegs.API.Plugins.Parameters;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.AssemblyLoading;
using Shearlegs.Core.Plugins.Info;
using Shearlegs.Core.Plugins.Result;
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

            IPluginLoadResult loadResult;

            try
            {
                loadResult = await pluginLoader.LoadPluginAsync(context, ms);
            } catch (Exception e)
            {
                return new PluginErrorResult("Error loading plugin", e);
            }

            IPlugin pluginInstance;
            try
            {
               pluginInstance = ActivatePlugin(loadResult.PluginAssembly.Assembly, parametersJson, loadResult.FileStore);
            } catch (Exception e)
            {
                return new PluginErrorResult("Error activating plugin", e);
            }
            
            IPluginResult result;

            try
            {
                result = await pluginInstance.ExecuteAsync();
            } catch (Exception e)
            {
                return new PluginErrorResult("Error executing plugin", e);
            }

            return result;
        }

        public async Task<IPluginInfo> GetPluginInfoAsync(byte[] pluginData)
        {
            using AssemblyContext context = AssemblyContext.Create();
            using MemoryStream ms = new MemoryStream(pluginData);

            IPluginLoadResult loadResult = await pluginLoader.LoadPluginAsync(context, ms);

            List<IPluginParameterInfo> parameters = new();

            IPluginInfo info = new PluginInfo() 
            {
                PackageId = loadResult.PluginAssembly.PackageId,
                Version = loadResult.PluginAssembly.Version,
                IsPrerelease = loadResult.PluginAssembly.IsPrerelease,
                Parameters = parameters    
            };

            IEnumerable<Type> types = loadResult.PluginAssembly.Assembly.GetTypes().Where(t => t.GetCustomAttribute<ParametersAttribute>() != null);
            if (types.Count() == 0)
            {
                return info;
            }

            if (types.Count() > 1)
            {
                throw new MultipleParametersTypeException();
            }

            Type type = types.First();
            object instance = Activator.CreateInstance(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                ParameterAttribute attribute = property.GetCustomAttribute<ParameterAttribute>(true);
                IPluginParameterInfo parameter = new PluginParameterInfo() 
                { 
                    Name = property.Name,
                    Type = property.PropertyType,
                    Value = property.GetValue(instance),
                    Description = attribute?.Description ?? null,
                    IsRequired = attribute?.IsRequired ?? false,
                    IsSecret = property.GetCustomAttribute<SecretAttribute>() != null
                };
                parameters.Add(parameter);
            }

            foreach (FieldInfo field in type.GetFields())
            {
                ParameterAttribute attribute = field.GetCustomAttribute<ParameterAttribute>(true);
                IPluginParameterInfo parameter = new PluginParameterInfo()
                {
                    Name = field.Name,
                    Type = field.FieldType,
                    Value = field.GetValue(instance),
                    Description = attribute?.Description ?? null,
                    IsRequired = attribute?.IsRequired ?? false,
                    IsSecret = field.GetCustomAttribute<SecretAttribute>() != null
                };
                parameters.Add(parameter);
            }

            return info;
        }

        private IPlugin ActivatePlugin(Assembly assembly, string parametersJson, IContentFileStore contentFileStore)
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
            serviceCollection.AddSingleton(contentFileStore);

            foreach (Type serviceType in services)
            {
                serviceCollection.Add(new ServiceDescriptor(serviceType, serviceType, serviceType.GetCustomAttribute<ServiceAttribute>().Lifetime));
            }

            if (parameters != null && parametersJson != null)
                serviceCollection.Add(new ServiceDescriptor(parameters, JsonConvert.DeserializeObject(parametersJson, parameters)));

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return (IPlugin)serviceProvider.GetRequiredService(pluginType);
        }
    }
}
