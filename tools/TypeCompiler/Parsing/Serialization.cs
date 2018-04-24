using LanguageExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DslCompiler.Parsing
{
    using static JsonConvert;

    public static class Serialization
    {
        static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore
        };

        public static string Serialize(this object self) => SerializeObject(self, Settings);

        public static T Deserialize<T>(this string json) =>
            DeserializeObject<T>(json, Settings);
        
        public static T Deserialize<T>(this JToken token) =>
            DeserializeObject<T>(token.ToString(), Settings);
    }
}