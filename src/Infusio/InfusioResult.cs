using LanguageExt;

namespace Infusio
{
    public class InfusioResult<A> : Record<InfusioResult<A>>
    {
        public readonly A Value;
        public readonly Seq<string> Logs;

        public InfusioResult(A value, Seq<string> logs)
        {
            Value = value;
            Logs = logs;
        }

        public void Deconstruct(out A value, out Seq<string> logs)
        {
            value = Value;
            logs = Logs;
        }

        public static implicit operator A(InfusioResult<A> result) => result.Value;
    }
}