using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Newtonsoft.Json;

namespace Infusio.Auth
{
    public delegate Task<AccessToken> AccessTokenRequest(AccessCode code);

    public delegate Task<AccessToken> RefreshTokenRequest(RefreshToken token);

    public static class Authorization
    {
        public static ClientId ClientId(string value) => Auth.ClientId.New(value);
        public static ClientSecret ClientSecret(string value) => Auth.ClientSecret.New(value);
        public static RedirectUri RedirectUri(string value) => Auth.RedirectUri.New(value);
        public static AccessCode AccessCode(string value) => Auth.AccessCode.New(value);

        public static AccessTokenRequest CreateAccessTokenRequest(HttpClient client, ClientId id, ClientSecret secret, RedirectUri uri) =>
            code => RequestAccessToken(client, id, secret, uri, code);

        public static async Task<AccessToken> RequestAccessToken(HttpClient client, ClientId id, ClientSecret secret, RedirectUri uri, AccessCode code)
        {
            var t = await Post(client, Prelude.Map(
                ("client_id", id.Value),
                ("client_secret", secret.Value),
                ("code", code.Value),
                ("grant_type", "authorization_code"),
                ("redirect_uri", uri.Value)
            ));
            return t;
        }

        public static RefreshTokenRequest CreateRefreshTokenRequest(HttpClient client, ClientId id, ClientSecret secret) =>
            token => RequestAccessToken(client, id, secret, token);

        public static async Task<AccessToken> RequestAccessToken(HttpClient client, ClientId id, ClientSecret secret, RefreshToken token)
        {
            var t = await Post(client, Prelude.Map(
                ("client_id", id.Value),
                ("client_secret", secret.Value),
                ("refresh_token", token.Value),
                ("grant_type", "refresh_token"),
                ("Header:Authorization", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id.Value}:{secret.Value}")))
            ));
            return t;
        }

        static Task<HttpRequestMessage> Request(Map<string, string> values) =>
            from dic in values.ToDictionary().AsTask()
            from content in new FormUrlEncodedContent(values.ToDictionary()).AsTask()
            from msg in new HttpRequestMessage(HttpMethod.Post, MakeUri(values))
            {
                Content = content
            }.AsTask()
            select msg;

        static Task<AccessToken> Post(HttpClient client, Map<string, string> values) =>
            from message in Request(values)
            from resp in client.SendAsync(message)
            where resp.IsSuccessStatusCode
            from json in resp.Content.ReadAsStringAsync()
            select JsonConvert.DeserializeObject<AccessToken>(json);


        static string MakeUri(Map<string, string> values)
        {
            return "https://api.infusionsoft.com/token";
            return values.Head().Map(head =>
                values.Tail().Fold(
                    new StringBuilder($"https://api.infusionsoft.com/token?{head.Item1}={head.Item2}"),
                    ((builder, pair) => builder.Append($"&{pair.Item1}={pair.Item2}"))
                ).ToString()
            );
        }
    }
}