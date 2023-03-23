namespace Shearlegs.Web.Dashboard.Brokers.Cookies
{
    public interface ICookieBroker
    {
        ValueTask<string> GetValue(string key);
        ValueTask SetValue(string key, string value, DateTimeOffset? expireDate);
    }
}