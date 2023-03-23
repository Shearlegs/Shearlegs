using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.HttpContexts
{
    public interface IHttpContextBroker
    {
        string GetRequestAuthorizationHeaderValue();
        string GetRequestHostName();
        string GetRequestIPAddress();
        string GetRequestUserAgentHeaderValue();
    }
}