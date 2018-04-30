using LanguageExt;

namespace Infusio
{
    public class InfusioResult<A> : Record<InfusioResult<A>>
    {
        public A Value;
        public Seq<string> Logs;
        public InfusioResult(A value, Seq<string> logs)
        {
            Value = value;
            Logs = logs;
        }
    }
}
