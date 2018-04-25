using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infusio.Compiler.Parsing
{
    public static class Serialization
    {
        static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore
        };

        public static string Serialize(this object self) => JsonConvert.SerializeObject(self, Settings);

        public static T Deserialize<T>(this string json) =>
            JsonConvert.DeserializeObject<T>(json, Settings);
        
        public static T Deserialize<T>(this JToken token) =>
            JsonConvert.DeserializeObject<T>(token.ToString(), Settings);
    }
}