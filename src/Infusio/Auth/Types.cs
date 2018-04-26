using LanguageExt;
using Newtonsoft.Json;

namespace Infusio.Auth
{
    public class Error : NewType<Error, string>
    {
        public Error(string value) : base(value)
        {
        }
    }

    public class AccessCode : NewType<AccessCode, string>
    {
        AccessCode(string value) : base(value)
        {
        }
    }

    public class RefreshToken : NewType<RefreshToken, string>
    {
        public RefreshToken(string value) : base(value)
        {
        }
    }

    public class RedirectUri : NewType<RedirectUri, string>
    {
        public RedirectUri(string value) : base(value)
        {
        }
    }

    public class ClientSecret : NewType<ClientSecret, string>
    {
        public ClientSecret(string value) : base(value)
        {
        }
    }

    public class ClientId : NewType<ClientId, string>
    {
        public ClientId(string value) : base(value)
        {
        }
    }

    public class AccessToken
    {
        [JsonProperty("access_token")] public readonly string Token;
        [JsonProperty("token_type")] public readonly string TokenType;
        [JsonProperty("expires_in")] public readonly long ExpiresIn;
        [JsonProperty("refresh_token")] public readonly string RefreshToken;
        [JsonProperty("scope")] public readonly string Scope;

        public AccessToken(string token = default, string tokenType = default, long expiresIn = default,
            string refreshToken = default, string scope = default)
        {
            Token = token;
            TokenType = tokenType;
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
            Scope = scope;
        }

        public static implicit operator RefreshToken(AccessToken token) =>
            Auth.RefreshToken.New(token.RefreshToken);

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public void Deconstruct(out string token, out string refreshToken, out long expiresIn)
        {
            token = Token;
            expiresIn = ExpiresIn;
            refreshToken = RefreshToken;
        }
    }
}