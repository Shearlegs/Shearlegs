using System;

namespace Shearlegs.Web.API.Brokers.JWTs
{
    public interface IJWTBroker
    {
        string CreateToken<T>(T payload, DateTimeOffset expireTime);
        T GetTokenPayload<T>(string jwt);
    }
}