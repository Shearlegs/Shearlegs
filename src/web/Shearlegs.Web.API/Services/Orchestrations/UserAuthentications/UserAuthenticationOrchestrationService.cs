using Microsoft.AspNetCore.Mvc;
using Shearlegs.Web.API.Brokers.Encryptions;
using Shearlegs.Web.API.Models.HttpUsers;
using Shearlegs.Web.API.Models.HttpUsers.Exceptions;
using Shearlegs.Web.API.Models.JWTs;
using Shearlegs.Web.API.Models.UserAuthentications;
using Shearlegs.Web.API.Models.UserAuthentications.Exceptions;
using Shearlegs.Web.API.Models.UserAuthentications.Params;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.UserSessions;
using Shearlegs.Web.API.Models.UserSessions.Params;
using Shearlegs.Web.API.Services.Foundations.HttpUsers;
using Shearlegs.Web.API.Services.Foundations.JWTs;
using Shearlegs.Web.API.Services.Foundations.Users;
using Shearlegs.Web.API.Services.Foundations.UserSessions;
using Shearlegs.Web.API.Services.Processings.Users;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.UserAuthentications
{
    public class UserAuthenticationOrchestrationService : IUserAuthenticationOrchestrationService
    {
        private readonly IHttpUserService httpUserService;
        private readonly IUserProcessingService userService;
        private readonly IUserSessionService userSessionService;
        private readonly IJWTService jwtService;

        public UserAuthenticationOrchestrationService(
            IHttpUserService httpUserService,
            IUserProcessingService userService,
            IUserSessionService userSessionService,
            IJWTService jwtService)
        {
            this.httpUserService = httpUserService;
            this.userService = userService;
            this.userSessionService = userSessionService;
            this.jwtService = jwtService;
        }

        public async ValueTask<UserAuthenticationToken> LoginUserWithPasswordAsync(LoginUserWithPasswordParams @params)
        {
            HttpUser httpUser = await httpUserService.RetrieveHttpUserAsync();

            User user = await userService.RetrieveUserByNameAndPasswordAsync(@params.Username, @params.Password);

            CreateUserSessionParams createUserSessionParams = new()
            {
                UserId = user.Id,
                HostName = httpUser.HostName,
                IPAddress  = httpUser.IPAddress,
                UserAgent = httpUser.UserAgent,
            };

            UserSession userSession = await userSessionService.CreateUserSessionAsync(createUserSessionParams);

            JWTUserPayload payload = new()
            {
                SessionId = userSession.Id
            };

            string token = await jwtService.CreateUserTokenAsync(payload, userSession.ExpireDate);

            return new UserAuthenticationToken()
            {
                AuthenticatedUser = new()
                {
                    User = user,
                    UserSession = userSession
                },
                Token = token
            };
        }

        public async ValueTask<AuthenticatedUser> RetrieveAuthenticatedUserAsync()
        {
            try
            {
                string token = await httpUserService.RetrieveAuthorizationJWTAsync();

                JWTUserPayload payload = await jwtService.RetrieveUserTokenPayloadAsync(token);

                UserSession userSession = await userSessionService.RetrieveUserSessionByIdAsync(payload.SessionId);

                if (userSession.IsInvalid)
                {
                    throw new NotAuthenticatedUserException();
                }

                User user = await userService.RetrieveUserByIdAsync(userSession.UserId);

                return new AuthenticatedUser()
                {
                    User = user,
                    UserSession = userSession
                };
            } catch (InvalidJWTAuthorizationHeaderException)
            {
                throw new NotAuthenticatedUserException();
            }
        }

        public async ValueTask LogoutUserAsync()
        {
            AuthenticatedUser authenticatedUser = await RetrieveAuthenticatedUserAsync();
            await userSessionService.RevokeUserSessionByIdAsync(authenticatedUser.UserSession.Id);
        }
    }
}
