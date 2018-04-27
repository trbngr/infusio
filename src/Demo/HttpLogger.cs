using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    public class HttpLogger : DelegatingHandler
    {
        public HttpLogger() : this(new HttpClientHandler())
        {
        }

        public HttpLogger(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Console.WriteLine($"{request.Method}: {request.RequestUri}");

            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                Console.WriteLine($"{response.StatusCode}: {response.ReasonPhrase}");
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