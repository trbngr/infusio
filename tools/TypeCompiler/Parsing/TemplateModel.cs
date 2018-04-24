using DotLiquid;
using LanguageExt;
using Newtonsoft.Json.Linq;

namespace DslCompiler.Parsing
{
    class TemplateModel : ILiquidizable
    {
        public Lst<Definition> Definitions { get; }
        public Lst<Operation> Operations { get; set; }

        public Seq<EnumModel> Enums => (
                from def in Definitions
                from prop in def.Properties
                select prop
            )
            .Filter(x => x.IsEnum)
            .Map(x => new EnumModel(x.Name, x.Enum, x.Description))
            .Fold(HashSet<EnumModel.NameEq, EnumModel>.Empty, (set, model) => set.TryAdd(model))
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
                    Set<string>.Empty,
                    (set, definition) => set.TryAdd(definition.Name))
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
                .FoldT(Lst<Operation>.Empty, (lst, def) => lst.Add(def));

        public object ToLiquid() => new {Definitions, Operations, Enums};
    }
}