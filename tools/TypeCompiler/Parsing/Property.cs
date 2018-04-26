using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DotLiquid;
using Humanizer;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infusio.Compiler.Parsing
{
    using static Prelude;

    public class Property : ILiquidizable
    {
        public string JsonProperty { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public PropertyTypeItems Items { get; set; }
        public bool Required { get; set; }
        public JToken Source { get; set; }
        public string ParentType { get; set; }
        public Lst<string> Enum { get; set; }
        public bool IsEnum => Enum.Any();

        public bool IsValueType => TypeResolver.ValueTypes.Find(Type).Map(_ => true).IfNone(false);
        public bool IsOptionalType => TypeResolver.OptionalTypes.Find(Type).Map(_ => true).IfNone(Type == "object");

        public string OptionalTypeName => IsValueType ? $"{Type}?" : Type;

        public string VariableName => NoReserved(Name.CamelCase());

        [JsonProperty("$ref")] public string Ref { get; set; }

        public static Set<string> Reserved = Set(
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue",
            "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float",
            "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object",
            "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof",
            "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe",
            "ushort", "using", "virtual", "void", "volatile", "while"
        );

        static string NoReserved(string value) =>
            Reserved.Find(value).Map(x => $"_{x}").IfNone(value);

        static string NoSameNameAsParent(string parent, string value) =>
            value == parent ? $"Inner{value}" : value;

        public static string NoStartWithNumber(string value) =>
            Regex.Match(value, @"(\d*)(.+)").Map((digits, rest) =>
            {
                var justWord =
                    from wordMatch in rest
                    from word in Some(wordMatch.Value)
                    select word;

                var withNums =
                    from numStr in digits
                    from numVal in Some(numStr.Value)
                    from num in parseInt(numVal)
                    from word in justWord
                    select $"{num.ToWords(new CultureInfo("en")).UnSnake()}{word}";

                return (withNums | justWord).IfNone(value);
            });

        static string NormalizeName(string parent, string name) => (
            from us in Some(name.UnSnake())
            from nt in Some(us.RenameTaskIfNeeded())
            from np in Some(NoSameNameAsParent(parent, nt))
            from nn in Some(NoStartWithNumber(np))
            select NoReserved(nn)
        ).ValueUnsafe();

        public static Option<Property> Parse(string parentType, JToken token) =>
            from p in Optional(token as JProperty)
            let json = p.First.ToString()
            from prop in Some(p.First.Deserialize<Property>())
            select prop
                .Set(x => x.Source = p.First)
                .Set(x => x.JsonProperty = p.Name)
                .Set(x => x.ParentType = parentType)
                .Set(x => x.Name = NormalizeName(parentType, p.Name))
                .Set(x => x.Type = NoStartWithNumber(TypeResolver.ResolveType(x)));

        public object ToLiquid() => new
        {
            Name,
            Type,
            Required,
            ParentType,
            Enum,
            IsEnum,
            VariableName,
            JsonProperty,
            OptionalTypeName,
            IsOptionalType
        };
    }
}