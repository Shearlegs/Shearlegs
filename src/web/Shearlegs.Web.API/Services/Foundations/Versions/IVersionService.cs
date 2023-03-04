using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Versions
{
    public interface IVersionService
    {
        ValueTask<Version> AddVersionAsync(AddVersionParams @params);
        ValueTask<IEnumerable<Version>> RetrieveAllVersionsAsync();
        ValueTask<Version> RetrieveVersionByIdAsync(int versionId);
        ValueTask<IEnumerable<Version>> RetrieveVersionsByPluginIdAsync(int pluginId);
    }
}