using System.Text;
using System.Text.RegularExpressions;
using DotLiquid;
using DslCompiler.Parsing;

namespace DslCompiler
{
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
}