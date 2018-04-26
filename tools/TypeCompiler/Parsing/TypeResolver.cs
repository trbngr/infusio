using System;
using System.Linq;
using LanguageExt;
using Newtonsoft.Json.Linq;

namespace Infusio.Compiler.Parsing
{
    using static Prelude;

    static class TypeResolver
    {
        static Map<string, Func<Property, string>> _typeMap = Map<string, Func<Property, string>>(
            ("double", p => "double"),
            ("number", p => "int"),
            ("boolean", p => "bool"),
            ("integer", p => p.Format == "int32" ? "int" : p.Format == "int64" ? "long" : throw new NotSupportedException($"{p.Format} is not supported")),
            ("string", p => p.IsEnum ? Ext.UnSnake(p.Name) : "string"),
            ("array", p => ifNone(
                from items in Optional(p.Items)
                from type in Optional(items.Ref).Map(ReadRef) |
                             from t in Optional(items.Type)
                             from f in _typeMap.Find(t)
                             select f(new Property {Format = items.Format})
                select $"Lst<{RenameTaskIfNeeded(type)}>",
                () => throw new NotSupportedException($"Unable to determine array type for property {p.ParentType}.{p.JsonProperty}")
            ))
        );

        public static Set<string> ValueTypes = Set(
            "int", "long", "double"
        );

        public static Set<string> OptionalTypes = ValueTypes.Add("string");

        public static string RenameTaskIfNeeded(this string value) =>
            value == "Task" ? "InfusionTask" : value;

        public static string ReadRef(string reference) =>
            Filters.Normalize(reference.Split("/").Last().RenameTaskIfNeeded());

        public static string ReadRef(Option<JToken> token) => ifNoneUnsafe(
            from t in token
            from value in Optional(t as JValue)
            select ReadRef(value.Value<string>()),
            () => null
        );

        public static string ResolveOperationName(string operationId) => Filters.Normalize(ifNone(
            from index in Some(operationId.IndexOf("Using", StringComparison.Ordinal))
            where index > 0
            select $"{operationId.Substring(0, index)}",
            operationId
        ).UnSnake());

        public static string ResolveType(Property property) => RenameTaskIfNeeded(
            _typeMap
                .Find(property.Type)
                .Map(resolve => resolve(property))
                .IfNone(() => ifNone(Optional(property.Ref).Map(ReadRef), property.Type))
        );
    }
}