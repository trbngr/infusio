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

        // Handle enum values not documented in the swaggerdoc:
        private Map<string, Lst<string>> _extraEnumMembers = Map(
            ("Field", List("EMAIL1","EMAIL2","EMAIL3","PHONE1","PHONE2","PHONE3","PHONE4","PHONE5","FAX1","FAX2")),
            ("EmailStatus", List("SingleOptIn"))
        );

        public Seq<EnumModel> Enums => (
                from def in Definitions
                from prop in def.Properties
                select prop
            )
            .Filter(x => x.IsEnum)
            .Map(x => new EnumModel(x.Name, x.Enum.Append(_extraEnumMembers.Find(x.Name).IfNone(Lst<string>.Empty)),
                x.Description))
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
                        (set, definition) => set.TryAdd(definition.Name)
                    )
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