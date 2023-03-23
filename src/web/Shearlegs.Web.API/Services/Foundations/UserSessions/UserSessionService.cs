using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.UserSessions;
using Shearlegs.Web.API.Models.UserSessions.Exceptions;
using Shearlegs.Web.API.Models.UserSessions.Params;
using Shearlegs.Web.API.Models.UserSessions.Results;
using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.UserSessions
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IStorageBroker storageBroker;

        public UserSessionService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<UserSession> CreateUserSessionAsync(CreateUserSessionParams @params)
        {
            CreateUserSessionResult result = await storageBroker.CreateUserSessionAsync(@params);

            if (result.StoredProcedureResult.ReturnValue == 1)
            {
                throw new InvalidCredentialsUserSessionException();
            }

            return await RetrieveUserSessionByIdAsync(result.SessionId.Value);
        }

        public async ValueTask<UserSession> RetrieveUserSessionByIdAsync(Guid sessionId)
        {
            GetUserSessionsParams @params = new()
            {
                SessionId = sessionId
            };

            return await storageBroker.GetUserSessionAsync(@params);
        }
    }
}
