using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Models.Versions.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AddVersionResult> AddVersionAsync(AddVersionParams @params);
        ValueTask<Version> GetVersionAsync(GetVersionsParams @params);
        ValueTask<IEnumerable<Version>> GetVersionsAsync(GetVersionsParams @params);
        ValueTask<VersionContent> SelectVersionContentByIdAsync(int versionId);
    }
}
