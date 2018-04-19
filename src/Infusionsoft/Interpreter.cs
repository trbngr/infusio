using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Newtonsoft.Json;
// ReSharper disable ConvertClosureToMethodGroup

namespace Infusionsoft
{
    using static Prelude;
    using static JsonConvert;

    public static class Interpreter
    {
        public static Task<Either<Error, T>> Interpret<T>(Op<T> op, InfusionsoftConfig config) =>
            RunAsync(op, config).ToEither();

        public static Task<Either<Error, T>> Run<T>(this Op<T> op, InfusionsoftConfig config) =>
            RunAsync(op, config).ToEither();

        static EitherAsync<Error, T> RunAsync<T>(Op<T> op, InfusionsoftConfig config) =>
            op is Op<T>.Return r                  ? RightAsync<Error, T>(r.Value.AsTask()) :
            op is Op<T>.GetAccountProfile gap     ? GetAccountProfile(gap, config) :
            op is Op<T>.UpdateAccountProfile upap ? UpdateAccountProfile(upap, config) :
                                                      throw new NotSupportedException();

        static EitherAsync<Error, T> UpdateAccountProfile<T>(Op<T>.UpdateAccountProfile op, InfusionsoftConfig config) =>
            from profile in Execute<AccountProfile>(config, HttpMethod.Put, "account/profile", op.Profile)
            from next in RunAsync(op.Next(profile), config)
            select next;

        static EitherAsync<Error, T> GetAccountProfile<T>(Op<T>.GetAccountProfile op, InfusionsoftConfig config) =>
            from profile in Execute<AccountProfile>(config, HttpMethod.Get, "account/profile")
            from next in RunAsync(op.Next(profile), config)
            select next;

        static EitherAsync<Error, T> Execute<T>(InfusionsoftConfig config, HttpMethod method, string relativeUri, object body = null) => match(
            from msg in TryAsync(CreateMessage(method, config.MakeUri(relativeUri), config, body))
            from res in TryAsync(() => Execute<T>(msg))
            select res,
            Succ: t => Right<Error, T>(t),
            Fail: e => Left<Error, T>(Error.New(e.Message))
        ).ToAsync();

        static HttpRequestMessage CreateMessage(HttpMethod method, Uri uri, InfusionsoftConfig config, object body)
        {
            var msg = new HttpRequestMessage(method, uri);
            msg.Headers.Add("Accept", "application/json, */*");
            msg.Headers.Add("Authorization", $"Bearer {config.ApiKey}");
            if (body != null)
            {
                msg.Content = new StringContent(SerializeObject(body), Encoding.UTF8, "application/json");
            }

            return msg;
        }

        static readonly HttpClient Client = new HttpClient();

        static Task<T> Execute<T>(HttpRequestMessage message) =>
            from response in Client.SendAsync(message)
            from json in response.Content.ReadAsStringAsync()
            from result in DeserializeObject<T>(json).AsTask()
            select result;
    }
}