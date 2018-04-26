namespace Infusio
{
    public sealed class InfusioConfig
    {
        public readonly string ApiKey;
        public InfusioConfig(string apiKey) => ApiKey = apiKey;
    }
}