using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Cookies
{
    public interface ICookieBroker
    {
        ValueTask SetValue(string key, string value, DateTimeOffset? expireDate);
    }
}