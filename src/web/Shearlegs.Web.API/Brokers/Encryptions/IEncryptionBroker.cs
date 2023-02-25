namespace Shearlegs.Web.API.Brokers.Encryptions
{
    public interface IEncryptionBroker
    {
        bool VerifyPassword(string passwordText, string passwordHash);
    }
}