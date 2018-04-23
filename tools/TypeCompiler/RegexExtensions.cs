using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using LanguageExt;

namespace DslCompiler
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class RegexExtensions
    {
	    public static IEnumerable<T> Map<T>(this MatchCollection matches, Func<Match, T> fn)
	    {
		    foreach (Match match in matches)
		    {
			    yield return fn(match);
		    }
	    }

	    static IEnumerable<T> Select<T>(this MatchCollection matches, Func<Match, T> fn) =>
		    Map(matches, fn);

        static Option<Group> Opt(Group group) =>
            group.Success ? group : null;

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]), Opt(match.Groups[4]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]), Opt(match.Groups[4]), Opt(match.Groups[5]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]), Opt(match.Groups[4]), Opt(match.Groups[5]), Opt(match.Groups[6]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]), Opt(match.Groups[4]), Opt(match.Groups[5]), Opt(match.Groups[6]), Opt(match.Groups[7]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]), Opt(match.Groups[4]), Opt(match.Groups[5]), Opt(match.Groups[6]), Opt(match.Groups[7]), Opt(match.Groups[8]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]), Opt(match.Groups[4]), Opt(match.Groups[5]), Opt(match.Groups[6]), Opt(match.Groups[7]), Opt(match.Groups[8]), Opt(match.Groups[9]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]), Opt(match.Groups[4]), Opt(match.Groups[5]), Opt(match.Groups[6]), Opt(match.Groups[7]), Opt(match.Groups[8]), Opt(match.Groups[9]), Opt(match.Groups[10]));

		public static IEnumerable<TReturn> Map<TReturn>(this MatchCollection matches, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			from match in matches select Map(match, fn);

		public static TReturn Map<TReturn>(this Match match, Func<Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, Option<Group>, TReturn> fn) =>
			fn(Opt(match.Groups[1]), Opt(match.Groups[2]), Opt(match.Groups[3]), Opt(match.Groups[4]), Opt(match.Groups[5]), Opt(match.Groups[6]), Opt(match.Groups[7]), Opt(match.Groups[8]), Opt(match.Groups[9]), Opt(match.Groups[10]), Opt(match.Groups[11]));


    }
}