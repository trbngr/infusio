using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Infusio;
using Infusio.Http;
using Infusio.Model;
using Infusio.Ops;
using Infusionsoft;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Newtonsoft.Json;

namespace Runner
{
    using static Dsl;
    using static Prelude;
    using static HttpSupport;
    using static Infusio.Auth.Authorization;

    class Program
    {
        static async Task Main()
        {
            var httpClient = new HttpClient(new LoggingHandler());

            var accessToken = CreateAccessTokenRequest(
                httpClient,
                ClientId("tj7a3rtbs5dmsz2sbwxx3phd"),
                ClientSecret("EEecE6bBYz"),
                RedirectUri("http://localhost")
            );

            var refreshToken = CreateRefreshTokenRequest(
                httpClient,
                ClientId("tj7a3rtbs5dmsz2sbwxx3phd"),
                ClientSecret("EEecE6bBYz")
            );

            var token = await accessToken(AccessCode("nh4bydyaqsje96dycppue6jj"));
            var token2 = await refreshToken(token);
            Console.Out.WriteLine("");


            return;
//            var client = new InfusioClient(new HttpClient(new LoggingHandler()), config);

//            var either = await client.GetAccountProfile();
//            var result = await either.Match(
//                Left: e => Left<Error, AccountProfile>(new Error(message: $"Error: {e.Message}")).AsTask(),
//                Right: p => client.UpdateAccountInfo(p.Copy(phone: "602-555-8521"))
//            );

//            Func<string, InfusioOp<AccountProfile>> updatePhoneNumber = phone =>
//                from prof in GetAccountProfile()
//                from _ in UpdateAccountInfo(prof.Copy(phone: phone))
//                from updated in GetAccountProfile()
//                select updated;
//
//            await Display(updatePhoneNumber("602-555-8521").RunWith(client));
//            await Display(interpret(updatePhoneNumber("888-888-8888"), client));
//
//            Console.Out.WriteLine("");
        }

        static async Task<Unit> Display<T>(Task<Either<Error, T>> either) =>
            Display(await either);

        static Unit Display<T>(Either<Error, T> either) => either.Match(
            Left: e => Console.WriteLine($"Error: {e.Message}"),
            Right: x => Console.WriteLine($"Result: {JsonConvert.SerializeObject(x, Formatting.Indented)}")
        );
    }

    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler() : this(new HttpClientHandler())
        {
        }

        public LoggingHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Request: {request}");
            try
            {
                // base.SendAsync calls the inner handler
                var response = await base.SendAsync(request, cancellationToken);
                Console.WriteLine($"Response: {response}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get response: {ex}");
                throw;
            }
        }
    }
}