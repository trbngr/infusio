using System;

namespace Infusio
{
    public sealed class InfusioConfig
    {
        //To generate access/refresh tokens...
        //https://accounts.infusionsoft.com/app/oauth/userToken
        /*
         * application:
            scope: full
            client_id:
            client_secret:
         */

        public string ApiKey { get; }
        public static readonly Uri Uri = new Uri("https://api.infusionsoft.com/crm/rest/v1/");

        public InfusioConfig(string apiKey) => ApiKey = apiKey;

        public Uri MakeUri(string relativeUri) => new Uri(Uri, relativeUri);
    }
}