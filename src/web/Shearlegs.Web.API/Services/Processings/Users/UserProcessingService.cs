using Shearlegs.Web.API.Brokers.Encryptions;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.Users.Params;
using Shearlegs.Web.API.Services.Foundations.Users;
using Shearlegs.Web.Shared.Enums;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Processings.Users
{
    public partial class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IEncryptionBroker encryptionBroker;

        public UserProcessingService(IUserService userService, IEncryptionBroker encryptionBroker)
        {
            this.userService = userService;
            this.encryptionBroker = encryptionBroker;
        }

        public async ValueTask<User> AddUserWithPasswordAsync(AddUserWithPasswordParams @params)
        {
            string passwordHash = encryptionBroker.HashPassword(@params.PasswordText);

            AddUserParams addUserParams = new()
            {
                Name = @params.Name,
                Role = @params.Role,
                AuthenticationType = UserAuthenticationType.Password,
                PasswordHash = passwordHash
            };

            User user = await userService.AddUserAsync(addUserParams);

            return user;
        }

        public async ValueTask<User> AddUserWithWindowsAsync(AddUserWithWindowsParams @params)
        {
            AddUserParams addUserParams = new()
            {
                Name = @params.Name,
                Role = @params.Role,
                AuthenticationType = UserAuthenticationType.Windows
            };

            User user = await userService.AddUserAsync(addUserParams);

            return user;
        }

        public async ValueTask<User> RetrieveUserByNameAndPasswordAsync(string username, string passwordText)
        {
            User user = await userService.RetrieveUserByNameAsync(username);

            if (!encryptionBroker.VerifyPassword(passwordText, user.PasswordHash)) 
            {
                throw new InvalidPasswordUserException();
            }

            return user;
        }
    }
}
