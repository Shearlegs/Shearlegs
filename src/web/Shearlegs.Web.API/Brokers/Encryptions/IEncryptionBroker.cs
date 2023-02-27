namespace Shearlegs.Web.API.Brokers.Encryptions
{
    public interface IEncryptionBroker
    {
        string HashPassword(string passwordText);
        bool VerifyPassword(string passwordText, string passwordHash);
    }
}