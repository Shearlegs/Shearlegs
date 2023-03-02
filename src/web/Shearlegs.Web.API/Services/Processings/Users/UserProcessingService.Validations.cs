using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.Users.Params;
using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Services.Processings.Users
{
    public partial class UserProcessingService
    {
        public void ValidateCreateUserParams(CreateUserParams @params)
        {
            CreateUserValidationException validationException = new();

            if (!string.IsNullOrEmpty(@params.PasswordText) && validationBroker.IsPasswordInvalid(@params.PasswordText))
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

            if (string.IsNullOrEmpty(@params.Name))
            {
                validationException.UpsertDataList("Name", "This field is required");
            }

            validationException.ThrowIfContainsErrors();            
        }
    }
}
