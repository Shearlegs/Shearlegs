using Microsoft.AspNetCore.Http;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Versions
{
    public interface IVersionOrchestrationService
    {
        ValueTask ExecuteVersionAsync(ExecuteVersionParams @params);
        ValueTask<Version> UploadVersionAsync(IFormFile formFile);
    }
}