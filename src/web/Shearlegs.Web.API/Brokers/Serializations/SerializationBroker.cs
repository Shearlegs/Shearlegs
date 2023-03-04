using System.Text.Json;

namespace Shearlegs.Web.API.Brokers.Serializations
{
    public class SerializationBroker : ISerializationBroker
    {
        public string SerializeToJson(object value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}
