using Shearlegs.Web.API.Brokers.Encryptions;
using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Brokers.Validations;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.Users.Params;
using Shearlegs.Web.API.Services.Foundations.Users;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Users
{
    public partial class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IValidationBroker validationBroker;
        private readonly IEncryptionBroker encryptionBroker;

        public UserService(IStorageBroker storageBroker, IValidationBroker validationBroker, IEncryptionBroker encryptionBroker)
        {
            this.storageBroker = storageBroker;
            this.validationBroker = validationBroker;
            this.encryptionBroker = encryptionBroker;
        }

        public async ValueTask<IEnumerable<User>> RetrieveAllUsersAsync()
        {
            IEnumerable<User> users = await storageBroker.SelectAllUsersAsync();

            return users;
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            User user = await storageBroker.SelectUserByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundUserException();
            }

            return user;
        }

        public async ValueTask<User> RetrieveUserByNameAsync(string username)
        {
            User user = await storageBroker.SelectUserByNameAsync(username);

            if (user == null)
            {
                throw new NotFoundUserException();
            }

            return user;
        }

        public async ValueTask<User> CreateUserAsync(CreateUserParams @params)
        {
            ValidateCreateUserParams(@params);

            string passwordHash = encryptionBroker.HashPassword(@params.PasswordText);

            AddUserParams addUserParams = new()
            {
                Name = @params.Name,
                Role = @params.Role,
                AuthenticationType = @params.AuthenticationType,
                PasswordHash = passwordHash
            };

            StoredProcedureResult<User> result = await storageBroker.AddUserAsync(addUserParams);

            if (result.ReturnValue == 1)
            {
                throw new AlreadyExistsUserException();
            }

            return result.Result;
        }

        public async ValueTask<User> ModifyUserIdentityAsync(ModifyUserIdentityParams @params)
        {
            ValidateModifyUserIdentityParams(@params);

            UpdateUserIdentityParams updateUserIdentityParams = new()
            {
                UserId = @params.UserId,
                Role = @params.Role
            };

            if (@params.Password != null)
            {
                updateUserIdentityParams.PasswordHash = encryptionBroker.HashPassword(@params.Password);
            }

            StoredProcedureResult<User> result = await storageBroker.UpdateUserIdentityAsync(updateUserIdentityParams);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundUserException();
            }

            return result.Result;
        }
    }
}
