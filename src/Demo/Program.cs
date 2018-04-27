using System.Net.Http;
using System.Threading.Tasks;
using Demo.Demos;
using Infusio;
using Infusio.Auth;
using Infusio.Http;

namespace Demo
{
    using static TokenCache;
    using static Authorization;

    class Program
    {
        static async Task Main()
        {
            // These credentials are a test account.
            // I don't care about the data.
            // of course, you would provide your own.
            var httpClient = new HttpClient(new HttpLogger());
            var clientId = ClientId("tj7a3rtbs5dmsz2sbwxx3phd");
            var clientSecret = ClientSecret("EEecE6bBYz");
            var redirectUri = RedirectUri("https://localhost");

            // These are meant to be created once and used throughout your application.
            // Inject into your DI container, or whatever your preference.
            AccessTokenRequest requestAccessToken = AccessTokenRequest(httpClient, clientId, clientSecret, redirectUri);
            RefreshTokenRequest requestRefreshToken = RefreshTokenRequest(httpClient, clientId, clientSecret);

            /*
             * ===============
             * STEP 1
             * ===============
             * Infusionsoft does not provide a way for an application to automatically authenticate.
             * They currently require a user's consent.
             *
             * To grab an access code from infusionsoft, complete the following steps.
             * 1. Point your browser to:
             *     https://accounts.infusionsoft.com/app/oauth/authorize?client_id=tj7a3rtbs5dmsz2sbwxx3phd&response_type=code&redirect_uri=https://localhost
             * 2. Click the "Allow" button
             * 3. Copy the code that is returned in the adddress bar of your browser
             * 4. Paste the code below in the AccessCode
             * ===============
             */

            // Utilize cache so we can run this program many times without the above hassle.
            // This is not part of the core library.
            var authorization = await AuthorizationInfoFromCache()
                .IfNoneAsync(() =>
                    requestAccessToken(AccessCode("PASTE CODE HERE"))
                        .Map(CacheAuthorizationInfo)
                );

            var client = new InfusioClient(httpClient, new InfusioConfig(authorization.Token));

            // DSL Demo
            await InfusioDslDemo.Run(client);

            // Api Client Demo
            await ClassicApiClientDemo.Run(client);
        }
    }
}