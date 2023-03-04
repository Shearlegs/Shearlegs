namespace Shearlegs.Web.API.Brokers.Serializations
{
    public interface ISerializationBroker
    {
        string SerializeToJson(object value);
    }
}