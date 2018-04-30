using LanguageExt;
using static LanguageExt.Prelude;

namespace Infusio.Http
{
    public class InterpreterState
    {
        public static readonly InterpreterState Empty = new InterpreterState(Seq<string>());
        public readonly Seq<string> Logs;
        public InterpreterState(Seq<string> logs) =>
            Logs = logs;

        public InterpreterState Log(string message) =>
            new InterpreterState(message.Cons(Logs));
    }
}
