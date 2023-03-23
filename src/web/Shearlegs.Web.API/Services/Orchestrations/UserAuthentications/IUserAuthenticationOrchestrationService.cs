using Shearlegs.Web.API.Models.UserAuthentications;
using Shearlegs.Web.API.Models.UserAuthentications.Params;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.UserAuthentications
{
    public interface IUserAuthenticationOrchestrationService
    {
        ValueTask<UserAuthenticationToken> LoginUserWithPasswordAsync(LoginUserWithPasswordParams @params);
        ValueTask LogoutUserAsync();
        ValueTask<AuthenticatedUser> RetrieveAuthenticatedUserAsync();
    }
}