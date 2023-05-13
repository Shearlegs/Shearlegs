using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.NodeClient.Models;

namespace Shearlegs.Web.API.Services.Foundations.NodeDaemons
{
    public partial class NodeDaemonService
    {
        public ProcessedPluginInfo MapPluginInformationToProcessedPluginInfo(PluginInformation pluginInformation)
        {
            ProcessedPluginInfo processedPluginInfo = new()
            {
                PackageId = pluginInformation.PackageId,
                Version = pluginInformation.Version,
                IsPrerelease = pluginInformation.IsPrerelease,
                Parameters = new(),
                ContentFiles = new()
            };

            foreach (PluginInformation.ParameterInfo parameterInfo in pluginInformation.Parameters)
            {
                processedPluginInfo.Parameters.Add(new()
                {
                    Name = parameterInfo.Name,
                    Description = parameterInfo.Description,
                    Type = parameterInfo.Type,
                    Value = parameterInfo.Value,
                    IsRequired = parameterInfo.IsRequired,
                    IsSecret = parameterInfo.IsSecret
                });
            }

            foreach (PluginInformation.ContentFileInfo contentFileInfo in pluginInformation.ContentFiles)
            {
                processedPluginInfo.ContentFiles.Add(new()
                {
                    Name = contentFileInfo.Name,
                    Length = contentFileInfo.Length
                });
            }

            return processedPluginInfo;
        }
    }
}
