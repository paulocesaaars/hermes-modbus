using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Deviot.Hermes.ModbusTcp.BDD.Helpers
{
    public static class JsonHelper
    {
        public static StringContent CreateStringContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static T Deserializer<T>(string json)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static string Serializer<T>(T value)
        {
            return JsonSerializer.Serialize<T>(value);
        }
    }
}
