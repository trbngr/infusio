using LanguageExt;

namespace Infusio
{
    public class InfusioError : NewType<InfusioError, string>
    {
        public InfusioError(string value) : base(value)
        {}
    }
}