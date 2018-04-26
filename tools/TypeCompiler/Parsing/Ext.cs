using System;
using System.Text;
using LanguageExt;
using Newtonsoft.Json.Linq;

namespace Infusio.Compiler.Parsing
{
    static class Ext
    {
        public static T Set<T>(this T self, Action<T> act)
        {
            act(self);
            return self;
        }

        public static string UnSnake(this string text) => text.Replace("-", "_").Split('_')
            .Fold(new StringBuilder(), (lst, str) => lst.Append(PascalCase(str)))
            .ToString();

        public static string PascalCase(this string text) =>
            $"{text.Substring(0, 1).ToUpperInvariant()}{text.Substring(1)}".Replace("_", "");

        public static string CamelCase(this string text) =>
            $"{text.Substring(0, 1).ToLowerInvariant()}{text.Substring(1)}".Replace("_", "");

        public static string StringOrNull(this JToken token) =>
            Prelude.Optional(token).Map(x => x.Value<string>()).IfNoneUnsafe(() => null);
    }
}