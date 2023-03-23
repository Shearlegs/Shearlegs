using Shearlegs.Web.API.Models.HttpUsers;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.HttpUsers
{
    public interface IHttpUserService
    {
        ValueTask<string> RetrieveAuthorizationJWTAsync();
        ValueTask<HttpUser> RetrieveHttpUserAsync();
    }
}