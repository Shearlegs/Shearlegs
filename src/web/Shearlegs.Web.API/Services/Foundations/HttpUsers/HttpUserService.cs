using Shearlegs.Web.API.Brokers.HttpContexts;
using Shearlegs.Web.API.Models.HttpUsers;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.HttpUsers
{
    public class HttpUserService : IHttpUserService
    {
        private readonly IHttpContextBroker httpContextBroker;

        public HttpUserService(IHttpContextBroker httpContextBroker)
        {
            this.httpContextBroker = httpContextBroker;
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
