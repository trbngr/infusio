using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DotLiquid;
using DslCompiler.Parsing;
using LanguageExt;

namespace DslCompiler
{
    using static Prelude;
    
    public static class Filters
    {
        public static string Normalize(string input) =>
            Regex.Matches(input, @"(\w+)")
                .Fold(new StringBuilder(), (sb, match) => sb.Append(match.Groups[1].Value.PascalCase()))
                .ToString();

        public static string Camelcase(string input) =>
            $"{input.Substring(0, 1).ToLowerInvariant()}{input.Substring(1)}";

        public static string Firstupper(string input) =>
            input.ToLowerInvariant().PascalCase();
    }

    public class TemplateTag : Tag
    {
        private string _name;
        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);
            _name = markup.Trim();
        }

        public override void Render(Context context, TextWriter result)
        {
            base.Render(context, result);
            
            var code = from temp in Try(() => Template.Parse(File.ReadAllText($"CodeGen/{_name}.liquid")))
                from res in Try(() => temp.Render(new RenderParameters(new CultureInfo("en"))
                {
                    Context = context,
                    Filters = new []{typeof(Filters)},
                }))
                select res;
            
            result.WriteLine(code);
        }
    }
}