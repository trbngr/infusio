using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Infusio.Auth;

// ReSharper disable RedundantStringInterpolation

namespace Infusio.Http
{
    public class InfusioClient
    {
        private readonly HttpClient _httpClient;
        private readonly AccessToken _accessToken;

        public InfusioClient(HttpClient httpClient, AccessToken token)
        {
            _httpClient = httpClient;
            _accessToken = token;
        }

        public Task<HttpResponseMessage> Send(HttpRequestMessage message) =>
            _httpClient.SendAsync(AddAuthorization(message, _accessToken));

        static HttpRequestMessage AddAuthorization(HttpRequestMessage message, AccessToken token)
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            return message;
        }
    }
}