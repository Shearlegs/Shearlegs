using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Cookies
{
    public class CookieBroker : ICookieBroker
    {
        private readonly IJSRuntime jsRuntime;

        public CookieBroker(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async ValueTask<string> GetValue(string key)
        {
            var cValue = await GetCookie();
            if (string.IsNullOrEmpty(cValue))
            {
                return null;
            }

            var vals = cValue.Split(';');
            foreach (var val in vals)
            {
                if (!string.IsNullOrEmpty(val) && val.IndexOf('=') > 0)
                {
                    if (val.Substring(0, val.IndexOf('=')).Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        return val.Substring(val.IndexOf('=') + 1);
                    }
                }
            }

            return null;
        }

        public async ValueTask SetValue(string key, string value, DateTimeOffset? expireDate)
        {
            string curExp = "";
            if (expireDate.HasValue)
            {
                curExp = expireDate.Value.ToUniversalTime().ToString("R");
            }

            await SetCookie($"{key}={value}; expires={curExp}; path=/");
        }

        private async ValueTask SetCookie(string value)
        {
            await jsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
        }

        private async ValueTask<string> GetCookie()
        {
            return await jsRuntime.InvokeAsync<string>("eval", $"document.cookie");
        }
    }
}
