namespace Shearlegs.Web.API.Brokers.Validations
{
    public class ValidationBroker : IValidationBroker
    {
        public bool IsPasswordInvalid(string password)
        {
            if (string.IsNullOrEmpty(password))
                return true;

            if (password.Length < 8)
                return true;

            return false;
        }

        public bool IsStringInvalid(string value, bool required, int maxLength, int minLength = 0)
        {
            if (string.IsNullOrEmpty(value) && required)
                return true;

            int length = value?.Length ?? 0;

            if (length < minLength)
                return true;

            if (length > maxLength)
                return true;

            return false;
        }
    }
}
