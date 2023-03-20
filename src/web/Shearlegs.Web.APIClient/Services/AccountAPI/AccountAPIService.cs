using Shearlegs.Web.APIClient.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Services.AccountAPI
{
    public class AccountAPIService
    {
        private readonly ShearlegsWebAPIClient client;

        public AccountAPIService(ShearlegsWebAPIClient client)
        {
            this.client = client;
        }

        public async ValueTask<User> GetUserAsync()
        {
            string requestUri = "/account/user";

            return await client.GetFromJsonAsync<User>(requestUri);
        }
    }
}
