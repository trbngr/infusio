using System;
using System.Threading.Tasks;
using Infusionsoft;
using Newtonsoft.Json;

namespace Runner
{
    using static Dsl;
    using static Formatting;
    using static Interpreter;
    using static JsonConvert;

    class Program
    {
        static async Task Main()
        {

            var config = new InfusionsoftConfig("8akh7bm2fwvnaezu42hs5gwb");

            var dsl = UpdatePhoneNumber("555-555-5555");

            var profile = await Interpret(dsl, config);
            // or
            var profile2 = await dsl.Run(config);

            Console.Out.WriteLine(SerializeObject(profile, Indented));
        }

        static Op<AccountProfile> UpdatePhoneNumber(string phone) =>
            from prof in GetAccountProfile()
            from _ in UpdateAccountProfile(prof.With(x => x.Phone = phone))
            from updated in GetAccountProfile()
            select updated;
    }
}