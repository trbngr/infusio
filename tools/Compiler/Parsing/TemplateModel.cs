using System;
using DotLiquid;
using LanguageExt;
using Newtonsoft.Json.Linq;

namespace Infusio.Compiler.Parsing
{
    using static Prelude;

    class TemplateModel : ILiquidizable
    {
        public Lst<Definition> Definitions { get; }
        public Lst<Operation> Operations { get; set; }

        //FullContact is returning EMAIL1 which is not documented in the swagger doc.
        private Map<string, Lst<string>> extraEnumMembers = Map(
            ("Field", List("EMAIL1"))
        );

        public Seq<EnumModel> Enums => (
                from def in Definitions
                from prop in def.Properties
                select prop
            )
            .Filter(x => x.IsEnum)
            .Map(x => new EnumModel(x.Name, x.Enum.Append(extraEnumMembers.Find(x.Name).IfNone(Lst<string>.Empty)), x.Description))
            .Fold(HashSet<EnumModel.NameEq, EnumModel>(), (set, model) => set.TryAdd(model))
            .ToSeq();

        TemplateModel(Lst<Definition> definitions, Lst<Operation> operations)
        {
            Definitions = definitions;
            Operations = operations;
        }

        public static TemplateModel Parse(string json) =>
            Parse(JObject.Parse(json));

        public static TemplateModel Parse(JObject swagger)
        {
            var definitions = ParseDefinitions(swagger);

            return new TemplateModel(
                definitions: definitions,
                operations: ParseOperations(swagger, definitions.Fold(
                    Set<string>(),
                    (set, definition) =>
                    {
                        Console.Out.WriteLine($"Op: {definition.Name}");
                        return set.TryAdd(definition.Name);
                    })
                )
            );
        }

        static Lst<Definition> ParseDefinitions(JObject swagger) =>
            swagger["definitions"].Children()
                .Map(Definition.ParseDefinitions)
                .FoldT(Lst<Definition>.Empty, (lst, def) => lst.Add(def));

        static Lst<Operation> ParseOperations(JObject swagger, Set<string> definitions) =>
            swagger["paths"].Children()
                .Map(path => Operation.ParseOperations(path, definitions))
                .FoldT(Lst<Operation>.Empty, (lst, def) => lst.Add(def))
                .MakeUnique();

        public object ToLiquid() => new {Definitions, Operations, Enums};
    }
}