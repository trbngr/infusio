using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotLiquid;
using LanguageExt;
using Newtonsoft.Json.Linq;
using static Infusio.Compiler.Parsing.OperationParameter;
using static LanguageExt.Prelude;

namespace Infusio.Compiler.Parsing
{
    interface IHaveName
    {
        string Name { get; set; }
    }

    static class Named
    {
        class NameEq : IEqualityComparer<IHaveName>
        {
            public bool Equals(IHaveName x, IHaveName y) =>
                x.Name.Equals(y.Name);

            public int GetHashCode(IHaveName x) =>
                x.Name.GetHashCode();
        }

        static readonly IEqualityComparer<IHaveName> NameEquality = new NameEq();

        public static Lst<T> MakeUnique<T>(this Lst<T> items) where T : IHaveName =>
            MakeUnique(items.Cast<IHaveName>().Freeze()).Cast<T>().Freeze();

        static Lst<IHaveName> MakeUnique(Lst<IHaveName> items) => items.Fold(
            Lst<IHaveName>.Empty,
            (lst, p) => lst.Add(lst.Contains(p, NameEquality) ? p.Set(x => x.Name = $"{x.Name}2") : p)
        );
    }

    [DebuggerDisplay("{HttpMethod} {Name} {Path}")]
    class Operation : ILiquidizable, IHaveName
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string HttpMethod { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public Lst<OperationResponse> Responses { get; set; }
        public Lst<OperationParameter> Parameters { get; set; }
        public bool HasParameters => Parameters.Any();
        public bool HasRequestBodyParameters => RequestBodyParameters.Any();
        public Lst<OperationParameter> RequestBodyParameters => Parameters.Filter(x => x.In == "body");
        public bool HasSingleBodyParameter => RequestBodyParameters.Count == 1;
        public OperationParameter SingleBodyParameter => RequestBodyParameters.SingleOrDefault();
        public Lst<OperationParameter> QueryParameters => Parameters.Filter(x => x.In == "query");
        public bool HasQueryParameters => QueryParameters.Any();

        public object ToLiquid() => new
        {
            Path,
            Name,
            HttpMethod,
            Summary,
            Description,
            Responses,
            Parameters,
            HasParameters,
            ResponseType,
            HasRequestBodyParameters,
            HasSingleBodyParameter,
            SingleBodyParameter,
            RequestBodyParameters,
            QueryParameters,
            HasQueryParameters
        };

        public string ResponseType => Responses
            .Find(x => x.StatusCode >= 200 && x.StatusCode <= 299)
            .Map(x => x.Type)
            .Match(
                Some: type => Optional(type).IfNone("Unit"),
                None: () => "Unit"
            );

        public static Lst<Operation> ParseOperations(JToken token, Set<string> definitions) => (
            from prop in Optional(token as JProperty)
            let path = prop.Name
            from child in prop.Children()
            select ParseOperations(child, path, definitions)
        ).Fold(
            Lst<Operation>.Empty,
            (lst, operations) => lst.AddRange(operations)
        );

        //TODO: Read ALL Operations here. Currently only picking up the first one.
        static Lst<Operation> ParseOperations(JToken token, string path, Set<string> definitions) => (
            from obj in Optional(token as JObject)
            from child in obj.Children()
            from methodNode in Optional(child as JProperty)
            let method = methodNode.Name.PascalCase()
            from p in methodNode.Children()
            select ParseOperation(path, method, p, definitions)
        ).FoldT(Lst<Operation>.Empty, (lst, op) => lst.Add(op));

        private static Option<Operation> ParseOperation(string path, string method, JToken token, Set<string> definitions) =>
            from opNode in Optional(token as JObject)
            from operationId in Optional(opNode["operationId"])
            let operationName = operationId.Value<string>()
            let responses = opNode["responses"]
            let parameters =
                Optional(opNode["parameters"])
                    .Map(node => node
                        .Children()
                        .Map(n => ParseOperationParameter(n, definitions))
                    ).IfNone(Enumerable.Empty<OperationParameter>())
            select new Operation
            {
                Path = path,
                HttpMethod = method,
                Summary = opNode["summary"].StringOrNull(),
                Description = opNode["description"].StringOrNull(),
                Name = TypeResolver.ResolveOperationName(operationId.Value<string>()),
                Responses = OperationResponse.ParseResponses(responses),
                Parameters = parameters.OrderBy(p => p.Required).Rev().Freeze().MakeUnique()
            };
    }

    class OperationParameter : ILiquidizable, IHaveName
    {
        public string In { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }

        public bool IsValueType => TypeResolver.ValueTypes.Find(Type).Map(_ => true).IfNone(false);
        public bool IsOptionalType => TypeResolver.OptionalTypes.Find(Type).Map(_ => true).IfNone(Type == "object");
        public string OptionalTypeName => IsValueType ? $"{Type}?" : Type;

        public string Format { get; set; }
        public PropertyTypeItems Items { get; set; }

        public object ToLiquid() => new
        {
            In,
            Name,
            Description,
            Required,
            OptionalTypeName,
            Type
        };

        public static OperationParameter ParseOperationParameter(JToken node, Set<string> definitions)
        {
            var param = node.Deserialize<OperationParameter>()
                .Set(x => x.Name = x.Name.UnSnake().CamelCase());

            return (isnull(param.Type)
                    ? param.Set(p => p.Type = TypeResolver.ReadRef(Optional(node["schema"]).Map(x => x["$ref"])))
                    : param.Set(p => p.Type = TypeResolver.ResolveType(p))
                ).Set(x => x.Type = definitions.Contains(x.Type) ? $"Model.{x.Type}" : x.Type);
        }

        public static implicit operator Property(OperationParameter p) => new Property
        {
            Type = p.Type,
            Items = p.Items,
            Format = p.Format,
        };
    }

    class OperationResponse : ILiquidizable
    {
        public int StatusCode { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool HasType => notnull(Type);

        public static Lst<OperationResponse> ParseResponses(JToken token) => (
            from obj in Optional(token as JObject)
            from response in obj.Children()
            select
                from statusCode in Optional(response as JProperty)
                from code in parseInt(statusCode.Name)
                let data = statusCode.First
                let description = data["description"]
                let type = TypeResolver.ReadRef(Optional(data["schema"]).Map(x => x["$ref"]))
                select new OperationResponse
                {
                    StatusCode = code,
                    Description = description.StringOrNull(),
                    Type = type ?? "Unit"
                }).FoldT(Lst<OperationResponse>.Empty, (lst, response) => lst.Add(response));

        public object ToLiquid() => new
        {
            StatusCode,
            Description,
            Type,
            HasType
        };
    }
}