using System;
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

    public class AccessToken : NewType<AccessToken, string>
    {
        AccessToken(string value) : base(value)
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

    public class AuthorizationInfo
    {
        [JsonProperty("access_token")] public readonly string Token;
        [JsonProperty("token_type")] public readonly string TokenType;
        [JsonProperty("expires_in")] public readonly long ExpiresIn;
        [JsonProperty("refresh_token")] public readonly string RefreshToken;
        [JsonProperty("scope")] public readonly string Scope;
        public readonly DateTime Expiration;

        public AuthorizationInfo(string token = default, string tokenType = default, long expiresIn = default,
            string refreshToken = default, string scope = default, DateTime expiration = default)
        {
            Token = token;
            TokenType = tokenType;
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
            Scope = scope;
            Expiration = expiration;
        }

        public AuthorizationInfo Copy(string token = default, string tokenType = default, long expiresIn = default,
            string refreshToken = default, string scope = default, DateTime expiration = default) => new AuthorizationInfo(
            token: token ?? Token,
            tokenType: tokenType ?? TokenType,
            expiresIn: expiresIn == default ? ExpiresIn : expiresIn,
            refreshToken: refreshToken ?? RefreshToken,
            scope: scope ?? Scope,
            expiration: expiration == default ? Expiration : expiration
        );

        public static implicit operator RefreshToken(AuthorizationInfo token) =>
            Auth.RefreshToken.New(token.RefreshToken);

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public void Deconstruct(out AccessToken token, out RefreshToken refreshToken, out DateTime expiration)
        {
            token = AccessToken.New(Token);
            refreshToken = Auth.RefreshToken.New(RefreshToken);
            expiration = Expiration;
        }
    }
}