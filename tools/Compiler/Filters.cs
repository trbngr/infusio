using System.Text;
using System.Text.RegularExpressions;
using Infusio.Compiler.Parsing;

namespace Infusio.Compiler
{
    public static class Filters
    {
        public static string Normalize(string input) =>
            Regex.Matches(input, @"([\w_]+)")
                .Fold(new StringBuilder(), (sb, match) => sb.Append(match.Groups[1].Value.PascalCase()))
                .ToString();

        public static string Camelcase(string input) =>
            $"{input.Substring(0, 1).ToLowerInvariant()}{input.Substring(1)}";

        public static string Firstupper(string input) =>
            input.ToLowerInvariant().PascalCase();

        public static string Showoperationpath(string input) =>
            Regex.Replace(input, @"{(\w+)}", match => $"{{op.{match.Groups[1].Value.PascalCase()}}}");

        public static string Enummember(string input) =>
            Regex.Matches(input, @"(\w+)")
                .Fold(new StringBuilder(), (sb, word) => sb.Append(word.Groups[1].Value.PascalCase())
                ).ToString();
    }
}