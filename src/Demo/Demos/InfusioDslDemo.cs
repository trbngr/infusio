using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infusio;
using Infusio.Auth;
using Infusio.Http;
using Infusio.Model;
using Infusio.Ops;
using LanguageExt;
using Newtonsoft.Json;

namespace Demo.Demos
{
    public static class InfusioDslDemo
    {
        public static async Task<Unit> Run(InfusioClient client)
        {
            /*
             * ===============
             * STEP 1
             * ===============
             * Describe the Infusionsoft operation that want to execute.
             * InfusioOp objects are composable. You combine any number of InfusioOps together to form one InfusioOp.
             * You're not stuck with running one operation at a time like you're probably used to traditionally.
             * You can, however, use this library as a traditional API client as well.
             * See the Classic Api Client demo for more information.
             * ===============
             */

            InfusioOp<FullContact> operation = CustomOperations.AddTagToContact(
                new Tag(name: "developers"),
                new EmailAddress("chris@caliberweb.com")
            );

            /*
             * ===============
             * STEP 2
             * ===============
             * Execute your operation.
             * This returns a data type with two possible values.
             * Either<InfusioError, T>
             * ===============
             */

            var result = await operation.RunWith(client);

            return result.Match(
                Left: error => Console.WriteLine($"error: {error.Value}"),
                Right: contact => Console.WriteLine($"contact: {JsonConvert.SerializeObject(contact, Formatting.Indented)}")
            );
        }
    }
}