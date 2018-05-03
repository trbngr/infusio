using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infusio.Http
{
    using static Prelude;
    using static JsonConvert;

    internal delegate Task<Either<InfusioError, T>> HttpWorkflow<T>(HttpClient client);

    internal static class HttpUtils
    {
        internal static HttpWorkflow<T> HttpWorkflow<T>(HttpRequestMessage message, HashSet<KnownResponse.Eq, KnownResponse> responses) where T : class =>
            client =>
                from httpResponse in SendRequest(client, message)
                from responseType in FindResponse(httpResponse, responses)
                from result in ReadResult<T>(httpResponse, responseType)
                select result;

        internal static HttpWorkflow<Unit> HttpWorkflow(HttpRequestMessage message, HashSet<KnownResponse.Eq, KnownResponse> responses) =>
            client =>
                from httpResponse in SendRequest(client, message)
                from responseType in FindResponse(httpResponse, responses)
                select unit;

        internal static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        internal static Option<(string name, object value)> RequestParameter(string name, object value) =>
            Optional(value).Map(x => (name, x));

        static HttpContent MakeHttpContent(params Option<(string name, object value)>[] values) => ifNoneUnsafe(
            from body in Some(values.FoldT(HashMap<string, object>(), (acc, x) => acc.Add(x.name, x.value)))
            where !body.IsEmpty
            select new StringContent(SerializeObject(body, SerializerSettings)),
            () => null
        );

        internal static string MakeUri(string relative, params Option<(string name, object value)>[] values) => ifNone(
            from pair in Some(values.FoldT(HashMap<string, object>(), (acc, x) => acc.Add(x.name, x.value)))
            where !pair.IsEmpty
            let qs = pair.Map((key, value) => $"{key}={value}").Values
            select $"{relative}?{string.Join("&", qs)}",
            relative
        );

        internal static HttpRequestMessage Request(HttpMethod method, string relativeUrl, string accessToken,
            params Option<(string name, object value)>[] values) =>
            Request(method, relativeUrl, accessToken, MakeHttpContent(values));

        internal static HttpRequestMessage Request(HttpMethod method, string relativeUrl, string accessToken, object body) =>
            Request(method, relativeUrl, accessToken, new StringContent(SerializeObject(body), Encoding.UTF8, "application/json"));

        internal static HttpRequestMessage Request(HttpMethod method, string relativeUrl, string accessToken, HttpContent content) =>
            new HttpRequestMessage(method, $"https://api.infusionsoft.com/crm/rest/v1{relativeUrl}")
            {
                Content = content,
                Headers =
                {
                    Accept = {MediaTypeWithQualityHeaderValue.Parse("application/json")},
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
                }
            };

        static Task<Either<InfusioError, KnownResponse>> FindResponse(HttpResponseMessage message, HashSet<KnownResponse.Eq, KnownResponse> responses) => match(
            responses.Find(KnownResponse.For(message.StatusCode)),
            None: () => message.Content.ReadAsStringAsync()
                .Map(x => Left<InfusioError, KnownResponse>(new InfusioError($"Unexpected response: {message.StatusCode} {x}"))),
            Some: response => Right<InfusioError, KnownResponse>(response).AsTask()
        );

        static Task<Either<InfusioError, HttpResponseMessage>> SendRequest(HttpClient client, HttpRequestMessage message) => match(
            TryAsync(() => client.SendAsync(message)),
            Fail: e => Left<InfusioError, HttpResponseMessage>(new InfusioError($"Generic send InfusioError: {e.Message}")),
            Succ: x => Right<InfusioError, HttpResponseMessage>(x)
        );

        static Task<Either<InfusioError, T>> ReadResult<T>(HttpResponseMessage message, KnownResponse response) where T : class =>
            !response.IsSuccess
                ? Left<InfusioError, T>(new InfusioError(response.Description)).AsTask()
                : match(
                    from json in TryAsync(() => message.Content.ReadAsStringAsync())
                    from result in Try(() => DeserializeObject(json, response.Type)).ToAsync()
                    select result,
                    Fail: e => Left<InfusioError, T>(new InfusioError($"Generic read InfusioError: {e.Message}")),
                    Succ: t => match(
                        Optional((T) t),
                        None: () => Left<InfusioError, T>(new InfusioError($"Unable to read {typeof(T)}")),
                        Some: x => Right<InfusioError, T>(x)
                    )
                );
    }
}