using System.Text.Json;
using System.Text.Json.Serialization;

namespace GTA5OnlineTools.Common.Utils
{
    public class JsonUtil
    {
        private static JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static T JsonDese<T>(string result)
        {
            return JsonSerializer.Deserialize<T>(result, Options);
        }
    }
}
