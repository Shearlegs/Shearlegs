using Shearlegs.Web.API.Brokers.HttpContexts;
using Shearlegs.Web.API.Brokers.JWTs;
using Shearlegs.Web.API.Models.HttpUsers;
using Shearlegs.Web.API.Models.HttpUsers.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.HttpUsers
{
    public class HttpUserService : IHttpUserService
    {
        private readonly IHttpContextBroker httpContextBroker;
        private readonly IJWTBroker jwtBroker;

        public HttpUserService(IHttpContextBroker httpContextBroker, IJWTBroker jwtBroker)
        {
            this.httpContextBroker = httpContextBroker;
            this.jwtBroker = jwtBroker;
        }

        public ValueTask<string> RetrieveAuthorizationJWTAsync()
        {
            string authorizationValue = httpContextBroker.GetRequestAuthorizationHeaderValue();

            if (string.IsNullOrEmpty(authorizationValue) || !authorizationValue.StartsWith("Bearer"))
            {
                throw new InvalidJWTAuthorizationHeaderException();
            }

            string jwt = authorizationValue.Split(' ').LastOrDefault();

            return ValueTask.FromResult(jwt);
        }

        public ValueTask<HttpUser> RetrieveHttpUserAsync()
        {
            HttpUser httpUser = new()
            {
                HostName = httpContextBroker.GetRequestHostName(),
                IPAddress = httpContextBroker.GetRequestIPAddress(),
                UserAgent = httpContextBroker.GetRequestUserAgentHeaderValue()
            };

            return ValueTask.FromResult(httpUser);
        }
    }
}
