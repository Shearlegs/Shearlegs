using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Processings.Versions
{
    public interface IVersionProcessingService
    {
        ValueTask<Version> CreateVersionAsync(CreateVersionParams @params);
    }
}