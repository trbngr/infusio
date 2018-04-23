using System;
using Infusion;
using Infusion.Client;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace Runner
{
    using static InfusionDsl;
    using static Formatting;
    using static Interpreter;
    using static JsonConvert;

    class Program
    {
        static async Task Main()
        {

            var config = new InfusionConfig("8akh7bm2fwvnaezu42hs5gwb");

            var dsl = UpdatePhoneNumber("666-555-5555");

            var profile = await Interpret(dsl, config);
            // or
            var profile2 = await dsl.Run(config);

            Console.Out.WriteLine(SerializeObject(profile, Indented));
        }

        static InfusionOp<AccountProfile> UpdatePhoneNumber(string phone) =>
            from prof in GetAccountProfile()
            from _ in UpdateAccountInfo(prof.With(x => x.Phone = phone))
            from updated in GetAccountProfile()
            select updated;
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