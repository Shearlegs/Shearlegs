namespace Shearlegs.Web.Dashboard.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogException(Exception exception);
        void LogInformation(string message);
    }
}