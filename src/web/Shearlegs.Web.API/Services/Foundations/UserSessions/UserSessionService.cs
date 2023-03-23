using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.UserSessions;
using Shearlegs.Web.API.Models.UserSessions.Exceptions;
using Shearlegs.Web.API.Models.UserSessions.Params;
using Shearlegs.Web.API.Models.UserSessions.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
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
                throw new NotFoundUserException();
            }

            return await RetrieveUserSessionByIdAsync(result.SessionId.Value);
        }

        public async ValueTask<UserSession> RevokeUserSessionByIdAsync(Guid sessionId)
        {
            RevokeUserSessionParams @params = new()
            {
                SessionId = sessionId
            };

            StoredProcedureResult result = await storageBroker.RevokeUserSessionAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundUserSessionException();
            }

            return await RetrieveUserSessionByIdAsync(sessionId);
        }

        public async ValueTask<UserSession> RetrieveUserSessionByIdAsync(Guid sessionId)
        {
            GetUserSessionsParams @params = new()
            {
                SessionId = sessionId
            };

            UserSession userSession = await storageBroker.GetUserSessionAsync(@params);

            if (userSession == null)
            {
                throw new NotFoundUserSessionException();
            }

            return userSession;
        }
    }
}
