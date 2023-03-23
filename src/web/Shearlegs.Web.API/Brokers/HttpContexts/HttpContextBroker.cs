using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.HttpContexts
{
    public class HttpContextBroker : IHttpContextBroker
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private HttpContext httpContext => httpContextAccessor.HttpContext;

        public HttpContextBroker(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetRequestIPAddress()
        {
            string ipAddress = httpContext.Connection.RemoteIpAddress.ToString();

            return ipAddress;
        }

        public string GetRequestHostName()
        {
            string hostName = Dns.GetHostEntry(httpContext.Connection.RemoteIpAddress).HostName;

            return hostName;
        }

        public string GetRequestUserAgentHeaderValue()
        {
            string userAgent = httpContext.Request.Headers.UserAgent.ToString();

            return userAgent;
        }        

        public string GetRequestAuthorizationHeaderValue()
        {
            IHeaderDictionary requestHeaders = httpContext.Request.Headers;

            string value = null;
            if (requestHeaders.TryGetValue("Authorization", out StringValues values))
            {
                value = values.ToString();
            }

            return value;
        }
    }
}
