using Newtonsoft.Json;

namespace Infusio.Auth
{
    public class AccessToken
    {
        [JsonProperty("access_token")] public string Token { get; set; }

        [JsonProperty("token_type")] public string TokenType { get; set; }

        [JsonProperty("expires_in")] public long ExpiresIn { get; set; }

        [JsonProperty("refresh_token")] public string RefreshToken { get; set; }

        [JsonProperty("scope")] public string Scope { get; set; }

        public static implicit operator RefreshToken(AccessToken token) =>
            Auth.RefreshToken.New(token.RefreshToken);
    }
}