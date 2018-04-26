using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infusio;
using Infusio.Http;
using Infusio.Model;
using Infusio.Ops;
using LanguageExt;
using Newtonsoft.Json;

namespace Runner
{
    using static Dsl;
    using static Prelude;
    using static HttpSupport;

    class Program
    {
        static async Task Main()
        {
            var httpClient = new HttpClient();
//            var clientId = ClientId("tj7a3rtbs5dmsz2sbwxx3phd");
//            var clientSecret = ClientSecret("EEecE6bBYz");
//            var redirectUri = RedirectUri("https://localhost");
//
//            var accessToken = CreateAccessTokenRequest(httpClient, clientId, clientSecret, redirectUri);
//            var refreshToken = CreateRefreshTokenRequest(httpClient, clientId, clientSecret);
//
//            var token = await (
//                from t1 in Log(accessToken(AccessCode("knxmcjvqmge2hpn7uvkwfysf")))
//                from t2 in Log(refreshToken(t1))
//                select t2
//            );
//            token.Match(
//                Left: e => { },
//                Right: _ => Console.WriteLine("Authenticated")
//            );

            var client = new InfusioClient(httpClient, new InfusioConfig("d85tpwgubxkrpk52q2pp39d8"));

            InfusioOp<AccountProfile> UpdatePhoneNumber(string phone) =>
                from prof in GetAccountProfile()
                from _ in UpdateAccountInfo(prof.Copy(phone: phone))
                from updated in GetAccountProfile()
                select updated;
            
//            var program = 
//                from c in ListContacts("chris@caliberweb.com")

            InfusioOp<Tag> GetTag(string tag)
            {
                var xxx = GetTag(tag).Match(
                    Some: Return,
                    None: () => CreateTag(new CreateTag(name: tag))
                );
            }


            await Display(UpdatePhoneNumber("602-555-8521").RunWith(client));
            await Display(interpret(UpdatePhoneNumber("888-888-8888"), client));

            Console.Out.WriteLine("");
        }

        static async Task<T> Log<T>(Task<T> self)
        {
            T value = await self;
            Console.Out.WriteLine(value);
            return value;
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

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Console.WriteLine($"Request: {request}");
            if (request.Content != null)
            {
                var stream = new MemoryStream();
                await request.Content.CopyToAsync(stream);
                var bytes = stream.ToArray();
                var content = Encoding.GetEncoding(28591).GetString(bytes);
                Console.WriteLine($"Request content: {content}");
            }

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