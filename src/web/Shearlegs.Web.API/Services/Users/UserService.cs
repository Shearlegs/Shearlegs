using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Brokers.Validations;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.Users.Params;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Users
{
    public partial class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IValidationBroker validationBroker;

        public UserService(IStorageBroker storageBroker, IValidationBroker validationBroker)
        {
            this.storageBroker = storageBroker;
            this.validationBroker = validationBroker;
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

        public async ValueTask<User> AddUserAsync(AddUserParams @params)
        {
            ValidateAddUserParams(@params);

            StoredProcedureResult<User> result = await storageBroker.AddUserAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new AlreadyExistsUserException();
            }

            return result.Result;
        }
    }
}
