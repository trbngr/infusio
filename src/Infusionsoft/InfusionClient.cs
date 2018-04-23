using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Threading.Tasks;
using Infusion.Model;
using LanguageExt;
using Newtonsoft.Json;

namespace Infusion
{
    using static Prelude;

    public class InfusionClient
    {
        private readonly HttpClient _client;
        private readonly InfusionConfig _config;

        public InfusionClient(HttpClient client, InfusionConfig config)
        {
            _client = client;
            _config = config;
        }

        EitherAsync<Error, T> Send<T>(HttpRequestMessage message) =>
            from response in TryAsync(_client.SendAsync(message))
            from end in response.IsSuccessStatusCode
                ? ReadResult<T>(response)
                : ReadError(response).Bind(e => TryAsync<T>(() => throw new Exception(e.Message)))
            select end;

        TryAsync<T> ReadResult<T>(HttpResponseMessage response) =>
            from json in TryAsync(response.Content.ReadAsStringAsync())
            from result in Try(JsonConvert.DeserializeObject<T>(json)).ToAsync()
            select result;

        TryAsync<Error> ReadError(HttpResponseMessage response) =>
            from json in TryAsync(response.Content.ReadAsStringAsync())
            from result in Try(JsonConvert.DeserializeObject<Error>(json)).ToAsync()
            select result;


    }
}