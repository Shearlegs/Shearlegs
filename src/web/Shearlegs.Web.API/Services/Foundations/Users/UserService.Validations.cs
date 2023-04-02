using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.Users.Params;
using Shearlegs.Web.Shared.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shearlegs.Web.API.Services.Users
{
    public partial class UserService
    {
        public void ValidateCreateUserParams(CreateUserParams @params)
        {
            CreateUserValidationException validationException = new();

            if (@params.PasswordText != null && validationBroker.IsPasswordInvalid(@params.PasswordText))
            {
                validationException.UpsertDataList("Password", "Must be at least 8 characters long");
            }

            if (@params.AuthenticationType == UserAuthenticationType.Password)
            {
                if (string.IsNullOrEmpty(@params.PasswordText))
                {
                    validationException.UpsertDataList("Password", "This field is required for the authentication type 'Password'");
                }
            }

            if (validationBroker.IsStringInvalid(@params.Name, true, 255, 0))
            {
                validationException.UpsertDataList("Name", "The value is required and must not exceed 255 characters");
            }

            validationException.ThrowIfContainsErrors();
        }

        public void ValidateModifyUserIdentityParams(ModifyUserIdentityParams @params)
        {
            ModifyUserIdentityException validationException = new();

            if (@params.Password != null && validationBroker.IsPasswordInvalid(@params.Password))
            {
                validationException.UpsertDataList("Password", "Must be at least 8 characters long");
            }
        }
    }
}
