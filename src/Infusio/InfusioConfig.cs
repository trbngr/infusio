using Infusio.Auth;

namespace Infusio
{
    public sealed class InfusioConfig
    {
        public readonly AccessToken AccessToken;
        public InfusioConfig(AccessToken accessToken) => AccessToken = accessToken;
    }
}