using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infusio;
using Infusio.Http;
using Infusio.Model;
using LanguageExt;

namespace Runner
{
    using static Prelude;

    class Program
    {
        static async Task Main()
        {

            var config = new InfusioConfig("mghcpcq8qwyhary7p33s6pu2");
            var client = new InfusioClient(new HttpClient(), config);

            var either = await client.GetAccountProfile();
            AccountProfile profile = either.Match(
                Left: e => throw new Exception(e.Message),
                Right: identity
            );

//            await client.Pro
        }

//        static InfusionOp<AccountProfile> UpdatePhoneNumber(string phone) =>
//            from prof in GetAccountProfile()
//            from _ in UpdateAccountInfo(prof.With(x => x.Phone = phone))
//            from updated in GetAccountProfile()
//            select updated;
    }

    static class Ext
    {
        public static T With<T>(this T self, Action<T> act)
        {
            act(self);
            return self;
        }
    }
}