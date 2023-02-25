using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.Users.Params;

namespace Shearlegs.Web.API.Services.Users
{
    public partial class UserService
    {
        public void ValidateAddUserParams(AddUserParams @params)
        {
            AddUserValidationException validationException = new();

            if (validationBroker.IsStringInvalid(@params.Name, true, 255, 0))
            {
                validationException.UpsertDataList("Name", "The value is required and must not exceed 255 characters");
            }

            validationException.ThrowIfContainsErrors();
        }
    }
}
