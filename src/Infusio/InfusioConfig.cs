namespace Infusio
{
    public sealed class InfusioConfig
    {
        public readonly string AccessToken;
        public InfusioConfig(string accessToken) => AccessToken = accessToken;
    }
}