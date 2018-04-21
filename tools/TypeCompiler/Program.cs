using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DslCompiler.CodeGen;
using LanguageExt;
using NJsonSchema;
using NJsonSchema.CodeGeneration;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.OperationNameGenerators;
using static LanguageExt.Prelude;

namespace DslCompiler
{
    class Program
    {
        class CustomTypeNameGenerator : DefaultTypeNameGenerator
        {
            protected override string Generate(JsonSchema4 schema, string typeNameHint)
            {
                var input = base.Generate(schema, typeNameHint);
                foreach (char c in new[] {'«', '»'})
                {
                    input = input.Replace(c, '_');
                }

                return input;
            }
        }

        class CustomEnumNameGenerator : IEnumNameGenerator
        {
            public string Generate(int index, string name, object value, JsonSchema4 schema) =>
                ConversionUtilities.ConvertToUpperCamelCase(
                    name.Replace(":", "-").Replace(".", "_").Replace("#", "_").Replace("(", "").Replace(")", ""), true
                );
        }

        static async Task Main()
        {
            var document =
                await SwaggerDocument.FromUrlAsync("https://developer.infusionsoft.com/docs/rest/infusion.json");

            var settings = new SwaggerToCSharpClientGeneratorSettings
            {
                InjectHttpClient = true,
                DisposeHttpClient = false,
                ClassName = "InfusionsoftClient",
                ExposeJsonSerializerSettings = false,
                ParameterArrayType = "LanguageExt.Lst",
                ResponseArrayType = "LanguageExt.Lst",
                ParameterDictionaryType = "LanguageExt.Map",
                ResponseDictionaryType = "LanguageExt.Map",
                OperationNameGenerator = new SingleClientFromOperationIdOperationNameGenerator(),
                ParameterNameGenerator = new DefaultParameterNameGenerator(),
                CSharpGeneratorSettings =
                {
                    Namespace = "Infusionsoft.Client",
                    ArrayType = "LanguageExt.Lst"
                },
                CodeGeneratorSettings =
                {
                    TypeNameGenerator = new CustomTypeNameGenerator(),
                    EnumNameGenerator = new CustomEnumNameGenerator()
                }
            };

            var clientGenerator = new SwaggerToCSharpClientGenerator(document, settings);
            var clientCode = clientGenerator.GenerateFile(ClientGeneratorOutputType.Full);

            var dslGenerator = new FreeMonadGenerator(document, new FreeMonadGeneratorSettings
                {
                    DslModuleName = "InfusionDsl",
                    FreeType = "InfusionOp",
                    ParameterArrayType = "LanguageExt.Lst",
                    ResponseArrayType = "LanguageExt.Lst",
                    ParameterDictionaryType = "LanguageExt.Map",
                    ResponseDictionaryType = "LanguageExt.Map",
                    ParameterNameGenerator = new DefaultParameterNameGenerator(),
                    CSharpGeneratorSettings =
                    {
                        Namespace = "Infusionsoft",
                        ArrayType = "LanguageExt.Lst"
                    },
                    CodeGeneratorSettings =
                    {
                        TypeNameGenerator = new CustomTypeNameGenerator(),
                        EnumNameGenerator = new CustomEnumNameGenerator()
                    }
                }
            );
            var dslCode = dslGenerator.GenerateFile();
            
            var dest = Path.GetFullPath("../../src/Infusionsoft");

            use(new StreamWriter(Path.Combine(dest, "InfusionsoftClient.cs")),
                writer => writer.Return(x => x.Write(clientCode)));

            use(new StreamWriter(Path.Combine(dest, "Dsl.cs")),
                writer => writer.Return(x => x.Write(dslCode)));
        }
    }

    static class Ext
    {
        public static Unit Return<T>(this T self, Action<T> act)
        {
            act(self);
            return unit;
        }

        public static StringBuilder AppendLines<T>(this StringBuilder sb, IEnumerable<T> ts, Func<T, string> fn) =>
            ts.Fold(sb, (builder, t) => builder.AppendLine(fn(t)));

        public static StringBuilder AppendNextOpProp(this StringBuilder b, SwaggerOperation operation,
            SwaggerToCSharpClientGenerator generator) => ifNone(
            from resp in Optional(operation.Responses["200"])
            from rtype in Optional(generator.GetTypeName(resp.ActualResponseSchema, false, resp.Description))
            select b.Append($"public Func<{rtype}, Op<A>> Next").AppendLine(" { get; }"),
            b
        );

        public static StringBuilder AppendNextOpParam(this StringBuilder b, SwaggerOperation operation,
            SwaggerToCSharpClientGenerator generator) => ifNone(
            from resp in Optional(operation.Responses["200"])
            from rtype in Optional(generator.GetTypeName(resp.ActualResponseSchema, false, resp.Description))
            select b.Append($"Func<{rtype}, Op<A>> next"),
            b
        );

        public static string UnSnake(this string text) => text.Split('_')
            .Fold(new StringBuilder(), (lst, str) => lst.Append(PascalCase(str)))
            .ToString();

        public static string PascalCase(this string text) =>
            $"{text.Substring(0, 1).ToUpperInvariant()}{text.Substring(1)}".Replace("_", "");

        public static string CamelCase(this string text) =>
            $"{text.Substring(0, 1).ToLowerInvariant()}{text.Substring(1)}".Replace("_", "");
    }
}