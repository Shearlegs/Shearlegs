using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Coordinations.VersionUploads
{
    public interface IVersionUploadCoordinationService
    {
        ValueTask QueueProcessVersionAsync(int versionUploadId);
    }
}