namespace Shearlegs.Web.API.Brokers.Validations
{
    public interface IValidationBroker
    {
        bool IsPasswordInvalid(string password);
        bool IsStringInvalid(string value, bool required, int maxLength, int minLength = 0);
    }
}
