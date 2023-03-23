using Shearlegs.Web.API.Brokers.JWTs;
using Shearlegs.Web.API.Models.JWTs;
using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.JWTs
{
    public class JWTService : IJWTService
    {
        private readonly IJWTBroker jwtBroker;

        public JWTService(IJWTBroker jwtBroker)
        {
            this.jwtBroker = jwtBroker;
        }

        public ValueTask<string> CreateUserTokenAsync(JWTUserPayload payload, DateTimeOffset expireTime)
        {
            string token = jwtBroker.CreateToken(payload, expireTime);

            return ValueTask.FromResult(token);
        }

        public ValueTask<JWTUserPayload> RetrieveUserTokenPayloadAsync(string token)
        {
            JWTUserPayload payload = jwtBroker.GetTokenPayload<JWTUserPayload>(token);

            return ValueTask.FromResult(payload);
        }
    }
}
