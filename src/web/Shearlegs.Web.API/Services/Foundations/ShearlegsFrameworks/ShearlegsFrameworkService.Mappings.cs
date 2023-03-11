using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Web.API.Models.ShearlegsFrameworks;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Results;
using System;

namespace Shearlegs.Web.API.Services.Foundations.ShearlegsFrameworks
{
    public partial class ShearlegsFrameworkService
    {
        private ShearlegsPluginInfo MapPluginInfoToShearlegsPluginInfo(IPluginInfo pluginInfo)
        {
            ShearlegsPluginInfo shearlegsPluginInfo = new()
            {
                PackageId = pluginInfo.PackageId,
                Version = pluginInfo.Version,
                Parameters = new()
            };

            foreach (IPluginParameterInfo parameterInfo in pluginInfo.Parameters)
            {
                ShearlegsPluginParameterInfo shearlegsPluginParameterInfo = new()
                {
                    Name = parameterInfo.Name,
                    Description = parameterInfo.Description,
                    Type = parameterInfo.Type.ToString(),
                    Value = parameterInfo.Value.ToString(),
                    IsSecret = parameterInfo.IsSecret,
                    IsRequired = parameterInfo.IsRequired,
                    IsArray = parameterInfo.Type.IsArray
                };

                shearlegsPluginInfo.Parameters.Add(shearlegsPluginParameterInfo);
            }

            return shearlegsPluginInfo;
        }

        private ShearlegsPluginResult MapPluginResultToShearlegsPluginResult(IPluginResult pluginResult)
        {
            if (pluginResult is PluginErrorResult pluginErrorResult)
            {
                return new ShearlegsPluginErrorResult()
                {
                    Message = pluginErrorResult.Message,
                    ExceptionString = pluginErrorResult.ExceptionString,
                    ExceptionMessage = pluginErrorResult.ExceptionMessage
                };
            }

            if (pluginResult is PluginTextResult pluginTextResult)
            {
                return new ShearlegsPluginTextResult()
                {
                    Text = pluginTextResult.Text,
                    IsMarkupString = pluginTextResult.IsMarkupString
                };
            }

            if (pluginResult is PluginFileResult pluginFileResult)
            {
                return new ShearlegsPluginFileResult()
                {
                    Name = pluginFileResult.Name,
                    Content = pluginFileResult.Content,
                    MimeType = pluginFileResult.MimeType
                };
            }

            throw new NotImplementedException();
        }
    }
}
