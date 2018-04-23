using LanguageExt;
using Newtonsoft.Json.Linq;

namespace DslCompiler.Parsing
{
    class TemplateModel
    {
        public Lst<Definition> Definitions { get; }
        public Lst<Operation> Operations { get; set;  }

        public Lst<EnumModel> Enums => (
                from def in Definitions
                from prop in def.Properties
                select prop
            )
            .Filter(x => x.IsEnum)
            .Map(x => new EnumModel(x.Name, x.Enum, x.Description));

        TemplateModel(Lst<Definition> definitions, Lst<Operation> operations)
        {
            Definitions = definitions;
            Operations = operations;
        }

        public static TemplateModel Parse(string json) =>
            Parse(JObject.Parse(json));

        public static TemplateModel Parse(JObject swagger) => new TemplateModel(
            definitions: ParseDefinitions(swagger),
            operations: ParseOperations(swagger)
        );

        static Lst<Definition> ParseDefinitions(JObject swagger) =>
            swagger["definitions"].Children()
                .Map(Definition.ParseDefinitions)
                .FoldT(Lst<Definition>.Empty, (lst, def) => lst.Add(def));

        static Lst<Operation> ParseOperations(JObject swagger) =>
            swagger["paths"].Children()
                .Map(Operation.ParseOperations)
                .FoldT(Lst<Operation>.Empty, (lst, def) => lst.Add(def));
    }
}