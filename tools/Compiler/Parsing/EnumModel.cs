using System.Diagnostics;
using DotLiquid;
using LanguageExt;
using LanguageExt.TypeClasses;

namespace Infusio.Compiler.Parsing
{
    [DebuggerDisplay("Enum {Name}")]
    public class EnumModel : Record<EnumModel>, ILiquidizable
    {
        public EnumModel(string name = default, Lst<string> values = default, string description = default)
        {
            Name = name;
            Values = values;
            Description = description;
        }

        public string Name { get; }
        public Lst<string> Values { get; }
        public string Description { get; }
        public bool HasDescription => Prelude.notnull(Description);

        public object ToLiquid() => new {Name, Values, Description, HasDescription};

        public struct NameEq : Eq<EnumModel>
        {
            public bool Equals(EnumModel x, EnumModel y) => x.Name.Equals(y.Name);
            public int GetHashCode(EnumModel x) => x.Name.GetHashCode();
        }
    }
}