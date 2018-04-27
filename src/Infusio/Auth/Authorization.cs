using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Newtonsoft.Json;

namespace Infusio.Auth
{
    using static Prelude;

    public delegate Task<AuthorizationInfo> AccessTokenRequest(AccessCode code);

    public delegate Task<AuthorizationInfo> RefreshTokenRequest(RefreshToken token);

    public static class Authorization
    {
        public static ClientId ClientId(string value) => Auth.ClientId.New(value);
        public static ClientSecret ClientSecret(string value) => Auth.ClientSecret.New(value);
        public static RedirectUri RedirectUri(string value) => Auth.RedirectUri.New(value);
        public static AccessCode AccessCode(string value) => Auth.AccessCode.New(value);

        public static AccessTokenRequest AccessTokenRequest(HttpClient client, ClientId id, ClientSecret secret,
            RedirectUri uri) =>
            code => RequestAccessToken(client, id, secret, uri, code);

        public static RefreshTokenRequest
            RefreshTokenRequest(HttpClient client, ClientId id, ClientSecret secret) =>
            token => RequestAccessToken(client, id, secret, token);

        public static Task<AuthorizationInfo> RequestAccessToken(HttpClient client, ClientId id,
            ClientSecret secret,
            RedirectUri uri, AccessCode code) => Post(client, Map(
            ("client_id", id.Value),
            ("client_secret", secret.Value),
            ("code", code.Value),
            ("grant_type", "authorization_code"),
            ("redirect_uri", uri.Value)
        ));

        public static Task<AuthorizationInfo> RequestAccessToken(HttpClient client, ClientId id,
            ClientSecret secret,
            RefreshToken token) => Post(client, Map(
            ("client_id", id.Value),
            ("client_secret", secret.Value),
            ("refresh_token", token.Value),
            ("grant_type", "refresh_token"),
            ("Header:Authorization", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id.Value}:{secret.Value}")))
        ));

        static HttpRequestMessage Request(Map<string, string> values) => new HttpRequestMessage(
            method: HttpMethod.Post,
            requestUri: "https://api.infusionsoft.com/token"
        ) {Content = new FormUrlEncodedContent(values.ToDictionary())};

        static Task<AuthorizationInfo> Post(HttpClient client, Map<string, string> values) =>
            from message in Request(values).AsTask()
            from resp in client.SendAsync(message)
            from result in ReadResponse(resp).Map(x => x.Copy(expiration: DateTime.UtcNow.AddSeconds(x.ExpiresIn)))
            select result;

        static Task<AuthorizationInfo> ReadResponse(HttpResponseMessage response) =>
            response.IsSuccessStatusCode
                ? response.Content.ReadAsStringAsync().Map(JsonConvert.DeserializeObject<AuthorizationInfo>)
                : throw new Exception($"Error retrieving access token: {response.ReasonPhrase}");
    }
}