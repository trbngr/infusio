using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infusion.Client
{
    public partial class InfusionsoftClient
    {
        public InfusionConfig Config { get; }

        public InfusionsoftClient(HttpClient httpClient, InfusionConfig config)
        {
            Config = config;
            _httpClient = httpClient;
            _settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings) =>
            settings.Converters.Add(new StringEnumConverter());

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            request.Headers.Add("Accept", "application/json, */*");
            request.Headers.Add("Authorization", $"Bearer {Config.ApiKey}");
        }
    }
}