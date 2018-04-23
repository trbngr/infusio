using LanguageExt;

namespace DslCompiler.Parsing
{
    using static Prelude;

    public class EnumModel
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
        public bool HasDescription => notnull(Description);
    }
}