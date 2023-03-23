using Shearlegs.Web.API.Models.UserSessions;
using Shearlegs.Web.API.Models.UserSessions.Params;
using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.UserSessions
{
    public interface IUserSessionService
    {
        ValueTask<UserSession> CreateUserSessionAsync(CreateUserSessionParams @params);
        ValueTask<UserSession> RetrieveUserSessionByIdAsync(Guid sessionId);
    }
}