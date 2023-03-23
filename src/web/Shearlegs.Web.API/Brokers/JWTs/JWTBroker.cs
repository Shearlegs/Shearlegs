using LitJWT;
using LitJWT.Algorithms;
using Microsoft.Extensions.Options;
using Shearlegs.Web.API.Models.Options;
using System;

namespace Shearlegs.Web.API.Brokers.JWTs
{
    public class JWTBroker : IJWTBroker
    {
        private readonly IJwtAlgorithm algorithm;
        private readonly JWTOptions options;

        public JWTBroker(IOptions<JWTOptions> options)
        {
            this.options = options.Value;
            algorithm = new HS256Algorithm(this.options.KeyBytes);
        }

        public string CreateToken<T>(T payload, DateTimeOffset expireTime)
        {
            JwtEncoder encoder = new(algorithm);
            string token = encoder.Encode(payload, expireTime);

            return token;
        }

        public T GetTokenPayload<T>(string jwt)
        {
            JwtDecoder decoder = new(algorithm);

            DecodeResult result = decoder.TryDecode(jwt, out T payloadObj);

            if (result == DecodeResult.Success)
            {
                return payloadObj;
            }

            return default;
        }
    }
}
