using System.Runtime.Serialization;
using DotLiquid;
using LanguageExt;
using Newtonsoft.Json.Linq;
using static LanguageExt.Prelude;

namespace Infusio.Compiler.Parsing
{
    class Definition : ILiquidizable
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Lst<Property> Properties { get; set; }

        public static Option<Definition> ParseDefinitions(JToken token) =>
            from prop in Optional(token as JProperty)
            let name = prop.Name.RenameTaskIfNeeded()
            select new Definition
            {
                Name = name,
                Type = name,
                Properties = prop.First["properties"].Children()
                    .Map(x => Property.Parse(name, x))
                    .FoldT(Lst<Property>.Empty, (lst, p) => lst.Add(p))
            };

        public object ToLiquid() => new {Name, Type, Properties};
    }

    /// <summary>
    ///
    /// </summary>
    public enum Test
    {
        [EnumMember(Value = "")] One,
    }
}