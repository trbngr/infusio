using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DotLiquid;
using DslCompiler.Parsing;
using Humanizer;
using LanguageExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static LanguageExt.Prelude;

namespace DslCompiler
{
    class Program
    {
        static Task<JObject> LoadDocument() => File.Exists("infusion.json")
            ? File.ReadAllTextAsync("infusion.json").Map(JObject.Parse)
            : from client in new HttpClient().AsTask()
            from resp in client.GetAsync("https://developer.infusionsoft.com/docs/rest/infusion.json")
            from json in resp.Content.ReadAsStringAsync()
            from save in File.WriteAllTextAsync("infusion.json", json).Lift()
            select JObject.Parse(json);

        static async Task Main()
        {
            var model = await LoadDocument().Map(TemplateModel.Parse);

            var dest = Path.GetFullPath("../../src/Infusionsoft");

            Template.RegisterFilter(typeof(Filters));

            WriteToDisc(("Dsl", Render("Dsl", model)), dest);

            model.Operations.Map(op => (op.Name, Render("Op", op)))
                .Iter(x => WriteToDisc(x, Path.Combine(dest, "Ops")));

            return;

            model.Enums.Map(en => (en.Name, Render("Enum", en)))
                .Iter(x => WriteToDisc(x, Path.Combine(dest, "Model")));

            model.Definitions.Map(def => (def.Name, Render("Dto", def)))
                .Iter(x => WriteToDisc(x, Path.Combine(dest, "Model")));

            Console.Out.WriteLine("");
        }

        static Unit WriteToDisc((string Name, Try<string> Attempt) result, string path) => match(
            from code in result.Attempt
            from end in TryAction(() => File.WriteAllText(Path.Combine(path, $"{result.Name}.cs"), code))
            select end,
            Succ: identity,
            Fail: e =>
            {
                Console.Out.WriteLine($"Error generating file {result.Name}. {e.Message}");
                return unit;
            }
        );

        static Try<string> Render(string templateName, object model) =>
            from temp in Try(Template.Parse(File.ReadAllText($"CodeGen/{templateName}.liquid")))
            from code in Try(temp.Render(Hash.FromAnonymousObject(model)))
            select code;

        static Try<Unit> TryAction(Action action) => Try(() =>
        {
            action();
            return unit;
        });
    }

    static class Ext
    {
        public static Unit Return<T>(this T self, Action<T> act)
        {
            act(self);
            return unit;
        }

        public static async Task<Unit> Lift(this Task task)
        {
            await task;
            return Unit.Default;
        }
    }
}