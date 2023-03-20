using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Shearlegs.Web.APIClient.Models.Users;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shearlegs.Web.Dashboard
{
    public partial class App
    {
        public bool IsInitialized { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User user = await client.Account.GetUserAsync();

            string jsonString = JsonSerializer.Serialize(user);

            await Console.Out.WriteLineAsync(jsonString);
            await Task.Delay(1000);
            IsInitialized = true;
        }
    }
}
