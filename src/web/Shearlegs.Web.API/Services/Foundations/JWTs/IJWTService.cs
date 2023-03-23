using Shearlegs.Web.API.Models.JWTs;
using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.JWTs
{
    public interface IJWTService
    {
        ValueTask<string> CreateUserTokenAsync(JWTUserPayload payload, DateTimeOffset expireTime);
        ValueTask<JWTUserPayload> RetrieveUserTokenPayloadAsync(string token);
    }
}