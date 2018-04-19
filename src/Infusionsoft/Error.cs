using System.Runtime.Serialization;
using LanguageExt;

namespace Infusionsoft
{
    public class Error : NewType<Error, string>
    {
        public Error(string value) : base(value) { }
        public Error(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}