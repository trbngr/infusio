using System;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using LanguageExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public readonly string ApiKey;
        public readonly string Application;
        public readonly string ClientId;
        public readonly string ClientSecret;

        public static readonly Uri Uri = new Uri("https://api.infusionsoft.com/crm/rest/v1/");

        public InfusioConfig(string application, string clientId, string clientSecret)
        {
            Application = application.EndsWith("infusionsoft.com") ? application : $"{application}.infusionsoft.com";
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public Uri MakeUri(string relativeUri) => new Uri(Uri, relativeUri);
    }
}