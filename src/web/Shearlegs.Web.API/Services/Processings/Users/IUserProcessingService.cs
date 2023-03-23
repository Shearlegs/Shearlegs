using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Params;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask<User> CreateUserAsync(CreateUserParams @params);
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<User> RetrieveUserByNameAndPasswordAsync(string username, string passwordText);
        ValueTask<User> RetrieveUserByNameAsync(string username);
    }
}