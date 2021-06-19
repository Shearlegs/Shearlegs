using Newtonsoft.Json;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Models
{
    public class MResult
    {
        public int Id { get; set; }
        public int VersionId { get; set; }
        public int UserId { get; set; }
        public string ParametersJson { get; set; }
        public string ResultJson { get; set; }
        public string ResultType { get; set; }
        public DateTime CreateDate { get; set; }

        public MVersion Version { get; set; }
        public MUser User { get; set; }

        public static MResult Create(IPluginResult pluginResult, string parametersJson, int versionId, int userId)
        {
            MResult result = new()
            {
                VersionId = versionId,
                UserId = userId,
                ParametersJson = parametersJson,
                ResultJson = JsonConvert.SerializeObject(pluginResult)
            };

            if (pluginResult is PluginTextResult)
            {
                result.ResultType = nameof(PluginTextResult);
            } else if (pluginResult is PluginFileResult)
            {
                result.ResultType = nameof(PluginFileResult);
            }
            return result;
        }
    }
}
