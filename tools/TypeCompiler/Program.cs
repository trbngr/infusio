using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DotLiquid;
using DslCompiler.Parsing;
using LanguageExt;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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

        static readonly string Dest = Path.GetFullPath("../../src/Infusionsoft");

        static OutputDirectory OutDir(string dir) =>
            OutputDirectory.New(Path.Combine(Dest, dir));

        static async Task Main()
        {
            var settings = new CodeGenSettings();
            var model = await LoadDocument().Map(TemplateModel.Parse);

            Template.RegisterFilter(typeof(Filters));
            Template.RegisterTag<TemplateTag>("template");

            GenerateForSingleFile(model)
                .Map(result => WriteToDisc(OutputDirectory.New(Dest), result));

            Console.Out.WriteLine("");
        }

        static Lst<(FileName, Try<GeneratedCode>)> GenerateForSingleFile(TemplateModel model) => 
            List((FileName.New("Dsl"), Render("Dsl", model)))
                .Add((FileName.New("Dto"), Render("Dto", model)))
                .Add((FileName.New("Ops"), Render("Ops", model)))
                .Add((FileName.New("Client"), Render("Client", model)));

        static Lst<(OutputDirectory, FileName, Try<GeneratedCode>)> GenerateForMultiFile(TemplateModel model) => 
            List((OutDir(Dest), FileName.New("Dsl"), Render("Dsl", model)))
            .AddRange(model.Definitions.Map(def =>(OutDir("Model"), FileName.New(def.Name), Render("Dto.MultiFile", def))))
            .AddRange(model.Operations.Map(op => (OutDir("Ops"), FileName.New(op.Name), Render("Op", op))))
            .AddRange(model.Enums.Map(en => (OutDir("Dto"), FileName.New(en.Name), Render("Enum", en))));

        static Unit WriteToDisc(OutputDirectory directory, (FileName Name, Try<GeneratedCode> Attemp) result) =>
            WriteToDisc((directory, result.Name, result.Attemp));
        
        static Unit WriteToDisc((OutputDirectory Directory, FileName Name, Try<GeneratedCode> Attempt) result) => match(
            from code in result.Attempt
            from ast in Try(CSharpSyntaxTree.ParseText(code))
            let _ = Directory.CreateDirectory(result.Directory)
            let path = Path.Combine(result.Directory, $"{result.Name}.cs")
            let writer = new StreamWriter(new FileStream(path, FileMode.Create))
            from syntaxNode in Try(ast.GetRoot().NormalizeWhitespace())
            from end in use(writer, TryAction<TextWriter>(syntaxNode.WriteTo))
            select end,
            Succ: identity,
            Fail: e =>
            {
                Console.Out.WriteLine($"Error generating file {result.Name}. {e.Message}");
                return unit;
            }
        );

        static Try<GeneratedCode> Render(string templateName, ILiquidizable model) =>
            from temp in Try(() => Template.Parse(File.ReadAllText($"CodeGen/{templateName}.liquid")))
            from code in Try(() => temp.Render(Hash.FromAnonymousObject(model)))
            select GeneratedCode.New(code);

        static Func<T, Try<Unit>> TryAction<T>(Action<T> action) => arg =>
        {
            action(arg);
            return Try(unit);
        };
    }

    class OutputDirectory : NewType<OutputDirectory, string>
    {
        OutputDirectory(string value) : base(value)
        {
        }

        public static implicit operator string(OutputDirectory d) => d.Value;
    }

    class GeneratedCode : NewType<GeneratedCode, string>
    {
        GeneratedCode(string code) : base(code)
        {
        }

        public static implicit operator string(GeneratedCode d) => d.Value;
    }

    class FileName : NewType<FileName, string>
    {
        FileName(string value) : base(value)
        {
        }

        public static implicit operator string(FileName d) => d.Value;
    }

    class CodeGenSettings : Record<CodeGenSettings>
    {
        public readonly bool SingleFiles;

        public CodeGenSettings(bool singleFiles = true) => SingleFiles = singleFiles;
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