using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
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

        public async ValueTask<string> GetAuthorizationHeaderValueAsync()
        {
            IHeaderDictionary requestHeaders = httpContext.Request.Headers;

            if (httpContext.Request.Headers.TryGetValue("Authorization", out StringValues values))
            {
                return token.ToString();
            }

            throw new MissingAuthorizationHeaderException();

            // Bearer <JWT>
            // Possible exceptions
            // 1. Header Authorization not found
            // 2. Authorization type not supported (only Bearer)
            // 3. JWT token is invalid
            // 4. JWT token expired

            string jwtToken = requestHeaders

            string jwtToken = httpContext.Reque.Substring("Bearer ".Length).Trim();
            .Authorization

        }

    }
}
